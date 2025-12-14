namespace Frameworks3.Models.Entities
{
    public class Telemetry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsOk { get; set; }
        public double Voltage { get; set; }
        public double Temp { get; set; }
        public string SourceFile { get; set; } = string.Empty;
    }
}
