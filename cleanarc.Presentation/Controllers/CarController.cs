using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using System.Collections.Generic;

namespace QudraSaaS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICar Icar;
        public CarController(ICar Icar) 
        {
            this.Icar = Icar;
        }
        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar(CarDTO carDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Icar.Creat(carDTO);

            if (result)
                return Ok(new
                {
                    success = "true",
                    message = "CAR added successfully"
                });
            else
                return BadRequest(new
                {
                    success = "false",
                    message = "NOT to add Car"
                });
        }
        [HttpGet("GetCarById")]
        public async Task<IActionResult> GetCarId(int Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await Icar.GetbyId(Id));
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            if (ModelState.IsValid)
            {
                return Ok(await Icar.GetAll());
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteCar")]
        public async Task<IActionResult> DeleteCar(int Id)
        {
            if (ModelState.IsValid)
            {
                if (await Icar.Delet(Id))
                {
                    return Ok(new
                    {
                        success = "true",
                        message = "The Car has been successfully deleted"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "Car not found or could not be deleted."
                    });
                }
            }
            return BadRequest(new
            {
                success = "false",
                message = ModelState
            });
        }
    }
}
