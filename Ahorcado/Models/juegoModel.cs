using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Ahorcado.Conexion;
using MySql.Data.MySqlClient;

namespace Ahorcado.Models
{
    internal class juegoModel
    {

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

        // Actualiza las puntuaciones de todos todos lo jugadores.
        public void actualizarPuntuaciones()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta
            string sql = @"
                        UPDATE jugadores j
                        LEFT JOIN (
                            SELECT idJugador, SUM(puntuacion) AS total_puntuacion
                            FROM partidas
                            GROUP BY idJugador
                        ) subconsulta ON j.idJugador = subconsulta.idJugador
                        SET j.puntuacion = IFNULL(subconsulta.total_puntuacion, 0);

                        SELECT * FROM jugadores;
                    ";



            MySqlCommand command = new MySqlCommand(sql, conexion);
            MySqlDataReader reader = command.ExecuteReader();
        }

        // Guarda la puntuacion del jugador
        public int guardarPartida(int idJugador, int puntuacion)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "INSERT INTO partidas ( puntuacion, idJugador ) VALUES  ( @puntuacion, @idJugador )";

            // Preparo la consulta
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            // Le paso como parametro el nombre 
            comando.Parameters.AddWithValue("@puntuacion", puntuacion);
            // Le como parametro la pista de la palabra
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



    }
}
