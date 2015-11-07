namespace TrafficAnalyzer.Tool.Parsing
{
    using System;
    using System.Runtime.Serialization;

    public class LogQueryingException : Exception, ISerializable
    {
        public LogQueryingException()
        {
        }

        public LogQueryingException(string message) : base(message)
        {
        }
        public LogQueryingException(string message, Exception inner) : base(message, inner)
        {
        }

        protected LogQueryingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}