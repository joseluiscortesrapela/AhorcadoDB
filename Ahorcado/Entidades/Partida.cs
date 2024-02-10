using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahorcado.Entidades
{
    internal class Partida
    {

        private int id;
        private int idJugador;
        private int puntuacion;
        private DateTime fecha;

 
        public Partida(int id, int idJugador, int puntuacion, DateTime fecha)
        {
            this.id = id;
            this.idJugador = idJugador;
            this.puntuacion = puntuacion;
            this.fecha = fecha;
        }

        public int Id { get => id; set => id = value; }
        public int IdJugador { get => idJugador; set => idJugador = value; }
        public int Puntuacion { get => puntuacion; set => puntuacion = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }

        public override string ToString()
        {
            return $"Partida id: {Id}, jugador: {IdJugador}, fecha: {Fecha} ";
        }
    }
}
