sing MySql.Data.MySqlClient;
using System;

namespace Lavadero.Modelos
{
    public class Conexion
    {
        private string servidor = "localhost";
        private string bd = "lavadero";
        private string usuario = "root";
        private string password = "";
        private MySqlConnection conexion;

        public MySqlConnection ObtenerConexion()
        {
            if (conexion == null)
            {
                string cadenaConexion = $"server={servidor};database={bd};uid={usuario};pwd={password}";
                conexion = new MySqlConnection(cadenaConexion);
            }
            return conexion;
        }

        public void Abrir()
        {
            if (conexion.State != System.Data.ConnectionState.Open)
                conexion.Open();
        }

        public void Cerrar()
        {
            if (conexion.State != System.Data.ConnectionState.Closed)
                conexion.Close();
        }
    }
}