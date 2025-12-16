namespace Lavadero.Modelos
{
    public class Dueno
    {
        public int IdDueno { get; set; }
        public required string Dni { get; set; }
        public required string Apellido { get; set; }
        public required string Nombres { get; set; }
        public required string Domicilio { get; set; }
        public required string CodigoPostal { get; set; }
        public required string Telefono { get; set; }
        public required string NombreLocalidad { get; set; } // Para mostrar
    }
}