using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Dmain
{
    public class OTPCode
    {
        public int Id { get; set; }
        public string Email { get; set; }  // ربط OTP بالبريد الإلكتروني للمستخدم
        public string phon {  get; set; }
        public string OTPHash { get; set; }  // تخزين OTP بتجزئة SHA256
        public bool IsVerified { get; set; }
        public DateTime Expiry { get; set; }  // مدة صلاحية OTP
    }
}
