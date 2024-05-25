using Dominio.Models;

namespace Dominio.Interfaces;

public interface IHermanosRepository
{
    public Task<List<Hermano>> GetAll();
    public Task<Hermano> Get(int id); 
    public Task<Hermano> Upsert(Hermano model);    
}