namespace Aplicacion.Entidades;

public sealed class Servicio : Entidad
{
    public string Nombre { get; set; } = string.Empty;

    public int DuracionMinutos { get; set; }

    public decimal Precio { get; set; }
}
