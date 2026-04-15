using System;
using System.Collections.Generic;
using System.Text;

namespace MinhasFinancas.Common.Services.Interfaces
{
    public interface ILocalStorageService
    {
        ValueTask SetAsync<T>(string key, T value);
        ValueTask<T?> GetAsync<T>(string key);
        ValueTask RemoveAsync(string key);
        ValueTask ClearAsync();
        ValueTask<bool> ContainsKeyAsync(string key);
    }
}
