namespace Proyecto1
{
    public class NodoCiudad
    {
        private Ciudad dato;
        private NodoCiudad siguiente;

        public NodoCiudad(Ciudad dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }

        public Ciudad GetDato() { return this.dato; }
        public void SetDato(Ciudad dato) { this.dato = dato; }

        public NodoCiudad GetSiguiente() { return this.siguiente; }
        public void SetSiguiente(NodoCiudad siguiente) { this.siguiente = siguiente; }
    }
}