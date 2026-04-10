using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MinhasFinancas.Common.DTOs.Account
{
    public class SignInDto
    {
        [EmailAddress(ErrorMessage = "Email inválido.")]

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
