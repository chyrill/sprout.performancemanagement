using al.performancemanagement.BOL.Model;
using al.performancemanagement.DAL.Models;
using AutoMapper;

namespace al.performancemanagement.BOL
{
    public class MappingConfiguration
    {
        private static bool DestinationSourceMappingLoaded = false;
        private static bool SourceDestinationMappingLoaded = false;
        public static bool Loaded { get { return DestinationSourceMappingLoaded && SourceDestinationMappingLoaded; } }

        public static void LoadConfiguration()
        {
            if (!DestinationSourceMappingLoaded)
            {
                DestinationSourceMapping();
            }

            if (!SourceDestinationMappingLoaded)
            {
                SourceDestinationMapping();
            }
        }

        private static void DestinationSourceMapping()
        {
            Mapper.CreateMap<UserLogin, UserLoginData>();
            Mapper.CreateMap<UserInfo, UserInfoData>();
            Mapper.CreateMap<ReviewTemplate, ReviewTemplateData>();
            Mapper.CreateMap<Rating, RatingData>();
            Mapper.CreateMap<EmployeeReview, EmployeeReviewData>();
            Mapper.CreateMap<AnswerItem, AnswerItemData>();

            DestinationSourceMappingLoaded = true;
        }

        private static void SourceDestinationMapping()
        {
            Mapper.CreateMap<UserLoginData, UserLogin>();
            Mapper.CreateMap<UserInfoData, UserInfo>();
            Mapper.CreateMap<ReviewTemplateData, ReviewTemplate>();
            Mapper.CreateMap<RatingData, Rating>();
            Mapper.CreateMap<EmployeeReviewData, EmployeeReview>();
            Mapper.CreateMap<AnswerItemData, AnswerItem>();

            SourceDestinationMappingLoaded = true;
        }
    }
}
