using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.DTOs.Account
{
    public class TokenResultDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
