namespace Proyecto1
{
    public class NodoFila
    {
        private ListaCelda fila; // El dato ahora es una lista entera de celdas
        private NodoFila siguiente; // Apunta a la fila de abajo

        public NodoFila(ListaCelda fila)
        {
            this.fila = fila;
            this.siguiente = null;
        }

        public ListaCelda GetFila() { return this.fila; }
        public void SetFila(ListaCelda fila) { this.fila = fila; }

        public NodoFila GetSiguiente() { return this.siguiente; }
        public void SetSiguiente(NodoFila siguiente) { this.siguiente = siguiente; }
    }
}