namespace TrafficAnalyzer.Tool.Detection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

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

            return this.MergeDuplicatingEntries(result);
        }

        private IList<CrawlerTraffic> MergeDuplicatingEntries(IList<CrawlerTraffic> entries)
        {
            return entries.GroupBy(x => x.CrawlerName)
                    .Select(
                        group =>
                        new CrawlerTraffic()
                        {
                            CrawlerName = group.First().CrawlerName,
                            AccessAttempts = group.Sum(item => item.AccessAttempts),
                            TransferedBytes = group.Sum(item => item.TransferedBytes)
                        })
                    .ToList();
        }

    }
}