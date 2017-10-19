namespace al.performancemanagement.BOL.Model
{
    public class AnswerItem
    {
        public long Id { get; set; }
        public long EmployeeReviewId { get; set; }
        public string Question { get; set; }
        public decimal EmployeeScore { get; set; }
        public string EmployeeRemark { get; set; }
        public decimal SupervisorScore { get; set; }
        public string SupervisorRemark { get; set; }
    }
}
