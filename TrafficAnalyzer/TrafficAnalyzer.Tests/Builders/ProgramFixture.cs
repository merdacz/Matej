namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using System.Collections.Generic;

    using Tool;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;

    using static Tool.Support.Wiring;

    public class ProgramFixture : IDisposable
    {
        private TemporaryFile temporaryFile;

        private ILogStorage logStorage;

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

        public Program Build()
        {
            this.logStorage = Inject<ILogStorage>();
            this.logStorage.SwitchToInMemory();
            return new Program(
                new ConfigurationProviderStub(this.temporaryFile.FileName),
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