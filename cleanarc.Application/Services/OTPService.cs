using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class OTPService
    {
        public string GenerateOTP()
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();  // توليد OTP مكون من 6 أرقام
            return otp;
        }
        public string HashOTP(string otp)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(otp));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();  // تحويل إلى string بتنسيق Hex
            }
        }
        // التحقق من صحة OTP من خلال مقارنة التجزئة
        public bool VerifyOTP(string otp, string hashedOtp)
        {
            var otpHash = HashOTP(otp);  // تجزئة OTP المدخل
            return otpHash == hashedOtp;  // التحقق إذا كانت التجزئة متساوية
        }
    }
}
