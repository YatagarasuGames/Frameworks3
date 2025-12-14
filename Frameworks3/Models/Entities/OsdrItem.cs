namespace Frameworks3.Models.Entities
{
    public class OsdrItem
    {
        public long Id { get; set; }
        public string DatasetId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public DateTime InsertedAt { get; set; } = DateTime.UtcNow;
        public string Raw { get; set; } = null!;
    }
}
