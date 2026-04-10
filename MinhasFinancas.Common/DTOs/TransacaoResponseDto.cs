using MinhasFinancas.Domain.Models;
using MinhasFinancas.Domain.Models.Enums;
using MinhasFinancas.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MinhasFinancas.Common.DTOs
{
    public class TransacaoResponseDto
    {
        public Guid TransacaoId { get; set; }
        public Guid CategoriaId { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataPagamento { get; set; }

        public FrequenciaRecorrencia? FrequenciaRecorrencia { get; set; }
        public int? QtdRepeticoes { get; set; }

        public TipoTransacao TipoTransacao { get; set; }
        public StatusTransacao Status { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public string? Usuario { get; set; }
        public string? Categoria { get; set; }
    }
}
