using Microsoft.AspNetCore.Mvc;
using NetCoreApi.Dtos.User;
using NetCoreApi.Interfaces;
using NetCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDto user)
        {
            var response = await _authRepository.Register
                ( 
                new User { UserName = user.UserName},
                user.Password
                );
            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto user)
        {
            var response = await _authRepository.Login(user.UserName, user.Password);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
