using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ahorcado.Conexion;
using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;

namespace Ahorcado.Models
{
    internal class juegoModel
    {
    

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


        // Actuliza la puntuacion del jugador
        public int updatePuntuacion(int idJugador, int puntuacion)
        {
            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            // Consulta sql
            string sql = "UPDATE jugadores SET puntuacion = @puntuacion  WHERE idJugador = @idJugador";

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
