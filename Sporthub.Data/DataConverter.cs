using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Sporthub.Model;

namespace Sporthub.Data
{
	public class DataConverter 
    {
		public static List<T> ToTypeList<T>(DataTable table) 
        {
			Type listType = typeof(List<>).MakeGenericType(new Type[] { typeof(T) });
			List<T> listInstance = (List<T>)Activator.CreateInstance(listType, null);

			foreach (DataRow row in table.Rows)
				listInstance.Add(ToType<T>(row));

			return listInstance;
		}

		public static T ToType<T>(DataRow row) 
        {
			T type = (T)Activator.CreateInstance(typeof(T));
			object data = null;
			string propertyName;

			PropertyInfo[] properties = typeof(T).GetProperties();

			foreach (PropertyInfo property in properties) 
            {
				try 
                {
					propertyName = property.Name;

					DataColumnReference[] dataCols = (DataColumnReference[])property.GetCustomAttributes(typeof(DataColumnReference), false);
					if (dataCols.Length > 0) 
                    {
						propertyName = dataCols[0].Name;
					}
					data = row[propertyName];

				}
				catch {
					//Ignore the exception                        
				}

				if (data != null) 
                {
					if (!(data is DBNull)) 
                    {
						property.SetValue(type, data, null);
					}
				}
				data = null;
			}

			return type;
		}

		public static T ToType<T>(DataSet dataset) 
        {
			T type = (T)Activator.CreateInstance(typeof(T));
			object data = null;
			string propertyName;

			DataRowCollection rows = dataset.Tables[0].Rows;

			if (rows.Count > 0) 
            {
				PropertyInfo[] properties = typeof(T).GetProperties();

				foreach (PropertyInfo property in properties) {
					try {
						propertyName = property.Name;

						DataColumnReference[] dataCols = (DataColumnReference[])property.GetCustomAttributes(typeof(DataColumnReference), false);
						if (dataCols.Length > 0) {
							propertyName = dataCols[0].Name;
						}
						data = rows[0][propertyName];

					}
					catch {
						//Ignore the exception                        
					}

					if (data != null) {
						if (!(data is DBNull)) {
							property.SetValue(type, data, null);
						}
					}

					data = null;

				}
			}
			return type;
		}
	}
}