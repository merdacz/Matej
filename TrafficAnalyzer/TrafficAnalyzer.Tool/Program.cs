namespace TrafficAnalyzer.Tool
{
    using System;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;
    using TrafficAnalyzer.Tool.Support;

    using static Support.Wiring;

    public class Program
    {
        private readonly IConfigurationProvider configuration;

        private readonly ILogQueries queries;

        private readonly ILogStorage storage;

        private readonly ICrawlerTrafficProcessor crawlerTrafficProcessor;

        public Program(
            IConfigurationProvider configuration,
            ILogQueries queries,
            ILogStorage storage,
            ICrawlerTrafficProcessor crawlerTrafficProcessor)
        {
            this.configuration = configuration;
            this.queries = queries;
            this.storage = storage;
            this.crawlerTrafficProcessor = crawlerTrafficProcessor;
        }

        public static void Main(string[] args)
        {
            var program = Bootstrap();
            program.Run();
        }

        public static Program Bootstrap()
        {
            return new Program(Inject<IConfigurationProvider>(), Inject<ILogQueries>(), Inject<ILogStorage>(), Inject<ICrawlerTrafficProcessor>());
        }

        public void Run()
        {
            var logsPath = this.configuration.GetLogsPath();
            var now = DateTime.Now;
            var trafficReport = this.queries.GetTrafficReport(
                logsPath,
                now.AddHours(-24),
                now);

            var trafficDtos = this.crawlerTrafficProcessor.Process(trafficReport);
            this.storage.RefreshTrafficReport(trafficDtos);
        }
    }
}
