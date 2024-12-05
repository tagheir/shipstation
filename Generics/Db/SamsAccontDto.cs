using Generics.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generics.Db
{
    public  class SamsAccontDto
    {
        [DbGenerated]
        public int Id { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cookie { get;set; }
        public string Token  { get; set; }
        public string EmailID { get; set; }
        public string PrivateKey { get; set; }
    }
}
