using System;
using Spectre.Console;

class Program
{
    static void Main(string[] args)
    {
        MostrarTitulo();

        var datos = CapturarDatos();

        decimal cuota = CalcularCuota(datos.monto, datos.interesAnual, datos.meses);

        GenerarTabla(datos.monto, datos.interesAnual, datos.meses, cuota);

        MostrarMensajeFinal();
        Console.ReadKey();
    }

    // ===============================
    // MÉTODO 1 - Mostrar Título
    // ===============================
    static void MostrarTitulo()
    {
        AnsiConsole.MarkupLine("[bold yellow]Calculadora de Préstamos[/]");
        AnsiConsole.WriteLine();
    }

    // ===============================
    // MÉTODO 2 - Capturar Datos
    // ===============================
    static (decimal monto, decimal interesAnual, int meses) CapturarDatos()
    {
        decimal monto = AnsiConsole.Ask<decimal>("Ingrese el [green]monto del préstamo[/]:");
        decimal interesAnual = AnsiConsole.Ask<decimal>("Ingrese la [green]tasa de interés anual (%) [/]:");
        int meses = AnsiConsole.Ask<int>("Ingrese el [green]plazo en meses[/]:");

        return (monto, interesAnual, meses);
    }

    // ===============================
    // MÉTODO 3 - Calcular Cuota
    // ===============================
    static decimal CalcularCuota(decimal monto, decimal interesAnual, int meses)
    {
        decimal tasaMensual = (interesAnual / 12) / 100;

        decimal cuota = monto *
            (tasaMensual * (decimal)Math.Pow(1 + (double)tasaMensual, meses)) /
            ((decimal)Math.Pow(1 + (double)tasaMensual, meses) - 1);

        return cuota;
    }

    // ===============================
    // MÉTODO 4 - Generar Tabla
    // ===============================
    static void GenerarTabla(decimal monto, decimal interesAnual, int meses, decimal cuota)
    {
        decimal tasaMensual = (interesAnual / 12) / 100;
        decimal saldo = monto;

        var tabla = new Table();
        tabla.Border(TableBorder.Rounded);
        tabla.AddColumn("No.");
        tabla.AddColumn("Pago de cuota");
        tabla.AddColumn("Interés");
        tabla.AddColumn("Abono capital");
        tabla.AddColumn("Saldo");

        for (int n = 1; n <= meses; n++)
        {
            decimal interes = saldo * tasaMensual;
            decimal abonoCapital = cuota - interes;
            saldo -= abonoCapital;

            if (n == meses)
                saldo = 0;

            tabla.AddRow(
                n.ToString(),
                cuota.ToString("N2"),
                interes.ToString("N2"),
                abonoCapital.ToString("N2"),
                saldo.ToString("N2")
            );
        }

        AnsiConsole.Write(tabla);
    }

    // ===============================
    // MÉTODO 5 - Mensaje Final
    // ===============================
    static void MostrarMensajeFinal()
    {
        AnsiConsole.MarkupLine("\n[bold green]Cálculo finalizado correctamente.[/]");
    }
}