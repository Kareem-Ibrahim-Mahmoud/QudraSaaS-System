using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.DTOs
{
    public class OTPDto
    {
        public int Id { get; set; }
        public string Email { get; set; }  // ربط OTP بالبريد الإلكتروني للمستخدم
        public string OTPHash { get; set; }  // تخزين OTP بتجزئة SHA256
        public bool IsVerified { get; set; }
        public string phon {  get; set; }
        public DateTime Expiry { get; set; }  // مدة صلاحية OTP
    }
}
