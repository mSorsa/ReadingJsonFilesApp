namespace ReadingJsonFiles.Models
{
    public class City
    {
        public string Name { get; set; } = string.Empty;
        public float Temperature { get; set; }
        public bool? IsCloudy { get; set; } = true;
    }
}
