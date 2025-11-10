using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
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
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class UserRepo: IUser
    {
        private readonly UserManager<applicationUser> _userManager;
        private readonly IConfiguration _config;
        public readonly Context context;

        public UserRepo(UserManager<applicationUser> userManager, IConfiguration config, Context context)
        {
            _userManager = userManager;
            _config = config;
            this.context = context;
        }

        public async Task<bool>AddUser(CustmerDTO registerUserDto)
        {
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
            app.workShopId = registerUserDto.workShopId;
            app.RankId= registerUserDto.RankId;
            app.Rank = registerUserDto.Rank;
            app.numberOfVisits = registerUserDto.numberOfVisits;
            app.notes = registerUserDto.notes;
            //app.cars = registerUserDto.carid;
            //app.serviceSessions = registerUserDto.ServiceSessionid;

            context.Users.Add(app);
            await context.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<CustmerDTO>> GetAll()
        {
            var cust=await context.Customers.ToListAsync();
            var custmerdto = new List<CustmerDTO>();
            foreach (var custmer in cust)
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
                ca.cars = custmer.cars;
                ca.serviceSessions = custmer.serviceSessions;
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
            custmerDTO.cars = user.cars;
            custmerDTO.serviceSessions = user.serviceSessions;
            return custmerDTO;

        }

        public async Task<bool> Update(CustmerDTO registerUserDto,string Id)
        {
            var user= await context.Customers.FirstOrDefaultAsync(x=>x.Id == Id);
            if (user == null)
                throw new InvalidOperationException($"The ServiceSession With Id:{Id} Not found");
            if (registerUserDto.whats != null)
                user.whats = registerUserDto.whats;

            if (registerUserDto.name != null)
                user.name = registerUserDto.name;

            if (registerUserDto.Email != null)
                user.Email = registerUserDto.Email;

            if (registerUserDto.phone != null)
                user.phone = registerUserDto.phone;

            if (registerUserDto.RankId != null)
                user.RankId = registerUserDto.RankId;

            if (registerUserDto.numberOfVisits != null)
                user.numberOfVisits = registerUserDto.numberOfVisits;

            if (registerUserDto.notes != null)
                user.notes = registerUserDto.notes;

            if (registerUserDto.workShopId != null)
                user.workShopId = registerUserDto.workShopId;

            if (registerUserDto.cars != null)
                user.cars = registerUserDto.cars;

            if (registerUserDto.serviceSessions != null)
                user.serviceSessions = registerUserDto.serviceSessions;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if(user == null)
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
    }
}
