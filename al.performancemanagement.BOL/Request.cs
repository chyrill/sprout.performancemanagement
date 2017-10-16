using System.Collections.Generic;

namespace al.performancemanagement.BOL
{
    public partial class Request<T>
    {
        public T Model { get; set; }
        public Request(T data)
        {
            Model = data;
        }
    }

    public class Request<TModel, TID>
    {
        public Request()
        {
            NewCollection = new List<TModel>();
            DbIdCollection = new List<TID>();
        }

        public IEnumerable<TModel> NewCollection { get; set; }
        public IEnumerable<TID> DbIdCollection { get; set; }
    }
}
