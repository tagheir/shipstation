using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class Orders
    {
        public long Id { get; set; }
        public string OrderJson { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public DateTime? OrderCreatedOn { get; set; }
        public string OrderCreatedUrl { get; set; }
        public string OrderCreatedInformationJson { get; set; }
        public string Step1Status { get; set; }
        public string Step1Error { get; set; }
        public string Step2Status { get; set; }
        public string Step2Error { get; set; }
        public string Step3Status { get; set; }
        public string Step3Error { get; set; }
        public string OrderWebsite { get; set; }
    }
}
