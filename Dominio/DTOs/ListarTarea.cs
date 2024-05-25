using Dominio.Models;

namespace Dominio.DTOs;

public class ListarTarea
{
    public ListarTarea()
    {
        
    }
    public ListarTarea(Tarea model)
    {
        Id = model.Id;
        Descripcion = model.Descripcion;
        Fecha = model.Fecha;
        HermanoId = model.HermanoId;
        Hermano = model.Hermano.Nombre;
    }

    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public int? HermanoId { get; set; }
    public string Hermano { get; set; } = string.Empty;
}