using System.Net;

namespace HttpRequester
{
    public class HttpResponser
    {
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Response { get; set; }
        public string Cookie { get; set; }
        public CookieCollection CookieContainer { get; set; }
       

    }
}
