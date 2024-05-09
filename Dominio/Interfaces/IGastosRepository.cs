using Dominio.Models;
using Dominio.DTOs;

namespace Dominio.Interfaces;

public interface IGastosRepository
{
    public Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime value);
    public Task<List<ListarGasto>> ListAll(DateTime mesAno, int categoriaId);
    public Task<Gasto> Get(int id); 
    public Task<Gasto> Upsert(Gasto model);
}