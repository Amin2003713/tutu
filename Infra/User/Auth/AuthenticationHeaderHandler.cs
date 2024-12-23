﻿using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Infra.Utils;
using Infra.Utils.Constants.Storage;

namespace Infra.User.Auth;

public class AuthenticationHeaderHandler(ILocalStorage localStorage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization?.Scheme == "Bearer")
            return await base.SendAsync(request, cancellationToken);

        var savedToken = await localStorage.GetAsync<string>(StorageConstants.Local.AuthToken);

        if (!string.IsNullOrWhiteSpace(savedToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

        return await base.SendAsync(request, cancellationToken);
    }
}