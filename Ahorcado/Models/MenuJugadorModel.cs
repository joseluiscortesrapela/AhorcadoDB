using Ahorcado.Conexion;
using Ahorcado.Entidades;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Ahorcado.Models
{
    internal class MenuJugadorModel
    {
        // Declaro las variables
        private List<Jugador> jugadores;
        private List<Partida> partidas;

        // Constructor
        public MenuJugadorModel()
        {
            jugadores = new List<Jugador>();
            partidas = new List<Partida>();
        }

        //  Obtiene el 
        public List<Jugador> getRanking()
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta
            string sql = "SELECT * FROM jugadores ORDER BY puntuacion DESC LIMIT 10";

            MySqlCommand command = new MySqlCommand(sql, conexion);
            MySqlDataReader reader = command.ExecuteReader();

            List<Jugador> jugadores = new List<Jugador>();

            while (reader.Read())
            {
                Jugador jugador = new Jugador();
                jugador.Nombre = reader["jugador"].ToString();
                jugador.Puntuacion = int.Parse(reader["puntuacion"].ToString());
                jugadores.Add(jugador);
            }


            return jugadores;

        }

        // Obtengo las partidas de un jugador
        public List<Partida> getPartidas(int idJugador)
        {
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();
            // Mi consulta
            string sql = "SELECT * FROM partidas WHERE idJugador = " + idJugador;

            MySqlCommand commando = new MySqlCommand(sql, conexion);
            MySqlDataReader reader = commando.ExecuteReader();

            // Mientra haya contenido 
            while (reader.Read())
            {
                // Obtengo los valores de la fila, convierto el dato y lo guardo en las variables.
                int idPartida = int.Parse(reader["id"].ToString());
                int puntuacion = int.Parse(reader["puntuacion"].ToString());
                DateTime fecha = DateTime.Parse(reader["fecha"].ToString());
                     
                // Creo e inicializo el objeto partida con los datos
                Partida partida = new Partida(idPartida, idJugador, puntuacion, fecha);
                
                // Guardo la partia en el array.
                partidas.Add(partida);
            }

            return partidas;

        }



    }
}
