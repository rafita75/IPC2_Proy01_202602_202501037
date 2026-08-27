namespace Proyecto1
{
    public class ListaCiudad
    {
        private NodoCiudad cabeza;
        private int contador;

        public ListaCiudad()
        {
            this.cabeza = null;
            this.contador = 0;
        }

        public void Agregar(Ciudad nuevaCiudad)
        {
            NodoCiudad nuevoNodo = new NodoCiudad(nuevaCiudad);

            if (this.cabeza == null)
            {
                this.cabeza = nuevoNodo;
            }
            else
            {
                NodoCiudad actual = this.cabeza;
                while (actual.GetSiguiente() != null)
                {
                    actual = actual.GetSiguiente();
                }
                actual.SetSiguiente(nuevoNodo);
            }
            this.contador++;
        }

        // Método: Busca una ciudad por su nombre para cargarla al menú de misiones
        public Ciudad BuscarCiudad(string nombreBuscado)
        {
            NodoCiudad actual = this.cabeza;
            while (actual != null)
            {
                if (actual.GetDato().GetNombre() == nombreBuscado)
                {
                    return actual.GetDato(); // Retorna la ciudad si la encuentra
                }
                actual = actual.GetSiguiente();
            }
            return null; // Retorna null si la ciudad no existe en la lista
        }

        public NodoCiudad GetCabeza() { return this.cabeza; }
        public int GetContador() { return this.contador; }
    }
}