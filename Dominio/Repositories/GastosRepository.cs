using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Data;

namespace Dominio.Repositories;

public class GastosRepository(Contexto db) : IGastosRepository
{
    private readonly Contexto _db = db;

    public async Task<Gasto> Get(int id)
    {
        return await _db.Gastos.FindAsync(id);
    }

    public async Task<List<Gasto>> ListAll(DateTime mesAno, int categoriaId)
    {
        var query = _db.Gastos
                .AsNoTrackingWithIdentityResolution()
                .Include(a => a.Categoria)
                .Where(w =>
                    w.Fecha.Month == mesAno.Month &&
                    w.Fecha.Year == mesAno.Year);

        if (categoriaId > 0)
            query = query.Where(w => w.CategoriaId == categoriaId);

        var lista = await query
            .OrderByDescending(o => o.Fecha)
            .ToListAsync();

        return lista;
    }

    public async Task<List<Tuple<string, decimal>>> ListarPorCategoria(DateTime mesAno)
    {
        var totaisPorCategoria = await _db.Gastos
            .Where(w =>
                w.Fecha.Month == mesAno.Month &&
                w.Fecha.Year == mesAno.Year)
            .Select(s => new
            {
                Categoria = s.Categoria.Nombre,
                s.Valor
            })
            .GroupBy(g => g.Categoria)
            .Select(s => new Tuple<string, decimal>(s.Key, s.Sum(d => d.Valor.Value)))
            .ToListAsync();

        totaisPorCategoria = totaisPorCategoria.OrderByDescending(o => o.Item2).ToList();

        return totaisPorCategoria;
    }

    public async Task<Gasto> Upsert(Gasto model)
    {
        if (model.Id == 0)
            await _db.AddAsync(model);
        else
            _db.Update(model);

        await _db.SaveChangesAsync();

        return model;
    }
}