namespace Lavadero.Modelos
{
    public class Dueno
    {
        public int IdDueno { get; set; }
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Domicilio { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string NombreLocalidad { get; set; } // Para mostrar
    }
}