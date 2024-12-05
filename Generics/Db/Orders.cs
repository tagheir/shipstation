using Generics.Common.Attributes;
using System;

namespace Generics.Db
{
    public class Orders
    {
        [DbGenerated]
        public long Id { get; set; }
        public string OrderJson { get; set; }
        public DateTime OnCreated { get; set; }
        public DateTime OnModified { get; set; }
        public DateTime OrderCreatedOn { get; set; }
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
