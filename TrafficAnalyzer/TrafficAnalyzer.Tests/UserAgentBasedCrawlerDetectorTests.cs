namespace TrafficAnalyzer.Tests
{
    using FluentAssertions;

    using TrafficAnalyzer.Tool;
    using TrafficAnalyzer.Tool.Detection;
    using TrafficAnalyzer.Tool.Parsing;
    using TrafficAnalyzer.Tool.Support;

    using Xunit;

    public class UserAgentBasedCrawlerDetectorTests
    {
        [Fact]
        public void detects_googlebot()
        {
            var userAgent = "Googlebot/2.1 (+http://www.google.com/bot.html)".CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser();
            var sut = new UserAgentBasedCrawlerDetector();
            var entry = new TrafficReportEntry("1.1.1.1", userAgent, 1, 1);
            var crawler = sut.Recognize(entry);
            crawler.Should().Be(Crawler.Googlebot);
        }

        [Fact]
        public void does_not_identify_regular_connection_as_crawler()
        {
            var sut = new UserAgentBasedCrawlerDetector();
            var entry = new TrafficReportEntry("1.1.1.1", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1", 1, 1);
            var crawler = sut.Recognize(entry);
            crawler.Should().Be(Crawler.Unrecognized);
        }
    }
}