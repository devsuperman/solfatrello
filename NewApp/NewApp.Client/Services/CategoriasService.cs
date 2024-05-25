using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;

namespace NewApp.Client.Services;

public class CategoriasService(HttpClient httpClient) : IHermanosRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Hermano> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Hermano>($"/api/categorias/{id}");
    }    

    public async Task<List<Hermano>> GetAll()
    {
        return await _httpClient.GetFromJsonAsync<List<Hermano>>("/api/categorias");
    }

    public async Task<Hermano> Upsert(Hermano categoria)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/categorias", categoria);
        var response = await client.Content.ReadFromJsonAsync<Hermano>();
        return response;
    }
}
