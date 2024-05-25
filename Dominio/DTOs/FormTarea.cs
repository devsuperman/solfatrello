namespace Dominio.DTOs;

public class FormTarea
{
    public FormTarea()
    {
        
    }
    public FormTarea(ListarTarea model)
    {
        Id = model.Id;
        Fecha = model.Fecha;
        HermanoId = model.HermanoId;
        Descripcion = model.Descripcion;
    }
    public int Id { get; set; }
    public int? HermanoId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Descripcion { get; set; } = string.Empty;
}