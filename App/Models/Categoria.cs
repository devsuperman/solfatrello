using System.ComponentModel.DataAnnotations;

namespace App.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }
}