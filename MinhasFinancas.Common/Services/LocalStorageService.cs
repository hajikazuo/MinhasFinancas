using Microsoft.JSInterop;
using MinhasFinancas.Common.Services.Interfaces;
using System.Text.Json;

namespace MinhasFinancas.Common.Services
{
    public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
    {
        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true
        };

        public async ValueTask SetAsync<T>(string key, T value)
        {
            if (value is null)
            {
                await RemoveAsync(key);
                return;
            }

            var json = JsonSerializer.Serialize(value, JsonOptions);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        public async ValueTask<T?> GetAsync<T>(string key)
        {
            var json = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public ValueTask RemoveAsync(string key) =>
            jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);

        public ValueTask ClearAsync() =>
            jsRuntime.InvokeVoidAsync("localStorage.clear");

        public async ValueTask<bool> ContainsKeyAsync(string key)
        {
            var value = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
            return value is not null;
        }

        public async ValueTask<int> LengthAsync() =>
            await jsRuntime.InvokeAsync<int>("eval", "localStorage.length");

        public async ValueTask<string?> KeyAsync(int index) =>
            await jsRuntime.InvokeAsync<string?>("localStorage.key", index);
    }
}
