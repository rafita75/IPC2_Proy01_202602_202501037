namespace Proyecto1
{
    public class NodoRobot
    {
        private Robot dato;
        private NodoRobot siguiente;

        public NodoRobot(Robot dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }

        public Robot GetDato() { return this.dato; }
        public void SetDato(Robot dato) { this.dato = dato; }

        public NodoRobot GetSiguiente() { return this.siguiente; }
        public void SetSiguiente(NodoRobot siguiente) { this.siguiente = siguiente; }
    }
}