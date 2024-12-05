using Generics.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics.Db
{
    public class EmailRecorderDto
    {
        [DbGenerated]
        public int Id { get; set; }
        public string EmailStatus { get; set; }
        public string EmailPurpose { get; set; }
        public DateTime OnCreated { get; set; } 
        public string OrderId { get; set; } 
        public string OrderNumber { get; set; }
    }
}
