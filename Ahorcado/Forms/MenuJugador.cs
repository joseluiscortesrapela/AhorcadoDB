using Ahorcado.Models;
using Ahorcado.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ahorcado
{
    public partial class MenuJugador : Form
    {
        //private List<Jugador> jugadores;
        private MenuJugadorModel model_jugador;

        public MenuJugador()
        {
            InitializeComponent();
            // Peticiones a la base de datos.
            model_jugador = new MenuJugadorModel();
            // Muestro el nombre del jugador.
            lbNombreUsuario.Text = SesionUsuario.Usuario;
            // Muestro su puntuacion
            lbPuntuacion.Text = SesionUsuario.Puntuacion.ToString();

            // Obtengo las las puntuaciones de los jugadores
            List<Jugador> jugadores = model_jugador.getRanking();
            // Muestro el ranking 

            mostrarRanking( jugadores );
        }

        // Muestro las siete mejores puntuaciones.
        private void mostrarRanking(List<Jugador> jugadores)
        {

            foreach( Jugador jugador in jugadores )
            {
                Console.WriteLine( jugador.Nombre + " puntuacion: " + jugador.Puntuacion );
            }


            // Ordena la lista de jugadores de mayor a menor puntuación
            // jugadores = jugadores.OrderByDescending(jugador => jugador.Puntuacion).ToList();

            // Filtra a los jugadores con más de 0 puntos y toma las 7 mejores puntuaciones.
            // var mejoresPuntuaciones = jugadores.Where(jugador => jugador.Puntuacion > 0).Take(7).ToList();

            // Imprime los 7 mejores puntajes
            for (int i = 0; i < jugadores.Count; i++)
            {
                Label labelJugador = Controls.Find("lbJugador" + (i + 1), true).FirstOrDefault() as Label;
                Label labelScore = Controls.Find("lbScore" + (i + 1), true).FirstOrDefault() as Label;

                if (labelJugador != null)
                {
                    // Si es el jugador que esta jugadno la partida
                    if (SesionUsuario.Usuario == jugadores[i].Nombre)
                    {
                        labelJugador.Text = jugadores[i].Nombre;
                        labelScore.Text = jugadores[i].Puntuacion.ToString();
                        // Cambio de color el nombre y su puntuacion asi destaco al jugador que esta jugando
                        labelJugador.ForeColor = Color.SpringGreen;
                        labelScore.ForeColor = Color.SpringGreen;
                    }
                    else
                    {   // Muestro el resto de jugadores que jugaron.
                        labelJugador.Text = jugadores[i].Nombre;
                        labelScore.Text = jugadores[i].Puntuacion.ToString();
                    }

                }
            }
        }


        private void labelJugar_Click(object sender, EventArgs e)
        {
            // Oculto el menu
            this.Hide();
            Juego halloween = new Juego();
            // Muestro el juego
            halloween.Show();
        }


        // Salgo de la ventana de administracion y abro la ventana de login
        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }


        // Sales del programa
        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
