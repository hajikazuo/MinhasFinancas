using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.DTOs.Account
{
    public class AuthResponseDto
    {
        public UserDto User { get; set; }
        public string AuthToken { get; set; }
    }
}
