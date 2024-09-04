using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorProjetc.UI.Services.Auth;

public class LocalStorage(ProtectedLocalStorage storage)
{
    public async Task SetAsync<TItem>(string key, TItem data) => await storage.SetAsync(key, data!);

    public async Task<TItem> GetAsync<TItem>(string key)
    {
        var result = (await storage.GetAsync<TItem>(key));
        return result.Success ? result.Value! : default!;
    }


    public async Task DeleteAsync(string key)
        => await storage.DeleteAsync(key);
}