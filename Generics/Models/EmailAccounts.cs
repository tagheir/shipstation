using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class EmailAccounts
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string PrivateKey { get; set; }
        public string ServiceAccount { get; set; }
        public string Purpose { get; set; }
    }
}
