using System;
using System.Linq.Expressions;

namespace al.performancemanagement.DAL.Helpers
{
    public class SortExpression<T>
    {
        public Expression<Func<T, object>> Sort { get; set; }
        public bool IsAscending { get; set; }
        public SortExpression(Expression<Func<T, object>> sort, bool isAscending)
        {
            Sort = sort;
            IsAscending = isAscending;
        }
        public SortExpression(Expression<Func<T, object>> sort)
            : this(sort, true)
        {
        }
        public SortExpression()
            : this(null, true)
        {
        }
    }
}