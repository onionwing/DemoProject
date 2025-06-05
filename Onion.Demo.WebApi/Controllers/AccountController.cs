using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Onion.Demo.Application.Services;
using Onion.Demo.Application.ViewModels;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.Services;

namespace Onion.Demo.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AccountController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        // 登录
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            try
            {
                var token = await _authenticationService.AuthenticateAsync(request.UserName, request.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

        // 注册
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel request)
        {
            try
            {
                await _authenticationService.RegisterAsync(request.UserName, request.Password);
                return Ok("User registered successfully");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("User already exists");
            }
        }

    }
}
