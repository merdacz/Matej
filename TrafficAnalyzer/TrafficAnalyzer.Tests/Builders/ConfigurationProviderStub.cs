namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using TrafficAnalyzer.Tool;
    using TrafficAnalyzer.Tool.Support;

    internal class ConfigurationProviderStub : IConfigurationProvider
    {
        private readonly string logsPath;

        private readonly TimeSpan logsTimespan;

        public ConfigurationProviderStub(string logsPath, TimeSpan logsTimespan)
        {
            this.logsPath = logsPath;
            this.logsTimespan = logsTimespan;
        }

        public string GetLogsPath()
        {
            return this.logsPath;
        }

        public TimeSpan GetLogsTimespan()
        {
            return this.logsTimespan;
        }
    }
}