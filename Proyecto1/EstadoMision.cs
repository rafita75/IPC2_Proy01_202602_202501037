namespace Proyecto1
{
    public class EstadoMision
    {
        private Celda celdaActual;
        private EstadoMision pasoAnterior; // El "Padre", guarda de dónde venimos
        private int capacidadRestante; // Vital para calcular el desgaste de los ChapinFighter

        public EstadoMision(Celda celdaActual, EstadoMision pasoAnterior, int capacidadRestante)
        {
            this.celdaActual = celdaActual;
            this.pasoAnterior = pasoAnterior;
            this.capacidadRestante = capacidadRestante;
        }

        public Celda GetCeldaActual() { return this.celdaActual; }
        public EstadoMision GetPasoAnterior() { return this.pasoAnterior; }
        public int GetCapacidadRestante() { return this.capacidadRestante; }
    }
}
