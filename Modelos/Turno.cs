using System;

namespace Lavadero.Modelos
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Patente { get; set; } = string.Empty;
        public string TipoLavado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string InfoAuto { get; set; } = string.Empty; // Para mostrar
    }
}