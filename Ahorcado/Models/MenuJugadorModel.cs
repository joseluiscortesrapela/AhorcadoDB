using Ahorcado.Conexion;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Ahorcado.Models
{
    internal class MenuJugadorModel
    {
        private List<Jugador> jugadores = new List<Jugador>();

        public List<Jugador> getRanking()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta
            string sql = "SELECT * FROM jugadores ORDER BY puntuacion DESC LIMIT 10";
           
            MySqlCommand command = new MySqlCommand(sql, conexion);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Jugador jugador = new Jugador();
                jugador.Nombre = reader["jugador"].ToString();
                jugador.Puntuacion = int.Parse( reader["puntuacion"].ToString() );
                jugadores.Add(jugador);
            }


            return jugadores;

        }

    }
}
