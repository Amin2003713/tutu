using Application.User.Auth.Responses;

namespace Application.Common;

/// <summary>
///     Provides an interface for making HTTP requests with support for generic response types.
/// </summary>
public interface IBaseHttpClient
{
    /// <summary>
    ///     Sends a GET request to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the GET request is sent.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> GetAsync<TResponse>(string uri);

    /// <summary>
    ///     Sends a POST request with JSON content to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the POST request is sent.</param>
    /// <param name="data">The data to be sent as JSON content.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data);

    /// <summary>
    ///     Sends a PUT request with JSON content to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the PUT request is sent.</param>
    /// <param name="data">The data to be sent as JSON content.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data);

    /// <summary>
    ///     Sends a PATCH request with JSON content to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the PATCH request is sent.</param>
    /// <param name="data">The data to be sent as JSON content.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PatchAsync<TRequest, TResponse>(string uri, TRequest data);

    /// <summary>
    ///     Sends a DELETE request to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the DELETE request is sent.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> DeleteAsync<TResponse>(string uri);

    /// <summary>
    ///     Sends a POST request with multipart form data to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <param name="uri">The URI to which the POST request is sent.</param>
    /// <param name="data">data that will be converted to Multipart </param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PostMultipartAsync<TRequest, TResponse>(string uri, TRequest data);

    /// <summary>
    ///     Sends a PUT request with multipart form data to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the PUT request is sent.</param>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <param name="data">data that will be converted to Multipart </param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PutMultipartAsync<TRequest, TResponse>(string uri, TRequest data);

    /// <summary>
    ///     Sends a PATCH request with multipart form data to the specified URI.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the PATCH request is sent.</param>
    /// <param name="data">data that will be converted to Multipart </param>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    Task<TResponse?> PatchMultipartAsync<TRequest, TResponse>(string uri, TRequest data);


    Task SetAuthHeader(LoginResponse response = default!);
}