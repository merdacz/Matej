using System;

namespace TrafficAnalyzer.Tool.Support
{
    public interface IConfigurationProvider
    {
        string GetLogsPath();

        TimeSpan GetLogsTimespan();
    }
}