using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.DTOs;

namespace App.Client.Services;

public class TareasService(HttpClient httpClient) : ITareasRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Tarea> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<Tarea>($"/api/tareas/{id}");
    }

    public async Task<List<ListarTarea>> GetAll()
    {
        var url = $"/api/tareas";
        return await _httpClient.GetFromJsonAsync<List<ListarTarea>>(url);
    }

    public async Task<Tarea> Upsert(Tarea tarea)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/tareas", tarea);
        var response = await client.Content.ReadFromJsonAsync<Tarea>();
        return response;
    }
}
