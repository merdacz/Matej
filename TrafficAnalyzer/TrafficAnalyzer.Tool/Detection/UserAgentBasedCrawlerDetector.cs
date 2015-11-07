namespace TrafficAnalyzer.Tool.Detection
{
    using System.Collections.Generic;

    using TrafficAnalyzer.Tool.Parsing;
    using TrafficAnalyzer.Tool.Support;

    /// <summary>
    /// Detects crawler based on user agent. 
    /// </summary>
    /// <remarks>
    /// Currently supports only GoogleBot and is based on User Agent listing from 
    /// https://support.google.com/webmasters/answer/1061943?hl=en
    /// </remarks>
    public class UserAgentBasedCrawlerDetector : ICrawlerDetector
    {
        private IDictionary<string, Crawler> knownCrawlers = 
            new Dictionary<string, Crawler>()
                {
                    ["Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)"] = Crawler.Googlebot,
                    ["Googlebot/2.1 (+http://www.google.com/bot.html)"] = Crawler.Googlebot,
                    ["Scrubby/2.2 (http://www.scrubtheweb.com/)"] = Crawler.Scrubby,
                    ["Mozilla/5.0 (compatible; Scrubby/2.2; +http://www.scrubtheweb.com/)"] = Crawler.Scrubby,
                    ["Mozilla/5.0 (compatible; Scrubby/2.2; http://www.scrubtheweb.com/)"] = Crawler.Scrubby,
                    ["Scrubby/2.1 (http://www.scrubtheweb.com/)"] = Crawler.Scrubby,
                    ["Mozilla/5.0 (compatible; Scrubby/2.1; +http://www.scrubtheweb.com/abs/meta-check.html)"] = Crawler.Scrubby,
                    ["Gigabot/3.0 (http://www.gigablast.com/spider.html)"] = Crawler.Gigabot,
                    ["Gigabot 2.0"] = Crawler.Gigabot,
                    ["Gigabot/2.0/gigablast.com/spider.html"] = Crawler.Gigabot,
                    ["Gigabot/2.0 (http://www.gigablast.com/spider.html)"] = Crawler.Gigabot,
                    ["Gigabot/2.0"] = Crawler.Gigabot,
                    ["Gigabot 1.0"] = Crawler.Gigabot,
                    ["Gigabot/1.0"] = Crawler.Gigabot,
            };
        public Crawler Recognize(TrafficReportEntry entry)
        {
            foreach (var crawler in this.knownCrawlers)
            {
                if (crawler.Key.CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser() == entry.UserAgent)
                {
                    return crawler.Value;
                }
            }

            return Crawler.Unrecognized;
        }
    }
}