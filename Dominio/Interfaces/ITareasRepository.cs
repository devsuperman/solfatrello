using Dominio.Models;
using Dominio.DTOs;

namespace Dominio.Interfaces;

public interface ITareasRepository
{
    public Task<List<ListarTarea>> GetAll();
    public Task<Tarea> Get(int id); 
    public Task<Tarea> Upsert(Tarea model);
}