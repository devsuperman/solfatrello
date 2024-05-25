using Dominio.DTOs;

namespace Dominio.Interfaces;

public interface ITareasRepository
{
    public Task<List<ListarTarea>> GetAll();
    public Task<ListarTarea> Get(int id); 
    public Task<FormTarea> Upsert(FormTarea model);
}