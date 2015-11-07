namespace TrafficAnalyzer.Shared
{
    using System.Collections.Generic;

    public interface ILogStorage
    {
        void RefreshTrafficReport(IList<CrawlerTraffic> traffic);

        IEnumerable<CrawlerTraffic> GetAll();

        IList<CrawlerTraffic> GetDashboardReport();

        void SwitchToInMemory();
    }
}