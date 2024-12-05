using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class EmailSender
    {
        public long Id { get; set; }
        public string EmailStatus { get; set; }
        public string EmailPurpose { get; set; }
        public DateTime OnCreated { get; set; }
        public string OrderId { get; set; }
        public string OrderNumber { get; set; }
    }
}
