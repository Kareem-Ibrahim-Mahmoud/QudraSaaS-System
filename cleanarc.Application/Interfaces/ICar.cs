using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface ICar
    {
        public  Task<List<CarDTO>> GetAll();
        public  Task<CarDTO> GetbyId(int id);
        public  Task<bool> Creat(CarDTO cardto);
        public  Task<bool> Update(CarDTO cardto, int id);
        public  Task<bool> Delet(int id);
    }
}
