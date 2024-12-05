using System;
using Generics.Common.Attributes;

namespace Generics.Db
{
    public class ProductStatusDto
    {
        public ProductStatusDto()
        {
        }
        [DbGenerated]
        public long Id { get; set; }
        public string ItemSku { get; set; }
        public string Category { get; set; }
        public bool InStock { get; set; }
        public long Quantity { get; set; }
        public DateTime LastUpdated {get;set;}
    }
}
