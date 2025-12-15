using Lavadero.Modelos;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Lavadero.Controladores
{
    public class AutoController
    {
        private Conexion conexion = new Conexion();

        public List<Auto> ObtenerTodos()
        {
            List<Auto> lista = new List<Auto>();
            try
            {
                conexion.Abrir();
                string query = @"SELECT a.*, CONCAT(d.apellido, ', ', d.nombres) as dueno 
                               FROM autos a 
                               INNER JOIN duenos d ON a.idDueno = d.idDueno";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Auto
                    {
                        Patente = reader.GetString("patente"),
                        Marca = reader.GetString("marca"),
                        Modelo = reader.GetString("modelo"),
                        Anio = reader.GetInt32("anio"),
                        IdDueno = reader.GetInt32("idDueno"),
                        NombreDueno = reader.GetString("dueno")
                    });
                }
                reader.Close();
                conexion.Cerrar();
            }
            catch { }
            return lista;
        }

        public bool Agregar(Auto auto)
        {
            try
            {
                conexion.Abrir();
                string query = "INSERT INTO autos VALUES (@patente, @marca, @modelo, @anio, @idDueno)";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@patente", auto.Patente);
                cmd.Parameters.AddWithValue("@marca", auto.Marca);
                cmd.Parameters.AddWithValue("@modelo", auto.Modelo);
                cmd.Parameters.AddWithValue("@anio", auto.Anio);
                cmd.Parameters.AddWithValue("@idDueno", auto.IdDueno);
                cmd.ExecuteNonQuery();
                conexion.Cerrar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Modificar(Auto auto)
        {
            try
            {
                conexion.Abrir();
                string query = "UPDATE autos SET marca=@marca, modelo=@modelo, anio=@anio, idDueno=@idDueno WHERE patente=@patente";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@patente", auto.Patente);
                cmd.Parameters.AddWithValue("@marca", auto.Marca);
                cmd.Parameters.AddWithValue("@modelo", auto.Modelo);
                cmd.Parameters.AddWithValue("@anio", auto.Anio);
                cmd.Parameters.AddWithValue("@idDueno", auto.IdDueno);
                cmd.ExecuteNonQuery();
                conexion.Cerrar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(string patente)
        {
            try
            {
                conexion.Abrir();
                string query = "DELETE FROM autos WHERE patente=@patente";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@patente", patente);
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