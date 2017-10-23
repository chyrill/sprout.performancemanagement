using al.performancemanagement.BOL.BO;
using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using System.Threading.Tasks;
using System.Web.Http;

namespace al.performancemanagement.App.Controllers
{
    public class RatingController:ApiController
    {
        RatingBO _bo = new RatingBO();

        [HttpGet]
        [Route("api/rating/{id}")]
        public async Task<SearchResult<Rating>> Search([FromUri]long id)
        {
            return await _bo.SearchById(new BOL.Request<long>(id));
        }
    }
}