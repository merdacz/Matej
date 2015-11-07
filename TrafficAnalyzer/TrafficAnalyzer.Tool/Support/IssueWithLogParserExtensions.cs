namespace TrafficAnalyzer.Tool.Support
{
    public static class IssueWithLogParserExtensions
    {
        /// <summary>
        /// Corrects user agent string to follow log parser logic. 
        /// </summary>
        /// <remarks>
        /// IIS logs do get encode spaces by plus sign. Since spaces are being used as entries separator.
        /// However some user agents do contain plus sign. In theory enclosing entry in quotes should work 
        /// but at least LogParser does not recognize those correctly. 
        /// </remarks>
        public static string CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser(this string @this)
        {
            return @this.Replace(" ", "+");
        } 
    }
}