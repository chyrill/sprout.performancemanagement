using System.Linq;

namespace al.performancemanagement.DAL.Helpers
{
    public partial class SearchResult<T> : Result
    {
        public object SearchContext { get; set; }
        public IQueryable<T> Items { get; set; }
        public int SearchTotal { get; set; }
        public int SearchPages { get; set; }
        public SearchResult()
            : base()
        {
        }
        public SearchResult(object searchContext, IQueryable<T> items, int searchTotal, int searchPages)
            : base()
        {
            SearchContext = searchContext;
            Items = items;
            SearchTotal = searchTotal;
            SearchPages = searchPages;
        }
        public SearchResult(string message)
            : base(message)
        {
        }
    }
}
