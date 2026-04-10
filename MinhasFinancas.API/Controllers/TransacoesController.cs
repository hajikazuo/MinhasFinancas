using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class TransacoesController : ControllerBase
    {
        private readonly MinhasFinancasDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICombProvider _comb;

        public TransacoesController(MinhasFinancasDbContext context, IMapper mapper, ICombProvider comb)
        {
            _context = context;
            _mapper = mapper;
            _comb = comb;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? mes, int? ano)
        {
            var dataAtual = DateTime.Now;
            mes = mes ?? dataAtual.Month;
            ano = ano ?? dataAtual.Year;
            var dataInicial = new DateTime(ano.Value, mes.Value, 1);
            var dataFinal = dataInicial.AddMonths(1).AddDays(-1);

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Usuário não identificado");
            }

            var response = await _context.Transacoes
                .Where(t => t.UsuarioId == userId && t.DataCadastro >= dataInicial && t.DataCadastro <= dataFinal)
                .Select(t => new TransacaoResponseDto
                {
                    TransacaoId = t.TransacaoId,
                    CategoriaId = t.CategoriaId,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    DataCadastro = t.DataCadastro,
                    DataPagamento = t.DataPagamento,
                    FrequenciaRecorrencia = t.FrequenciaRecorrencia,
                    QtdRepeticoes = t.QtdRepeticoes,
                    TipoTransacao = t.TipoTransacao,
                    Status = t.Status,
                    TipoPagamento = t.TipoPagamento,
                    Usuario = t.Usuario != null ? t.Usuario.NomeCompleto : null,
                    Categoria = t.Categoria != null ? t.Categoria.Nome : null
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Usuário não identificado");
            }

            var response = await _context.Transacoes
                .Where(t => t.TransacaoId == id && t.UsuarioId == userId)
                .Select(t => new TransacaoResponseDto
                {
                    TransacaoId = t.TransacaoId,
                    CategoriaId = t.CategoriaId,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    DataCadastro = t.DataCadastro,
                    DataPagamento = t.DataPagamento,
                    FrequenciaRecorrencia = t.FrequenciaRecorrencia,
                    QtdRepeticoes = t.QtdRepeticoes,
                    TipoTransacao = t.TipoTransacao,
                    Status = t.Status,
                    TipoPagamento = t.TipoPagamento,
                    Usuario = t.Usuario != null ? t.Usuario.NomeCompleto : null,
                    Categoria = t.Categoria != null ? t.Categoria.Nome : null
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
        public async Task<IActionResult> Post([FromBody] TransacaoRequestDto dto)
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out var userId))
                {
                    return BadRequest("Usuário não identificado");
                }

                var transacao = _mapper.Map<Transacao>(dto);
                transacao.TransacaoId = _comb.Create();
                transacao.UsuarioId = userId;
                transacao.DataCadastro = DateTime.Now;

                _context.Transacoes.Add(transacao);
                await _context.SaveChangesAsync();

                return Ok("Cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TransacaoRequestDto dto)
        {
            try
            {
                var transacao = _mapper.Map<Transacao>(dto);
                _context.Update(transacao);
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
                var transacao = await _context.Transacoes.FindAsync(id);
                if (transacao == null)
                {
                    return NotFound("Transação não encontrada");
                }

                _context.Transacoes.Remove(transacao);
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
