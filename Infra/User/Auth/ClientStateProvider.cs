using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Infra.Utils;
using Infra.Utils.Constants.Permission;
using Infra.Utils.Constants.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace Infra.User.Auth;

public class ClientStateProvider(
    HttpClient httpClient,
    ILocalStorage localStorage ,
    NavigationManager navigationManager , IHttpContextAccessor accessor)
    : AuthenticationStateProvider
{

    public ClaimsPrincipal AuthenticationStateUser { get; set; }

    public void MarkUserAsAuthenticated(ClaimsPrincipal authenticatedUser)
    {
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

        NotifyAuthenticationStateChanged(authState);

    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));

        NotifyAuthenticationStateChanged(authState);

    }

    public async Task<ClaimsPrincipal> GetAuthenticationStateProviderUserAsync()
    {
        var state = await GetAuthenticationStateAsync();
        var authenticationStateProviderUser = state.User;
        return authenticationStateProviderUser;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await localStorage.GetAsync<string>(StorageConstants.Local.AuthToken);
        if (string.IsNullOrWhiteSpace(savedToken))
        {
            if (!navigationManager.Uri.Contains("/login"))
                navigationManager.NavigateTo("/login", true);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
        var state = new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt")));
        AuthenticationStateUser = state.User;
        return state;
    }

    private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    claims.AddRange(parsedRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            keyValuePairs.TryGetValue(ApplicationClaimTypes.Permission, out var permissions);
            if (permissions != null)
            {
                if (permissions.ToString().Trim().StartsWith("["))
                {
                    var parsedPermissions = JsonSerializer.Deserialize<string[]>(permissions.ToString());
                    claims.AddRange(parsedPermissions.Select(permission =>
                        new Claim(ApplicationClaimTypes.Permission, permission)));
                }
                else
                {
                    claims.Add(new Claim(ApplicationClaimTypes.Permission, permissions.ToString()));
                }

                keyValuePairs.Remove(ApplicationClaimTypes.Permission);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}