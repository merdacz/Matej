namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class W3LogBuilder
    {
        public static DateTime ALittleBitBefore => DateTime.Now.AddHours(-2);

        public static DateTime ALongTimeAgo => DateTime.Now.AddDays(-11);

        private readonly IList<W3LogEntry> entries = new List<W3LogEntry>();

        private W3LogBuilder()
        {
        }

        public static W3LogBuilder Create()
        {
            return new W3LogBuilder();
        }

        public static InvalidW3LogBuilder CreateInvalid()
        {
            return new InvalidW3LogBuilder();
        }

        public W3LogBuilder WithEntry(params Action<W3LogEntry>[] adjustments)
        {
            var entry = this.CreateDefaultEntry();
            foreach (var adjustment in adjustments)
            {
                adjustment(entry);
            } 

            this.entries.Add(entry);
            return this;
        }

        public W3LogBuilder WithMultipleCrawlerFreshEntries()
        {
            this.WithEntry(Ip("1.1.1.1"), Fresh, GoogleBot);
            this.WithEntry(Ip("2.2.2.2"), Fresh, GoogleBot);
            this.WithEntry(Ip("3.3.3.3"), Fresh, GoogleBot);
            return this;
        }

        public W3LogBuilder WithMultipleCrawlerOldEntries()
        {
            this.WithEntry(Ip("1.1.1.1"), Old, GoogleBot);
            this.WithEntry(Ip("2.2.2.2"), Old, GoogleBot);
            this.WithEntry(Ip("3.3.3.3"), Old, GoogleBot);
            return this;
        }

        public W3LogBuilder WithMultipleRegularUsersFreshEntries()
        {
            this.WithEntry(Ip("1.1.1.1"), Fresh, Firefox);
            this.WithEntry(Ip("2.2.2.2"), Fresh, Firefox);
            this.WithEntry(Ip("3.3.3.3"), Fresh, Firefox);
            return this;
        }

        public TemporaryFile Build()
        {
            return new TemporaryFile(this.FillContent);
        }

        public static Action<W3LogEntry> Ip(string ip)
        {
            return entry => entry.Client_Ip = ip;
        }

        public static Action<W3LogEntry> Sent(int bytes)
        {
            return entry => entry.ClientToServer_Bytes = bytes;
        }

        public static Action<W3LogEntry> Received(int bytes)
        {
            return entry => entry.ServerToClient_Bytes = bytes;
        }

        public static void Fresh(W3LogEntry entry)
        {
            entry.Timestamp = ALittleBitBefore;
        }
        public static void Old(W3LogEntry entry)
        {
            entry.Timestamp = ALongTimeAgo;
        }

        public static void GoogleBot(W3LogEntry entry)
        {
            entry.ClientToServer_Header_UserAgent = "Googlebot/2.1 (+http://www.google.com/bot.html)";
        }

        public static void Firefox(W3LogEntry entry)
        {
            entry.ClientToServer_Header_UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1";
        }

        private void FillContent(TextWriter writer)
        {
            writer.WriteLine(W3LogEntry.CreateHeader());
            foreach (var entry in this.entries)
            {
                writer.WriteLine(entry.ToString());
            }
        }

        private W3LogEntry CreateDefaultEntry()
        {
            var result = new W3LogEntry();
            result.Timestamp = new DateTime(2011, 07, 30);
            result.ClientToServer_Bytes = 1234;
            result.ClientToServer_UriQuery = "-";
            result.ClientToServer_UriStem = "/couponfollow/";
            result.ClientToServer_UserName = "-";
            result.ClientToServer_Header_Referer = "-";
            result.ClientToServer_Header_UserAgent = "crawler";
            result.ClientToServer_Method = "GET";
            result.Client_Ip = "1.1.1.1";
            result.Server_Ip = "1.1.1.1";
            result.Server_Port = 80;
            result.ServerToClient_Bytes = 4567;
            result.ServerToClient_Win32Status = 0;
            result.ServerToClient_Status = 200;
            result.ServerToClient_SubStatus = 0;
            return result;
        }
    }
}