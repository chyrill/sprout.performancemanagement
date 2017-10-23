using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using al.performancemanagement.DAL.Models;
using al.performancemanagement.DAL.Repository;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace al.performancemanagement.BOL.BO
{
    public class ReviewTemplateBO
    {
        public ReviewTemplateBO()
        {
            if (!MappingConfiguration.Loaded)
                MappingConfiguration.LoadConfiguration();
        }

        ReviewTemplateDataRepository _repo = new ReviewTemplateDataRepository();

        public async Task<Result<ReviewTemplate>> Create(Request<ReviewTemplate> request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Model.Name))
                    return new Result<ReviewTemplate>("Name is required");

                if (request.Model.PointsPerItem <= 0)
                    return new Result<ReviewTemplate>("Points per Item is required");

                if (request.Model.QuestionsArray.ToList().Count <= 0)
                    return new Result<ReviewTemplate>("Question is required");

                var searchReviewTemplate = _repo.Search(new SearchRequest<ReviewTemplateData>()
                {
                    Filter = f=>f.Name==request.Model.Name
                });

                if (searchReviewTemplate.SearchTotal > 0)
                    return new Result<ReviewTemplate>("Review Template already exist");

                request.Model.Questions = string.Join("|",request.Model.QuestionsArray);
                var data = Mapper.Map<ReviewTemplateData>(request.Model);
                data.DateCreated = DateTime.Now;

                var createRes = _repo.Insert(data);

                if (!createRes)
                    return new Result<ReviewTemplate>("Unable to create review template");


                var searchRes = _repo.Search(new SearchRequest<ReviewTemplateData>());

                var id = searchRes.Items.OrderByDescending(x => x.Id).FirstOrDefault().Id;

                await CreateRating(data.PointsPerItem, id);

                return new Result<ReviewTemplate>() { Successful = true, Message = "Successfully created review template", Model = request.Model };
            }
            catch(Exception e)
            {
                return new Result<ReviewTemplate>(e.Message);
            }
        }
        
        public async Task<Result<ReviewTemplate>> GetById(Request<long> request)
        {
            try
            {
                var searchData = _repo.GetById(request.Model);

                if (searchData == null)
                    return new Result<ReviewTemplate>("review template not found");

                ReviewTemplate result = new ReviewTemplate();

                result = Mapper.Map<ReviewTemplate>(searchData);

                result.QuestionsArray = searchData.Questions.Split('|');

                return new Result<ReviewTemplate>() { Successful = true, Message = "Succesfully retrieve data", Model = result };
            }
            catch(Exception e)
            {
                return new Result<ReviewTemplate>(e.Message);
            }
        }

        public async Task<Result<ReviewTemplate>> Update(Request<ReviewTemplate> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Model.Name))
                    return new Result<ReviewTemplate>("Name is required");

                if (request.Model.PointsPerItem <= 0)
                    return new Result<ReviewTemplate>("Points per item can not be null or zero");

                if (request.Model.QuestionsArray.Count() < 0)
                    return new Result<ReviewTemplate>("Questions can not be null");


                request.Model.Questions = string.Join("|", request.Model.QuestionsArray);

                var updateRes = _repo.Update(Mapper.Map<ReviewTemplateData>(request.Model));

                if (!updateRes)
                    return new Result<ReviewTemplate>("Unable to update review template record");

                await CreateRating(request.Model.PointsPerItem, request.Model.Id);

                return new Result<ReviewTemplate>() { Successful = true, Message = "Successfully updated data", Model = request.Model };
            }
            catch(Exception e)
            {
                return new Result<ReviewTemplate>(e.Message);
            }
        }

        public async Task<SearchResult<ReviewTemplate>> Search(SearchRequest<ReviewTemplate> queryOptions)
        {
            var result = new SearchResult<ReviewTemplate>();
            try
            {
                var dataSearchRequest = new SearchRequest<ReviewTemplateData>();

                new ConvertSearchRequest<ReviewTemplateData, ReviewTemplate>().ConvertToDataSearchRequest(dataSearchRequest, queryOptions);

                var searchRes = _repo.Search(dataSearchRequest);

                if (searchRes.SearchTotal <= 0)
                    return new SearchResult<ReviewTemplate>("no record found");

                List<ReviewTemplate> data = new List<ReviewTemplate>();

                foreach(var item in searchRes.Items)
                {
                    data.Add(Mapper.Map<ReviewTemplate>(item));
                }

                result.Items = data.AsQueryable<ReviewTemplate>();
                result.Message = "Successfully retrieve data";
                result.Successful = true;
                result.SearchTotal = searchRes.SearchTotal;
                result.SearchPages = searchRes.SearchPages;

                return result;
            }
            catch(Exception e)
            {
                return new SearchResult<ReviewTemplate>(e.Message);
            }
        }

        public async Task<Result> CreateRating(int points, long id)
        {
            try
            {
                var searchData = new RatingDataRepository().Search(new SearchRequest<RatingData>()
                {
                    Filter = f => f.ReviewTemplateId == id
                });

                if(searchData.SearchTotal>0)
                {
                    foreach(var item in searchData.Items)
                    {
                        new RatingDataRepository().Delete(item.Id);
                    }
                }

                var div = points / 5;

                int cnt = 1;
                for(decimal i = 0; i < points;)
                {
                    Rating data = new Rating() {
                        RangeFrom = i,
                        ReviewTemplateId = id
                    };

                    if (div <= 1)
                    {
                        if (cnt == 5)
                            data.RangeTo = points;

                        else
                            data.RangeTo = i + div - (decimal)0.01;
                    }
                        
                    else
                        data.RangeTo = i + div;
                    
                    if(cnt == 1)
                    {
                        data.Description = "very poor";
                    }
                    else if (cnt == 2)
                    {
                        data.Description = "poor";
                    }
                    else if (cnt == 3)
                    {
                        data.Description = "Average";
                    }
                    else if(cnt ==4)
                    {
                        data.Description = "Above Average";
                    }
                    else if (cnt == 5)
                    {
                        data.Description = "Outstanding";
                    }
                    
                    var insertRes = new RatingDataRepository().Insert(Mapper.Map<RatingData>(data));

                    i = i + div;
                    cnt++;
                }

                return new Result() { Successful = true, Message = "Successfully created rating table" };
            }
            catch(Exception e)
            {
                return new Result(e.Message);
            }
        }
    }
}
