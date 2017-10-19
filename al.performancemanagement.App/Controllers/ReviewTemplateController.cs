using al.performancemanagement.BOL;
using al.performancemanagement.BOL.BO;
using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace al.performancemanagement.App.Controllers
{
    public class ReviewTemplateController:ApiController
    {
        ReviewTemplateBO _bo = new ReviewTemplateBO();

        [HttpPost]
        [Route("api/reviewtemplate")]
        public async Task<Result<ReviewTemplate>> Create([FromBody]ReviewTemplate data)
        {
            return await _bo.Create(new Request<ReviewTemplate>(data));
        }

        [HttpGet]
        [Route("api/reviewtemplate/{id}")]
        public async Task<Result<ReviewTemplate>> GetById([FromUri]long id)
        {
            return await _bo.GetById(new Request<long>(id));
        }

        [HttpPut]
        [Route("api/reviewtemplate")]
        public async Task<Result<ReviewTemplate>> Update([FromBody]ReviewTemplate data)
        {
            return await _bo.Update(new Request<ReviewTemplate>(data));
        }

        [HttpGet]
        [Route("api/reviewtemplate")]
        public async Task<SearchResult<ReviewTemplate>> Search(ODataQueryOptions<ReviewTemplate> queryOptions)
        {
            var request = new ConvertSearchRequest<ReviewTemplate, ReviewTemplate>().ConvertToSearchRequest<ReviewTemplate>(queryOptions);

            return await _bo.Search(null);
        }
    }
}