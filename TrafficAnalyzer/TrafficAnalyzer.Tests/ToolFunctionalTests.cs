namespace TrafficAnalyzer.Tests
{
    using FluentAssertions;

    using TrafficAnalyzer.Tests.Builders;
    using TrafficAnalyzer.Tool;

    using Xunit;

    public class ToolFunctionalTests
    {
        [Fact]
        public void success_default_timespan_scenario()
        {
            using (var fixture = ProgramFixture.Create())
            {
                fixture.With(W3LogBuilder.Create().WithMultipleCrawlerFreshEntries());
                var sut = fixture.Build();

                sut.Run();
                
                fixture.InsertedEntries.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void no_matching_rows_default_timespan_scenario()
        {
            using (var fixture = ProgramFixture.Create())
            {
                fixture.With(W3LogBuilder.Create().WithMultipleCrawlerOldEntries());
                var sut = fixture.Build();

                sut.Run();

                fixture.InsertedEntries.Should().BeEmpty();
            }
        }

        [Fact]
        public void no_crawlers()
        {
            using (var fixture = ProgramFixture.Create())
            {
                fixture.With(W3LogBuilder.Create().WithMultipleRegularUsersFreshEntries());
                var sut = fixture.Build();

                sut.Run();

                fixture.InsertedEntries.Should().BeEmpty();
            }
        }

        [Fact]
        public void success_unbounded_timespan_scenario()
        {
            using (var fixture = ProgramFixture.Create())
            {
                fixture.WithUnboundedTimespan();
                fixture.With(W3LogBuilder.Create().WithMultipleCrawlerOldEntries());
                var sut = fixture.Build();

                sut.Run();

                fixture.InsertedEntries.Should().NotBeEmpty();
            }
        }
    }
}