using System;
using System.Collections.Generic;

namespace al.performancemanagement.BOL.Model
{
    public class EmployeeReview
    {
        public long Id { get; set; }
        public long ReviewTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public decimal EmployeeAverageScore { get; set; }
        public decimal SupervisorAverageScore { get; set; }
        public string Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        public List<AnswerItem> AnswerScore { get; set; }
        public List<Rating> RatingArray { get; set; }
    }
}
