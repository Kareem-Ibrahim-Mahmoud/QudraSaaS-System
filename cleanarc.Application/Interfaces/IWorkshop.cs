using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface IWorkshop
    {
        public  Task<bool> RegisterWorkshop(WorkshopDTO workshopDTO);
        public  Task<bool> ResetPasswordworkshop(ResetPasswordWorkshopDTO resetPasswordWorkshopDTO);
        public  Task<string> loginworkshop(LoginWorkshopDto loginWorkshopDto);
    }
}
