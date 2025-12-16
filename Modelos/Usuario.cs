using System.Security.Cryptography;
using System.Text;

namespace Lavadero.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public required string NombreUsuario { get; set; }
        public required string Contrasena { get; set; }
        public required string Nombre { get; set; }

        public static string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}