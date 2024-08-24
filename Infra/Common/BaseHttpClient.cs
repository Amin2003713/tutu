using System.Net;
using System.Net.Http.Json;
using Application.Common;
using Domain.Common;
using Infra.Utils;

namespace Infra.Common;

/// <summary>
/// A base HTTP client implementation that provides methods for making HTTP requests
/// with support for generic response types and multipart form data.
/// </summary>
public class BaseHttpClient(HttpClient client) : IBaseHttpClient
{
    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    public async Task<TResponse?> GetAsync<TResponse>(string uri)
    {
        return await GetResponse<TResponse>(await client.GetAsync(uri));
    }

    /// <summary>
    /// Sends a POST request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PostAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Sends a PUT request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PutAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Sends a PATCH request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PatchAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Sends a DELETE request to the specified URI.
    /// </summary>
    public async Task<TResponse?> DeleteAsync<TResponse>(string uri)
    {
        return await GetResponse<TResponse>(await client.DeleteAsync(uri));
    }

    /// <summary>
    /// Sends a POST request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PostMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PostAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Sends a PUT request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PutMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PutAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Sends a PATCH request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PatchMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PatchAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    /// Handles the HTTP response, deserializes it if successful, and throws exceptions for errors.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="response">The HTTP response message.</param>
    /// <returns>The deserialized response if successful; otherwise, throws an exception.</returns>
    /// <exception cref="HttpRequestException">Thrown when the response indicates an error.</exception>
    private async Task<TResponse?> GetResponse<TResponse>(HttpResponseMessage response)
    {
        // Handle based on status code
        return response.StatusCode switch
        {
            HttpStatusCode.OK or HttpStatusCode.Created =>
                // Deserialize the response if the status code is 200 OK or 201 Created
                (await response.Content.ReadFromJsonAsync<TResponse>()),
            HttpStatusCode.BadRequest =>
                // Throw an exception for 400 Bad Request
                throw new HttpRequestException("Bad Request: The server could not understand the request."),
            HttpStatusCode.Unauthorized =>
                // Throw an exception for 401 Unauthorized
                throw new HttpRequestException("Unauthorized: Access is denied due to invalid credentials."),
            HttpStatusCode.Forbidden =>
                // Throw an exception for 403 Forbidden
                throw new HttpRequestException("Forbidden: You do not have permission to access this resource."),
            HttpStatusCode.NotFound =>
                // Throw a custom NotFoundException for 404 Not Found
                throw new NotFoundException("Not Found: The requested resource could not be found."),
            HttpStatusCode.InternalServerError =>
                // Throw an exception for 500 Internal Server Error
                throw new HttpRequestException("Internal Server Error: The server encountered an error."),

            _ => throw new HttpRequestException($"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}. Details: {await response.Content.ReadAsStringAsync()}")
        };
    }
}
