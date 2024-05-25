using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.DTOs;
using Dominio.Data;

namespace Dominio.Repositories;

public class TareasRepository(Contexto db) : ITareasRepository
{
    private readonly Contexto _db = db;

    public async Task<Tarea> Get(int id)
    {
        return await _db.Tareas.FindAsync(id);
    }

    public async Task<List<ListarTarea>> GetAll()
    {
        var lista = await _db.Tareas
            .AsNoTrackingWithIdentityResolution()
            .Include(a => a.Hermano)
            .OrderByDescending(o => o.Fecha)
            .Select(s => new ListarTarea
            {
                Id = s.Id,
                Fecha = s.Fecha,
                Descripcion = s.Descripcion,
                HermanoId = s.HermanoId,
                Hermano = s.Hermano.Nombre
            })
            .ToListAsync();

        return lista;
    }

    public async Task<Tarea> Upsert(Tarea model)
    {
        if (model.Id == 0)
            await _db.AddAsync(model);
        else
            _db.Update(model);

        await _db.SaveChangesAsync();

        return model;
    }
}