namespace Aplicacion.Entidades;

public sealed class Turno : Entidad
{
    public int ClienteId { get; set; }

    public int ProfesionalId { get; set; }

    public int ServicioId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Estado { get; set; } = "Solicitado";
}
