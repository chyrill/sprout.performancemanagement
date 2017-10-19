namespace al.performancemanagement.DAL.Models
{
    public class RatingData:BaseEntity
    {
        public long ReviewTemplateId { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public string Description { get; set; }
    }
}
