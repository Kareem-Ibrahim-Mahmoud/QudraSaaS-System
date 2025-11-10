using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;
using QudraSaaS.Dmain;
using System.Collections.Generic;

namespace QudraSaaS.Presentation.Controllers
{
    [Route("api/ServiceSession")]
    [ApiController]
    public class ServiceSessionController : ControllerBase
    {
        private readonly IServiceSession Iserv;
        public ServiceSessionController(IServiceSession Iserv)
        {
            this.Iserv = Iserv;
        }
        [HttpGet("GetAllServiceSession")]
        public async Task<IActionResult> GetAllServiceSession()
        {
            if (ModelState.IsValid)
            {
                return Ok(await Iserv.GetAll());
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetServiceSessionId")]
        public async Task<IActionResult> GetServiceSessionId(int Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await Iserv.GetById(Id));
            }
            return BadRequest(ModelState);
        }
        [HttpPost("CreateCarserviceSession")]
        public async Task<IActionResult>CreateCarserviceSession(ServiceSessionDTO serviceSessionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Iserv.Creat(serviceSessionDTO);

            if (result)
                return Ok(new
                {
                    success = "true",
                    message = "serviceSession added successfully"
                });
            else
                return BadRequest(new
                {
                    success = "false",
                    message = "NOT to add serviceSession"
                });
        }

        [HttpPatch("UpdateserviceSession")]
        public async Task<IActionResult> UpdateserviceSession(ServiceSessionDTO serviceSessionDTO, int id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Iserv.Update(serviceSessionDTO, id)
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

        [HttpDelete("DeleteServiceSession")]
        public async Task<IActionResult> DeleteServiceSession(int Id)
        {
            if (ModelState.IsValid)
            {
                if (await Iserv.Delet(Id))
                {
                    return Ok(new
                    {
                        success = "true",
                        message = "The ServiceSession has been successfully deleted"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "ServiceSession not found or could not be deleted."
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
