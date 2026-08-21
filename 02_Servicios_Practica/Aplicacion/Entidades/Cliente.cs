namespace Aplicacion.Entidades;

public sealed class Cliente : Persona
{
    public string Apellido { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
