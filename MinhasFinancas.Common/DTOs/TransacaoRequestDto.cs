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
    public class TransacaoRequestDto
    {
        public Guid CategoriaId { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        public decimal Valor { get; set; }
        public DateTime? DataPagamento { get; set; }

        public FrequenciaRecorrencia? FrequenciaRecorrencia { get; set; }
        public int? QtdRepeticoes { get; set; } = 0;

        [Required(ErrorMessage = "TipoTransacao é obrigatório")]
        public TipoTransacao TipoTransacao { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public StatusTransacao Status { get; set; }


        [Required(ErrorMessage = "TipoPagamento é obrigatório")]
        public TipoPagamento TipoPagamento { get; set; }
    }
}
