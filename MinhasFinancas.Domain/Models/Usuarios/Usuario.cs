using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinhasFinancas.Domain.Models.Usuarios
{
    public class Usuario : IdentityUser<Guid>
    {
        [MaxLength(200)]
        public string NomeCompleto { get; set; }
    }
}
