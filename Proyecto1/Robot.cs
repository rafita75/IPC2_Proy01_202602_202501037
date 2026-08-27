namespace Proyecto1
{
    public class Robot
    {
        private string nombre;
        private string tipo; // "ChapinRescue" o "ChapinFighter"
        private int capacidadCombate; // Los Rescue tendrán 0, los Fighter > 0

        public Robot(string nombre, string tipo, int capacidadCombate)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.capacidadCombate = capacidadCombate;
        }

        public string GetNombre() { return this.nombre; }
        public void SetNombre(string nombre) { this.nombre = nombre; }

        public string GetTipo() { return this.tipo; }
        public void SetTipo(string tipo) { this.tipo = tipo; }

        public int GetCapacidadCombate() { return this.capacidadCombate; }
        public void SetCapacidadCombate(int capacidadCombate) { this.capacidadCombate = capacidadCombate; }
    }
}