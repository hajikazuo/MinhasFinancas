using MinhasFinancas.Common.DTOs.Account;
using MinhasFinancas.Common.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.Services.Interfaces
{
    public interface ITokenService
    {
        TokenResultDto CreateJwtToken(Usuario usuario, IList<string> funcoes);
    }
}
