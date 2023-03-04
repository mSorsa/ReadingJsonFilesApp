namespace ReadingJsonFiles.Models
{
    public class WeatherReport
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public List<City> Cities { get; set; } = new();
    }
}
