namespace TrafficAnalyzer.Tool.Support
{
    using System;
    using System.Collections.Generic;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;

    public static class Wiring
    {
        private static readonly Dictionary<Type, Func<object>> Map;

        static Wiring()
        {
            Map = new Dictionary<Type, Func<object>>()
            {
                [typeof(IConfigurationProvider)] = () => new AppSettingsConfigurationProvider(),
                [typeof(ILogQueries)] = () => new WrapErrors(new W3LogParserQueries()),
                [typeof(ILogStorage)] = () => new SimpleDataLogStorage(),
                [typeof(ICrawlerTrafficProcessor)] = () => new CrawlerTrafficProcessor(new UserAgentBasedCrawlerDetector()),
            };
        }

        public static T Inject<T>()
        {
            var creator = Map[typeof(T)];
            return (T)creator();
        }
    }
}