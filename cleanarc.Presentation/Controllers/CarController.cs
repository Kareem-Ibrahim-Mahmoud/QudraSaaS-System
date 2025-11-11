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

            try
            {
                if (await Icar.Creat(carDTO))
                {
                    return Ok(new
                    {
                        success = "true",
                        message = "Account Car successfully"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "NOT to add Car"
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = "False",
                    message = ex.Message
                });
            }
        }
        [HttpGet("GetCarById")]
        public async Task<IActionResult> GetCarId(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Icar.GetbyId(Id)
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        success = "False",
                        message = ex.Message
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
        }

        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetAllCars()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Icar.GetAll()
                    });
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        success = "False",
                        message = ex.Message
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
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
