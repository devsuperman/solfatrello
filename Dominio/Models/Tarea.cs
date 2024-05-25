using System.ComponentModel.DataAnnotations;

namespace Dominio.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La Fecha es obligatoria")]
    public string Descripcion { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "La Fecha es obligatoria")]
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Display(Name = "Hermano")]
    public int? HermanoId { get; set; }

    public Hermano Hermano { get; set; }
}