using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;


namespace BlazorProjetc.UI.Client.Common.Services;

public class WebAssemblyLocalStorage(ILocalStorageService storage)
{

    public async Task SetAsync<TItem>(string key, TItem data)
    {
        var itemJson = JsonSerializer.Serialize(data);
        var itemEncoded = Encoding.UTF32.GetBytes(itemJson);
        var base64 = Convert.ToBase64String(itemEncoded);

        await storage.SetItemAsync(key, base64);
    }

    public async Task<TItem> GetAsync<TItem>(string key)
    {
        var resultBase64 = await storage.GetItemAsync<string>(key)!;
        var resultEncoded = Convert.FromBase64String(resultBase64!);
        var itemJson = Encoding.UTF32.GetString(resultEncoded);
        var item = JsonSerializer.Deserialize<TItem>(itemJson);
        return item ?? default!;
    }


    public async Task DeleteAsync(string key)
        => await storage.RemoveItemAsync(key);
    
}