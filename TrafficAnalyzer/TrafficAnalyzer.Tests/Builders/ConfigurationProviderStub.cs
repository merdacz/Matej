namespace TrafficAnalyzer.Tests.Builders
{
    using TrafficAnalyzer.Tool;
    using TrafficAnalyzer.Tool.Support;

    internal class ConfigurationProviderStub : IConfigurationProvider
    {
        private readonly string logsPath;

        public ConfigurationProviderStub(string logsPath)
        {
            this.logsPath = logsPath;
        }

        public string GetLogsPath()
        {
            return this.logsPath;
        }
    }
}