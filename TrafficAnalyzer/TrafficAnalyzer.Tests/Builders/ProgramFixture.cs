namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using System.Collections.Generic;

    using Tool;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;
    using TrafficAnalyzer.Tool.Support;

    using static Tool.Support.Wiring;

    public class ProgramFixture : IDisposable
    {
        private TemporaryFile temporaryFile;

        private ILogStorage logStorage;
        
        private TimeSpan logsTimespan = TimeSpan.FromHours(24);

        public IEnumerable<CrawlerTraffic> InsertedEntries => this.logStorage?.GetAll();

        private ProgramFixture()
        {
        }

        public static ProgramFixture Create()
        {
            return new ProgramFixture();
        }

        public ProgramFixture With(W3LogBuilder builder)
        {
            this.temporaryFile = builder.Build();
            return this;
        }

        public ProgramFixture WithDefaultTimespan()
        {
            this.logsTimespan = TimeSpan.FromHours(24);
            return this;
        }

        public ProgramFixture WithUnboundedTimespan()
        {
            this.logsTimespan = TimeSpan.MaxValue; 
            return this;
        }

        public Program Build()
        {
            this.logStorage = Inject<ILogStorage>();
            this.logStorage.SwitchToInMemory();
            var configuration = new ConfigurationProviderStub(this.temporaryFile.FileName, this.logsTimespan);
            return new Program(
                configuration,
                Inject<ILogQueries>(),
                this.logStorage,
                Inject<ICrawlerTrafficProcessor>());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.temporaryFile?.Dispose();
            }
        }
    }
}