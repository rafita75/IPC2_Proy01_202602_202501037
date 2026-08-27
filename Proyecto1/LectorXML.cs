using System;
using System.Xml; 

namespace Proyecto1
{
    public class LectorXML
    {
        // listas dinámicas
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

                //CARGAR ROBOTS
                XmlNodeList nodosRobot = documento.SelectNodes("//robots/robot");
                if (nodosRobot != null)
                {
                    foreach (XmlNode nodo in nodosRobot)
                    {
                        XmlNode nodoNombre = nodo.SelectSingleNode("nombre");
                        string nombre = nodoNombre.InnerText.Trim();
                        string tipo = nodoNombre.Attributes["tipo"].Value;

                        int capacidad = 0;
                        // Solo los ChapinFighter tienen atributo de capacidad
                        if (tipo == "ChapinFighter" && nodoNombre.Attributes["capacidad"] != null)
                        {
                            capacidad = int.Parse(nodoNombre.Attributes["capacidad"].Value);
                        }

                        Robot nuevoRobot = new Robot(nombre, tipo, capacidad);
                        this.robotsCargados.Agregar(nuevoRobot);
                    }
                }

                // CARGAR CIUDADES Y SU MAPA
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

                        // Cargar las filas y celdas
                        XmlNodeList nodosFila = nodoCiudad.SelectNodes("fila");
                        foreach (XmlNode nodoFila in nodosFila)
                        {
                            int numeroFila = int.Parse(nodoFila.Attributes["numero"].Value);
                            // Limpiamos las comillas 
                            string contenidoFila = nodoFila.InnerText.Trim().Replace("\"", "");

                            ListaCelda nuevaListaFila = new ListaCelda();

                            // Recorremos cada caracter (columna) 
                            for (int c = 0; c < contenidoFila.Length; c++)
                            {
                                char tipoTerreno = contenidoFila[c];
                                Celda nuevaCelda = new Celda(numeroFila, c + 1, tipoTerreno);
                                nuevaListaFila.Agregar(nuevaCelda);
                            }

                            // Agregamos la fila terminada a la matriz de la ciudad
                            nuevaCiudad.GetMalla().AgregarFila(nuevaListaFila);
                        }

                        //  actualizamos las celdas con Unidades Militares
                        XmlNodeList nodosMilitares = nodoCiudad.SelectNodes("unidadMilitar");
                        if (nodosMilitares != null)
                        {
                            foreach (XmlNode nodoMilitar in nodosMilitares)
                            {
                                int filaMilitar = int.Parse(nodoMilitar.Attributes["fila"].Value);
                                int columnaMilitar = int.Parse(nodoMilitar.Attributes["columna"].Value);
                                int capacidadCombate = int.Parse(nodoMilitar.InnerText.Trim());

                                // Buscamos la celda específica en nuestra matriz dinámica
                                Celda celdaObjetivo = nuevaCiudad.GetMalla().ObtenerCelda(filaMilitar, columnaMilitar);
                                if (celdaObjetivo != null)
                                {
                                    celdaObjetivo.SetTipoTerreno('M'); 
                                    celdaObjetivo.SetCapacidadMilitar(capacidadCombate);
                                }
                            }
                        }

                        // agregamos la ciudad completa a nuestra lista de ciudades
                        this.ciudadesCargadas.Agregar(nuevaCiudad);
                    }
                }

                Console.WriteLine("¡Archivo cargado y procesado exitosamente en memoria dinámica!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar el archivo XML: " + ex.Message);
            }
        }
    }
}