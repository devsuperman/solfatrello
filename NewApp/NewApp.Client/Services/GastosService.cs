using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.DTOs;

namespace NewApp.Client.Services;

public class GastosService(HttpClient httpClient) : ITareasRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Tarea> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Tarea>($"/api/gastos/{id}");
    }

    public async Task<List<ListarTarea>> ListAll(DateTime mesAno, int categoriaId)
    {
        var url = $"/api/gastos?mesAno={mesAno:yyyy-MM}&categoriaId={categoriaId}";
        return await _httpClient.GetFromJsonAsync<List<ListarTarea>>(url);
    }

    public async Task<List<Tuple<string, decimal>>> ListarPorHermano(DateTime mesAno)
    {
        var url = $"/api/gastos/listarPorCategoria?mesAno={mesAno:yyyy-MM}";
        return await _httpClient.GetFromJsonAsync<List<Tuple<string, decimal>>>(url);
    }

    public async Task<Tarea> Upsert(Tarea categoria)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/gastos", categoria);
        var response = await client.Content.ReadFromJsonAsync<Tarea>();
        return response;
    }
}
