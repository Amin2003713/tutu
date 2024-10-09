using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Application.Common;
using Application.User.Auth.Responses;
using Domain.Common.Api;
using Domain.Common.Exceptions;
using Domain.User.Auth;
using Infra.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Infra.Common;

/// <summary>
///     A base HTTP client implementation that provides methods for making HTTP requests
///     with support for generic response types and multipart form data.
/// </summary>
public class BaseHttpClient(HttpClient client , ILocalStorage localStorage , IHttpContextAccessor context) : IBaseHttpClient
{
    /// <summary>
    ///     Sends a GET request to the specified URI.
    /// </summary>
    public async Task<TResponse?> GetAsync<TResponse>(string uri)
    {
        await SetAuthHeader();
        return await GetResponse<TResponse>(await client.GetAsync(uri));
    }

    /// <summary>
    ///     Sends a POST request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PostAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a PUT request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PutAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a PATCH request with JSON content to the specified URI.
    /// </summary>
    public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PatchAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a DELETE request to the specified URI.
    /// </summary>
    public async Task<TResponse?> DeleteAsync<TResponse>(string uri)
    {
        await SetAuthHeader();
        return await GetResponse<TResponse>(await client.DeleteAsync(uri));
    }

    /// <summary>
    ///     Sends a POST request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PostMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PostAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a PUT request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PutMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PutAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a PATCH request with multipart form data to the specified URI.
    /// </summary>
    public async Task<TResponse?> PatchMultipartAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        var response = await client.PatchAsync(uri, data.CreateMultipartContent());
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Sends a DELETE request with a JSON body to the specified URI.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request content.</typeparam>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="uri">The URI to which the DELETE request is sent.</param>
    /// <param name="data">The data to be sent as JSON content.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the expected response type.</returns>
    public async Task<TResponse?> DeleteAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        await SetAuthHeader();
        // Serialize the request data to JSON
        var json = JsonConvert.SerializeObject(data);

        // Create the DELETE request with the body
        var request = new HttpRequestMessage(HttpMethod.Delete, uri)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        // Send the request
        var response = await client.SendAsync(request);

        // Handle the response
        return await GetResponse<TResponse>(response);
    }

    /// <summary>
    ///     Handles the HTTP response, deserializes it if successful, and throws exceptions for errors.
    /// </summary>
    /// <typeparam name="TResponse">The expected type of the response.</typeparam>
    /// <param name="response">The HTTP response message.</param>
    /// <returns>The deserialized response if successful; otherwise, throws an exception.</returns>
    /// <exception cref="HttpRequestException">Thrown when the response indicates an error.</exception>
    private async Task<TResponse?> GetResponse<TResponse>(HttpResponseMessage response)
    {
        // Handle based on status code
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK or HttpStatusCode.Created:
                // Deserialize the response if the status code is 200 OK or 201 Created
                return await response.Content.ReadFromJsonAsync<TResponse>();
            case HttpStatusCode.BadRequest:
               var a = await response.Content.ReadFromJsonAsync<TResponse>();
                throw new HttpRequestException(a is null ? "Bad Request: The server could not understand the request." : $"{(a as ApiResult)?.MetaData.Message}");
            case HttpStatusCode.Unauthorized:
                // Throw an exception for 401 Unauthorized
                throw new HttpRequestException("Unauthorized: Access is denied due to invalid credentials.");
            case HttpStatusCode.Forbidden:
                // Throw an exception for 403 Forbidden
                throw new HttpRequestException("Forbidden: You do not have permission to access this resource.");
            case HttpStatusCode.NotFound:
                // Throw a custom NotFoundException for 404 Not Found
                throw new NotFoundException("Not Found: The requested resource could not be found.");
            case HttpStatusCode.InternalServerError:
                // Throw an exception for 500 Internal Server Error
                throw new HttpRequestException("Internal Server Error: The server encountered an error.");
            default:
                throw new HttpRequestException(
                    $"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}. Details: {await response.Content.ReadAsStringAsync()}");
        }
    }


    public Task SetAuthHeader(LoginResponse response = default!)
    {
        if (response is not null)
        {
            if (client.DefaultRequestHeaders.Any(a => a.Key == "Authorization"))
                return Task.CompletedTask;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
        }
        try
        {
            var a  = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == AuthConfig.Token)?.Value;
            // var result = await localStorage.GetAsync<LoginResponse>("LoginResponse");
            if (a is null || string.IsNullOrEmpty(a))
                return Task.CompletedTask;

            if (client.DefaultRequestHeaders.Any(a => a.Key == "Authorization"))
                return Task.CompletedTask;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", a);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Task.CompletedTask;
    }

}