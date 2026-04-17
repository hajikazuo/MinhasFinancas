using MinhasFinancas.Common.Models.Enums;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "TipoPagamento é obrigatório")]
        public TipoPagamento TipoPagamento { get; set; }
    }
}
