namespace Proyecto1
{
    public class ListaRobot
    {
        private NodoRobot cabeza;
        private int contador;

        public ListaRobot()
        {
            this.cabeza = null;
            this.contador = 0;
        }

        // Método para insertar al final de la lista
        public void Agregar(Robot nuevoRobot)
        {
            NodoRobot nuevoNodo = new NodoRobot(nuevoRobot);

            if (this.cabeza == null) 
            {
                this.cabeza = nuevoNodo;
            }
            else 
            {
                NodoRobot actual = this.cabeza;

                while (actual.GetSiguiente() != null)
                {
                    actual = actual.GetSiguiente();
                }
                // Enlazamos el último nodo con el nuevo
                actual.SetSiguiente(nuevoNodo);
            }
            this.contador++;
        }

        public NodoRobot GetCabeza() { return this.cabeza; }
        public int GetContador() { return this.contador; }
    }
}