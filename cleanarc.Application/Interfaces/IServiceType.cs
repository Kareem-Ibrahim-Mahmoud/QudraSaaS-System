using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface IServiceType
    {
        public  Task<List<ServiceTypeDTO>> GetAll();
        public  Task<ServiceTypeDTO> GetbyId(int id);
        public  Task<bool> Creat(ServiceTypeDTO serviceTypeDTO);
        public  Task<bool> Delet(int id);
    }
}
