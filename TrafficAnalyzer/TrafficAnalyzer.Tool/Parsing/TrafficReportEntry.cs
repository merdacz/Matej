namespace TrafficAnalyzer.Tool.Parsing
{
    public class TrafficReportEntry
    {
        public TrafficReportEntry(string ip, string userAgent, int accessAttempts, long transferedBytes)
        {
            this.Ip = ip;
            this.UserAgent = userAgent;
            this.AccessAttempts = accessAttempts;
            this.TransferedBytes = transferedBytes;
        }

        public string Ip { get; }

        public string UserAgent { get; }

        public int AccessAttempts { get; }

        public long TransferedBytes { get; }
    }
}