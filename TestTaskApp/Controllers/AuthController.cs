using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTaskApp.Data;
using TestTaskApp.DTO;
using TestTaskApp.Helpers;
using TestTaskApp.Models;

namespace TestTaskApp.Controllers
{

    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var user = new User
            {
                Login = dto.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            return Created("Success", _repository.Create(user, dto.Login));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = _repository.GetByLogin(dto.Login);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }
            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _repository.GetById(userId);

                return Ok(user);
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
