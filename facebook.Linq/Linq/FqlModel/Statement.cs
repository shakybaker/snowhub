using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace facebook.Linq
{
	public class SqlStatement : SqlNode
	{
		// Methods
		internal SqlStatement(SqlNodeType nodeType, Expression sourceExpression)
			: base(nodeType, sourceExpression)
		{
		}

	}
}
