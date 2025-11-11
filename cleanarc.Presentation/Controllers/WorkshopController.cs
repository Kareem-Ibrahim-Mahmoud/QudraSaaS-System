using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Application.Services;
using QudraSaaS.Infrastructure;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QudraSaaS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshop workshop;
        private readonly Context context;
        public WorkshopController(IWorkshop workshop, Context context)
        {
            this.context = context;
            this.workshop = workshop;
        }
        [HttpPost("Registerworkshop")]
        public async Task<IActionResult> Registerworkshop(WorkshopDTO workshopDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await workshop.RegisterWorkshop(workshopDTO);

            if (result)
                return Ok(new
                {
                    success = "true",
                    message = "Account added successfully workshop"
                });
            else
                return BadRequest(new
                {
                    success = "false",
                    message = "NOT to add account workshop"
                });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordWorkshopDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await workshop.ResetPasswordworkshop(dto);
                if (success)
                    return Ok(new
                    {
                        success = true,
                        message = "Password has been reset successfully."
                    });
                else
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to reset password."
                    });
            }
            catch (InvalidOperationException ex)
            {
                // أخطاء متوقعة مثل: user not found أو OTP غير صالح
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
         
        }


        [HttpPost("LoginWorkShop")]
        public async Task<IActionResult> Login(LoginWorkshopDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // استدعاء الدالة من الريبو
                var token = await workshop.loginworkshop(dto);

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

    }
}
