using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using Dominio.Interfaces;
using System.Text.Json;

namespace NewApp.Client.Services;

public class ClientAuthenticationProvider(ITokenStorage tokenStorage, HttpClient http) : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage = tokenStorage;
    private readonly HttpClient _http = http;
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() => await LoadAuthenticationState();

    private async Task<AuthenticationState> LoadAuthenticationState()
    {
        var token = await _tokenStorage.Get();

        var identity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(token))
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        return state;
    }

    public async Task LoginAsync(string token)
    {
        await _tokenStorage.Set(token);
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var state = await LoadAuthenticationState();
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.Set("");
        _http.DefaultRequestHeaders.Authorization = null;

        var state = await LoadAuthenticationState();
        NotifyAuthenticationStateChanged(Task.FromResult(state));
    }
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
