using al.performancemanagement.BOL;
using al.performancemanagement.BOL.BO;
using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace al.performancemanagement.App.Controllers
{
    public class EmployeeReviewController:ApiController
    {
        EmployeeReviewBO _bo = new EmployeeReviewBO();

        [Route("api/employeereview")]
        [HttpPost]
        public async Task<Result<EmployeeReview>> Create([FromBody]EmployeeReview data)
        {
            return await _bo.Create(new Request<EmployeeReview>(data));
        }

        [Route("api/employeereview/{id}")]
        [HttpGet]
        public async Task<Result<EmployeeReview>> GetById([FromUri]long id)
        {
            return await _bo.GetById(new Request<long>(id));
        }

        [Route("api/employeereview")]
        [HttpGet]
        public async Task<SearchResult<EmployeeReview>> Search(ODataQueryOptions<EmployeeReview> queryOptions)
        {
            var request = new ConvertSearchRequest<EmployeeReview, EmployeeReview>().ConvertToSearchRequest<EmployeeReview>(queryOptions);

            return await _bo.Search(request);
        }

        [Route("api/employeereview")]
        [HttpPut]
        public async Task<Result<EmployeeReview>> Update([FromBody]EmployeeReview data)
        {
            return await _bo.Update(new Request<EmployeeReview>(data));
        }

        [Route("api/answer/employeereview")]
        [HttpPost]
        public async Task<Result<EmployeeReview>> Answer([FromBody]EmployeeReview data)
        {
            return await _bo.Answer(new Request<EmployeeReview>(data));
        }

        [Route("api/employeereview/{empId}")]
        [HttpGet]
        public async Task<Result<EmployeeReview>> GetByEmpId([FromUri]long id)
        {
            
        }
    }
}