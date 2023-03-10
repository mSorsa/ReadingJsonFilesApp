namespace ReadingJsonFiles.ReportData
{
    public class City
    {
        public string Name { get; set; } = string.Empty;
        public float Temperature { get; set; } = default;
        public bool IsCloudy { get; set; } = false;

        internal string IsCloudyToString() 
            => IsCloudy ? "Yes" : "No";
    }
}
