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
        private List<Jugador> jugadores;

        public MenuJugador()
        {
            InitializeComponent();
            // Muestro el nombre del jugador.
            lbNombreUsuario.Text = SesionUsuario.Usuario;
            // Muestro su puntuacion
            lbPuntuacion.Text = SesionUsuario.Puntuacion.ToString();
            // Obtengo la lista de jugadores
            jugadores = ProcesarFicherosXML.dameListaJugadores();
            // Muestro el ranking 
            mostrarRanking();
        }

        // Muestro las siete mejores puntuaciones.
        private void mostrarRanking()
        {
            // Ordena la lista de jugadores de mayor a menor puntuación
            jugadores = jugadores.OrderByDescending(jugador => jugador.Puntuacion).ToList();

            // Filtra a los jugadores con más de 0 puntos y toma los 7 mejores puntajes
            var mejoresPuntajes = jugadores.Where(jugador => jugador.Puntuacion > 0).Take(7).ToList();

            // Imprime los 7 mejores puntajes
            for (int i = 0; i < mejoresPuntajes.Count; i++)
            {
                Label labelJugador = Controls.Find("lbJugador" + (i + 1), true).FirstOrDefault() as Label;
                Label labelScore = Controls.Find("lbScore" + (i + 1), true).FirstOrDefault() as Label;

                if (labelJugador != null)
                {
                    // Si es el jugador que esta jugadno la partida
                    if (SesionUsuario.Usuario == mejoresPuntajes[i].Nombre)
                    {
                        labelJugador.Text = mejoresPuntajes[i].Nombre;
                        labelScore.Text = mejoresPuntajes[i].Puntuacion.ToString();
                        // Cambio de color el nombre y su puntuacion asi destaco al jugador que esta jugando
                        labelJugador.ForeColor = Color.DarkTurquoise;
                        labelScore.ForeColor = Color.DarkTurquoise;
                    }
                    else
                    {   // Muestro el resto de jugadores que jugaron.
                        labelJugador.Text = mejoresPuntajes[i].Nombre;
                        labelScore.Text = mejoresPuntajes[i].Puntuacion.ToString();
                    }

                }
            }
        }


        private void labelJugar_Click(object sender, EventArgs e)
        {
            // Oculto el menu
            this.Hide();
            Halloween halloween = new Halloween();
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
