using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Infrastructure.ListUtil.Extensions.IQueryable
{
    public abstract class OrderBy<TItem, TValue> : Dictionary<string, Expression<Func<TItem, TValue>>>
    {
        protected OrderBy()
            : base(new CaseInsensitiveEqualityComparer()) { }
    }

    internal class CaseInsensitiveEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y) => string.Equals(x, y, StringComparison.OrdinalIgnoreCase);

        public int GetHashCode(string obj) => obj.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }

    public class OrderByBool<TItem> : OrderBy<TItem, bool> { }
    public class OrderByString<TItem> : OrderBy<TItem, string> { }
    public class OrderByInt<TItem> : OrderBy<TItem, int?> { }
    public class OrderByDateTime<TItem> : OrderBy<TItem, DateTime?> { }
    public class OrderByDateTimeOffset<TItem> : OrderBy<TItem, DateTimeOffset?> { }
    public class OrderByDecimal<TItem> : OrderBy<TItem, decimal?> { }
}
