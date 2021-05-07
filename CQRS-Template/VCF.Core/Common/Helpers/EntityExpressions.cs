using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Project.Core.Helpers
{
	public static class EntityExpressions
	{
		public static Expression<Func<string, string, bool>> HasValueAndMatches = (string dbString, string inputString) =>
			dbString != null ? dbString.ToLower().Contains(inputString.ToLower()) : false;
	}

	public static class EntityExpressions<E> where E : Enum
	{
		public static Expression<Func<E, E[], bool>> HasValueAndMatches = (E dbEnum, E[] inputEnum) =>
			dbEnum != null ? inputEnum.Contains(dbEnum) : false;
	}
}
