using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class ServiceSessionRepo: IServiceSession
    {
        private readonly Context context;
        public ServiceSessionRepo(Context context)
        {
            this.context = context;
        }
        public async Task<List<ServiceSessionDTO>>GetAll()
        {
            var ser = await context.ServiceSessions.ToListAsync();
            var serDto = new List<ServiceSessionDTO>();
            foreach (var serviceSession in ser)
            {
                var services = new ServiceSessionDTO();
                services.description= serviceSession.description;
                services.date = serviceSession.date;
                services.UserName = serviceSession.UserName;
                services.KmReading = serviceSession.KmReading;
                services.NumberOfKilometers = serviceSession.NumberOfKilometers;
                services.FilterChanged = serviceSession.FilterChanged;
                services.AdditionalServices = serviceSession.AdditionalServices;
                services.cost = serviceSession.cost;
                services.carId = serviceSession.carId;
                services.customerId = serviceSession.customerId;
                services.workShopId = serviceSession.workShopId;
                serDto.Add(services);
            }
            return serDto;
        }
        public async Task<ServiceSessionDTO>GetById(int id)
        {
            var serviceSession = await context.ServiceSessions.FirstOrDefaultAsync(x=>x.id==id);
            if(serviceSession == null)
                throw new InvalidOperationException($"The ServiceSession Id:{id} are not found");
            ServiceSessionDTO services = new ServiceSessionDTO();
            services.description = serviceSession.description;
            services.date = serviceSession.date;
            services.UserName = serviceSession.UserName;
            services.KmReading = serviceSession.KmReading;
            services.NumberOfKilometers = serviceSession.NumberOfKilometers;
            services.FilterChanged = serviceSession.FilterChanged;
            services.AdditionalServices = serviceSession.AdditionalServices;
            services.cost = serviceSession.cost;
            services.carId = serviceSession.carId;
            services.customerId = serviceSession.customerId;
            services.workShopId = serviceSession.workShopId;
            return services;
        }
        public async Task<bool>Creat(ServiceSessionDTO serviceSession)
        {
            var services = new ServiceSession();
            services.description = serviceSession.description;
            services.date = serviceSession.date;
            services.UserName = serviceSession.UserName;
            services.KmReading = serviceSession.KmReading;
            services.NumberOfKilometers = serviceSession.NumberOfKilometers;
            services.FilterChanged = serviceSession.FilterChanged;
            services.AdditionalServices = serviceSession.AdditionalServices;
            services.cost = serviceSession.cost;
            services.carId = serviceSession.carId;
            services.customerId = serviceSession.customerId;
            services.workShopId = serviceSession.workShopId;
            context.ServiceSessions.Add(services);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool>Update(ServiceSessionDTO serviceSession ,int id)
        {
            var services = await context.ServiceSessions.FirstOrDefaultAsync(s => s.id == id);
            if (services == null)
                throw new InvalidOperationException($"The ServiceSession With Id:{id} Not found");
            if (serviceSession.description != null)
                services.description = serviceSession.description;

            if (serviceSession.date != null)
                services.date = serviceSession.date;

            if (serviceSession.UserName != null)
                services.UserName = serviceSession.UserName;

            if (serviceSession.KmReading != null)
                services.KmReading = serviceSession.KmReading;

            if (serviceSession.NumberOfKilometers != null)
                services.NumberOfKilometers = serviceSession.NumberOfKilometers;

            if (serviceSession.FilterChanged != null)
                services.FilterChanged = serviceSession.FilterChanged;

            if (serviceSession.AdditionalServices != null)
                services.AdditionalServices = serviceSession.AdditionalServices;

            if (serviceSession.carId != null)
                services.carId = serviceSession.carId;

            if (serviceSession.cost != null)
                services.cost = serviceSession.cost;

            if (serviceSession.customerId != null)
                services.customerId = serviceSession.customerId;

            if (serviceSession.workShopId != null)
                services.workShopId = serviceSession.workShopId;
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool>Delet(int id)
        {
            var services = await context.ServiceSessions.FirstOrDefaultAsync(x=>x.id==id);
            if(services == null)
                throw new InvalidOperationException($"The Category With Id:{id} Not found");
            context.ServiceSessions.Remove(services);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
