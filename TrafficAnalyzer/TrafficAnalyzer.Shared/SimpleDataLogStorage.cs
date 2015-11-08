namespace TrafficAnalyzer.Shared
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Simple.Data;

    public class SimpleDataLogStorage : ILogStorage
    {
        public void RefreshTrafficReport(IList<CrawlerTraffic> traffic)
        {
            var db = Database.Open();
            db.CrawlerTraffic.DeleteAll();
            db.CrawlerTraffic.Insert(traffic).ToList();
        }

        public IEnumerable<CrawlerTraffic> GetAll()
        {
            var db = Database.Open();
            var entries = db.CrawlerTraffic.All();
            return this.Map(entries);
        }

        public IList<CrawlerTraffic> GetDashboardReport()
        {
            var db = Database.Open();
            var entries = db.CrawlerTraffic.All();
            IEnumerable<CrawlerTraffic> x = this.Map(entries);
            return x.ToList();
        } 

        public void SwitchToInMemory()
        {
            var adapter = new InMemoryAdapter();
            Database.UseMockAdapter(adapter);
        }

        private IEnumerable<CrawlerTraffic> Map(dynamic entries)
        {
            foreach (var entry in entries)
            {
                yield return
                    new CrawlerTraffic()
                    {
                        CrawlerName = entry.CrawlerName,
                        AccessAttempts = entry.AccessAttempts,
                        TransferedBytes = entry.TransferedBytes
                    };
            }
        }
    }
}
 