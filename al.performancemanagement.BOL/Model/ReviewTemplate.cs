using System;

namespace al.performancemanagement.BOL.Model
{
    public class ReviewTemplate
    {
        public long Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointsPerItem { get; set; }
        public string Questions { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public string[] QuestionsArray { get; set; }
    }
}
