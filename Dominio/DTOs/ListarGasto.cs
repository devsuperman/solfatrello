namespace Dominio.DTOs;

public class ListarGasto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public decimal Valor { get; set; }
    public int CategoriaId { get; set; }
    public string Categoria { get; set; } = string.Empty;
}