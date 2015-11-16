namespace TrafficAnalyzer.Tool.Parsing
{
    using System;

    public class WrapErrors : ILogQueries
    {
        private readonly ILogQueries target;

        public WrapErrors(ILogQueries target)
        {
            this.target = target;
        }

        public TrafficReport GetTrafficReport(string filePath, DateTime startDate, DateTime endDate)
        {
            try
            {
                return this.target.GetTrafficReport(filePath, startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new LogQueryingException(
                    $"Error during processing traffic query for {filePath} and {startDate} to {endDate} period. ",
                    ex);
            }
        }

        public TrafficReport GetUnboundedTrafficReport(string filePath)
        {
            try
            {
                return this.target.GetUnboundedTrafficReport(filePath);
            }
            catch (Exception ex)
            {
                throw new LogQueryingException(
                    $"Error during processing traffic query for {filePath} and unbounded period. ",
                    ex);
            }
        }
    }
}