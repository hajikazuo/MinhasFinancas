using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Domain.Models.Enums
{
    public enum FrequenciaRecorrencia: byte
    {
        SemRecorrencia,
        Diaria = 1,
        Semanal = 2,
        Quinzenal = 3,
        Mensal = 4,
        Anual = 5
    }
}
