using al.performancemanagement.DAL.Helpers;
using System.Collections;
using System.Threading.Tasks;

namespace al.performancemanagement.DAL.Repository
{
    public interface IRepository<TModel>
    {
        bool Insert(TModel data);
        bool Update(TModel data);
        bool Delete(object id);
        TModel GetById(object id);
        SearchResult<TModel> Search(SearchRequest<TModel> request);
    }
}
