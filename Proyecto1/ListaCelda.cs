namespace Proyecto1
{
    public class ListaCelda
    {
        private NodoCelda cabeza;
        private int contador;

        public ListaCelda()
        {
            this.cabeza = null;
            this.contador = 0;
        }

        public void Agregar(Celda nuevaCelda)
        {
            NodoCelda nuevoNodo = new NodoCelda(nuevaCelda);

            if (this.cabeza == null)
            {
                this.cabeza = nuevoNodo;
            }
            else
            {
                NodoCelda actual = this.cabeza;
                while (actual.GetSiguiente() != null)
                {
                    actual = actual.GetSiguiente();
                }
                actual.SetSiguiente(nuevoNodo);
            }
            this.contador++;
        }

        // Método: Sirve para buscar una celda específica en esta fila
        public Celda ObtenerEnColumna(int indiceColumna)
        {
            NodoCelda actual = this.cabeza;
            int posicionActual = 1; // Asumiendo que el XML empieza las columnas en 1

            while (actual != null)
            {
                if (posicionActual == indiceColumna)
                {
                    return actual.GetDato();
                }
                actual = actual.GetSiguiente();
                posicionActual++;
            }
            return null; 
        }

        public NodoCelda GetCabeza() { return this.cabeza; }
        public int GetContador() { return this.contador; }
    }
}