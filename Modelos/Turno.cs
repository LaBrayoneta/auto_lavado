using System;

namespace Lavadero.Modelos
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Patente { get; set; }
        public string TipoLavado { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string InfoAuto { get; set; } // Para mostrar
    }
}