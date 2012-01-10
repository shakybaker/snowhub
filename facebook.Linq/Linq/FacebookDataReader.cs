using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using facebook.Utility;

namespace facebook.Linq
{
	internal class FacebookDataReader : IDataReader
	{
		public FacebookDataReader(string xml)
		{
			Xml = xml;
			doc = new XmlDocument();
			doc.LoadXml(xml);
			RootNode = doc.ChildNodes[1];
			if (RootNode.Name == "error_response")
			{
				throw ParseFacebookException();
			}
			if (!RootNode.Name.EndsWith("_response"))
			{
				throw new Exception("Unknown response XML");
			}
		}


		public int Count
		{
			get
			{
				return RootNode.ChildNodes.Count;
			}
		}

		private FacebookException ParseFacebookException()
		{
			string errorXml = "";
			string message = "";
			string requestXml = "";
			int errorCode = 0;

			for (var n = RootNode.FirstChild; n != null; n = n.NextSibling)
			{
				switch (n.Name)
				{
					case "error_code":
						errorCode = Int32.Parse(n.InnerText);
						break;
					case "error_xml":
						errorXml = n.InnerXml;
						break;
					case "error_msg":
						message = n.InnerText;
						break;
					case "request_args":
						requestXml = n.InnerXml;
						break;
					default:
						break;
				}
			}
			return new FacebookException(errorXml, errorCode, message, requestXml);

		}

		string Xml;
		XmlReader Reader;
		XmlDocument doc;
		XmlNode CurrentNode;
		XmlNode RootNode;

		#region IDataReader Members

		public void Close()
		{
			RootNode = null;
			CurrentNode = null;
			doc = null;
			//Reader.Close();
			//Reader = null;
			Xml = null;
		}

		public int Depth
		{
			get { throw new NotImplementedException(); }
		}

		public DataTable GetSchemaTable()
		{
			throw new NotImplementedException();
		}

		public bool IsClosed
		{
			get { return Reader != null; }
		}

		public bool NextResult()
		{
			if (CurrentNode == null)
			{
				if (RootNode != null)
					CurrentNode = RootNode.FirstChild;
			}
			else CurrentNode = CurrentNode.NextSibling;
			return CurrentNode != null;
		}

		public bool Read()
		{
			return NextResult();
			/*if (CurrentNode == null)
				return false;
			return (CurrentNode = CurrentNode.NextSibling) != null;*/
		}

		public int RecordsAffected
		{
			get { return 0; }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			Reader = null;
			Xml = null;
		}

		#endregion

		#region IDataRecord Members

		public int FieldCount
		{
			get { return 0; }
		}

		public bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		public byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		public string GetDataTypeName(int i)
		{
			throw new NotImplementedException();
		}

		public DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		public double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		public Type GetFieldType(int i)
		{
			throw new NotImplementedException();
		}

		public float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		public Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		public short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		public int GetInt32(int i)
		{
			throw new NotImplementedException();
		}

		public long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		public string GetName(int i)
		{
			throw new NotImplementedException();
		}

		public int GetOrdinal(string name)
		{
			throw new NotImplementedException();
		}

		public string GetString(int i)
		{
			throw new NotImplementedException();
		}

		public object GetValue(int i)
		{
			throw new NotImplementedException();
		}

		public int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		public bool IsDBNull(int i)
		{
			throw new NotImplementedException();
		}

		public object this[string name]
		{
			get
			{
				for (var cn = CurrentNode.FirstChild; cn != null; cn = cn.NextSibling)
				{
					if (cn.Name == name)
					{
						return ConvertPropertyFromXml(CurrentNode.Name, name, cn.InnerText);
					}
				}
				return null;
			}
		}

		public object this[int i]
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		object ConvertPropertyFromXml(string objectName, string propertyName, string value)
		{
			if (objectName.StartsWith("app."))
				objectName = objectName.Substring(4);
			var td = KnownTypeData.GetTypeDataByTableName(objectName);
			if (td == null)
				return ConvertNativePropertyFromXml(objectName, propertyName, value);
			var pd = td.GetPropertyByFieldName(propertyName);
			var propType = pd.PropertyInfo.PropertyType;
			var uType = Nullable.GetUnderlyingType(propType);
			if (uType!=null && uType!=propType) //is nullable
			{
				if (value.IsNullOrEmpty())
					return null;
				propType = uType;
			}
			if (propType == typeof(Int64))
			{
				return ParseLong(value);
			}
			else if (propType == typeof(Decimal))
			{
				return ParseDecimal(value);
			}
			else if (propType == typeof(string))
			{
				return value;
			}
			else if (propType == typeof(bool))
			{
				return ParseBool(value);
			}
			else if (propType == typeof(DateTime))
			{
				return ParseFacebookDateTime(value);
			}
			else if (propType == typeof(Int32))
			{
				return ParseInt32(value);
			}
			else if (propType.IsEnum)
			{
				return ParseFacebookEnum(propType, value);
			}
			else
				throw new NotImplementedException();
		}



		private object ParseFacebookEnum(Type propType, string value)
		{
			if (value.IsNullOrEmpty())
				return ReflectionHelper.GetDefaultValue(propType);
			value = value.Split('_').StringConcat("");
			return Enum.Parse(propType, value, true);
		}

		private static long ParseLong(string value)
		{
			if (value.IsNullOrEmpty())
				return 0;
			return Int64.Parse(value);
		}

		private int ParseInt32(string value)
		{
			return XmlConvert.ToInt32(value);
		}

		private static decimal ParseDecimal(string value)
		{
			if (value.IsNullOrEmpty())
				return 0;
			return Decimal.Parse(value);
		}

		private object ConvertNativePropertyFromXml(string objectName, string propertyName, string value)
		{
			switch (objectName)
			{
				case "friend_info":
					switch (propertyName)
					{
						case "uid1":
						case "uid2":
							return ParseLong(value);
					}
					break;
			}
			throw new NotImplementedException();
		}

		private TypeData GetFacebookExtendedTypeData(string objectName)
		{
			switch (objectName)
			{
				case "friend_info":
					{
						return new TypeData { FqlTableName = "friend_info", Properties = new Dictionary<string, PropertyData> { { "uid1", new PropertyData { FqlFieldName = "uid1", IsLinqIdentity = false, PropertyInfo = null } } } };
					}
				default:
					throw new NotImplementedException();
			}
		}

		public static bool ParseBool(string value)
		{
			switch (value.ToLower())
			{
				case "true":
				case "yes":
					return true;
				case "false":
				case "no":
					return false;
				case "0":
					return false;
				default:
					{
						int n = 0;
						if (Int32.TryParse(value, out n))
							return n != 0;
						return false;
					}
			}
		}
		public static DateTime ParseFacebookDateTime(string s)
		{
			if (s.IsNullOrEmpty())
				return DateTime.MinValue;
			int i = 0;
			if (Int32.TryParse(s, out i))
				return DateHelper.ConvertDoubleToDate(Convert.ToDouble(i));
			return DateTime.Parse(s);
		}
	}
}
