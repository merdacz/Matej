namespace TrafficAnalyzer.Tool.Detection
{
    using System.Collections.Generic;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Parsing;

    public class CrawlerTrafficProcessor : ICrawlerTrafficProcessor
    {
        private readonly ICrawlerDetector detector;

        public CrawlerTrafficProcessor(ICrawlerDetector detector)
        {
            this.detector = detector;
        }

        public IList<CrawlerTraffic> Process(TrafficReport report)
        {
            var result = new List<CrawlerTraffic>();
            foreach (var entry in report.Entries)
            {
                var crawler = this.detector.Recognize(entry);
                if (crawler == Crawler.Unrecognized)
                {
                    continue;
                }

                var traffic = new CrawlerTraffic();
                traffic.CrawlerName = $"{crawler.Name}({entry.Ip})";
                traffic.AccessAttempts = entry.AccessAttempts;
                traffic.TransferedBytes = entry.TransferedBytes;

                result.Add(traffic);
            }

            return result;
        }

    }
}