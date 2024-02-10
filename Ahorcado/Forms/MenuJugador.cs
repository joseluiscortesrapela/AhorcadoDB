using Ahorcado.Entidades;
using Ahorcado.Models;
using Mysqlx.Cursor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            // Obtengo las puntuaciones de todos los jugadores
            List<Jugador> jugadores = model_jugador.getRanking();
            // Obtengo las partidas del jugador
            List<Partida> partidas = model_jugador.getPartidas(SesionUsuario.Id);

            // Muestro el ranking 
            mostrarRanking(jugadores);
            mostrarPartidas(partidas);
        }

        // Muestro las siete mejores puntuaciones.
        private void mostrarRanking(List<Jugador> jugadores)
        {
            // Muestro solo diez puntuaciones
            for (int i = 0; i < jugadores.Count; i++)
            {
                // Muestra el nombre del jugador
                Label labelJugador = Controls.Find("lbJugador" + (i + 1), true).FirstOrDefault() as Label;
                // Muestra la puntuacion
                Label labelScore = Controls.Find("lbScore" + (i + 1), true).FirstOrDefault() as Label;

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

        // Muestra las partidas
        private void mostrarPartidas(List<Partida> partidas)
        {
            // Muestro solo diez puntuaciones
            for (int i = 0; i < partidas.Count; i++)
            {
                // Guardo la referencia del label
                Label labelPuntuacionPartida = Controls.Find("lbPartida" + (i + 1), true).FirstOrDefault() as Label;
                // Guardo la referencia del label
                Label labelFecha = Controls.Find("lbFecha" + (i + 1), true).FirstOrDefault() as Label;
                // Guardo la puntuacion en el label
                labelPuntuacionPartida.Text = partidas[i].Puntuacion.ToString();
                // Guardo la fecha con el formato dia/mes/año en el label.
                labelFecha.Text = partidas[i].Fecha.ToString("dd/MM/yyyy");     
            }
        }

        // Vamos a la ventana para jugar
        private void labelJugar_Click(object sender, EventArgs e)
        {
            // Oculto el menu
            this.Hide();
            // Muestro ventana del juego
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

        // El formulario se carga, muestro el nombre del jugador y la puntuacion
        private void MenuJugador_Load(object sender, EventArgs e)
        {
            // Muestro el nombre del jugador.
            lbNombreUsuario.Text = SesionUsuario.Usuario;
            // Muestro la puntuacion del jugador
            lbPuntuacion.Text = SesionUsuario.Puntuacion.ToString();

        }

        // Muestra el panel partidas
        private void lbMostrarPanelPartidas_Click(object sender, EventArgs e)
        {
            panelRanking.Visible = false;
            panelPartidas.Visible = true;
        }

        // Muestra el panel ranking  
        private void lbMostrarPanelRanking_Click(object sender, EventArgs e)
        {
            // Oculto panel de las partidas
            panelPartidas.Visible = false;
            // Muestro panel del ranking
            panelRanking.Visible = true;
        }
    }
}
