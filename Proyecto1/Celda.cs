namespace Proyecto1
{
    public class Celda
    {
        private int fila;
        private int columna;
        private char tipoTerreno; // 'E', '*', ' ', 'C', 'R', 'M' 
        private int capacidadMilitar;

        public Celda(int fila, int columna, char tipoTerreno)
        {
            this.fila = fila;
            this.columna = columna;
            this.tipoTerreno = tipoTerreno;
            this.capacidadMilitar = 0; // Por defecto es 0, si es militar se actualiza luego
        }

        public int GetFila() { return this.fila; }
        public int GetColumna() { return this.columna; }

        public char GetTipoTerreno() { return this.tipoTerreno; }
        public void SetTipoTerreno(char tipo) { this.tipoTerreno = tipo; }

        public int GetCapacidadMilitar() { return this.capacidadMilitar; }
        public void SetCapacidadMilitar(int capacidad) { this.capacidadMilitar = capacidad; }
    }
}