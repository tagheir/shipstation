using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class OrderDelayMessage
    {
        public long Id { get; set; }
        public string OrderNumber { get; set; }
        public long OrderId { get; set; }
        public DateTime SentAt { get; set; }
        public string Purpose { get; set; }
    }
}
