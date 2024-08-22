namespace Application.Common;

public interface IBaseHttpClient
{
    Task<TResponse?> GetAsync<TResponse>(string uri);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data);
    Task<TResponse?> PatchAsync<TRequest, TResponse>(string uri, TRequest data);
    Task<TResponse?> DeleteAsync<TResponse>(string uri);
}