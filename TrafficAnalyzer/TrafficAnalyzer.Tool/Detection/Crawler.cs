namespace TrafficAnalyzer.Tool.Detection
{
    /// <summary>
    /// Represents known crawlers we can detect.
    /// </summary>
    public class Crawler
    {
        public static readonly Crawler Unrecognized = new Crawler("Unrecognized", "Not suspected to be crawler. ");

        public static readonly Crawler Googlebot = new Crawler("Googlebot", "https://support.google.com/webmasters/answer/1061943?hl=en");

        public static readonly Crawler Scrubby = new Crawler("Scrubby", "http://www.useragentstring.com/pages/Scrubby/");

        public static readonly Crawler Gigabot = new Crawler("Gigabot", "http://www.useragentstring.com/pages/Gigabot/");

        private Crawler(string name, string moreDetails)
        {
            this.Name = name;
            this.MoreDetails = moreDetails;
        }

        public string Name { get; }

        public string MoreDetails { get; }
    }
}