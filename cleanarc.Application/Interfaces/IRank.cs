using QudraSaaS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interfaces
{
    public interface IRank
    {
        public  Task<List<RankDto>> GetAll();
        public  Task<RankDto> GetbyId(int id);
        public  Task<bool> Creat(RankDto rankDto);
        public  Task<bool> Delet(int id);

    }
}
