namespace C
{
    using System;
    using System.Diagnostics;

    using Xunit.Abstractions;

    public class MeassureTime : IDisposable
    {
        private readonly ITestOutputHelper output;

        private readonly Stopwatch stopwatch;

        public MeassureTime(ITestOutputHelper output)
        {
            this.output = output;
            this.stopwatch = new Stopwatch();
            GC.Collect();
            this.stopwatch.Start();
        }

        public void Dispose()
        {
            this.stopwatch.Stop();
            this.output.WriteLine($"Took {this.stopwatch.Elapsed.TotalMilliseconds} ms. ");
        }
    }
}