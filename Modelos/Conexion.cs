#nullable disable
using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace Lavadero.Modelos
{
    public class Conexion
    {
        private static Conexion _instancia;
        private static readonly object _lock = new object();
        private string servidor = "localhost";
        private string bd = "auto_lavado";
        private string usuario = "root";
        private string password = "";
        private string cadenaConexion;

        // Constructor privado para Singleton
        private Conexion()
        {
            cadenaConexion = $"server={servidor};database={bd};uid={usuario};pwd={password};SslMode=none;";
        }

        // Propiedad Singleton
        public static Conexion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new Conexion();
                        }
                    }
                }
                return _instancia;
            }
        }

        // Método mejorado para crear nuevas conexiones
        public MySqlConnection CrearConexion()
        {
            try
            {
                return new MySqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear conexión: {ex.Message}");
            }
        }

        // Método para probar la conexión
        public bool ProbarConexion()
        {
            try
            {
                using (var conn = CrearConexion())
                {
                    conn.Open();
                    return conn.State == System.Data.ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

        // NOTA: Los métodos Abrir/Cerrar antiguos se mantienen por compatibilidad
        // pero se recomienda usar 'using' con CrearConexion()
        private MySqlConnection conexion;

        public MySqlConnection ObtenerConexion()
        {
            if (conexion == null)
            {
                conexion = new MySqlConnection(cadenaConexion);
            }
            return conexion;
        }

        public void Abrir()
        {
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(cadenaConexion);
                }

                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al abrir conexión: {ex.Message}");
            }
        }

        public void Cerrar()
        {
            try
            {
                if (conexion != null && conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cerrar conexión: {ex.Message}");
            }
        }
    }
}