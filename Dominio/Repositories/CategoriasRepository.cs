using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Data;

namespace Dominio.Repositories;

public class CategoriasRepository(Contexto db) : ICategoriasRepository
{
    private readonly Contexto _db = db;

    public async Task<Categoria> Get(int id)
    {
        return await _db.Categorias.FindAsync(id);
    }

    public async Task<List<Categoria>> ListAll()
    {
        return await _db.Categorias.AsNoTracking().OrderBy(o => o.Nombre).ToListAsync();
    }

    public async Task<Categoria> Upsert(Categoria categoria)
    {
        if (categoria.Id == 0)
            await _db.AddAsync(categoria);
        else
            _db.Update(categoria);

        await _db.SaveChangesAsync();

        return categoria;
    }
}