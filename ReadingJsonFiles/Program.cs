using ReadingJsonFiles.ReportData;
using System.Text.Json;
using System.Xml.Serialization;

namespace ReadingJsonFiles
{
    class Program
    {
        static void Main()
        {
            var files = ParseFiles();

            foreach (var report in files)
            {
                // Read the JSON file contents
                var json = File.ReadAllText(report);
                try
                {
                    // Deserialize the JSON into WeatherReportData
                    var weatherReportData = JsonSerializer.Deserialize<WeatherReportData>(json);

                    if (weatherReportData is null)
                        throw new JsonException($"Could not Deserialize {report}");

                    // Process the weather reports
                    PrintJsonToConsole(weatherReportData);

                    PrintJsonToXml(weatherReportData);
                }
                catch (JsonException jsonEx)
                {
                    Console.Error.WriteLine($"Exception was thrown: ", jsonEx);
                    Environment.Exit(-1);
                }
            }
        }

        private static string[] ParseFiles()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var inputDir = string.Concat(currentDir.AsSpan(0, currentDir.IndexOf("bin")), "Data\\", "Reports");

            var files = Directory.GetFiles(inputDir) ?? default;

            if (files?.Length < 1 || files is null)
            {
                Console.Error.WriteLine("Could not parse files in search path.");
                Environment.Exit(-1);
            }

            return files;
        }

        private static void PrintJsonToConsole(WeatherReportData weatherReportData)
        {
            foreach (var weatherReport in weatherReportData.WeatherReports)
            {
                Console.WriteLine($"Weather report for {weatherReport.TimeStamp}:");
                foreach (var city in weatherReport.Cities)
                {
                    Console.WriteLine($"\tCity: {city.Name}");
                    Console.WriteLine($"\tTemperature: {city.Temperature}");

                    if (city.IsCloudy.HasValue)
                        Console.WriteLine($"\tIs Cloudy: {city.IsCloudy.Value}");

                    Console.WriteLine();
                }
            }
        }

        private static void PrintJsonToXml(WeatherReportData weatherReports)
        {
            var outputPath = GetOrCreateOutputDirectory();

            foreach (var weatherReport in weatherReports.WeatherReports)
            {
                var filePath = Path.Combine(outputPath, $"{weatherReport.TimeStamp.ToShortDateString()}.xml");

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(WeatherReport));
                    serializer.Serialize(fileStream, weatherReport);
                }
            }
        }

        private static string GetOrCreateOutputDirectory()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var outDirPath = string.Concat(currentDir.AsSpan(0, currentDir.IndexOf("bin")), "Data\\", "Outputs");

            if (!Directory.Exists(outDirPath))
                _ = Directory.CreateDirectory(outDirPath);

            return outDirPath;
        }
    }
}