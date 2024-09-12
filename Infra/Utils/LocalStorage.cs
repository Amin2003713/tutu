using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Infra.Utils;

public class LocalStorage(IJSRuntime jsRuntime) : ILocalStorage
{
    public async Task SetAsync<TItem>(string key, TItem data)
    {
        var itemJson = JsonSerializer.Serialize(data);
        var itemEncoded = Encoding.UTF32.GetBytes(itemJson);
        var base64 = Convert.ToBase64String(itemEncoded);

        await jsRuntime.InvokeVoidAsync($"localStorage.setItem", key, base64);
    }

    public async Task<TItem?> GetAsync<TItem>(string key)
    {
        var resultBase64 =await jsRuntime.InvokeAsync<string?>($"localStorage.getItem" , key);
        if (string.IsNullOrEmpty(resultBase64))
            return default;
        var resultEncoded = Convert.FromBase64String(resultBase64!);
        var itemJson = Encoding.UTF32.GetString(resultEncoded);
        var item = JsonSerializer.Deserialize<TItem>(itemJson);
        return item ?? default!;
    }


    public async Task DeleteAsync(string key)
        => await jsRuntime.InvokeVoidAsync($"localStorage.removeItem", key);
    
}