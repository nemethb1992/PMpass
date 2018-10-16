using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMpass.Models
{
    public class CredentialData
    {
            public string Username { get; set; }
            public string OldPasswrod { get; set; }
            public string NewPassword1 { get; set; }
            public string NewPassword2 { get; set; }
    }
}
