using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahorcado.Conexion;
using System.Windows.Forms;

namespace Ahorcado.Models
{
    internal class MenuAdminModel
    {

        public DataTable getJugadores()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT * FROM jugadores";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexion);
            DataTable table = new DataTable();

            try
            {
                adapter.Fill(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return table;
        }


        // Filtra la busqueda de los jugadores que coincidan por nombre.
        public DataTable buscadorJugadores(string nombre)
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta 
            string sql = "SELECT * FROM jugadores WHERE jugador LIKE '%" + nombre + "%'";

            // creo el adaptador
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexion);
            // Instancio una tabla vacia.
            DataTable table = new DataTable();

            try
            {
                adapter.Fill(table); // Relleno la tabla con el resulatado de la consulta.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return table;
        }

        // Filtra la busqueda de los jugadores que coincidan por nombre.
        public DataTable buscadorPalabras(string palabra)
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta 
            string sql = "SELECT * FROM palabras WHERE palabra LIKE '%" + palabra + "%'";

            // creo el adaptador
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexion);
            // Instancio una tabla vacia.
            DataTable table = new DataTable();

            try
            {
                adapter.Fill(table); // Relleno la tabla con el resulatado de la consulta.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return table;
        }

        public DataTable getPalabras()
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "SELECT * FROM palabras";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexion);
            DataTable table = new DataTable();

            try
            {
                adapter.Fill(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return table;

        }

        public int eliminarJugador(int id)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "DELETE FROM jugadores WHERE idJugador = @idJugador";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Nombre del jugador
            comando.Parameters.AddWithValue("@idJugador", id);


            int eliminado;

            try
            {
                // Return value is the number of rows affected by the SQL statement.
                eliminado = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                eliminado = 0;
            }

            return eliminado;

        }

        public int eliminarPalabra(int id)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "DELETE FROM palabras WHERE idPalabra = @idPalabra";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Nombre del jugador
            comando.Parameters.AddWithValue("@idPalabra", id);


            int eliminado;

            try
            {
                // Return value is the number of rows affected by the SQL statement.
                eliminado = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                eliminado = 0;
            }

            return eliminado;

        }

        // Registra un nuevo usuario
        public int registrarJugador(int idJugador, string jugador, string contraseña, string tipo)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "INSERT INTO jugadores (idJugador, jugador, contraseña, puntuacion, tipo ) VALUES ( @idJugador, @jugador, @contraseña, @puntuacion, @tipo )";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Identificador del usuario
            comando.Parameters.AddWithValue("@idJugador", idJugador);
            // Le paso como parametro el nombre del usuario.
            comando.Parameters.AddWithValue("@jugador", jugador);
            // Le como parametro la contraseña
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            // Puntuacion del jugador, por defecto sera cero.
            comando.Parameters.AddWithValue("@puntuacion", 0);
            // Tipo de usuario
            comando.Parameters.AddWithValue("@tipo", tipo);

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

        public int registrarPalabra(int idPalabra, string palabra, string pista, string categoria)
        {

            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "INSERT INTO palabras (idPalabra, palabra, pista, categoria ) VALUES ( @idPalabra, @palabra, @pista, @categoria )";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Identificador del usuario
            comando.Parameters.AddWithValue("@idPalabra", idPalabra);
            // Le paso como parametro el nombre del usuario.
            comando.Parameters.AddWithValue("@palabra", palabra);
            // Le como parametro la contraseña
            comando.Parameters.AddWithValue("@pista", pista);
            // Tipo de usuario
            comando.Parameters.AddWithValue("@categoria", categoria);

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

        public bool isUserExist(string jugador)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT * FROM jugadores WHERE jugador = @jugador";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre del jugador o de la palabra
            comando.Parameters.AddWithValue("@jugador", jugador);
            // Obtengo los resultado de la consulta
            MySqlDataReader reader = comando.ExecuteReader();
            // Si el numero de filas es false, no se ha encontrado el usuario.
            bool existe = reader.HasRows;

            // Devuelvo resultado
            return existe;

        }

        public bool isWordExist(string palabra)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT * FROM palabras WHERE palabra = @palabra";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre del jugador o de la palabra
            comando.Parameters.AddWithValue("@palabra", palabra);
            // Obtengo los resultado de la consulta
            MySqlDataReader reader = comando.ExecuteReader();
            // Si el numero de filas es false, no se ha encontrado el usuario.
            bool existe = reader.HasRows;

            // Devuelvo resultado
            return existe;
        }

        public int actualizarJugador(int idJugador, string jugador, string contraseña, int puntuacion, string tipo)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "UPDATE jugadores SET jugador = @jugador, contraseña = @contraseña, puntuacion = @puntuacion, tipo = @tipo WHERE idJugador = @idJugador";

            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre 
            comando.Parameters.AddWithValue("@jugador", jugador);
            // Le como parametro los apellidos
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            // Le paso como parametro el telefono
            comando.Parameters.AddWithValue("@puntuacion", puntuacion);
            // Le paso como parametro el dni
            comando.Parameters.AddWithValue("@tipo", tipo);
            // Le paso el identificador del jugador
            comando.Parameters.AddWithValue("@idJugador", idJugador);


            int actualizado;

            try
            {
                actualizado = comando.ExecuteNonQuery(); // Return value is the number of rows affected by the SQL statement.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                actualizado = 0;
            }

            return actualizado;


        }

        public int actualizarPalabra(int idPalabra, string palabra, string pista, string categoria)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "UPDATE palabras SET palabra = @palabra, pista = @pista, categoria = @categoria WHERE idPalabra = @idPalabra";

            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre 
            comando.Parameters.AddWithValue("@palabra", palabra);
            // Le como parametro la pista de la palabra
            comando.Parameters.AddWithValue("@pista", pista);
            // Le paso como parametro su categoria
            comando.Parameters.AddWithValue("@categoria", categoria);
            // Le paso el identificador del jugador
            comando.Parameters.AddWithValue("@idPalabra", idPalabra);


            int actualizado;

            try
            {
                actualizado = comando.ExecuteNonQuery(); // Return value is the number of rows affected by the SQL statement.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                actualizado = 0;
            }

            return actualizado;

        }

        public DataTable getCategorias()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT DISTINCT categoria FROM palabras";

            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conexion);
            DataTable table = new DataTable();

            try
            {
                adapter.Fill(table);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            return table;
        }






    }




}
