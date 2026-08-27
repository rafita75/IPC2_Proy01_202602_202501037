using System;

namespace Proyecto1
{
    class Program
    {
        static void Main(string[] args)
        {
            // instacia de estructuras principales vacías
            ListaCiudad ciudadesDelSistema = new ListaCiudad();
            ListaRobot robotsDelSistema = new ListaRobot();

            // le pasamos las listas al lector para rellenar
            LectorXML lector = new LectorXML(ciudadesDelSistema, robotsDelSistema);

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SISTEMA DE CHAPÍN WARRIORS, S.A.    ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Cargar archivo de configuración XML");
                Console.WriteLine("2. Ver datos en memoria (Prueba Fase 1 y 2)");
                Console.WriteLine("3. Salir");
                Console.WriteLine("========================================");
                Console.Write("Ingrese una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese la ruta del archivo XML: ");
                        string ruta = Console.ReadLine();
                        ruta = ruta.Replace("\"", "");
                        lector.CargarArchivo(ruta);
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.WriteLine("\n--- CIUDADES CARGADAS ---");
                        Console.WriteLine("Total de ciudades: " + ciudadesDelSistema.GetContador());
                        NodoCiudad actualCiudad = ciudadesDelSistema.GetCabeza();
                        while (actualCiudad != null)
                        {
                            Console.WriteLine("- Nombre: " + actualCiudad.GetDato().GetNombre() +
                                              " | Tamaño: " + actualCiudad.GetDato().GetCantidadFilas() +
                                              "x" + actualCiudad.GetDato().GetCantidadColumnas());
                            actualCiudad = actualCiudad.GetSiguiente();
                        }

                        Console.WriteLine("\n--- ROBOTS CARGADOS ---");
                        Console.WriteLine("Total de robots: " + robotsDelSistema.GetContador());
                        NodoRobot actualRobot = robotsDelSistema.GetCabeza();
                        while (actualRobot != null)
                        {
                            Console.WriteLine("- " + actualRobot.GetDato().GetNombre() +
                                              " | Tipo: " + actualRobot.GetDato().GetTipo() +
                                              " | Combate: " + actualRobot.GetDato().GetCapacidadCombate());
                            actualRobot = actualRobot.GetSiguiente();
                        }
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "3":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}