using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhasFinancas.Common.Data;
using MinhasFinancas.Common.DTOs;
using MinhasFinancas.Domain.Models;
using RT.Comb;
using System.Security.Claims;

namespace MinhasFinancas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly MinhasFinancasDbContext _context;
        private readonly ICombProvider _comb;

        public CategoriasController(MinhasFinancasDbContext context, ICombProvider comb)
        {
            _context = context;
            _comb = comb;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Usuário não identificado");
            }

            var response = await _context.Categorias
                .Where(c => c.UsuarioId == null || c.UsuarioId == userId)
                .Select(c => new CategoriaResponseDto
                {
                    CategoriaId = c.CategoriaId,
                    Nome = c.Nome,
                    Usuario = c.UsuarioId
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _context.Categorias
                .Where(c => c.CategoriaId == id)
                .Select(c => new CategoriaResponseDto
                {
                    CategoriaId = c.CategoriaId,
                    Nome = c.Nome,
                    Usuario = c.UsuarioId
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriaRequestDto dto)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                {
                    return BadRequest("Usuário não identificado");
                }

                var categoria = new Categoria
                {
                    CategoriaId = _comb.Create(),
                    Nome = dto.Nome,
                    UsuarioId = userId
                };

                await _context.Categorias.AddAsync(categoria);
                await _context.SaveChangesAsync();
                return Ok("Cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoriaRequestDto dto)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                {
                    return BadRequest("Usuário não identificado");
                }

                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id && c.UsuarioId == userId);

                if (categoria == null)
                {
                    return NotFound();
                }

                categoria.Nome = dto.Nome;
                await _context.SaveChangesAsync();
                return Ok("Alterado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                {
                    return BadRequest("Usuário não identificado");
                }

                var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id && c.UsuarioId == userId);

                if (categoria == null)
                {
                    return NotFound();
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return Ok("Removido com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
