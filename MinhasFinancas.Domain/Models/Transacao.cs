using MinhasFinancas.Domain.Models.Enums;
using MinhasFinancas.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Transactions;

namespace MinhasFinancas.Domain.Models
{
    public class Transacao
    {
        public Guid TransacaoId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid CategoriaId { get; set; }

        [MaxLength(500)]
        public string? Descricao { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataPagamento { get; set; }

        public FrequenciaRecorrencia? FrequenciaRecorrencia { get; set; }
        public int? QtdRepeticoes { get; set; }

        public TipoTransacao TipoTransacao { get; set; }
        public StatusTransacao Status { get; set; }
        public TipoPagamento TipoPagamento { get; set; }

        public virtual Usuario? Usuario { get; set; }    
        public virtual Categoria? Categoria { get; set; }

        public void DefinirStatus()
        {
            Status = DataPagamento.HasValue
                ? StatusTransacao.Concluida
                : StatusTransacao.Pendente;
        }
    }
}
