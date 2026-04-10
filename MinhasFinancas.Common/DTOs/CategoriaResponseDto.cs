using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.DTOs
{
    public class CategoriaResponseDto
    {
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; } 
        public Guid? Usuario { get; set; }
    }
}
