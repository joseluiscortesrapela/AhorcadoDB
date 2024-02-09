using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahorcado.Conexion;
using System.Windows.Forms;

namespace Ahorcado.Models
{
    internal class LoginModel
    {

        // Comprueba si existe un usuario
        public bool login(string jugador, string contraseña)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT * FROM jugadores WHERE jugador = @jugador AND contraseña = @contraseña";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre del usuario.
            comando.Parameters.AddWithValue("@jugador", jugador);
            // Le como parametro la contraseña
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            // Obtengo los resultado de la consulta
            MySqlDataReader reader = comando.ExecuteReader();
            // Si el numero de filas es false, no se ha encontrado el usuario.
            bool existe = reader.HasRows;
            // Si existe el usuario
            if (existe)
            {
                crearSesion(reader); // Creo una sesion para el usuario, para mantener los datos en cache.
            }

            // Devuelvo resultado 
            return existe;

        }

        // Registra un nuevo usuario
        public int registrarJugador( string jugador, string contraseña )
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "INSERT INTO jugadores ( jugador, contraseña ) VALUES ( @jugador, @contraseña )";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
           
            // Le paso como parametro el nombre del usuario.
            comando.Parameters.AddWithValue("@jugador", jugador);
            // Le como parametro la contraseña
            comando.Parameters.AddWithValue("@contraseña", contraseña);
   

            int creado;

            try
            {
                // Return value is the number of rows affected by the SQL statement.
                creado = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                creado = 0;
                MessageBox.Show(ex.Message);
            }

            return creado;

        }

        // Crea la sesion del usuario
        public void crearSesion(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                // Identificador del jugador
                int id = reader.GetInt32(0);
                // Nmbre usuario
                String usuario = reader.GetString(1);
                // Contraseña del usuario
                String contraseña = reader.GetString(2);
                // Puntuacion
                int puntuacion = reader.GetInt32(3);
                // Tipo de usuario
                String rol = reader.GetString(4);

                // Creo la sesion
                SesionUsuario.Id = id;
                SesionUsuario.Usuario = usuario;
                SesionUsuario.Contraseña = contraseña;
                SesionUsuario.Puntuacion = puntuacion;
                SesionUsuario.Tipo = rol;

            }

        }


    }
}
