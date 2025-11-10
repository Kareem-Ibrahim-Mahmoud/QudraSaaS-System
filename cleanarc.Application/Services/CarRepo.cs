using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using QudraSaaS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Services
{
    public class CarRepo : ICar
    {
        private readonly Context context;
        public CarRepo(Context context)
        {
            this.context = context;

        }
        public async Task<List<CarDTO>>GetAll()
        {
            var car = await context.Cars.ToListAsync();
            var cardto=new List<CarDTO>();
            foreach (var card in car)
            {
                var ca=new CarDTO();
                ca.Year = card.Year;
                ca.CarModel = card.CarModel;
                ca.Make = card.Make;
                ca.PlateNumber = card.PlateNumber;
                ca.CurrentKm = card.CurrentKm;
                ca.OilType = card.OilType;
                ca.customerId = card.customerId;
                ca.serviceSessions = card.serviceSessions;
                cardto.Add(ca);
            }
            return cardto;
        }
        public async Task<CarDTO>GetbyId(int id)
        {
            var car = await context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
                throw new InvalidOperationException($"The Car Id:{id} are not found");
            CarDTO cardto = new CarDTO();
            cardto.Year = car.Year;
            cardto.CarModel = car.CarModel;
            cardto.Make = car.Make;
            cardto.PlateNumber = car.PlateNumber;
            cardto.CurrentKm = car.CurrentKm;
            cardto.OilType = car.OilType;
            cardto.customerId = car.customerId;
            cardto.serviceSessions = car.serviceSessions;
            return cardto;
        }
        public async Task<bool>Creat(CarDTO cardto)
        {
            var car = new Car();
            car.Year = cardto.Year;
            car.CarModel = cardto.CarModel;
            car.Make = cardto.Make;
            car.PlateNumber = cardto.PlateNumber;
            car.CurrentKm = cardto.CurrentKm;
            car.OilType = cardto.OilType;
            car.customerId = cardto.customerId;
            car.serviceSessions = cardto.serviceSessions;
            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool>Update(CarDTO cardto ,int id)
        {
            var carfirst = await context.Cars.FirstOrDefaultAsync(s => s.Id == id);
            carfirst.Year = cardto.Year;
            carfirst.CarModel = cardto.CarModel;
            carfirst.Make = cardto.Make;
            carfirst.PlateNumber = cardto.PlateNumber;
            carfirst.CurrentKm = cardto.CurrentKm;
            carfirst.OilType = cardto.OilType;
            carfirst.customerId = cardto.customerId;
            carfirst.serviceSessions = cardto.serviceSessions;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool>Delet(int id)
        {
            var carfirst = await context.Cars.FirstOrDefaultAsync(s => s.Id == id);
            if (carfirst == null)
                throw new InvalidOperationException($"The Cars With Id:{id} Not found");
            context.Cars.Remove(carfirst);
            await context.SaveChangesAsync();
            return true;
        }


    }
}
