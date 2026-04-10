using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinhasFinancas.Common.DTOs
{
    public class CategoriaRequestDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }
    }
}
