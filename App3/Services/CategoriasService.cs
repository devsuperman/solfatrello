using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;

namespace App3.Services;

public class CategoriasService(HttpClient httpClient) : ICategoriasRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Categoria> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Categoria>($"/api/categorias/{id}");
    }    

    public async Task<List<Categoria>> ListAll()
    {
        return await _httpClient.GetFromJsonAsync<List<Categoria>>("/api/categorias");
    }

    public async Task<Categoria> Upsert(Categoria categoria)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/categorias", categoria);
        var response = await client.Content.ReadFromJsonAsync<Categoria>();
        return response;
    }
}
