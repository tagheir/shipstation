using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class ProductStatus
    {
        public long Id { get; set; }
        public string ItemSku { get; set; }
        public string Category { get; set; }
        public bool InStock { get; set; }
        public DateTime LastUpdated { get; set; }
        public long Quantity { get; set; }
    }
}
