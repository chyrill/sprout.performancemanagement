using System;
using System.Linq.Expressions;

namespace al.performancemanagement.DAL.Helpers
{
    public partial class SearchRequest<T> : SearchAllRequest<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public SearchRequest()
        {
            PageSize = 20;
        }
    }
    public partial class SearchAllRequest<T>
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public SortExpression<T>[] Sort { get; set; }
    }
}
