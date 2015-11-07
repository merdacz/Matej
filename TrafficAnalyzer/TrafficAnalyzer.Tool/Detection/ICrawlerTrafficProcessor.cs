namespace TrafficAnalyzer.Tool.Detection
{
    using System.Collections.Generic;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Parsing;

    public interface ICrawlerTrafficProcessor
    {
        IList<CrawlerTraffic> Process(TrafficReport report);
    }
}