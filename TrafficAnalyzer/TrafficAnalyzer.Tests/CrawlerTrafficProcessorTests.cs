namespace TrafficAnalyzer.Tests
{
    using FluentAssertions;

    using TrafficAnalyzer.Tests.Builders;
    using TrafficAnalyzer.Tool;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;
    using TrafficAnalyzer.Tool.Support;

    using Xunit;

    public class CrawlerTrafficProcessorTests
    {
        [Fact]
        public void only_keeps_recognized_crawler_entries()
        {
            var report = new TrafficReport();
            report.AddEntry("1.1.1.1", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)".CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser(), 12, 2048);
            report.AddEntry("2.1.2.1", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)".CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser(), 132, 11048);
            report.AddEntry(
                "1.1.1.1",
                "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1".CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser(),
                1,
                100);

            var sut = new CrawlerTrafficProcessor(new UserAgentBasedCrawlerDetector());

            var trafficDtos = sut.Process(report);
            trafficDtos.Should().HaveCount(2);
            trafficDtos.Should().NotContain(x => x.CrawlerName == Crawler.Unrecognized.Name);
        }
    }
}