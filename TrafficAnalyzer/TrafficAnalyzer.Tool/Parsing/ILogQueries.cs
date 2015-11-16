namespace TrafficAnalyzer.Tool.Parsing
{
    using System;

    public interface ILogQueries
    {
        TrafficReport GetTrafficReport(string filePath, DateTime startDate, DateTime endDate);

        TrafficReport GetUnboundedTrafficReport(string filePath);
    }
}