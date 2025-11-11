using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace QudraSaaS.Application.Services
{
    public class WorkshopRepo: IWorkshop
    {
        private readonly UserManager<applicationUser> _userManager;
        private readonly IConfiguration _config;
        public readonly Context context;
        //private readonly IWebHostEnvironment _env;

        public WorkshopRepo(UserManager<applicationUser> userManager, IConfiguration config, Context context)
        {
            _userManager = userManager;
            _config = config;
            this.context = context;
            
        }

        public async Task<bool> RegisterWorkshop(WorkshopDTO workshopDTO)
        {
            var exists = await context.Workshops.AnyAsync(u => u.PhoneNumber == workshopDTO.Phone);
            if (exists)
            {

                throw new InvalidOperationException("The Number Already exists");
            }

            //string imageUrl = null;
            //if (!string.IsNullOrWhiteSpace(workshopDTO.imageUrl))
            //{
            //    var uploadsPath = Path.Combine(_env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot"), "Uploads");
            //    if (!Directory.Exists(uploadsPath))
            //        Directory.CreateDirectory(uploadsPath);

            //    try
            //    {
            //        var imageBytes = Convert.FromBase64String(workshopDTO.imageUrl);
            //        var fileName = Guid.NewGuid().ToString() + ".jpg";
            //        var filePath = Path.Combine(uploadsPath, fileName);

            //        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            //        imageUrl = $"/Uploads/{fileName}"; // نرجع اللينك
            //    }
            //    catch
            //    {
            //        throw new InvalidOperationException("Workshop image is in invalid Base64 format.");
            //    }
            //}

            Workshop workshop = new Workshop();
            workshop.code= workshopDTO.code;
            workshop.phone = workshopDTO.Phone;
            workshop.address = workshopDTO.address;
            workshop.imageUrl = workshopDTO.imageUrl;
            context.Workshops.Add(workshop);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ResetPasswordworkshop(ResetPasswordWorkshopDTO resetPasswordWorkshopDTO)
        {
            var user = await context.Workshops.FirstOrDefaultAsync(x => x.phone == resetPasswordWorkshopDTO.phone && x.code == resetPasswordWorkshopDTO.code);
            if (user == null)
            {
                throw new InvalidOperationException("User Not Found");
            }
            //  التحقق من وجود OTP صالح
            var otpRecord = await context.oTPCodes.FirstOrDefaultAsync(x => x.phon == resetPasswordWorkshopDTO.phone);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordWorkshopDTO.NewPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("An error occurred while changing your password.");
            }
            context.oTPCodes.Remove(otpRecord);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<string>loginworkshop(LoginWorkshopDto loginWorkshopDto)
        {
            var work = await context.Workshops.FirstOrDefaultAsync(u => u.phone == loginWorkshopDto.phon);
            if (work == null)
            {
                throw new InvalidOperationException("The workshop is not registered. ");
            }
            bool passwordValid = await _userManager.CheckPasswordAsync(work, loginWorkshopDto.Password);
            if (!passwordValid)
                throw new InvalidOperationException("Invalid password...!");
            var roles = await _userManager.GetRolesAsync(work);
            if (!roles.Contains("workshop"))//عاوز اتاكد
                throw new InvalidOperationException("You are not a user");
            var claims = new List<Claim>
        {
            new Claim("Code", work.code),
            new Claim(ClaimTypes.NameIdentifier, work.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
