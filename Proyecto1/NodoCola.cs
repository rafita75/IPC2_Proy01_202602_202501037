namespace Proyecto1
{
    public class NodoCola
    {
        private EstadoMision dato;
        private NodoCola siguiente;

        public NodoCola(EstadoMision dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }

        public EstadoMision GetDato() { return this.dato; }
        public NodoCola GetSiguiente() { return this.siguiente; }
        public void SetSiguiente(NodoCola siguiente) { this.siguiente = siguiente; }
    }
}
