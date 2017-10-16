using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using al.performancemanagement.DAL.Models;
using al.performancemanagement.DAL.Repository;
using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace al.performancemanagement.BOL.BO
{
    public class UserBO
    {
        public UserBO()
        {
            if (!MappingConfiguration.Loaded)
                MappingConfiguration.LoadConfiguration();
        }

        public async Task<Result<UserLogin>> CreateUser(Request<UserInfo> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Model.FirstName))
                    return new Result<UserLogin>("First Name is required") { ResultCode = ErrorCodes.Field_value_Is_Required_Error };

                if (string.IsNullOrWhiteSpace(request.Model.LastName))
                    return new Result<UserLogin>("Last Name is required") { ResultCode = ErrorCodes.Field_value_Is_Required_Error };

                if (string.IsNullOrWhiteSpace(request.Model.Email))
                    return new Result<UserLogin>("Email is required") { ResultCode = ErrorCodes.Field_value_Is_Required_Error };

                var searchUser = new UserInfoDataRepository().Search(new SearchRequest<UserInfoData>()
                {
                    Filter  =f =>(f.FirstName == request.Model.FirstName && f.LastName == request.Model.LastName) || f.Email == request.Model.Email
                });

                if (searchUser.SearchTotal > 0)
                    return new Result<UserLogin>("User is already have a record");

                var data = Mapper.Map<UserInfoData>(request.Model);

                var createRes =  new UserInfoDataRepository().Insert(data);

                if (!createRes)
                    return new Result<UserLogin>("User can not be inserted.");

                var searchuserdata = new UserInfoDataRepository().Search(new SearchRequest<UserInfoData>());

                var userdata = searchuserdata.Items.OrderByDescending(x => x.Id).FirstOrDefault();
                
                var userlogin = new UserLoginData()
                {
                    UserInfoId = userdata.Id,
                    Username = request.Model.Email,
                    Password = request.Model.FirstName
                };

                var createUserLoginRes = new UserLoginDataRepository().Insert(Mapper.Map<UserLoginData>(userlogin));

                if (!createUserLoginRes)
                    return new Result<UserLogin>("Unable to create user login");

                return new Result<UserLogin> { Successful = true, Message = "Successfully created user" };


            }
            catch (Exception e)
            {
                return new Result<UserLogin>(e.Message) { Successful = false, ResultCode = ErrorCodes.Exception_Error };
            }
        }

        public async Task<Result<UserLogin>> Login(Request<UserLogin> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Model.Username))
                    return new Result<UserLogin>("Username is required");
                if (string.IsNullOrWhiteSpace(request.Model.Password))
                    return new Result<UserLogin>("Password is required");

                var searchUserLoginRes = new UserLoginDataRepository().Search(new SearchRequest<UserLoginData>()
                {
                    Filter = f => f.Username == request.Model.Username
                });

                if (searchUserLoginRes.Items.FirstOrDefault().Attempt > 3)
                    return new Result<UserLogin>("Account is locked");

                var userLoginData = searchUserLoginRes.Items.Where(x => x.Password == request.Model.Password);

                if (userLoginData == null)
                {
                    var data = searchUserLoginRes.Items.FirstOrDefault();
                    data.Attempt++;

                    if (data.Attempt > 3)
                    {
                        var updateUserLoginRes = new UserLoginDataRepository().Update(data);
                        return new Result<UserLogin>("Account is locked");
                    }

                    return new Result<UserLogin>("Invalid Username or password");
                }
                else
                {
                    var data = userLoginData.FirstOrDefault();

                    data.Attempt = 0;
                    data.LastSuccessfulLogin = DateTime.Now;
                    data.AuthCode = "at_" + new Guid().ToString();
                    var updateUserLoginRes = new UserLoginDataRepository().Update(data);

                    var searchUserInfo = new UserInfoDataRepository().GetById(data.UserInfoId);

                    UserLogin result = new UserLogin();
                    result = Mapper.Map<UserLogin>(data);
                    result.UserInfo = Mapper.Map<UserInfo>(searchUserInfo);

                    return new Result<UserLogin>() { Successful = true, Message = "Successfully Logged In", Model = result };
                }

            }
            catch (Exception e)
            {
                return new Result<UserLogin>(e.Message) { ResultCode = ErrorCodes.Exception_Error };
            }
        }
    }
}
