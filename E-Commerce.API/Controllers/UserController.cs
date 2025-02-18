using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO u)
        {
            Result result = new Result();

            try
            {
                result = await _userService.AddUserService(u);

                if (!result.Success)
                {
                    return StatusCode(500, result.Error);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(AuthApiViewModelDTO a)
        {
            Result result = new Result();

            try
            {
                result = await _userService.LoginUser(a);

                if (!result.Success)
                {
                    return StatusCode(500, result.Error);
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

            return Ok(result);
        }
    }
}
