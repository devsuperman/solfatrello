using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;

namespace NewApp.Client.Services;

public class AutenticacaoService(HttpClient httpClient) : IAutenticacaoService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<LoginResponse> LoginAsync(string password)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/autenticacao", new { password });
        var response = await client.Content.ReadFromJsonAsync<LoginResponse>();
        return response;
    }

}
