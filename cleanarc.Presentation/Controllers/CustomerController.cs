using Castle.Core.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Application.Services;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System.Security.Claims;
using Xamarin.Essentials;


namespace QudraSaaS.Presentation.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUser Iuser;
        private readonly UserManager<applicationUser> usermanger;
       
        private readonly Context _context;
        private readonly IEmailSender _emailSender;  // خدمة إرسال البريد الإلكتروني

        public CustomerController(IUser Iuser)
        {
           this.Iuser = Iuser;
        }
        [HttpPost("AddUser")]
       // [Authorize(Roles = "Workshop")]
        public async Task<IActionResult>AddUser(CustmerDTO registerUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        

            try
            {
                if (await Iuser.AddUser(registerUserDto/*User*/))
                {
                    return Ok(new
                    {
                        success = "true",
                        message = "Account added successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "NOT to add Account"
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = "False",
                    message = ex.Message
                });
            }

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
              try{
                    return Ok(new
                    {
                        success = "true",
                        message = await Iuser.GetAll()
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        success = "False",
                        message = ex.Message
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
            
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserId(string userId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Iuser.GetbyId(userId)
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        success = "False",
                        message = ex.Message
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
        }

        [HttpPatch("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(CustmerDTO userDto,string id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Iuser.Update(userDto,id)
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        success = "False",
                        message = ex.Message
                    });
                }
                catch (DbUpdateException)
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "حدث خطأ أثناء حفظ البيانات. تأكد من أن كل الحقول مطلوبة مكتملة."
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(LoginCustumerDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // استدعاء الدالة من الريبو
                var token = await Iuser.loginUser(dto);

                return Ok(new
                {
                    success = true,
                    token = token,
                    message = "Login successful"
                });
            }
            catch (InvalidOperationException ex)
            {
                // الأخطاء المتوقعة مثل: user not found / invalid password / not a user
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                // أي خطأ غير متوقع
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error"
                });
            }
        }


        //// إرسال OTP إلى البريد الإلكتروني
        //[HttpPost("SendOTP")]
        //public async Task<IActionResult> SendOTP(string phone)
        //{

        //    var user = await usermanger.Users.FirstOrDefaultAsync(x => x.phone == phone);
        //    if (user == null)
        //        return BadRequest(new
        //        {
        //            success = "false",
        //            message = "Email not registered"

        //        });

        //    // توليد OTP
        //    var otp = _otpService.GenerateOTP();
        //    var hashedOtp = _otpService.HashOTP(otp);

        //    // حفظ OTP في قاعدة البيانات

        //    var OTPsinDatabase = await _context.oTPCodes.FirstOrDefaultAsync(x => x.phon == phone);

        //    if (OTPsinDatabase != null)
        //        _context.oTPCodes.Remove(OTPsinDatabase);
        //    _context.SaveChanges();
        //    var otpRecord = new OTPDto
        //    {
        //        phon = phone,
        //        OTPHash = hashedOtp,
        //        Expiry = DateTime.UtcNow.AddMinutes(10)  // صلاحية لمدة 10 دقائق
        //    };
        //    var Otp = new OTPCode();
        //    Otp.Expiry = otpRecord.Expiry;
        //    Otp.Email = otpRecord.Email;
        //    Otp.OTPHash = otpRecord.OTPHash;

        //    _context.oTPCodes.Add(Otp);
        //    await _context.SaveChangesAsync();

        //    // إرسال OTP عبر البريد الإلكتروني
        //    await _emailSender.SendEmailAsync(phone, "Your OTP Code", $"Your OTP code is: {otp}");

        //    return Ok(new
        //    {
        //        success = "true",
        //        message = "Check your email"

        //    });
        //}

        //// التحقق من صحة OTP
        //[HttpPost("VerifyOTP")]
        //public async Task<IActionResult> VerifyOTP(VerifyOTPDTO model)
        //{
        //    var otpRecord = await _context.oTPCodes.FirstOrDefaultAsync(o => o.phon == model.phone);
        //    if (otpRecord == null || otpRecord.Expiry < DateTime.UtcNow)
        //        return BadRequest("OTP is invalid or expired");

        //    // التحقق من صحة OTP
        //    if (!_otpService.VerifyOTP(model.OTP, otpRecord.OTPHash))
        //        return BadRequest(new
        //        {
        //            success = "false",
        //            message = "Invalid OTP"

        //        });

        //    otpRecord.IsVerified = true;

        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        success = "true",
        //        message = "OTP is correct, you can change your password"

        //    });
        //}

        //// إعادة تعيين كلمة المرور
        //[HttpPost("ResetPassword")]
        //public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        //{
        //    var user = await usermanger.Users.FirstOrDefaultAsync(x => x.phone == model.phone);
        //    if (user == null)
        //        return BadRequest(new
        //        {
        //            success = "false",
        //            message = "User not found"

        //        });

        //    var otpRecord = await _context.oTPCodes.FirstOrDefaultAsync(o => o.phon == model.phone);//&&o.IsVerified==true);
        //    if (otpRecord == null)
        //        return BadRequest(new
        //        {
        //            success = "false",
        //            message = "OTP is invalid or does not exist"

        //        });

        //    var resetToken = await usermanger.GeneratePasswordResetTokenAsync(user);
        //    var result = await usermanger.ResetPasswordAsync(user, resetToken, model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        //var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        return BadRequest(new
        //        {
        //            success = "false",
        //            message = "An error occurred while changing your password."

        //        });
        //    }

        //    // حذف OTP بعد الاستخدام
        //    _context.oTPCodes.Remove(otpRecord);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        success = "true",
        //        message = "Your password has been changed successfully"

        //    });
        //}



    }
}
