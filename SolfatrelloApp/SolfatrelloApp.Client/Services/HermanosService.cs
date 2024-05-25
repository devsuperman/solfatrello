using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;

namespace SolfatrelloApp.Client.Services;

public class HermanosService(HttpClient httpClient) : IHermanosRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Hermano> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Hermano>($"/api/hermanos/{id}");
    }    

    public async Task<List<Hermano>> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<List<Hermano>>("/api/hermanos");
    }

    public async Task<Hermano> Upsert(Hermano hermano)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/hermanos", hermano);
        var response = await client.Content.ReadFromJsonAsync<Hermano>();
        return response;
    }
}
