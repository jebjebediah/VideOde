namespace VideOde.Core
{
    public class Clip
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Length { get; set; }
        public string? Description { get; set; }
    }
}