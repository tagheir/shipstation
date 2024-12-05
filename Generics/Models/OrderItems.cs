using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class OrderItems
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string ItemOrderedUrl { get; set; }
        public DateTime? ItemOrderedOn { get; set; }
        public string ItemError { get; set; }
        public string ItemFetchStatus1 { get; set; }
        public string ItemFetchStatus2 { get; set; }
        public string ItemFetchStatus3 { get; set; }
    }
}
