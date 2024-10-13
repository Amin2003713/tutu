using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Infra.Utils.Constants.Storage;

namespace Infra.User.Auth;

public class AuthenticationHeaderHandler(ILocalStorageService localStorage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization?.Scheme == "Bearer")
            return await base.SendAsync(request, cancellationToken);

        var savedToken = await localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken, cancellationToken);

        if (!string.IsNullOrWhiteSpace(savedToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

        return await base.SendAsync(request, cancellationToken);
    }
}