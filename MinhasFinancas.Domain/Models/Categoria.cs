using MinhasFinancas.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinhasFinancas.Domain.Models
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Nome { get; set; }
        
        public Guid? UsuarioId { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
