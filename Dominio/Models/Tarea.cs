using System.ComponentModel.DataAnnotations;
using Dominio.DTOs;

namespace Dominio.Models;

public class Tarea
{
    public Tarea()
    {
        
    }

    public Tarea(FormTarea model)
    {
        Update(model);
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "La Fecha es obligatoria")]
    public string Descripcion { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "La Fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Display(Name = "Hermano")]
    public int? HermanoId { get; set; }

    public Hermano Hermano { get; set; }

    internal void Update(FormTarea model)
    {
        Fecha = model.Fecha;
        HermanoId = model.HermanoId;
        Descripcion = model.Descripcion;
    }
}