using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinhasFinancas.Common.DTOs.Account
{
    public class SignUpDto
    {
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Required(ErrorMessage = "O email é obrigatório.")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
    }
}
