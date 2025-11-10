using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class applicationUser: IdentityUser
    {
        public string name { get; set; }
        public string phone { get; set; }
    }
}
