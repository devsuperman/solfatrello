using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.DTOs;
using Dominio.Data;

namespace Dominio.Repositories;

public class TareasRepository(Contexto db) : ITareasRepository
{
    private readonly Contexto _db = db;

    public async Task<ListarTarea> Get(int id)
    {
        var entity = await _db.Tareas
            .AsNoTracking()
            .Include(a => a.Hermano)
            .SingleOrDefaultAsync(a => a.Id == id);

        return new ListarTarea(entity);
    }

    public async Task<List<ListarTarea>> GetAll(int hermanoId = 0)
    {
        try
        {
            var lista = await _db.Tareas
                .AsNoTrackingWithIdentityResolution()
                .Include(a => a.Hermano)
                .Where(w => hermanoId == 0 || w.HermanoId == hermanoId)
                .OrderByDescending(o => o.Fecha)
                .Select(s => new ListarTarea(s))
                .ToListAsync();

            return lista;            
        }
        catch (Npgsql.PostgresException ex) 
        {
            if(ex.Message.Contains("starting up"))
            {
                await Task.Delay(5000);
                return await GetAll(hermanoId);
            }

            throw;
        }
    }

    public async Task<FormTarea> Upsert(FormTarea model)
    {
        var entity = new Tarea(model);

        if (model.Id == 0)
            await _db.AddAsync(entity);
        else
        {
            entity = await _db.Tareas.FindAsync(model.Id);
            entity.Update(model);
            _db.Update(entity);
        }

        await _db.SaveChangesAsync();

        model.Id = entity.Id;
        return model;
    }
}