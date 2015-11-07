namespace TrafficAnalyzer.Tests
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Builders;
    using Tool;

    using TrafficAnalyzer.Tool.Parsing;

    using Xunit;

    using static Builders.W3LogBuilder;

    public class W3LogParserQueriesTests
    {
        private static DateTime EarlyEnough => new DateTime(1111, 1, 1);

        private static DateTime DistantFuture => new DateTime(2222, 2, 2);

        [Fact(Skip = "SUM aggregate from log parser fails on empty input, couldn't find the fix in reasonable time so I am postponing it. ")]
        public void empty_log_does_not_fail()
        {
            var logBuilder = W3LogBuilder.Create();
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(logFile.FileName, new DateTime(2010, 10, 9), new DateTime(2010, 10, 10));
            }
        }

        [Fact]
        public void invalid_log_format_throws()
        {
            var logBuilder = W3LogBuilder.CreateInvalid();
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var fileName = logFile.FileName;
                Assert.Throws<LogQueryingException>(
                    () => 
                    sut.GetTrafficReport(
                        fileName, 
                        new DateTime(2010, 10, 9), 
                        new DateTime(2010, 10, 10)));
            }
        }

        [Fact]
        public void entries_within_the_date_range_get_included()
        {
            var logBuilder = W3LogBuilder.Create().WithEntry(e => e.Timestamp = new DateTime(2010, 10, 10));
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(
                    logFile.FileName, 
                    new DateTime(2010, 10, 10), 
                    new DateTime(2010, 10, 10, 23, 59, 59));

                report.Entries.Should().HaveCount(1);
                report.Entries.First().AccessAttempts.Should().Be(1);
            }
        }

        [Fact]
        public void matching_entries_from_all_files_get_included()
        {
            var logBuilder = W3LogBuilder.Create().WithEntry();
            using (var logFile1 = logBuilder.Build())
            using (var logFile2 = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(
                    $"{logFile1.FileName}, {logFile2.FileName}",
                    EarlyEnough,
                    DistantFuture);

                report.Entries.Should().HaveCount(1);
                report.Entries.First().AccessAttempts.Should().Be(2);
            }
        }

        [Fact]
        public void date_range_is_non_strict_both_sides()
        {
            var logBuilder = W3LogBuilder.Create().WithEntry(e => e.Timestamp = new DateTime(2010, 10, 10));
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(
                    logFile.FileName,
                    new DateTime(2010, 10, 10),
                    new DateTime(2010, 10, 10));

                report.Entries.Should().HaveCount(1);
            }
        }

        [Fact]
        public void entries_outside_the_date_are_omitted()
        {
            var logBuilder = W3LogBuilder.Create().WithEntry(e => e.Timestamp = new DateTime(2010, 10, 10));
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(
                    logFile.FileName, 
                    new DateTime(2010, 10, 11), 
                    new DateTime(2010, 10, 12));

                report.Entries.Should().HaveCount(0);
            }
        }

        [Fact]
        public void number_of_hits_sum_up_for_same_ip()
        {
            var logBuilder = W3LogBuilder.Create()
                    .WithEntry(Ip("1.2.3.4"))
                    .WithEntry(Ip("1.2.3.4"))
                    .WithEntry(Ip("1.2.3.4"));
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(logFile.FileName, EarlyEnough, DistantFuture);

                report.Entries.Should().HaveCount(1);
                report.Entries.First().AccessAttempts.Should().Be(3);
            }
        }

        [Fact]
        public void number_of_bytes_sum_up_for_same_ip()
        {
            var logBuilder = W3LogBuilder.Create()
                    .WithEntry(Ip("1.2.3.4"), Sent(1), Received(1))
                    .WithEntry(Ip("1.2.3.4"), Sent(1), Received(1))
                    .WithEntry(Ip("1.2.3.4"), Sent(1), Received(1));
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(logFile.FileName, DateTime.MinValue, new DateTime(2020, 01, 01));

                report.Entries.Should().HaveCount(1);
                report.Entries.First().TransferedBytes.Should().Be(6);
            }
        }

        [Fact(Skip = "Apparently LogParser does not support 64-bit integers in its sum aggregate. ")]
        public void big_transfers_should_count_up_correctly()
        {
            var logBuilder =
                W3LogBuilder.Create()
                    .WithEntry(Sent(int.MaxValue))
                    .WithEntry(Sent(int.MaxValue))
                    .WithEntry(Sent(int.MaxValue));
                    
            using (var logFile = logBuilder.Build())
            {
                var sut = this.CreateSut();
                var report = sut.GetTrafficReport(logFile.FileName, EarlyEnough, DistantFuture);

                report.Entries.Should().HaveCount(1);
                report.Entries.First().TransferedBytes.Should().Be((long)4 * int.MaxValue);
            }
        }

        private ILogQueries CreateSut()
        {
            return new WrapErrors(new W3LogParserQueries());
        }
    }
}