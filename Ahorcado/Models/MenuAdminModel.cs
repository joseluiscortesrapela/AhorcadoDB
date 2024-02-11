using MySql.Data.MySqlClient;
using System;
using System.Data;
using Ahorcado.Conexion;
using System.Windows.Forms;

namespace Ahorcado.Models
{
    internal class MenuAdminModel
    {

        // Obtengo todos los jugaores.
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

        // Obtengo las partidas de un jugador
        public DataTable getPartidasJugador(int idJugador)
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Consulta sql
            string sql = "SELECT * FROM partidas WHERE idJugador = " + idJugador;

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

        // Obtengo todas las palabras
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

        // Elimina un jugador y todas sus partidas
        public int eliminarJugador(int idJugador)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            int eliminado = 0;

            // Inicio una transacción
            MySqlTransaction transaccion = null;

            try
            {
                transaccion = conexion.BeginTransaction();

                // Consulta sql para eliminar todas las partidas del jugador
                string sqlEliminarPartidas = "DELETE FROM partidas WHERE idJugador = @idJugador";
                MySqlCommand comandoEliminarPartidas = new MySqlCommand(sqlEliminarPartidas, conexion);
                comandoEliminarPartidas.Parameters.AddWithValue("@idJugador", idJugador);
                comandoEliminarPartidas.Transaction = transaccion;

                // Ejecutar la consulta para eliminar las partidas
                comandoEliminarPartidas.ExecuteNonQuery();

                // Consulta sql para eliminar al jugador
                string sqlEliminarJugador = "DELETE FROM jugadores WHERE idJugador = @idJugador";
                MySqlCommand comandoEliminarJugador = new MySqlCommand(sqlEliminarJugador, conexion);
                comandoEliminarJugador.Parameters.AddWithValue("@idJugador", idJugador);
                comandoEliminarJugador.Transaction = transaccion;

                // Ejecutar la consulta para eliminar al jugador
                eliminado = comandoEliminarJugador.ExecuteNonQuery();

                // Confirmar la transacción
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // Si ocurre algún error, se realiza un rollback de la transacción
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }
                eliminado = 0;
            }
            finally
            {
                // Cierro la conexión
                conexion.Close();
            }

            return eliminado;
        }

        // Elimino una palabra
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

        // Eliminar partida
        public int eliminarPartida(int idPartida)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "DELETE FROM partidas WHERE id = @idPartida";
            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso el parametro
            comando.Parameters.AddWithValue("@idPartida", idPartida);


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

        // Compruebo si existe el usuario
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

        // Compruebo si existe la palabra.
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

        // Actualiza datos jugador
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

        // Actualiza la palabra
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

        // Obtengo todas las categorias.
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

        // Actualiza las puntuaciones de todos todos lo jugadores.
        public DataTable actualizarPuntuaciones()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta
            string sql = @"
                        UPDATE jugadores j
                        JOIN (
                            SELECT idJugador, SUM(puntuacion) AS total_puntuacion
                            FROM partidas
                            GROUP BY idJugador
                        ) subconsulta ON j.idJugador = subconsulta.idJugador
                        SET j.puntuacion = subconsulta.total_puntuacion;
                       
                        SELECT *
                        FROM jugadores";


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
