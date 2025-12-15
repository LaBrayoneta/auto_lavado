namespace Lavadero.Modelos
{
    public class Auto
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public int IdDueno { get; set; }
        public string NombreDueno { get; set; } // Para mostrar
    }
}