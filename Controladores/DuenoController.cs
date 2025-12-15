using Lavadero.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Lavadero.Controladores
{
    public class DuenoController
    {
        private Conexion conexion = new Conexion();

        public List<Dueno> ObtenerTodos()
        {
            List<Dueno> lista = new List<Dueno>();
            try
            {
                conexion.Abrir();
                string query = @"SELECT d.*, l.nombre as localidad 
                               FROM duenos d 
                               LEFT JOIN localidades l ON d.codigoPostal = l.codigoPostal";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Dueno
                    {
                        IdDueno = reader.GetInt32("idDueno"),
                        Dni = reader.GetString("dni"),
                        Apellido = reader.GetString("apellido"),
                        Nombres = reader.GetString("nombres"),
                        Domicilio = reader.IsDBNull(reader.GetOrdinal("domicilio")) ? "" : reader.GetString("domicilio"),
                        CodigoPostal = reader.IsDBNull(reader.GetOrdinal("codigoPostal")) ? "" : reader.GetString("codigoPostal"),
                        Telefono = reader.IsDBNull(reader.GetOrdinal("telefono")) ? "" : reader.GetString("telefono"),
                        NombreLocalidad = reader.IsDBNull(reader.GetOrdinal("localidad")) ? "" : reader.GetString("localidad")
                    });
                }
                reader.Close();
                conexion.Cerrar();
            }
            catch { }
            return lista;
        }

        public bool Agregar(Dueno dueno)
        {
            try
            {
                conexion.Abrir();
                string query = @"INSERT INTO duenos (dni, apellido, nombres, domicilio, codigoPostal, telefono) 
                               VALUES (@dni, @apellido, @nombres, @domicilio, @cp, @telefono)";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@dni", dueno.Dni);
                cmd.Parameters.AddWithValue("@apellido", dueno.Apellido);
                cmd.Parameters.AddWithValue("@nombres", dueno.Nombres);
                cmd.Parameters.AddWithValue("@domicilio", dueno.Domicilio);
                cmd.Parameters.AddWithValue("@cp", dueno.CodigoPostal);
                cmd.Parameters.AddWithValue("@telefono", dueno.Telefono);

                cmd.ExecuteNonQuery();
                conexion.Cerrar();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Modificar(Dueno dueno)
        {
            try
            {
                conexion.Abrir();
                string query = @"UPDATE duenos SET dni=@dni, apellido=@apellido, nombres=@nombres, 
                               domicilio=@domicilio, codigoPostal=@cp, telefono=@telefono 
                               WHERE idDueno=@id";

                MySqlCommand cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@id", dueno.IdDueno);
                cmd.Parameters.AddWithValue("@dni", dueno.Dni);
                cmd.Parameters.AddWithValue("@apellido", dueno.Apellido);
                cmd.Parameters.AddWithValue("@nombres", dueno.Nombres);
                cmd.Parameters.AddWithValue("@domicilio", dueno.Domicilio);
                cmd.Parameters.AddWithValue("@cp", dueno.CodigoPostal);
                cmd.Parameters.AddWithValue("@telefono", dueno.Telefono);

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
                string query = "DELETE FROM duenos WHERE idDueno=@id";
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

        public DataTable ObtenerLocalidades()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.Abrir();
                string query = "SELECT codigoPostal, nombre FROM localidades ORDER BY nombre";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion.ObtenerConexion());
                adapter.Fill(dt);
                conexion.Cerrar();
            }
            catch { }
            return dt;
        }
    }
}