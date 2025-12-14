namespace Frameworks3.Models.Entities
{
    public class SpaceCache
    {
        public long Id { get; set; }
        public string Source { get; set; } = null!;
        public DateTime FetchedAt { get; set; } = DateTime.UtcNow;
        public string Payload { get; set; } = null!;
    }
}
