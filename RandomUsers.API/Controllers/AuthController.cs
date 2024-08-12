using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomUsers.API.Models;
using RandomUsers.API.Repository;
using RandomUsers.API.Services;

namespace RandomUsers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly LoginRepository _loginRepository;
        private readonly PasswordService _passwordService;

        public AuthController(TokenService tokenService, LoginRepository loginRepository, PasswordService passwordService)
        {
            _tokenService = tokenService;
            _loginRepository = loginRepository;
            _passwordService = passwordService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Usuário ou senha inválidos.");
            }

            var login = await _loginRepository.GetLoginByUsernameAsync(model.Username);
            if (login == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            if (!_passwordService.VerifyPassword(login.Password, model.Password))
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            //Gerar token
            var token = _tokenService.GenerateToken(model.Username);
            return Ok(new { Token = token });
        }
    }
}
