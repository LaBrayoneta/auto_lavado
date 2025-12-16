namespace Lavadero.Modelos
{
    public class Auto
    {
        public required string Patente { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public int Anio { get; set; }
        public int IdDueno { get; set; }
        public required string NombreDueno { get; set; } // Para mostrar
    }
}