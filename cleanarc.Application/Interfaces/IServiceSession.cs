using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface IServiceSession
    {
        public  Task<List<ServiceSessionDTO>> GetAll();
        public  Task<ServiceSessionDTO> GetById(int id);
        public  Task<bool> Creat(ServiceSessionDTO serviceSession);
        public  Task<bool> Update(ServiceSessionDTO serviceSession, int id);
        public  Task<bool> Delet(int id);
    }
}
