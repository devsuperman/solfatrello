using Microsoft.EntityFrameworkCore;
using Dominio.Models;

namespace Dominio.Data;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Tarea> Tareas { get; set; }
    public DbSet<Hermano> Hermanos { get; set; }
}
