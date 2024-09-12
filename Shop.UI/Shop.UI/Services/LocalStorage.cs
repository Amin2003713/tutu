using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Shop.UI.Services;

public class LocalStorage(ProtectedLocalStorage storage)               :ILocalStorage
{
    public async Task SetAsync<TItem>(string key, TItem data) => await storage.SetAsync(key, data!);

    public async Task<TItem?> GetAsync<TItem>(string key)
    {
        ProtectedBrowserStorageResult<TItem> result = await storage.GetAsync<TItem>(key);
        return result.Value ?? default;
    }


    public async Task DeleteAsync(string key)
        => await storage.DeleteAsync(key);
}


public interface ILocalStorage
{
    Task SetAsync<TItem>(string key, TItem data);
    Task<TItem> GetAsync<TItem>(string key);
    Task DeleteAsync(string key);
}