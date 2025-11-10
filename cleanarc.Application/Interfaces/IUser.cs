using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface IUser
    {
        public  Task<List<CustmerDTO>> GetAll();
        public  Task<bool> AddUser(CustmerDTO registerUserDto);
        public  Task<CustmerDTO> GetbyId(string id);
        public  Task<bool> Update(CustmerDTO registerUserDto, string Id);
        public  Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, ClaimsPrincipal userPrincipal);
        public  Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO);
    }
}
