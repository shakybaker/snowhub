using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace facebook.Linq
{
	internal abstract class SqlSource : SqlNode
	{
		// Methods
		internal SqlSource(SqlNodeType nt, Expression sourceExpression)
			: base(nt, sourceExpression)
		{
		}
	}
}
