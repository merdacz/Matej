namespace TrafficAnalyzer.Tool.Support
{
    using System;
    using System.Configuration;

    public class AppSettingsConfigurationProvider : IConfigurationProvider
    {
        public string GetLogsPath()
        {
            var logsPath = ConfigurationManager.AppSettings["iis.logs.path"];
            if (string.IsNullOrEmpty(logsPath))
            {
                Console.WriteLine("Invalid configuration. Please check whether logs path has been set up. ");
                Environment.Exit(1);
            }

            return logsPath;
        }

        public TimeSpan GetLogsTimespan()
        {
            var logsTimespan = ConfigurationManager.AppSettings["iis.logs.timespan"];
            if (string.IsNullOrEmpty(logsTimespan))
            {
                return TimeSpan.MaxValue;
            }

            TimeSpan parsed;
            TimeSpan.TryParse(logsTimespan, out parsed);
            if (parsed == TimeSpan.Zero)
            {
                Console.WriteLine("Invalid configuration. Please check whether logs timespan has been set up correctly. ");
                Environment.Exit(1);
            }

            return parsed;
        }
    }
}