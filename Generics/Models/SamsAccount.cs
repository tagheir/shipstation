using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Generics.Models
{
    public partial class SamsAccount
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cookie { get; set; }
        public string Token { get; set; }
    }
}
