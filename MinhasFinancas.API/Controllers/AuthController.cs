using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasFinancas.Common.DTOs.Account;
using MinhasFinancas.Common.Services.Interfaces;
using MinhasFinancas.Domain.Models.Usuarios;
using RT.Comb;
using System.Security.Claims;

namespace MinhasFinancas.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ITokenService _tokenRepository;
        private readonly ICombProvider _comb;

        public AuthController(UserManager<Usuario> userManager, ITokenService tokenRepository, ICombProvider comb)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _comb = comb;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);

            if (identityUser is null || !await _userManager.CheckPasswordAsync(identityUser, request.Password))
            {
                ModelState.AddModelError("", "Email ou senha inválidos");
                return ValidationProblem(ModelState);
            }

            var response = await BuildAuthResponse(identityUser);

            return Ok(response);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            var user = new Usuario
            {
                Id = _comb.Create(),
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
                NomeCompleto = request.Name.Trim(),
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (!createResult.Succeeded)
            {
                foreach (var error in createResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return ValidationProblem(ModelState);
            }


            var roleResult = await _userManager.AddToRoleAsync(user, "Client");

            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError("", error.Description);

                return ValidationProblem(ModelState);
            }

            var response = await BuildAuthResponse(user);

            return Ok(response);
        }

        private async Task<AuthResponseDto> BuildAuthResponse(Usuario user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
            var response = new AuthResponseDto()
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.NomeCompleto,
                    Roles = roles.ToList()
                },
                AuthToken = jwtToken.Token
            };
            return response;
        }
    }
}
