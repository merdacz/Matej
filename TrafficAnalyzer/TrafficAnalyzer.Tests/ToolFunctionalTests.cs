namespace TrafficAnalyzer.Tests
{
    using FluentAssertions;

    using TrafficAnalyzer.Tests.Builders;
    using TrafficAnalyzer.Tool;

    using Xunit;

    public class ToolFunctionalTests
    {
        [Fact]
        public void success_scenario()
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
    }
}