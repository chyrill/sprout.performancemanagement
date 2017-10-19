using al.performancemanagement.App.Models;
using al.performancemanagement.BOL;
using al.performancemanagement.BOL.BO;
using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace al.performancemanagement.App.Controllers
{
    public class UserController : ApiController
    {
        UserBO _bo = new UserBO();

        [HttpPost]
        [Route("api/user/login")]
        public async Task<Result<UserLogin>> Login([FromBody]User request)
        {
            UserLogin data = new UserLogin();
            data.Username = request.Username;
            data.Password = request.Password;

            var result =  await _bo.Login(new Request<UserLogin>(data));

            return result;
        }

        [HttpPost]
        [Route("api/user")]
        public async Task<Result<UserLogin>> CreateUser([FromBody]UserInfo request)
        {
            return await _bo.CreateUser(new Request<UserInfo>(request));
        }
    }    
}