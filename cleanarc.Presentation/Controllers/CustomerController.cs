using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QudraSaaS.Application.DTOs;
using QudraSaaS.Application.Interfaces;

namespace QudraSaaS.Presentation.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUser Iuser;

        public CustomerController(IUser Iuser)
        {
           this.Iuser = Iuser;
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult>AddUser(CustmerDTO registerUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Iuser.AddUser(registerUserDto);

            if (result)
                return Ok(new
                {
                    success = "true",
                    message = "Account added successfully"
                });
            else
                return BadRequest(new
                {
                    success = "false",
                    message = "NOT to add account"
                });
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                return Ok(await Iuser.GetAll());
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserId(string userId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await Iuser.GetbyId(userId));
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(CustmerDTO userDto,string id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    return Ok(new
                    {
                        success = "true",
                        message = await Iuser.Update(userDto,id)
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
                catch (DbUpdateException)
                {
                    return BadRequest(new
                    {
                        success = "false",
                        message = "حدث خطأ أثناء حفظ البيانات. تأكد من أن كل الحقول مطلوبة مكتملة."
                    });
                }
            }
            return BadRequest(new
            {
                success = "False",
                message = ModelState
            });
        }



    }
}
