using Lavadero.Modelos;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Lavadero.Controladores
{
    public class TurnoController
    {
        private Conexion conexion = Conexion.Instancia;

        public List<Turno> ObtenerTodos()
        {
            List<Turno> lista = new List<Turno>();
            try
            {
                conexion.Abrir();
                string query = @"SELECT t.*, CONCAT(a.marca, ' ', a.modelo, ' - ', a.patente) as info 
                               FROM turnos t 
                               INNER JOIN autos a ON t.patente = a.patente 
                               ORDER BY t.fecha DESC, t.hora DESC";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Turno
                    {
                        IdTurno = reader.GetInt32("idTurno"),
                        Fecha = reader.GetDateTime("fecha"),
                        Hora = reader.GetTimeSpan("hora"),
                        Patente = reader.GetString("patente"),
                        TipoLavado = reader.GetString("tipoLavado"),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) ? "" : reader.GetString("descripcion"),
                        Monto = reader.GetDecimal("monto"),
                        InfoAuto = reader.GetString("info")
                    });
                }
                reader.Close();
                conexion.Cerrar();
            }
            catch { }
            return lista;
        }

        public bool Agregar(Turno turno)
        {
            try
            {
                conexion.Abrir();
                string query = "INSERT INTO turnos (fecha, hora, patente, tipoLavado, descripcion, monto) VALUES (@fecha, @hora, @patente, @tipo, @desc, @monto)";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@fecha", turno.Fecha);
                cmd.Parameters.AddWithValue("@hora", turno.Hora);
                cmd.Parameters.AddWithValue("@patente", turno.Patente);
                cmd.Parameters.AddWithValue("@tipo", turno.TipoLavado);
                cmd.Parameters.AddWithValue("@desc", turno.Descripcion);
                cmd.Parameters.AddWithValue("@monto", turno.Monto);
                cmd.ExecuteNonQuery();
                conexion.Cerrar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                conexion.Abrir();
                string query = "DELETE FROM turnos WHERE idTurno=@id";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conexion.Cerrar();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}