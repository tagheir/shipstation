namespace Generics.Common
{
    public class TemplateConstants
    {
        public const string CdnPath = "https://bluebellcdn.s3.ap-south-1.amazonaws.com/";//"https://nopstorageaccount.blob.core.windows.net/content/";

        public class Templates
        {
            public const string BasePath = "~/Views/Templates";
            public const string CategoryPath = BasePath + "/Category/{0}.cshtml";
            public const string ProductPath = BasePath + "/Product/{0}.cshtml";
            public const string TopicPath = BasePath + "/Topic/{0}.cshtml";
        }
    }
}
