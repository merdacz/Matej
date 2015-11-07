namespace TrafficAnalyzer.Tests.Builders
{
    public class InvalidW3LogBuilder
    {
        public TemporaryFile Build()
        {
            return new TemporaryFile(writer => writer.WriteLine("not good!"));
        }
    }
}