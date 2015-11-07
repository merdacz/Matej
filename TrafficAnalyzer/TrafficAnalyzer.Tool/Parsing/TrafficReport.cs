namespace TrafficAnalyzer.Tool.Parsing
{
    using System.Collections.Generic;

    public class TrafficReport
    {
        private readonly IList<TrafficReportEntry> entries = new List<TrafficReportEntry>();

        public IEnumerable<TrafficReportEntry> Entries => this.entries;

        public void AddEntry(string ip, string userAgent, int accessAttempts, long transferedBytes)
        {
            var entry = new TrafficReportEntry(ip, userAgent, accessAttempts, transferedBytes);
            this.entries.Add(entry);
        }
    }
}