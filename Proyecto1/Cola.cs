namespace Proyecto1
{
    public class Cola
    {
        private NodoCola frente;
        private NodoCola final;

        public Cola()
        {
            this.frente = null;
            this.final = null;
        }

        public void Encolar(EstadoMision dato)
        {
            NodoCola nuevo = new NodoCola(dato);

            if (this.frente == null) 
            {
                this.frente = nuevo;
                this.final = nuevo;
            }
            else 
            {
                this.final.SetSiguiente(nuevo);
                this.final = nuevo;
            }
        }

        public EstadoMision Desencolar()
        {
            if (this.frente == null)
            {
                return null;
            }

            EstadoMision extraido = this.frente.GetDato();

            this.frente = this.frente.GetSiguiente();

            if (this.frente == null)
            {
                this.final = null;
            }

            return extraido;
        }

        public bool EstaVacia()
        {
            return this.frente == null;
        }
    }
}
