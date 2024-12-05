using System.Collections.Generic;
using System.Net;

namespace HttpRequester
{
    public enum HttpMethod
    {
        GET,
        Post,
        Put,
        Delete,
    }
    public class HttpRequester
    {
        public string Url { get; set; }
        public HttpMethod Method { get; set; }
        public bool IsAuthorized { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<HeaderParams> Params { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public string Body { get; set; }
    }
    public class HeaderParams
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static HeaderParams ContentApplicationJson()
        {
            return new HeaderParams()
            {
                Key = "content-type",
                Value = "application/json"
            };
        }
        public static HeaderParams GetHeaderParam(HeaderType type, string value)
        {
            switch (type)
            {
                case HeaderType.authorization:
                    return new HeaderParams()
                    {
                        Key = "authorization",
                        Value = value
                    };

                case HeaderType.contenttype:
                    return new HeaderParams()
                    {
                        Key = "content-type",
                        Value = value
                    };
                case HeaderType.accept:
                    return new HeaderParams()
                    {
                        Key = "accept",
                        Value = value
                    };
                case HeaderType.cookie:
                    return new HeaderParams()
                    {
                        Key = "cookie",
                        Value = value
                    };
                case HeaderType.useragent:
                    return new HeaderParams()
                    {
                        Key = "user-agent",
                        Value = value
                    };
                case HeaderType.acceptlanguage:
                    return new HeaderParams()
                    {
                        Key = "accept-language",
                        Value = value
                    };
                case HeaderType.referer:
                    return new HeaderParams()
                    {
                        Key = "referer",
                        Value = value
                    };
            }
            return null;
        }
    }

    public enum HeaderType
    {
        authorization,
        contenttype,
        accept,
        useragent,
        cookie,
        acceptlanguage,
        referer

    };

}
