namespace Proyecto1
{
    public class MotorMisiones
    {
        private bool FueVisitada(ListaCelda visitados, Celda celda)
        {
            NodoCelda actual = visitados.GetCabeza();
            while (actual != null)
            {
                if (actual.GetDato() == celda) return true;
                actual = actual.GetSiguiente();
            }
            return false;
        }

        //Ejecuta la misión y devuelve el estado final (rita)
        public EstadoMision EjecutarMision(Ciudad ciudad, Robot robot, char tipoObjetivo, int filaDestino, int colDestino)
        {
            ListaMatriz mapa = ciudad.GetMalla();
            Cola frontera = new Cola(); // Nodos por explorar
            ListaCelda visitados = new ListaCelda(); // Nodos ya explorados para no caminar en círculos

            //Buscar un Punto de Entrada ('E') en el mapa
            Celda inicio = null;
            for (int f = 1; f <= ciudad.GetCantidadFilas(); f++)
            {
                for (int c = 1; c <= ciudad.GetCantidadColumnas(); c++)
                {
                    Celda temp = mapa.ObtenerCelda(f, c);
                    if (temp != null && temp.GetTipoTerreno() == 'E')
                    {
                        inicio = temp;
                        break;
                    }
                }
                if (inicio != null) break;
            }

            if (inicio == null) return null; // Si el mapa no tiene entrada, es imposible

            // Preparamos el primer paso
            EstadoMision estadoInicial = new EstadoMision(inicio, null, robot.GetCapacidadCombate());
            frontera.Encolar(estadoInicial);
            visitados.Agregar(inicio);

            // Vectores de movimiento: Arriba, Abajo, Izquierda, Derecha
            int[] movFila = { -1, 1, 0, 0 };
            int[] movCol = { 0, 0, -1, 1 };

            // Algoritmo BFS
            while (!frontera.EstaVacia())
            {
                EstadoMision actual = frontera.Desencolar();
                Celda celdaActual = actual.GetCeldaActual();

                if (celdaActual.GetFila() == filaDestino && celdaActual.GetColumna() == colDestino)
                {
                    return actual; // Retornamos este estado
                }

                // Explorar los 4 vecinos (Arriba, Abajo, Izquierda, Derecha)
                for (int i = 0; i < 4; i++)
                {
                    int nuevaFila = celdaActual.GetFila() + movFila[i];
                    int nuevaCol = celdaActual.GetColumna() + movCol[i];

                    // Usamos nuestra ListaMatriz para obtener el vecino (devuelve null si nos salimos del mapa)
                    Celda vecino = mapa.ObtenerCelda(nuevaFila, nuevaCol);

                    if (vecino != null && !FueVisitada(visitados, vecino))
                    {
                        char terrenoVecino = vecino.GetTipoTerreno();
                        int capRestante = actual.GetCapacidadRestante();
                        bool puedePasar = false;

                        if (terrenoVecino == '*')
                        {
                            puedePasar = false; // Intransitable
                        }
                        else if (terrenoVecino == 'R' && tipoObjetivo != 'R')
                        {
                            puedePasar = false; // Un recurso no puede ser transitado si no es nuestro destino
                        }
                        else if (terrenoVecino == 'C' || terrenoVecino == 'E' || terrenoVecino == ' ' || (terrenoVecino == 'R' && tipoObjetivo == 'R'))
                        {
                            puedePasar = true; // Caminos libres, entradas y civiles son transitables
                        }
                        else if (terrenoVecino == 'M')
                        {
                            if (robot.GetTipo() == "ChapinFighter")
                            {
                                if (capRestante > vecino.GetCapacidadMilitar())
                                {
                                    puedePasar = true;
                                    capRestante -= vecino.GetCapacidadMilitar(); // Disminuye la capacidad al derrotarlo
                                }
                            }
                            // Si es ChapinRescue, la variable 'puedePasar' se queda en false (no puede enfrentar militares)
                        }

                        // Si sobrevivió a las evaluaciones, lo encolamos para visitarlo
                        if (puedePasar)
                        {
                            visitados.Agregar(vecino);
                            EstadoMision nuevoEstado = new EstadoMision(vecino, actual, capRestante);
                            frontera.Encolar(nuevoEstado);
                        }
                    }
                }
            }

            // Si la cola se vació y nunca retornamos victoria, la ruta está bloqueada
            return null;
        }
    }
}
