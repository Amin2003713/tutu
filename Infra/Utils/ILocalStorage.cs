namespace Infra.Utils;

public interface ILocalStorage
{
    Task SetAsync<TItem>(string key, TItem data);
    Task<TItem?> GetAsync<TItem>(string key);
    Task DeleteAsync(string key);
}