using Lavadero.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace Lavadero.Controladores
{
    public class UsuarioController
    {
        private Conexion conexion = Conexion.Instancia;

        public bool ValidarLogin(string nombreUsuario, string contrasena)
        {
            try
            {
                conexion.Abrir();
                string hash = Usuario.EncriptarSHA256(contrasena);
                string query = "SELECT COUNT(*) FROM usuarios WHERE nombreUsuario=@user AND contrasena=@pass";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@user", nombreUsuario);
                cmd.Parameters.AddWithValue("@pass", hash);

                int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                conexion.Cerrar();

                return resultado > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}