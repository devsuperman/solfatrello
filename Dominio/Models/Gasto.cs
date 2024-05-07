using System.ComponentModel.DataAnnotations;

namespace Dominio.Models;

public class Gasto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    [Required, DataType(DataType.Date)]
    public DateTime Fecha { get; set; } = DateTime.Today;


    [Required, DataType(DataType.Currency)]
    public decimal? Valor { get; set; }


    [Required, Display(Name="Categoria")]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}