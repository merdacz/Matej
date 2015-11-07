namespace TrafficAnalyzer.Tests.Builders
{
    using System;
    using System.IO;

    public class TemporaryFile : IDisposable
    {
        private readonly FileInfo fileInfo;

        public string FileName => this.fileInfo.FullName;

        public TemporaryFile(Action<TextWriter> fillcontent)
        {
            var fileName = Path.GetTempFileName();
            this.fileInfo = new FileInfo(fileName) { Attributes = FileAttributes.Temporary };
            using (var writer = this.fileInfo.AppendText())
            {
                fillcontent(writer);
                writer.Flush();
                writer.Close();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.fileInfo.Exists)
            {
                this.fileInfo.Delete();
            }
        }
    }
}