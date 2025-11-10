using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class ResetPasswordDTO
    {
        public string phone { get; set; }
        public string NewPassword { get; set; }
    }
}
