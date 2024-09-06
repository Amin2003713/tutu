using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorProjetc.UI.Services;

public class LocalStorage(ProtectedLocalStorage storage)
{
    public async Task SetAsync<TItem>(string key, TItem data) => await storage.SetAsync(key, data!);

    public async Task<ProtectedBrowserStorageResult<TItem>> GetAsync<TItem>(string key)
    {
        return (await storage.GetAsync<TItem>(key));
    }


    public async Task DeleteAsync(string key)
        => await storage.DeleteAsync(key);
}