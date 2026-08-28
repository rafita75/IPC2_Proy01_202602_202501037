using System;
using System.Xml; 

namespace Proyecto1
{
    public class LectorXML
    {
        private ListaCiudad ciudadesCargadas;
        private ListaRobot robotsCargados;

        public LectorXML(ListaCiudad ciudades, ListaRobot robots)
        {
            this.ciudadesCargadas = ciudades;
            this.robotsCargados = robots;
        }

        public void CargarArchivo(string rutaArchivo)
        {
            try
            {
                XmlDocument documento = new XmlDocument();
                documento.Load(rutaArchivo);

                // CARGAR O ACTUALIZAR ROBOTS
                XmlNodeList nodosRobot = documento.SelectNodes("//robots/robot");
                if (nodosRobot != null)
                {
                    foreach (XmlNode nodo in nodosRobot)
                    {
                        XmlNode nodoNombre = nodo.SelectSingleNode("nombre");
                        string nombre = nodoNombre.InnerText.Trim();
                        string tipo = nodoNombre.Attributes["tipo"].Value;

                        int capacidad = 0;
                        if (tipo == "ChapinFighter" && nodoNombre.Attributes["capacidad"] != null)
                        {
                            capacidad = int.Parse(nodoNombre.Attributes["capacidad"].Value);
                        }

                        Robot nuevoRobot = new Robot(nombre, tipo, capacidad);

                        NodoRobot actualR = this.robotsCargados.GetCabeza();
                        bool robotActualizado = false;
                        while (actualR != null)
                        {
                            if (actualR.GetDato().GetNombre() == nombre)
                            {
                                actualR.SetDato(nuevoRobot); // Sobreescribe el objeto en memoria
                                robotActualizado = true;
                                break;
                            }
                            actualR = actualR.GetSiguiente();
                        }

                        if (!robotActualizado)
                        {
                            this.robotsCargados.Agregar(nuevoRobot); // Es nuevo, lo agrega al final
                        }
                    }
                }

                // CARGAR O ACTUALIZAR CIUDADES
                XmlNodeList nodosCiudad = documento.SelectNodes("//listaCiudades/ciudad");
                if (nodosCiudad != null)
                {
                    foreach (XmlNode nodoCiudad in nodosCiudad)
                    {
                        XmlNode nodoNombre = nodoCiudad.SelectSingleNode("nombre");
                        string nombreCiudad = nodoNombre.InnerText.Trim();
                        int filas = int.Parse(nodoNombre.Attributes["filas"].Value);
                        int columnas = int.Parse(nodoNombre.Attributes["columnas"].Value);

                        Ciudad nuevaCiudad = new Ciudad(nombreCiudad, filas, columnas);

                        XmlNodeList nodosFila = nodoCiudad.SelectNodes("fila");
                        foreach (XmlNode nodoFila in nodosFila)
                        {
                            int numeroFila = int.Parse(nodoFila.Attributes["numero"].Value);
                            string contenidoFila = nodoFila.InnerText.Trim().Replace("\"", "");

                            ListaCelda nuevaListaFila = new ListaCelda();
                            for (int c = 0; c < contenidoFila.Length; c++)
                            {
                                char tipoTerreno = contenidoFila[c];
                                Celda nuevaCelda = new Celda(numeroFila, c + 1, tipoTerreno);
                                nuevaListaFila.Agregar(nuevaCelda);
                            }
                            nuevaCiudad.GetMalla().AgregarFila(nuevaListaFila);
                        }

                        XmlNodeList nodosMilitares = nodoCiudad.SelectNodes("unidadMilitar");
                        if (nodosMilitares != null)
                        {
                            foreach (XmlNode nodoMilitar in nodosMilitares)
                            {
                                int filaMilitar = int.Parse(nodoMilitar.Attributes["fila"].Value);
                                int columnaMilitar = int.Parse(nodoMilitar.Attributes["columna"].Value);
                                int capacidadCombate = int.Parse(nodoMilitar.InnerText.Trim());

                                Celda celdaObjetivo = nuevaCiudad.GetMalla().ObtenerCelda(filaMilitar, columnaMilitar);
                                if (celdaObjetivo != null)
                                {
                                    celdaObjetivo.SetTipoTerreno('M');
                                    celdaObjetivo.SetCapacidadMilitar(capacidadCombate);
                                }
                            }
                        }

                        NodoCiudad actualC = this.ciudadesCargadas.GetCabeza();
                        bool ciudadActualizada = false;
                        while (actualC != null)
                        {
                            if (actualC.GetDato().GetNombre() == nombreCiudad)
                            {
                                actualC.SetDato(nuevaCiudad); // Sobreescribe la ciudad completa con su nuevo mapa
                                ciudadActualizada = true;
                                break;
                            }
                            actualC = actualC.GetSiguiente();
                        }

                        if (!ciudadActualizada)
                        {
                            this.ciudadesCargadas.Agregar(nuevaCiudad); // Es nueva, la agrega al final
                        }
                    }
                }

                Console.WriteLine("\n¡Archivo procesado exitosamente (Nuevos registros agregados, existentes actualizados)!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError al procesar el archivo XML: " + ex.Message);
            }
        }
    }
}