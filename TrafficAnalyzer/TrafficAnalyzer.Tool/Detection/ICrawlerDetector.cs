namespace TrafficAnalyzer.Tool.Detection
{
    using TrafficAnalyzer.Tool.Parsing;

    public interface ICrawlerDetector
    {
        Crawler Recognize(TrafficReportEntry entry);
    }
}