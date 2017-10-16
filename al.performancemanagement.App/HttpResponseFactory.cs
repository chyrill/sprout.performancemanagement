using System.Net;
using System.Net.Http;

namespace al.performancemanagement.App
{
    public class HttpResponseFactory
    {
        public static HttpResponseMessage Create(HttpStatusCode statusCode, string content)
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode);
            response.Content = new StringContent(content.ToString());
            return response;
        }
    }
}