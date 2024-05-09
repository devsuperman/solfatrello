using Dominio.Models;

namespace Dominio.Interfaces;

public interface IGastosRepository
{
    public Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime value);
    public Task<List<Gasto>> ListAll(DateTime mesAno, int categoriaId);
    public Task<Gasto> Get(int id); 
    public Task<Gasto> Upsert(Gasto model);
}