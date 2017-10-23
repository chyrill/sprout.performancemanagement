using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Helpers;
using al.performancemanagement.DAL.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace al.performancemanagement.BOL.BO
{
    public class RatingBO
    {
        public RatingBO()
        {
            if (!MappingConfiguration.Loaded)
                MappingConfiguration.LoadConfiguration();
        }

        RatingDataRepository _repo = new RatingDataRepository();

        public async Task<SearchResult<Rating>> SearchById(Request<long> id)
        {
            var result = new SearchResult<Rating>();
            try
            {
                var ratingRes = _repo.Search(new SearchRequest<DAL.Models.RatingData>()
                {
                    Filter = f => f.ReviewTemplateId == id.Model
                });

                List<Rating> items = new List<Rating>();

                foreach(var item in ratingRes.Items)
                {
                    items.Add(Mapper.Map<Rating>(item));
                }

                result.Items = items.OrderBy(x=>x.RangeFrom).AsQueryable<Rating>();
                result.Successful = true;
                result.Message = "Successfully retrieve data";
                result.SearchTotal = ratingRes.SearchTotal;

                return result;

            }
            catch(Exception e)
            {
                return new SearchResult<Rating>(e.Message);
            }
        }
    }
}
