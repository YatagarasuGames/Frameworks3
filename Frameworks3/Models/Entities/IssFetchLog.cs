namespace Frameworks3.Models.Entities
{
    public class IssFetchLog
    {
        public long Id { get; set; }
        public DateTime FetchedAt { get; set; } = DateTime.UtcNow;
        public string SourceUrl { get; set; } = null!;
        public string Payload { get; set; } = null!;
    }
}
