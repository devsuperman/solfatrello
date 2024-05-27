using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Data;

namespace Dominio.Repositories;

public class HermanosRepository(Contexto db) : IHermanosRepository
{
    private readonly Contexto _db = db;

    public async Task<Hermano> Get(int id)
    {
        return await _db.Hermanos.FindAsync(id);
    }

    public async Task<List<Hermano>> GetAll()
    {
        return await _db.Hermanos.AsNoTracking().OrderBy(o => o.Nombre).ToListAsync();
    }

    public async Task<Hermano> Upsert(Hermano hermano)
    {
        if (hermano.Id == 0)
            await _db.AddAsync(hermano);
        else
            _db.Update(hermano);

        await _db.SaveChangesAsync();

        return hermano;
    }
}