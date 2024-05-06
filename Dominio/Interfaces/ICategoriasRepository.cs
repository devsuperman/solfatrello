using Dominio.Models;

namespace Dominio.Interfaces;

public interface ICategoriasRepository
{
    public Task<List<Categoria>> ListAll();
    public Task<Categoria> Get(int id); 
    public Task Upsert(Categoria categoria);    
}