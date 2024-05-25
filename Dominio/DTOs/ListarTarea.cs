namespace Dominio.DTOs;

public class ListarTarea
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Today;
    public int? HermanoId { get; set; }
    public string Hermano { get; set; } = string.Empty;
}