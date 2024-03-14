using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Gasto> Gastos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}
