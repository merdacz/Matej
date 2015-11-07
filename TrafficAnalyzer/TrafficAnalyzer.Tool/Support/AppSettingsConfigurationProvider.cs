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
    }
}