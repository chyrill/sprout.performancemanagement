using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using al.performancemanagement.DAL.Models;
using al.performancemanagement.DAL.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace al.performancemanagement.BOL.BO
{
    public class EmployeeReviewBO
    {
        public EmployeeReviewBO()
        {
            if (!MappingConfiguration.Loaded)
                MappingConfiguration.LoadConfiguration();
        }

        EmployeeReviewDataRepository _repo = new EmployeeReviewDataRepository();

        public async Task<Result<EmployeeReview>> Create(Request<EmployeeReview> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Model.Name))
                    return new Result<EmployeeReview>("Name is Required");

                if (request.Model.ReviewTemplateId == 0 || request.Model.ReviewTemplateId == null)
                    return new Result<EmployeeReview>("Review Template is required");

                if (request.Model.ReviewDate == null)
                    return new Result<EmployeeReview>("Review date is required");

                if (request.Model.EmployeeId == 0)
                    return new Result<EmployeeReview>("Employee is required");

                if (request.Model.SupervisorId == 0)
                    return new Result<EmployeeReview>("Supervisor is required");

                request.Model.DateCreated = DateTime.Now;

                var searchExist = _repo.Search(new SearchRequest<DAL.Models.EmployeeReviewData>()
                {
                    Filter = f => f.Name == request.Model.Name
                });

                if (searchExist.SearchTotal > 0)
                    return new Result<EmployeeReview>("Employee Review is already exist");

                request.Model.Status = "Scheduled";

                var createRes = _repo.Insert(Mapper.Map<EmployeeReviewData>(request.Model));

                if (!createRes)
                    return new Result<EmployeeReview>("Unable to add employee review data");

                var searchEmployeeReview = _repo.Search(new SearchRequest<EmployeeReviewData>()).Items.OrderByDescending(x=>x.Id).FirstOrDefault();

                var searchReviewTemplate = new ReviewTemplateDataRepository().GetById(request.Model.ReviewTemplateId);

                CreateAnswer(searchReviewTemplate.Questions, searchEmployeeReview.Id);

                return new Result<EmployeeReview>() { Successful = true, Message = "Successfully created employee review data", Model = request.Model };
            }
            catch(Exception e)
            {
                return new Result<EmployeeReview>(e.Message);
            }
        }

        public async Task<Result<EmployeeReview>> GetById(Request<long> id)
        {
            try
            {
                var searchData = _repo.GetById(id.Model);

                if (searchData == null)
                    return new Result<EmployeeReview>("Record not found");

                var result = Mapper.Map<EmployeeReview>(searchData);

                var searchEmployee = new UserInfoDataRepository().GetById(result.EmployeeId);
                var searchSupervisor = new UserInfoDataRepository().GetById(result.SupervisorId);

                result.EmployeeName = searchEmployee.LastName + ", " + searchEmployee.FirstName;
                result.SupervisorName = searchSupervisor.LastName + ", " + searchSupervisor.FirstName;

                var searchAnswerItem = new AnswerItemDataRepository().Search(new SearchRequest<AnswerItemData>()
                {
                    Filter = f => f.EmployeeReviewId == searchData.Id
                });

                List<AnswerItem> data = new List<AnswerItem>();

                var searchRatingList = new RatingDataRepository().Search(new SearchRequest<RatingData>()
                {
                    Filter = f => f.ReviewTemplateId == result.ReviewTemplateId
                });

                List<Rating> ratingList = new List<Rating>();

                foreach(var rating in searchRatingList.Items)
                {
                    ratingList.Add(Mapper.Map<Rating>(rating));
                }

                result.RatingArray = ratingList.OrderBy(x => x.RangeFrom).ToList();
                data = Mapper.Map<List<AnswerItem>>(searchAnswerItem.Items.ToList());

                result.AnswerScore = data;

                return new Result<EmployeeReview>() { Successful = true, Message = "Successfully retrieve records", Model = result };

            }
            catch (Exception e)
            {
                return new Result<EmployeeReview>(e.Message);
            }
        }

        public async Task<SearchResult<EmployeeReview>> Search(SearchRequest<EmployeeReview> request)
        {
            var result = new SearchResult<EmployeeReview>();
            try
            {
                var dataSearchRequest = new SearchRequest<EmployeeReviewData>();

                new ConvertSearchRequest<EmployeeReview, EmployeeReviewData>().ConvertToDataSearchRequest(request, dataSearchRequest);

                var searchRes = _repo.Search(dataSearchRequest);

                if (searchRes.SearchTotal <= 0)
                    return new SearchResult<EmployeeReview>("No record found");

                List<EmployeeReview> list = new List<EmployeeReview>();

                foreach(var item in searchRes.Items)
                {
                    var itemdata = Mapper.Map<EmployeeReview>(item);

                    var searchemployee = new UserInfoDataRepository().GetById(item.EmployeeId);

                    itemdata.EmployeeName = searchemployee.LastName + ", " + searchemployee.FirstName;

                    var searchSupervisor = new UserInfoDataRepository().GetById(item.SupervisorId);

                    itemdata.SupervisorName = searchSupervisor.LastName + ", " + searchSupervisor.FirstName;

                    list.Add(itemdata);
                }

                result.Items = list.AsQueryable<EmployeeReview>();
                result.SearchTotal = searchRes.SearchTotal;
                result.SearchPages = searchRes.SearchPages;
                result.Successful = true;
                result.Message = "Successfully retrieve records";

                return result;
            }
            catch(Exception e)
            {
                return new SearchResult<EmployeeReview>(e.Message);
            }
        }

        public void CreateAnswer(string questions, long id)
        {
            try
            {
                var searchAnswerItemRes = new AnswerItemDataRepository().Search(new SearchRequest<AnswerItemData>()
                {
                    Filter = f => f.EmployeeReviewId == id
                });

                if(searchAnswerItemRes.SearchTotal>0)
                {
                    foreach(var item in searchAnswerItemRes.Items)
                    {
                        new AnswerItemDataRepository().Delete(item.Id);
                    }
                }

                var data = questions.Split('|');

                foreach (var question in data)
                {
                    AnswerItem item = new AnswerItem()
                    {
                        EmployeeReviewId = id,
                        Question = question,
                        EmployeeRemark = null,
                        EmployeeScore = 0,
                        SupervisorRemark = null,
                        SupervisorScore = 0
                    };

                    new AnswerItemDataRepository().Insert(Mapper.Map<AnswerItemData>(item));
                }
            }
            catch(Exception e)
            {

            }
        }

        public async Task<Result<EmployeeReview>> Update(Request<EmployeeReview> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Model.Name))
                    return new Result<EmployeeReview>("Name is Required");

                if (request.Model.ReviewTemplateId == 0 || request.Model.ReviewTemplateId == null)
                    return new Result<EmployeeReview>("Review Template is required");

                if (request.Model.ReviewDate == null)
                    return new Result<EmployeeReview>("Review date is required");

                if (request.Model.EmployeeId == 0)
                    return new Result<EmployeeReview>("Employee is required");

                if (request.Model.SupervisorId == 0)
                    return new Result<EmployeeReview>("Supervisor is required");

                request.Model.Rating = null;
                request.Model.EmployeeAverageScore = 0;
                request.Model.SupervisorAverageScore = 0;

                var updateRes = _repo.Update(Mapper.Map<EmployeeReviewData>(request.Model));

                if (!updateRes)
                    return new Result<EmployeeReview>("Unable to update data");

                var searchReviewTemplate = new ReviewTemplateDataRepository().GetById(request.Model.ReviewTemplateId);

                CreateAnswer(searchReviewTemplate.Questions, request.Model.Id);

                return new Result<EmployeeReview>() { Successful = true, Message = "Successfully updated record", Model = request.Model };
            }
            catch(Exception e)
            {
                return new Result<EmployeeReview>(e.Message);
            }
        }

        public async Task<Result<EmployeeReview>> Answer(Request<EmployeeReview> request)
        {
            try
            {
                decimal ave = 0,total=0;
                if(request.Model.Status == "Employee Review")
                {
                    int cnt = 1;
                    foreach(var item in request.Model.AnswerScore)
                    {
                        total = total + item.EmployeeScore;
                        cnt++;

                        new AnswerItemDataRepository().Update(Mapper.Map<AnswerItemData>(item));
                    }

                    ave = total / cnt;

                    request.Model.EmployeeAverageScore = ave;
                    request.Model.Status = "Supervisor Review";

                    var res = _repo.Update(Mapper.Map<EmployeeReviewData>(request.Model));

                    if (!res)
                        return new Result<EmployeeReview>("Unable to save review");

    
                    
                }

                else if (request.Model.Status =="Supervisor Review")
                {
                    int cnt = 1;
                    foreach (var item in request.Model.AnswerScore)
                    {
                        total = total + item.SupervisorScore;
                        cnt++;

                        new AnswerItemDataRepository().Update(Mapper.Map<AnswerItemData>(item));
                    }

                    ave = total / cnt;

                    request.Model.SupervisorAverageScore = ave;

                    var getRatings = new RatingDataRepository().GetDescription(request.Model.ReviewTemplateId, ave);

                    request.Model.Rating = getRatings.FirstOrDefault().Description;
                    request.Model.Status = "Reviewed";

                    var res = _repo.Update(Mapper.Map<EmployeeReviewData>(request.Model));

                    if (!res)
                        return new Result<EmployeeReview>("Unable to save review");

                   
                }

                return new Result<EmployeeReview>() { Successful = true, Message = "Successfully recorded review", Model = request.Model };
            }
            catch(Exception e)
            {
                return new Result<EmployeeReview>(e.Message);
            }
        }

        public async Task<SearchResult<EmployeeReview>> SearchByEmployee(Request<long> id)
        {
            var result = new SearchResult<EmployeeReview>();
            try
            {
                var searchRes = _repo.Search(new SearchRequest<EmployeeReviewData>() {
                    Filter = f=>(f.EmployeeId ==id.Model && f.Status=="Employee Review" ) || (f.SupervisorId == id.Model &&f.Status=="Supervisor Review" )
                });

                if (searchRes.SearchTotal <= 0)
                    return new SearchResult<EmployeeReview>("No record found");

                List<EmployeeReview> list = new List<EmployeeReview>();

                foreach (var item in searchRes.Items)
                {
                    list.Add(Mapper.Map<EmployeeReview>(item));
                }

                result.Items = list.AsQueryable<EmployeeReview>();
                result.SearchTotal = searchRes.SearchTotal;
                result.SearchPages = searchRes.SearchPages;
                result.Successful = true;
                result.Message = "Successfully retrieve records";

                return result;
            }
            catch(Exception e)
            {
                return new SearchResult<EmployeeReview>(e.Message);
            }
        }

        public async Task<SearchResult<EmployeeReview>> SearchAllByEmployee(Request<long> id)
        {
            var result = new SearchResult<EmployeeReview>();
            try
            {
                var searchRes = _repo.Search(new SearchRequest<EmployeeReviewData>()
                {
                    Filter = f => (f.EmployeeId == id.Model && (f.Status == "Employee Review" || f.Status == "Reviewed")) || (f.SupervisorId == id.Model && (f.Status == "Supervisor Review" || f.Status == "Reviewed"))
                });

                if (searchRes.SearchTotal <= 0)
                    return new SearchResult<EmployeeReview>("No record found");

                List<EmployeeReview> list = new List<EmployeeReview>();

                foreach (var item in searchRes.Items)
                {
                    var itemdata = Mapper.Map<EmployeeReview>(item);

                    var searchemployee = new UserInfoDataRepository().GetById(item.EmployeeId);

                    itemdata.EmployeeName = searchemployee.LastName + ", " + searchemployee.FirstName;

                    var searchSupervisor = new UserInfoDataRepository().GetById(item.SupervisorId);

                    itemdata.SupervisorName = searchSupervisor.LastName + ", " + searchSupervisor.FirstName;

                    list.Add(itemdata);
                }

                result.Items = list.AsQueryable<EmployeeReview>();
                result.SearchTotal = searchRes.SearchTotal;
                result.SearchPages = searchRes.SearchPages;
                result.Successful = true;
                result.Message = "Successfully retrieve records";

                return result;
            }
            catch (Exception e)
            {
                return new SearchResult<EmployeeReview>(e.Message);
            }
        }
    }
}
