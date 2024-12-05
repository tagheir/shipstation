using System;

namespace Generics.Db
{
    public class SiteMetaDto
    {
        public string KEY { get; set; }
        public string VALUE { get; set; }
        public DateTime LastUpdated { get; set; }
        public SiteMetaDto()
        {
        }
    }
}
