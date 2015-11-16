namespace TrafficAnalyzer.Tool.Parsing
{
    using System;

    using MSUtil;

    public class W3LogParserQueries : ILogQueries
    {
        public TrafficReport GetTrafficReport(string filePath, DateTime startDate, DateTime endDate)
        {
            var query =
               $@"
SELECT c-ip AS Ip, cs(User-Agent) AS UserAgent, COUNT (*) AS AccessAttempts, ADD(SUM(cs-bytes), SUM(sc-bytes)) As TransferedBytes
FROM {filePath}
WHERE 
    TO_LOCALTIME(TO_TIMESTAMP(date, time)) >= TO_LOCALTIME(TIMESTAMP('{startDate:yyyy-MM-dd HH:mm}', 'yyyy-MM-dd HH:mm')) 
    AND 
    TO_LOCALTIME(TO_TIMESTAMP(date, time)) <= TO_LOCALTIME(TIMESTAMP('{endDate:yyyy-MM-dd HH:mm}', 'yyyy-MM-dd HH:mm'))
GROUP BY Ip, UserAgent";
            return this.Execute(query);
        }

        public TrafficReport GetUnboundedTrafficReport(string filePath)
        {
            var query =
              $@"
SELECT c-ip AS Ip, cs(User-Agent) AS UserAgent, COUNT (*) AS AccessAttempts, ADD(SUM(cs-bytes), SUM(sc-bytes)) As TransferedBytes
FROM {filePath}
GROUP BY Ip, UserAgent";
            return this.Execute(query);
        }

        private TrafficReport Execute(string query)
        {
            var report = new TrafficReport();
            var parser = new LogQueryClass();
            var log = new COMW3CInputContextClass();

            var records = parser.Execute(query, log);
            while (!records.atEnd())
            {
                var record = records.getRecord();
                var ip = record.getValue("Ip");
                var userAgent = record.getValue("UserAgent");
                var accessAttempts = record.getValue("AccessAttempts");
                var transferedBytes = record.getValue("TransferedBytes");

                report.AddEntry(ip, userAgent, accessAttempts, transferedBytes);
                records.moveNext();
            }

            return report;
        }
    }
}