using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class ServiceTypeRepo: IServiceType
    {
        private readonly Context context;
        public ServiceTypeRepo(Context context)
        { 
            this.context = context;
        }

        public async Task<List<ServiceTypeDTO>>GetAll()
        {
            var servicestype=await context.serviceTypes.ToListAsync();
            var servicestypeDto=new List<ServiceTypeDTO>();
            foreach (var servicetype in servicestype)
            {
                var sere=new ServiceTypeDTO();
                sere.Name= servicetype.Name;
                sere.available = servicetype.available;
                sere.workShopId = servicetype.workShopId;

                servicestypeDto.Add(sere);
            }
            return servicestypeDto;

        }

        public async Task<ServiceTypeDTO>GetbyId(int id)
        {
            var serv=await context.serviceTypes.FirstOrDefaultAsync(x=>x.Id==id);
            if (serv==null)
                throw new InvalidOperationException($"The ServiceType Id are not found");
            ServiceTypeDTO serviceTypeDTO = new ServiceTypeDTO();
            serviceTypeDTO.Name= serv.Name;
            serviceTypeDTO.available= serv.available;
            serviceTypeDTO.workShopId = serv.workShopId;

            return serviceTypeDTO;
        }

        public async Task<bool>Creat(ServiceTypeDTO serviceTypeDTO)
        {
            var Ser=new ServiceType();
            Ser.Name= serviceTypeDTO.Name;
            Ser.available= serviceTypeDTO.available;
            Ser.workShopId= serviceTypeDTO.workShopId;
            context.serviceTypes.Add(Ser);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool>Delet(int id)
        {
            var ser = await context.serviceTypes.FirstOrDefaultAsync(x => x.Id == id);
            if(ser==null)
            {
                throw new InvalidOperationException($"ServesType with id:{id} does not exist.");
            }
            context.serviceTypes.Remove(ser);
            await context.SaveChangesAsync();
            return true;

        }
    }
}
