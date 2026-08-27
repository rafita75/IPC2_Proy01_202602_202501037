using System;
using System.IO;
using System.Diagnostics;

namespace Proyecto1
{
    public class GeneradorGraphviz
    {
        private ListaCelda ObtenerRuta(EstadoMision estadoFinal)
        {
            ListaCelda ruta = new ListaCelda();
            EstadoMision actual = estadoFinal;

            while (actual != null)
            {
                ruta.Agregar(actual.GetCeldaActual());
                actual = actual.GetPasoAnterior(); // Retrocedemos un paso
            }
            return ruta;
        }

        // Verifica si una celda específica forma parte de la ruta ganadora
        private bool EsParteDeLaRuta(ListaCelda ruta, Celda celda)
        {
            NodoCelda actual = ruta.GetCabeza();
            while (actual != null)
            {
                if (actual.GetDato() == celda) return true;
                actual = actual.GetSiguiente();
            }
            return false;
        }

        public void GenerarMapa(Ciudad ciudad, EstadoMision estadoFinal, string tipoMision, Robot robot)
        {
            try
            {
                ListaCelda rutaGanadora = new ListaCelda();
                if (estadoFinal != null)
                {
                    rutaGanadora = ObtenerRuta(estadoFinal);
                }

                string dotContent = "digraph G {\n";
                dotContent += "  node [shape=box, style=filled, width=0.5, height=0.5, fontcolor=white];\n";
                dotContent += "  edge [style=invis];\n"; 
                dotContent += "  rankdir=TB;\n";

                // Título superior
                dotContent += $"  labelloc=\"t\";\n";
                dotContent += $"  label=\"Ruta de {tipoMision}\\n\";\n";

                int filas = ciudad.GetCantidadFilas();
                int columnas = ciudad.GetCantidadColumnas();
                ListaMatriz mapa = ciudad.GetMalla();

                // nodos con su color respectivo
                for (int f = 1; f <= filas; f++)
                {
                    dotContent += "  { rank=same; ";
                    for (int c = 1; c <= columnas; c++)
                    {
                        Celda celda = mapa.ObtenerCelda(f, c);
                        string nombreNodo = $"nodo_{f}_{c}";
                        string color = "white"; // Por defecto

                        if (celda.GetTipoTerreno() == '*') color = "black";
                        else if (celda.GetTipoTerreno() == 'E') color = "#4CAF50"; // Verde
                        else if (celda.GetTipoTerreno() == 'C') color = "#2196F3"; // Azul
                        else if (celda.GetTipoTerreno() == 'M') color = "#F44336"; // Rojo
                        else if (celda.GetTipoTerreno() == 'R') color = "gray";

                        if (estadoFinal != null && EsParteDeLaRuta(rutaGanadora, celda))
                        {
                            char tipoTerreno = celda.GetTipoTerreno();

                            if (tipoTerreno != 'E' && tipoTerreno != 'C' && tipoTerreno != 'R' && tipoTerreno != 'M')
                            {
                                color = "#FFE082"; // Amarillo ruta
                            }
                        }

                        dotContent += $"{nombreNodo} [fillcolor=\"{color}\", label=\"\"]; ";
                    }
                    dotContent += "}\n";
                }

                // Conectar las filas para que Graphviz arme la cuadrícula
                for (int f = 1; f < filas; f++)
                {
                    for (int c = 1; c <= columnas; c++)
                    {
                        dotContent += $"  nodo_{f}_{c} -> nodo_{f + 1}_{c};\n";
                    }
                }

                // Conectar las columnas para mantener la alineación horizontal
                for (int f = 1; f <= filas; f++)
                {
                    for (int c = 1; c < columnas; c++)
                    {
                        dotContent += $"  nodo_{f}_{c} -> nodo_{f}_{c + 1};\n";
                    }
                }

                string infoAbajo = $"Tipo de mision: {tipoMision}\\n";
                if (estadoFinal != null)
                {
                    infoAbajo += $"Objetivo alcanzado en: {estadoFinal.GetCeldaActual().GetFila()},{estadoFinal.GetCeldaActual().GetColumna()}\\n";
                    if (robot.GetTipo() == "ChapinFighter")
                    {
                        infoAbajo += $"Robot utilizado: {robot.GetNombre()} (ChapinFighter Capacidad inicial {robot.GetCapacidadCombate()}, Capacidad final {estadoFinal.GetCapacidadRestante()})";
                    }
                    else
                    {
                        infoAbajo += $"Robot utilizado: {robot.GetNombre()} (ChapinRescue)";
                    }
                }
                else
                {
                    infoAbajo += "Mision Imposible";
                }

                dotContent += $"  labelloc=\"b\";\n";
                dotContent += $"  label=\"{infoAbajo}\";\n";
                dotContent += "}\n";

                string rutaDot = "mapa.dot";
                File.WriteAllText(rutaDot, dotContent);

                // Ejecutar Graphviz para convertir a PNG
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "dot";
                startInfo.Arguments = $"-Tpng {rutaDot} -o mapa.png";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                }

                // Abrir la imagen automáticamente
                Process.Start(new ProcessStartInfo("mapa.png") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al generar la imagen (¿Tienes instalado Graphviz en tu PC y agregado a las variables de entorno?): " + ex.Message);
            }
        }
    }
}
