using System;

namespace al.performancemanagement.DAL.Models
{
    public class ReviewTemplateData:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointsPerItem { get; set; }
        public string Questions { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
