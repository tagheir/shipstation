using System.Collections.Generic;

namespace Generics.Common
{
    public class StringConstants
    {
        /* Product */
        public const string PRODUCT_NOT_FOUND = "Product Not Found";
    }

    public class EmailConstants
    {
        public const string SENDER_EMAIL = "wajiha.mazhar@octacer.com";
        public const string SENDER_NAME = "Team bluebell";
    }

    public class ResourceConstants
    {
        public static List<string> LandscapeServicesIcons
            => new List<string>()
                {
                    "/img/landscape/installation-design.svg",
                    "/img/landscape/maintenance.svg",
                    "/img/landscape/consultancy.svg",
                    "/img/landscape/planters.svg"
                };
    }
}
