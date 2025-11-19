using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QudraSaaS.Application;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using QudraSaaS.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QudraSaaS.Application.Services
{
    public class UserRepo: IUser
    {
        private readonly UserManager<applicationUser> _userManager;
        private readonly IConfiguration _config;
        public readonly Context context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserRepo(UserManager<applicationUser> userManager, IConfiguration config, Context context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _config = config;
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<bool>AddUser(CustmerDTO registerUserDto /*ClaimsPrincipal userPrincipal*/)
        //{
        //  //  var user = await _userManager.GetUserAsync(userPrincipal);
        //    var exists = await context.Customers.AnyAsync(u => u.PhoneNumber == registerUserDto.phone);
        //    if (exists)
        //    {
                
        //        throw new InvalidOperationException("The Number Already exists");
        //    }
        //    Customer app = new Customer();
        //    app.name = registerUserDto.name;
        //    app.phone = registerUserDto.phone;
        //    app.Email = registerUserDto.Email;
        //    app.whats = registerUserDto.whats;
        //    app.workShopId = "1";//user.Id;
        //    app.RankId= registerUserDto.RankId;
        //  //  app.Rank = registerUserDto.Rank;
        //    app.numberOfVisits = registerUserDto.numberOfVisits=0;
        //    app.notes = registerUserDto.notes;  
            
        //    //app.cars = registerUserDto.carid;
        //    //app.serviceSessions = registerUserDto.ServiceSessionid;
        //    context.Customers.Add(app);
        //    await context.SaveChangesAsync();

        //    IdentityResult result = await _userManager.CreateAsync(app, registerUserDto.phone);
        //    if (result.Succeeded)
        //    {
        //        await _userManager.AddToRoleAsync(app,"Customer");

        //        return true;


        //    }
        //    else {

        //        throw new InvalidOperationException("An error occurred while registering the account.");
        //    }
         
           
            
        //}

        public async Task<bool> AddUser(CustmerDTO registerUserDto /*ClaimsPrincipal userPrincipal*/)
        {
            //  var user = await _userManager.GetUserAsync(userPrincipal);
            var exists = await context.Customers.AnyAsync(u => u.PhoneNumber == registerUserDto.phone);
            if (exists)
            {

                throw new InvalidOperationException("The Number Already exists");
            }
            Customer app = new Customer();
            app.name = registerUserDto.name;
            app.phone = registerUserDto.phone;
            app.Email = registerUserDto.Email;
            app.whats = registerUserDto.whats;
            app.UserName = registerUserDto.phone;
            app.workShopId = "1";//user.Id;
            app.RankId = registerUserDto.RankId;
            //  app.Rank = registerUserDto.Rank;
            app.numberOfVisits = registerUserDto.numberOfVisits = 0;
            app.notes = registerUserDto.notes;

            //app.cars = registerUserDto.carid;
            //app.serviceSessions = registerUserDto.ServiceSessionid;
            //context.Customers.Add(app);
            //await context.SaveChangesAsync();

            IdentityResult result = await _userManager.CreateAsync(app, registerUserDto.phone);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(app, "Customer");

                return true;

            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Error(s) occurred while registering the account: {errors}");
            }



        }


        //جميع العملاء الخاصة بالورشة يمعلم
        public async Task<List<CustmerDTO>> GetAll()
        {
            var workshopIdFromToken = _httpContextAccessor.HttpContext?.User?
            .FindFirst("workShopId")?.Value;

            if (string.IsNullOrEmpty(workshopIdFromToken))
                throw new UnauthorizedAccessException("Invalid workshop token.");

            var user = await context.Customers
                .Where(x => x.workShopId == workshopIdFromToken)
                .ToListAsync();

            if (user == null)
                throw new InvalidOperationException("This customer does not belong to your workshop.");
           // var cust=await context.Customers.ToListAsync();
            var custmerdto = new List<CustmerDTO>();
            foreach (var custmer in user)
            {
                var ca = new CustmerDTO();
                ca.name = custmer.name;
                ca.phone = custmer.phone;
                ca.Email = custmer.Email;
                ca.name = custmer.name;
                ca.whats = custmer.whats;
                ca.Rank = custmer.Rank;
                ca.numberOfVisits = custmer.numberOfVisits;
                ca.notes = custmer.notes;
                ca.RankId = custmer.RankId;
                ca.workShopId = custmer.workShopId;
                
       
                
                custmerdto.Add(ca);
            }
            return custmerdto;
        }
        public async Task<CustmerDTO>GetbyId(string id)
        {
            var user = await context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new InvalidOperationException($"The User Id:{id} are not found");
            CustmerDTO custmerDTO = new CustmerDTO();
            custmerDTO.phone = user.phone;
            custmerDTO.Email = user.Email;
            custmerDTO.name = user.name;
            custmerDTO.whats = user.whats;
            custmerDTO.Rank = user.Rank;
            custmerDTO.numberOfVisits = user.numberOfVisits;
            custmerDTO.notes = user.notes;
            custmerDTO.RankId = user.RankId;
            custmerDTO.workShopId = user.workShopId;
       
            return custmerDTO;

        }
        public async Task<bool> Update(CustmerDTO registerUserDto,string Id)
        {
            // 1️⃣ استخراج الـ UserId من التوكن الحالي
            var userIdFromToken = _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdFromToken))
                throw new UnauthorizedAccessException("Token is missing or invalid.");

            // 2️⃣ التحقق إن المستخدم اللي في التوكن هو نفسه اللي بيعدّل
            if (userIdFromToken != Id)
                throw new UnauthorizedAccessException("You are not allowed to update another user's data.");



            var user= await context.Customers.FirstOrDefaultAsync(x=>x.Id == Id);
            if (user == null)
                throw new InvalidOperationException($"The ServiceSession With Id:{Id} Not found");
 
            if (registerUserDto.name != null)
                user.name = registerUserDto.name;

            if (!string.IsNullOrEmpty(registerUserDto.phone) && registerUserDto.phone != user.phone)
            {
                bool phoneExists = await context.Customers
                    .AnyAsync(x => x.phone == registerUserDto.phone);

                if (phoneExists)
                    throw new InvalidOperationException("This phone number is already in use.");

                user.phone = registerUserDto.phone; // تحديث الرقم
            }

            if (!string.IsNullOrEmpty(registerUserDto.Email) && registerUserDto.Email != user.Email)
            {
                bool phoneExists = await context.Customers
                    .AnyAsync(x => x.Email == registerUserDto.Email);

                if (phoneExists)
                    throw new InvalidOperationException("This phone number is already in use.");

                user.Email = registerUserDto.Email; // تحديث الرقم
            }

            if (registerUserDto.notes != null)
                user.notes = registerUserDto.notes;

            if (registerUserDto.workShopId != null)
                user.workShopId = registerUserDto.workShopId;

            //if (registerUserDto.carlist != null)
            //    user.cars = registerUserDto.carlist;

            //if (registerUserDto.serviceSessions != null)
            //    user.serviceSessions = registerUserDto.serviceSessions;

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                throw new InvalidOperationException("User Not Found");
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to change password: {errors}");
            }
            return true;
        }
        public async Task<bool>ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.phone == resetPasswordDTO.phone);
            if (user == null)
            {
                throw new InvalidOperationException("User Not Found");
            }
            //  التحقق من وجود OTP صالح
            var otpRecord = await context.oTPCodes.FirstOrDefaultAsync(x=>x.phon == resetPasswordDTO.phone);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDTO.NewPassword);

            if (!result.Succeeded) 
            {  
                throw new InvalidOperationException("An error occurred while changing your password.");
            }
            context.oTPCodes.Remove(otpRecord);
            await context.SaveChangesAsync();
            return true;
        }

        //public async Task<string> loginUser(LoginCustumerDTO loginDto)
        //{
        //    var work = await context.Customers.FirstOrDefaultAsync(u => u.phone == loginDto.phone);
        //    if (work == null)
        //    {
        //        throw new InvalidOperationException("The User is not registered.");
        //    }
        //    bool passwordValid = await _userManager.CheckPasswordAsync(work, loginDto.Password);
        //    if (!passwordValid)
        //        throw new InvalidOperationException("Invalid password...!");
        //    var roles = await _userManager.GetRolesAsync(work);
        //    if (!roles.Contains("User"))//عاوز اتاكد
        //        throw new InvalidOperationException("You are not a user");
        //    var claims = new List<Claim>
        //{
        //    new Claim("phone", work.phone),
        //    new Claim(ClaimTypes.NameIdentifier, work.Id.ToString()),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //};
        //    foreach (var role in roles)
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //        issuer: _config["JWT:ValidIssuer"],
        //        audience: _config["JWT:ValidAudiance"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddDays(30),
        //        signingCredentials: creds
        //    );
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


        public async Task<string> loginUser(LoginCustumerDTO loginDto)
        {
            var work = await context.Customers.FirstOrDefaultAsync(u => u.phone == loginDto.phone);
            if (work == null)
            {
                throw new InvalidOperationException("The User is not registered.");
            }
            bool passwordValid = await _userManager.CheckPasswordAsync(work, loginDto.Password);
            if (!passwordValid)
                throw new InvalidOperationException("phone or number");
            var roles = await _userManager.GetRolesAsync(work);

            if (!roles.Contains("Customer"))
                throw new InvalidOperationException("You are not a user");
            var claims = new List<Claim>
    {
        new Claim("phone", work.phone),
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
