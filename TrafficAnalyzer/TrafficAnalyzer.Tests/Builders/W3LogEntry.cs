namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using System.ComponentModel;

    using TrafficAnalyzer.Tool;
    using TrafficAnalyzer.Tool.Support;

    public class W3LogEntry
    {
        public DateTime Timestamp { get; set; }

        public string Client_Ip { get; set; }

        public string Server_Ip { get; set; }

        public int Server_Port { get; set; }

        public string ClientToServer_Method { get; set; }

        public string ClientToServer_UriStem { get; set; }

        public string ClientToServer_UriQuery { get; set; }

        public string ClientToServer_UserName { get; set; }

        public string ClientToServer_Header_UserAgent { get; set; }

        public string ClientToServer_Header_Referer { get; set; }

        public int ClientToServer_Bytes { get; set; }

        public int ServerToClient_Status { get; set; }
        
        public int ServerToClient_SubStatus { get; set; }

        public int ServerToClient_Win32Status { get; set; }

        public int  ServerToClient_Bytes { get; set; }

        public int TimeTaken { get; set; }

        public static string CreateHeader()
        {
            return "#Fields: date time s-ip cs-method cs-uri-stem cs-uri-query s-port cs-username "
                   + "c-ip cs(User-Agent) cs(Referer) sc-status sc-substatus sc-win32-status sc-bytes "
                   + "cs-bytes time-taken";
        }
        public override string ToString()
        {
            var userAgent =
                this.ClientToServer_Header_UserAgent.CorrectSpacesInUserAgentBecauseTheyWontWorkInLogParser();
            return $"{this.Timestamp:yyyy-MM-dd} {this.Timestamp:T} {this.Server_Ip} {this.ClientToServer_Method} "
                   + $"{this.ClientToServer_UriStem} {this.ClientToServer_UriQuery} {this.Server_Port} "
                   + $"{this.ClientToServer_UserName} {this.Client_Ip} {userAgent} "
                   + $"{this.ClientToServer_Header_Referer} {this.ServerToClient_Status} {this.ServerToClient_SubStatus} "
                   + $"{this.ServerToClient_Win32Status} {this.ServerToClient_Bytes} {this.ClientToServer_Bytes} "
                   + $"{this.TimeTaken}";
        }

        

    }
}
