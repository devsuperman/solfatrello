using System.Net.Http.Json;
using Dominio.Interfaces;
using Dominio.DTOs;

namespace SolfatrelloApp.Client.Services;

public class TareasService(HttpClient httpClient) : ITareasRepository
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ListarTarea> Get(int id)
    {
        return await _httpClient.GetFromJsonAsync<ListarTarea>($"/api/tareas/{id}");
    }

    public async Task<List<ListarTarea>> GetAll(int hermanoId = 0)
    {
        var url = $"/api/tareas?hermanoId={hermanoId}";
        return await _httpClient.GetFromJsonAsync<List<ListarTarea>>(url);
    }

    public async Task<FormTarea> Upsert(FormTarea tarea)
    {
        var client = await _httpClient.PostAsJsonAsync("/api/tareas", tarea);
        var response = await client.Content.ReadFromJsonAsync<FormTarea>();
        return response;
    }
}
