using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Domain.Models.Enums
{
    public enum TipoPagamento: byte
    {
        Dinheiro = 1,
        CartaoCredito = 2,
        CartaoDebito = 3,
        Pix = 4,
        TransferenciaBancaria = 5,
        Boleto = 6,
        Outros = 7
    }
}
