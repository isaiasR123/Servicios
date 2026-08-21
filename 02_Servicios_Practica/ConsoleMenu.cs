using System.Globalization;
using Aplicacion.Entidades;
using Aplicacion.Servicios;

namespace Presentacion;

internal sealed class ConsoleMenu(
    ClienteServicio clienteServicio,
    ProfesionalServicio profesionalServicio,
    ServicioServicio servicioServicio,
    TurnoServicio turnoServicio)
{
    private readonly ClienteServicio _clienteServicio =
        clienteServicio ?? throw new ArgumentNullException(nameof(clienteServicio));

    private readonly ProfesionalServicio _profesionalServicio =
        profesionalServicio ?? throw new ArgumentNullException(nameof(profesionalServicio));

    private readonly ServicioServicio _servicioServicio =
        servicioServicio ?? throw new ArgumentNullException(nameof(servicioServicio));

    private readonly TurnoServicio _turnoServicio =
        turnoServicio ?? throw new ArgumentNullException(nameof(turnoServicio));

    public void Ejecutar()
    {
        var salir = false;

        while (!salir)
        {
            MostrarOpciones();
            var opcion = Console.ReadLine()?.Trim();

            try
            {
                switch (opcion)
                {
                    case "1":
                        ListarClientes();
                        break;
                    case "2":
                        RegistrarCliente();
                        break;
                    case "3":
                        ListarProfesionales();
                        break;
                    case "4":
                        RegistrarProfesional();
                        break;
                    case "5":
                        ListarServicios();
                        break;
                    case "6":
                        RegistrarServicio();
                        break;
                    case "7":
                        SolicitarTurno();
                        break;
                    case "8":
                        ListarTurnos();
                        break;
                    case "9":
                        CambiarEstadoTurno();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida.");
                        break;
                }
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo completar la operacion: {ex.Message}");
            }

            if (!salir)
            {
                Console.WriteLine();
                Console.Write("Presione Enter para continuar...");
                Console.ReadLine();
            }
        }
    }

    private static void MostrarOpciones()
    {
        Console.Clear();
        Console.WriteLine("Solicitud de turnos");
        Console.WriteLine("-------------------");
        Console.WriteLine("1. Listar clientes");
        Console.WriteLine("2. Registrar cliente");
        Console.WriteLine("3. Listar profesionales");
        Console.WriteLine("4. Registrar profesional");
        Console.WriteLine("5. Listar servicios");
        Console.WriteLine("6. Registrar servicio");
        Console.WriteLine("7. Solicitar turno");
        Console.WriteLine("8. Listar turnos");
        Console.WriteLine("9. Cambiar estado de turno");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opcion: ");
    }

    private void ListarClientes()
    {
        var clientes = _clienteServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Clientes");

        foreach (var cliente in clientes)
        {
            Console.WriteLine(
                $"{cliente.Id}. {cliente.Apellido}, {cliente.Nombre} - {cliente.Telefono} - {cliente.Email}");
        }

        MostrarSinDatos(clientes.Count);
    }

    private void RegistrarCliente()
    {
        var cliente = new Cliente
        {
            Nombre = LeerTexto("Nombre: "),
            Apellido = LeerTexto("Apellido: "),
            Telefono = LeerTexto("Telefono: "),
            Email = LeerTexto("Email: ")
        };

        var id = _clienteServicio.Crear(cliente);

        Console.WriteLine($"Cliente registrado con Id {id}.");
    }

    private void ListarProfesionales()
    {
        var profesionales = _profesionalServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Profesionales");

        foreach (var profesional in profesionales)
        {
            Console.WriteLine(
                $"{profesional.Id}. {profesional.Nombre} - {profesional.Especialidad}");
        }

        MostrarSinDatos(profesionales.Count);
    }

    private void RegistrarProfesional()
    {
        var profesional = new Profesional
        {
            Nombre = LeerTexto("Nombre: "),
            Especialidad = LeerTexto("Especialidad: ")
        };

        var id = _profesionalServicio.Crear(profesional);

        Console.WriteLine($"Profesional registrado con Id {id}.");
    }

    private void ListarServicios()
    {
        var servicios = _servicioServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Servicios");

        foreach (var servicio in servicios)
        {
            Console.WriteLine(
                $"{servicio.Id}. {servicio.Nombre} - {servicio.DuracionMinutos} minutos - ${servicio.Precio:N2}");
        }

        MostrarSinDatos(servicios.Count);
    }

    private void RegistrarServicio()
    {
        var servicio = new Servicio
        {
            Nombre = LeerTexto("Nombre: "),
            DuracionMinutos = LeerEntero("Duracion en minutos: "),
            Precio = LeerDecimal("Precio: ")
        };

        var id = _servicioServicio.Crear(servicio);

        Console.WriteLine($"Servicio registrado con Id {id}.");
    }

    private void SolicitarTurno()
    {
        ListarClientes();
        var clienteId = LeerEntero("Cliente Id: ");

        ListarProfesionales();
        var profesionalId = LeerEntero("Profesional Id: ");

        ListarServicios();
        var servicioId = LeerEntero("Servicio Id: ");

        var turno = new Turno
        {
            ClienteId = clienteId,
            ProfesionalId = profesionalId,
            ServicioId = servicioId,
            FechaHora = LeerFechaHora("Fecha y hora (yyyy-MM-dd HH:mm): "),
            Estado = "Solicitado"
        };

        var id = _turnoServicio.Crear(turno);

        Console.WriteLine($"Turno solicitado con Id {id}.");
    }

    private void ListarTurnos()
    {
        var turnos = _turnoServicio.ObtenerTodos();

        Console.WriteLine();
        Console.WriteLine("Turnos");

        foreach (var turno in turnos)
        {
            Console.WriteLine(
                $"{turno.Id}. Cliente {turno.ClienteId} | Profesional {turno.ProfesionalId} | Servicio {turno.ServicioId} | {turno.FechaHora:yyyy-MM-dd HH:mm} | {turno.Estado}");
        }

        MostrarSinDatos(turnos.Count);
    }

    private void CambiarEstadoTurno()
    {
        ListarTurnos();

        var turnoId = LeerEntero("Turno Id: ");
        var estado = LeerTexto("Nuevo estado: ");

        var actualizado = _turnoServicio.ActualizarEstado(turnoId, estado);

        Console.WriteLine(
            actualizado
                ? "Estado actualizado."
                : "No existe un turno con ese Id.");
    }

    private static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    private static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(
                    Console.ReadLine(),
                    NumberStyles.Integer,
                    CultureInfo.CurrentCulture,
                    out var valor))
            {
                return valor;
            }

            Console.WriteLine("Ingrese un numero entero valido.");
        }
    }

    private static decimal LeerDecimal(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            var entrada = Console.ReadLine();

            if (decimal.TryParse(
                    entrada,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out var valor)
                || decimal.TryParse(
                    entrada,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out valor))
            {
                return valor;
            }

            Console.WriteLine("Ingrese un importe valido.");
        }
    }

    private static DateTime LeerFechaHora(string mensaje)
    {
        const string formato = "yyyy-MM-dd HH:mm";

        while (true)
        {
            Console.Write(mensaje);

            if (DateTime.TryParseExact(
                    Console.ReadLine(),
                    formato,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var fechaHora))
            {
                return fechaHora;
            }

            Console.WriteLine(
                "Ingrese la fecha con el formato yyyy-MM-dd HH:mm.");
        }
    }

    private static void MostrarSinDatos(int cantidad)
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay datos cargados.");
        }
    }
}