using Lavadero.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Lavadero.Controladores
{
    public class AutoController
    {
        private Conexion conexion = Conexion.Instancia;

        public List<Auto> ObtenerTodos()
        {
            List<Auto> lista = new List<Auto>();
            try
            {
                conexion.Abrir();
                string query = @"SELECT a.*, CONCAT(d.apellido, ', ', d.nombres) as dueno 
                               FROM autos a 
                               INNER JOIN duenos d ON a.idDueno = d.idDueno
                               ORDER BY a.patente";

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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ObtenerTodos: {ex.Message}");
                throw new Exception($"Error al obtener autos: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }
            return lista;
        }

        public bool Agregar(Auto auto)
        {
            try
            {
                // Validaciones
                if (!ValidarPatente(auto.Patente))
                {
                    throw new Exception("Formato de patente inválido. Use ABC123 o AB123CD");
                }

                if (!ValidarAnio(auto.Anio))
                {
                    throw new Exception($"Año inválido. Debe estar entre 1900 y {DateTime.Now.Year + 1}");
                }

                if (string.IsNullOrWhiteSpace(auto.Marca) || string.IsNullOrWhiteSpace(auto.Modelo))
                {
                    throw new Exception("Marca y modelo son obligatorios");
                }

                // Verificar si ya existe
                if (ExistePatente(auto.Patente))
                {
                    throw new Exception($"La patente {auto.Patente} ya está registrada");
                }

                conexion.Abrir();
                string query = "INSERT INTO autos (patente, marca, modelo, anio, idDueno) VALUES (@patente, @marca, @modelo, @anio, @idDueno)";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());

                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = auto.Patente.ToUpper();
                cmd.Parameters.Add("@marca", MySqlDbType.VarChar, 50).Value = auto.Marca;
                cmd.Parameters.Add("@modelo", MySqlDbType.VarChar, 50).Value = auto.Modelo;
                cmd.Parameters.Add("@anio", MySqlDbType.Int32).Value = auto.Anio;
                cmd.Parameters.Add("@idDueno", MySqlDbType.Int32).Value = auto.IdDueno;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error MySQL: {ex.Message}");
                throw new Exception($"Error de base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Agregar: {ex.Message}");
                throw;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public bool Modificar(Auto auto)
        {
            try
            {
                // Validaciones
                if (!ValidarAnio(auto.Anio))
                {
                    throw new Exception($"Año inválido. Debe estar entre 1900 y {DateTime.Now.Year + 1}");
                }

                if (string.IsNullOrWhiteSpace(auto.Marca) || string.IsNullOrWhiteSpace(auto.Modelo))
                {
                    throw new Exception("Marca y modelo son obligatorios");
                }

                conexion.Abrir();
                string query = @"UPDATE autos 
                               SET marca=@marca, modelo=@modelo, anio=@anio, idDueno=@idDueno 
                               WHERE patente=@patente";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = auto.Patente;
                cmd.Parameters.Add("@marca", MySqlDbType.VarChar, 50).Value = auto.Marca;
                cmd.Parameters.Add("@modelo", MySqlDbType.VarChar, 50).Value = auto.Modelo;
                cmd.Parameters.Add("@anio", MySqlDbType.Int32).Value = auto.Anio;
                cmd.Parameters.Add("@idDueno", MySqlDbType.Int32).Value = auto.IdDueno;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró el auto para modificar");
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Modificar: {ex.Message}");
                throw new Exception($"Error al modificar auto: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public bool Eliminar(string patente)
        {
            try
            {
                // Verificar si tiene turnos asociados
                if (TieneTurnos(patente))
                {
                    throw new Exception("No se puede eliminar el auto porque tiene turnos registrados");
                }

                conexion.Abrir();
                string query = "DELETE FROM autos WHERE patente=@patente";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = patente;

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    throw new Exception("No se encontró el auto para eliminar");
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Eliminar: {ex.Message}");
                throw new Exception($"Error al eliminar auto: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // Método para obtener un auto por patente
        public Auto? ObtenerPorPatente(string patente)
        {
            try
            {
                conexion.Abrir();
                string query = @"SELECT a.*, CONCAT(d.apellido, ', ', d.nombres) as dueno 
                               FROM autos a 
                               INNER JOIN duenos d ON a.idDueno = d.idDueno
                               WHERE a.patente = @patente";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = patente;

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Auto auto = new()
                    {
                        Patente = reader.GetString("patente"),
                        Marca = reader.GetString("marca"),
                        Modelo = reader.GetString("modelo"),
                        Anio = reader.GetInt32("anio"),
                        IdDueno = reader.GetInt32("idDueno"),
                        NombreDueno = reader.GetString("dueno")
                    };
                    reader.Close();
                    return auto;
                }
                reader.Close();
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ObtenerPorPatente: {ex.Message}");
                return null;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // Método auxiliar para validar patente
        private bool ValidarPatente(string patente)
        {
            if (string.IsNullOrWhiteSpace(patente))
                return false;

            // Formato viejo: ABC123
            // Formato nuevo: AB123CD
            var regex = new Regex(@"^[A-Z]{3}\d{3}$|^[A-Z]{2}\d{3}[A-Z]{2}$", RegexOptions.IgnoreCase);
            return regex.IsMatch(patente);
        }

        // Método auxiliar para validar año
        private bool ValidarAnio(int anio)
        {
            return anio >= 1900 && anio <= DateTime.Now.Year + 1;
        }

        // Método auxiliar para verificar si existe la patente
        private bool ExistePatente(string patente)
        {
            try
            {
                conexion.Abrir();
                string query = "SELECT COUNT(*) FROM autos WHERE patente = @patente";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = patente.ToUpper();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // Método auxiliar para verificar si tiene turnos
        private bool TieneTurnos(string patente)
        {
            try
            {
                conexion.Abrir();
                string query = "SELECT COUNT(*) FROM turnos WHERE patente = @patente";
                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@patente", MySqlDbType.VarChar, 10).Value = patente;

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        // Método para buscar autos
        public List<Auto> Buscar(string criterio)
        {
            List<Auto> lista = new List<Auto>();
            try
            {
                conexion.Abrir();
                string query = @"SELECT a.*, CONCAT(d.apellido, ', ', d.nombres) as dueno 
                               FROM autos a 
                               INNER JOIN duenos d ON a.idDueno = d.idDueno
                               WHERE a.patente LIKE @criterio 
                                  OR a.marca LIKE @criterio 
                                  OR a.modelo LIKE @criterio
                                  OR CONCAT(d.apellido, ', ', d.nombres) LIKE @criterio
                               ORDER BY a.patente";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.Add("@criterio", MySqlDbType.VarChar).Value = "%" + criterio + "%";

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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Buscar: {ex.Message}");
            }
            finally
            {
                conexion.Cerrar();
            }
            return lista;
        }
    }
}