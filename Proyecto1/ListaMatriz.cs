namespace Proyecto1
{
    public class ListaMatriz
    {
        private NodoFila cabeza; // La primera fila de hasta arriba

        public ListaMatriz()
        {
            this.cabeza = null;
        }

        public void AgregarFila(ListaCelda nuevaFila)
        {
            NodoFila nuevoNodo = new NodoFila(nuevaFila);

            if (this.cabeza == null)
            {
                this.cabeza = nuevoNodo;
            }
            else
            {
                NodoFila actual = this.cabeza;
                while (actual.GetSiguiente() != null)
                {
                    actual = actual.GetSiguiente();
                }
                actual.SetSiguiente(nuevoNodo);
            }
        }

        // Método: Obtener una celda por su (fila, columna)
        public Celda ObtenerCelda(int indiceFila, int indiceColumna)
        {
            NodoFila actual = this.cabeza;
            int posicionFila = 1;

            while (actual != null)
            {
                if (posicionFila == indiceFila)
                {
                    // Encontramos la fila, ahora buscamos la columna dentro de esa fila
                    return actual.GetFila().ObtenerEnColumna(indiceColumna);
                }
                actual = actual.GetSiguiente();
                posicionFila++;
            }
            return null; // Si la coordenada no existe
        }
    }
}