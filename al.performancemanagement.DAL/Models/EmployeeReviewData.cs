using System;

namespace al.performancemanagement.DAL.Models
{
    public class EmployeeReviewData:BaseEntity
    {
        public long ReviewTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long EmployeeId { get; set; }
        public long SupervisorId { get; set; }
        public decimal EmployeeAverageScore { get; set; }
        public decimal SupervisorAverageScore { get; set; }
        public string Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}
