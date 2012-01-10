using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace facebook.Linq
{
	class FqlExecuteResult : IExecuteResult
	{
		public FqlExecuteResult(string responseXml, Type resultType, Type tableRowType, QueryOptions options)
		{
			ResponseXml = responseXml;
			TableRowType = tableRowType;
			ResultType = resultType;
			QueryOptions = options;
			if (options.Select != null)
			{
				//Select = options.Select;
				TableRowType = (options.Select as LambdaExpression).Parameters[0].Type;
			}

			//var funcType = typeof(Func<>).MakeGenericType(tableRowType, resultType);
			//Activator.CreateInstance(typeof(Expression).MakeGenericType(funcType));
			//Expression<Func<tableRowType, resultType>> e =new Expression<Func<object,object>>(
		}
		QueryOptions QueryOptions;
		string ResponseXml;
		Type TableRowType;
		Type ResultType;

		#region IExecuteResult Members

		public object GetParameterValue(int parameterIndex)
		{
			throw new NotImplementedException();
		}

		public object ReturnValue
		{
			get
			{
				if (QueryOptions.IsCount)
				{
					var Reader = new FacebookDataReader(ResponseXml);
					return Reader.Count;
				}
				return Activator.CreateInstance(typeof(FqlQueryResultEnumerable<,>).MakeGenericType(TableRowType, ResultType), ResponseXml, QueryOptions.Select);
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}

	class QueryOptions
	{
		public Expression Select;
		//public bool IsScalar;
		public bool IsCount;
	}
}
