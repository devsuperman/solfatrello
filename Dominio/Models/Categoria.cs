using System.ComponentModel.DataAnnotations;

namespace Dominio.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }
}