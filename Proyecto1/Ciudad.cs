namespace Proyecto1
{
    public class Ciudad
    {
        private string nombre;
        private int cantidadFilas;
        private int cantidadColumnas;
        private ListaMatriz malla; // El mapa de la ciudad

        public Ciudad(string nombre, int cantidadFilas, int cantidadColumnas)
        {
            this.nombre = nombre;
            this.cantidadFilas = cantidadFilas;
            this.cantidadColumnas = cantidadColumnas;
            this.malla = new ListaMatriz(); // Inicializamos el mapa vacío
        }

        public string GetNombre() { return this.nombre; }
        public int GetCantidadFilas() { return this.cantidadFilas; }
        public int GetCantidadColumnas() { return this.cantidadColumnas; }

        public ListaMatriz GetMalla() { return this.malla; }
    }
}