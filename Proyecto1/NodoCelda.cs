namespace Proyecto1
{
    public class NodoCelda
    {
        private Celda dato;
        private NodoCelda siguiente;

        public NodoCelda(Celda dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }

        public Celda GetDato() { return this.dato; }
        public void SetDato(Celda dato) { this.dato = dato; }

        public NodoCelda GetSiguiente() { return this.siguiente; }
        public void SetSiguiente(NodoCelda siguiente) { this.siguiente = siguiente; }
    }
}