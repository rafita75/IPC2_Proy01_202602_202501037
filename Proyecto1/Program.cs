using System;

namespace Proyecto1
{
    class Program
    {
        static void Main(string[] args)
        {
            ListaCiudad ciudadesDelSistema = new ListaCiudad();
            ListaRobot robotsDelSistema = new ListaRobot();
            LectorXML lector = new LectorXML(ciudadesDelSistema, robotsDelSistema);
            MotorMisiones motor = new MotorMisiones();

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SISTEMA DE CHAPÍN WARRIORS, S.A.    ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Cargar archivo de configuración XML");
                Console.WriteLine("2. Ejecutar Misión");
                Console.WriteLine("3. Salir");
                Console.WriteLine("========================================");
                Console.Write("Ingrese una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("\nIngrese la ruta del archivo XML: ");
                        string ruta = Console.ReadLine().Replace("\"", "");
                        lector.CargarArchivo(ruta);
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "2":
                        EjecutarMenuMisiones(ciudadesDelSistema, robotsDelSistema, motor);
                        break;

                    case "3":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void EjecutarMenuMisiones(ListaCiudad ciudades, ListaRobot robots, MotorMisiones motor)
        {
            if (ciudades.GetContador() == 0 || robots.GetContador() == 0)
            {
                Console.WriteLine("\nError: Debe cargar un archivo XML primero.");
                Console.ReadKey();
                return;
            }

            // SELECCIÓN DE CIUDAD
            Console.WriteLine("\n--- CIUDADES DISPONIBLES ---");
            NodoCiudad actualCiudad = ciudades.GetCabeza();
            while (actualCiudad != null)
            {
                Console.WriteLine("- " + actualCiudad.GetDato().GetNombre());
                actualCiudad = actualCiudad.GetSiguiente();
            }
            Console.Write("\nEscriba el nombre de la ciudad objetivo: ");
            string nombreCiudad = Console.ReadLine();
            Ciudad ciudadSeleccionada = ciudades.BuscarCiudad(nombreCiudad);

            if (ciudadSeleccionada == null)
            {
                Console.WriteLine("Ciudad no encontrada.");
                Console.ReadKey();
                return;
            }

            //SELECCIÓN DE TIPO DE MISIÓN
            Console.WriteLine("\n--- TIPO DE MISIÓN ---");
            Console.WriteLine("1. Misión de rescate");
            Console.WriteLine("2. Misión de extracción de recursos");
            Console.Write("Seleccione una opción (1-2): ");
            string tipoOp = Console.ReadLine();
            char tipoObjetivo = tipoOp == "1" ? 'C' : (tipoOp == "2" ? 'R' : ' ');

            if (tipoObjetivo == ' ') return;

            string tipoRobotRequerido = tipoObjetivo == 'C' ? "ChapinRescue" : "ChapinFighter";

            //SELECCIÓN DE ROBOT
            Console.WriteLine($"\n--- ROBOTS {tipoRobotRequerido.ToUpper()} DISPONIBLES ---");
            NodoRobot actualRobot = robots.GetCabeza();
            bool hayRobots = false;

            while (actualRobot != null)
            {
                Robot r = actualRobot.GetDato();
                if (r.GetTipo() == tipoRobotRequerido)
                {
                    Console.WriteLine($"- Nombre: {r.GetNombre()} | Combate: {r.GetCapacidadCombate()}");
                    hayRobots = true;
                }
                actualRobot = actualRobot.GetSiguiente();
            }

            if (!hayRobots)
            {
                Console.WriteLine($"No hay robots {tipoRobotRequerido} disponibles. Misión no realizable.");
                Console.ReadKey();
                return;
            }

            Console.Write("\nEscriba el nombre del robot a utilizar: ");
            string nombreRobot = Console.ReadLine();
            Robot robotSeleccionado = null;

            
            actualRobot = robots.GetCabeza();
            while (actualRobot != null)
            {
                if (actualRobot.GetDato().GetNombre() == nombreRobot)
                {
                    robotSeleccionado = actualRobot.GetDato();
                    break;
                }
                actualRobot = actualRobot.GetSiguiente();
            }

            if (robotSeleccionado == null || robotSeleccionado.GetTipo() != tipoRobotRequerido)
            {
                Console.WriteLine("Robot inválido para esta misión.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("\n--- ESCANEANDO OBJETIVOS EN EL MAPA ---");

            ListaCelda objetivosEncontrados = new ListaCelda();

            for (int f = 1; f <= ciudadSeleccionada.GetCantidadFilas(); f++)
            {
                for (int c = 1; c <= ciudadSeleccionada.GetCantidadColumnas(); c++)
                {
                    Celda celdaTemp = ciudadSeleccionada.GetMalla().ObtenerCelda(f, c);
                    if (celdaTemp != null && celdaTemp.GetTipoTerreno() == tipoObjetivo)
                    {
                        objetivosEncontrados.Agregar(celdaTemp);
                    }
                }
            }

            int cantidadObjetivos = objetivosEncontrados.GetContador();
            int filaObj = -1;
            int colObj = -1;

            if (cantidadObjetivos == 0)
            {
                string nombreObj = tipoObjetivo == 'C' ? "civiles" : "recursos";
                Console.WriteLine($"\nError: No hay {nombreObj} en la ciudad. Misión abortada.");
                Console.ReadKey();
                return;
            }
            else if (cantidadObjetivos == 1)
            {
                Celda unico = objetivosEncontrados.GetCabeza().GetDato();
                filaObj = unico.GetFila();
                colObj = unico.GetColumna();
                Console.WriteLine($"\nSolo se encontró 1 objetivo en la Fila {filaObj}, Columna {colObj}.");
                Console.WriteLine("¡Objetivo asignado automáticamente por el sistema!");
            }
            else
            {
                Console.WriteLine($"\nSe encontraron {cantidadObjetivos} objetivos disponibles:");
                NodoCelda actualObj = objetivosEncontrados.GetCabeza();
                int indice = 1;
                while (actualObj != null)
                {
                    Celda c = actualObj.GetDato();
                    Console.WriteLine($"[{indice}] -> Fila: {c.GetFila()}, Columna: {c.GetColumna()}");
                    actualObj = actualObj.GetSiguiente();
                    indice++;
                }

                string accion = tipoObjetivo == 'C' ? "rescatar" : "extraer";
                Console.Write($"\nSeleccione el número del objetivo que desea {accion} (1-{cantidadObjetivos}): ");
                int seleccion = int.Parse(Console.ReadLine());

                // Buscamos el objetivo seleccionado iterando la lista temporal
                NodoCelda nodoElegido = objetivosEncontrados.GetCabeza();
                for (int i = 1; i < seleccion; i++)
                {
                    if (nodoElegido != null) nodoElegido = nodoElegido.GetSiguiente();
                }

                if (nodoElegido != null)
                {
                    filaObj = nodoElegido.GetDato().GetFila();
                    colObj = nodoElegido.GetDato().GetColumna();
                }
                else
                {
                    Console.WriteLine("Selección inválida.");
                    Console.ReadKey();
                    return;
                }
            }

            // EJECUCIÓN DEL ALGORITMO
            Console.WriteLine("\nCalculando ruta estratégica...");
            EstadoMision resultado = motor.EjecutarMision(ciudadSeleccionada, robotSeleccionado, tipoObjetivo, filaObj, colObj);

            if (resultado == null)
            {
                Console.WriteLine("\nMisión Imposible");
            }
            else
            {
                Console.WriteLine("\n¡Misión Exitosa!");
                Console.WriteLine($"Capacidad final del robot: {resultado.GetCapacidadRestante()}");

                //GENERAR REPORTE GRÁFICO
                GeneradorGraphviz graficador = new GeneradorGraphviz();
                string nombreMision = tipoObjetivo == 'C' ? "rescate" : "extracción de recursos";

                graficador.GenerarMapa(ciudadSeleccionada, resultado, nombreMision, robotSeleccionado);
                Console.WriteLine("\nSe ha generado y abierto el mapa de la misión automáticamente.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}