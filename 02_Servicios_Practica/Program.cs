using System.Text.Json;
using Aplicacion.Servicios;
using Persistencia;
using Persistencia.Repositorios;
using Presentacion;

var connectionString = ObtenerCadenaConexion();
var connectionFactory = new MySqlConnectionFactory(connectionString);

var clienteRepositorio = new ClienteRepositorio(connectionFactory);
var profesionalRepositorio = new ProfesionalRepositorio(connectionFactory);
var servicioRepositorio = new ServicioRepositorio(connectionFactory);
var turnoRepositorio = new TurnoRepositorio(connectionFactory);

var clienteServicio = new ClienteServicio(clienteRepositorio);
var profesionalServicio = new ProfesionalServicio(profesionalRepositorio);
var servicioServicio = new ServicioServicio(servicioRepositorio);
var turnoServicio = new TurnoServicio(turnoRepositorio);

var menu = new ConsoleMenu(
    clienteServicio,
    profesionalServicio,
    servicioServicio,
    turnoServicio);

menu.Ejecutar();

static string ObtenerCadenaConexion()
{
    const string fallback = "Server=localhost;Port=3306;Database=turnos_app;User ID=root;Password=;";
    var ruta = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

    if (!File.Exists(ruta))
    {
        return fallback;
    }

    using var stream = File.OpenRead(ruta);
    using var document = JsonDocument.Parse(stream);

    if (!document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings))
    {
        return fallback;
    }

    if (!connectionStrings.TryGetProperty("Default", out var defaultConnection))
    {
        return fallback;
    }

    var value = defaultConnection.GetString();
    return string.IsNullOrWhiteSpace(value) ? fallback : value;
}