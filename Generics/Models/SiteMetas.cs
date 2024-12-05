using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class SiteMetas
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public long Id { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
