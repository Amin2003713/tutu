using System.Net;
using System.Net.Http.Json;
using Application.Common;
using Domain.Common;

namespace Infra.Common;

public class BaseHttpClient(HttpClient client) :IBaseHttpClient
{
    public async Task<TResponse?> GetAsync<TResponse>(string uri)
    {
      return await GetResponse<TResponse>(await client.GetAsync(uri));
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PostAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }


    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PutAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        var response = await client.PatchAsJsonAsync(uri, data);
        return await GetResponse<TResponse>(response);
    }

    public async Task<TResponse?> DeleteAsync<TResponse>(string uri)
    {
        return await GetResponse<TResponse>(await client.GetAsync(uri));
    }


    private async Task<TResponse?> GetResponse<TResponse>(HttpResponseMessage response)
    {
        // Handle based on status code
        return response.StatusCode switch
        {
            HttpStatusCode.OK or HttpStatusCode.Created =>
                // Deserialize the response if the status code is 200 OK
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
                throw new HttpRequestException("Internal Server Error: The server encountered an error "),

            _ =>throw new HttpRequestException($"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}. Details: {await response.Content.ReadAsStringAsync()}")
        };
    }
}