namespace al.performancemanagement.BOL.Model
{
    public class Rating
    {
        public long Id { get; set; }
        public long ReviewTemplateId { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public string Description { get; set; }
    }
}
