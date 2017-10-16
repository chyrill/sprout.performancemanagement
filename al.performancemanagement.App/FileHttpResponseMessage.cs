using System.IO;
using System.Net.Http;

namespace al.performancemanagement.App
{
    public class FileHttpResponseMessage:HttpResponseMessage
    {
        string m_filePath;

        public FileHttpResponseMessage(string filePath)
        {
            m_filePath = filePath;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Content.Dispose();
            File.Delete(m_filePath);
        }
    }
}