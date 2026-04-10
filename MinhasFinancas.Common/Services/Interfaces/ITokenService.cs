using MinhasFinancas.Common.DTOs.Account;
using MinhasFinancas.Domain.Models.Usuarios;
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
