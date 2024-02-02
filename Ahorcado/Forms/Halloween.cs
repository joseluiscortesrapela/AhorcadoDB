using Ahorcado.Utilidades;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ahorcado
{
    public partial class Halloween : Form
    {
        // Lista de plabras de la categoria que aun no han sido jugadas.
        private List<String> palabras = new List<String>();
        // La palabra que esta en juego.
        private String palabra;
        // La categoria que juega
        private String categoria;
        // La pista de la palabra
        private String pista;
        // La palabra que esta en juego convertida en un array de caracteres
        private char[] charsPalabra;
        // Los espacios de la palabra que se esta jugando convertida en un array con guiones.
        private char[] charsGionesPalabra;
        // Lista letras jugadas
        private List<Char> letras = new List<Char>();
        // Numero de aciertos
        private int numeroAciertos;
        // Numero de fallos
        private int numeroFallos;
        // Puntuación jugador.
        private int puntuacion;
        // Clases para reproducir musica
        private IWavePlayer player;
        private AudioFileReader audioFile;

        public Halloween()
        {
            InitializeComponent();

            // Los sonidos que utilizare en el juego
            // Sonido de fondo
            player = new WaveOut(); ;
            string rutaRelativa = @"..\..\Resources\Juegos\Halloween\Sonidos\halloween.mp3";
            audioFile = new AudioFileReader(rutaRelativa);

            // Añado las palabras al dgv
            añadirPalabrasDGV();

            // Compruebo si el dgv se cargo con la lista de palabras
            if (dgvPalabras != null)
            {   // Obtengo las categorias.
                cargarCategorias(); // Cargo las categorias

            }
            else
            {
                Console.WriteLine("No se han ecnontrado palabras en el dgv, tabla vacia.");
            }

        }

        // Obtengo la categoria 
        private void comboBoxCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtengo la categoria
            categoria = cbCategorias.GetItemText(cbCategorias.Text);
            // Muestro al jugador la categoria.
            labelCategoria.Text = categoria;
            // Obtengo la lista de palabras que pertenecen a la categoria.
            palabras = dameListaPalabras(categoria);
            // Preparo la partida
            prepararPartida();
        }

        // Incia el juego.
        private void prepararPartida()
        {

            mostrarPresentacionJuego();

            // La palabra a jugar.
            palabra = damePalabra();
            // Convierto la palabra a un array de caractres.
            charsPalabra = palabra.ToCharArray();
            // Reseteo a cero el mensaje final partida 
            labelFinalPartida.Text = "";
            // Muestro los la palabra con los guiones.
            convertirPalabraEnGuiones(palabra);
            // Muestro al jugador la palabra que tiene que adivinar.
            mostrarPalabraPorAdivinar();
            // Limpio la lista de letras jugadas de una partida anterior.
            limpiarListaLetrasJugadas();
            // Reseteo los valores de las puntuaciones.
            resetearPuntuacionesJugador();
            // Oculto panel game over
            panelGameOver.Hide();
            // Muestro el panel con las letras/botones
            panelLetras.Show();
            // Muestro los guiones de la palabra que hay que adivinar
            lbPalabraGuiones.Show();
            //Muestro panel puntuacion
            panelScore.Show();
            // Muestro el panel de herramientas 
            panelBarraHerramientas.Show();
            // Muestro el boton para resolver
            pbResolver.Show();
            // Oculto select categorias
            cbCategorias.Hide();
            // Muestro el buho
            pbBuho.Show();
            // Muestro otros pajaros
            pbPajaros.Show();
            // Muestros los muercielagos
            pbMuercielagos.Show();
            // Muestro los fuegos.
            pbFuego.Show();
            pbFuego2.Show();
            // Muestro las letras
            buttonA.Show();
            buttonB.Show();
            buttonC.Show();
            buttonD.Show();
            buttonE.Show();
            buttonF.Show();
            buttonG.Show();
            buttonH.Show();
            buttonI.Show();
            buttonJ.Show();
            buttonK.Show();
            buttonL.Show();
            buttonM.Show();
            buttonN.Show();
            buttonÑ.Show();
            buttonO.Show();
            buttonP.Show();
            buttonQ.Show();
            buttonR.Show();
            buttonS.Show();
            buttonT.Show();
            buttonU.Show();
            buttonV.Show();
            buttonW.Show();
            buttonX.Show();
            buttonY.Show();
            buttonZ.Show();

        }

        // Obtengo la palabra que se jugara.
        private String damePalabra()
        {
            // Obtengo un numero aleatorio entre cero y el numero de palabras.
            int indice = generarNumeroAleatorio();
            // Palabra disponible
            String palabra = palabras[indice];
            // Paso a minusculas 
            palabra = palabra.ToLower();
            // Retorno la palabra
            return palabra;
        }

        // Obtengo las palabras de una categoria.
        private List<String> dameListaPalabras(String categoria)
        {
            // Lista de palabras que no han sido repetidas
            List<String> palabras = new List<String>();

            // Recorro el array
            foreach (DataGridViewRow palabra in dgvPalabras.Rows)
            {
                // Si la palabra pertenece a la categoria 
                if (palabra.Cells[3].Value.Equals(categoria))
                {
                    // Añado la palabra a la lista.
                    palabras.Add(palabra.Cells[1].Value.ToString());
                }

            }
            // Retorno la lista de palabras
            return palabras;
        }

        // Muestra los giones o esapcios que conforman la palabra por adivinar.
        private void convertirPalabraEnGuiones(String palabra)
        {

            // Creo un array de caractres con los espacios que tiene la palabra
            charsGionesPalabra = palabra.ToCharArray();
            // Convierto la palabra en un array de caracteres.
            char[] giones = palabra.ToCharArray();
            // Recorro cada uno de los caracteres de la palabra.
            for (int i = 0; i < palabra.Length; i++)
            {
                // Si en la posicion i tengo un caracter escribo - sino espacio en blanco.
                charsGionesPalabra[i] = (Char.IsLetter(charsPalabra[i])) ? '-' : ' ';
            }


        }

        // Actualiza la plabra en guiones con las letras que se hayan podido acertar.
        private void mostrarPalabraPorAdivinar()
        {   // Convierto el array de caracteres con los guiones y letras completadas a String.
            String adivinar = new string(charsGionesPalabra);
            // Muestro la palabra por adivinar con giones y letras acertadas.
            lbPalabraGuiones.Text = adivinar;
        }

        // Genera un numero aleatorio
        private int generarNumeroAleatorio()
        {
            Random random = new Random();
            int numero = random.Next(palabras.Count);
            return numero;
        }

        // Carga las catgorias
        private void cargarCategorias()
        {
            // Lista de categorias repetidas.
            List<String> lista = new List<string>();

            // Recorro por la columna categorias.
            foreach (DataGridViewRow fila in dgvPalabras.Rows)
            {

                // Si la palabra NO EXISTE
                if (!lista.Contains(fila.Cells["categoria"].Value))
                {
                    // Añado categoria al select princpal
                    cbCategorias.Items.Add(fila.Cells["categoria"].Value);
                    // Guardo en la lista la catetoria para no repetirla
                    lista.Add(fila.Cells["categoria"].Value.ToString());
                }

            }

        }

        private String damePista(String palabra)
        {
            String pista = "";

            // Recorro todas las palabras
            foreach (DataGridViewRow fila in dgvPalabras.Rows)
            {
                // Si encuentras la palabra
                if (fila.Cells["palabra"].Value.ToString().ToLower() == palabra)
                {   // Guardo su pista
                    pista = fila.Cells["pista"].Value.ToString();
                }

            }
            // Devuelvo valor
            return pista;
        }

        // Carga inicial de las palabras al cargar el juego.
        private void añadirPalabrasDGV()
        {
            // Obtengo las palabras y las guardo en el dgv
            dgvPalabras.DataSource = ProcesarFicherosXML.dameListaPalabras();
        }

        // Permite al jugador resolver
        private void buttonRespuestaRapida_Click(object sender, EventArgs e)
        {
            // Obtengo la respuesta del jugador, le quito los espacios y la convierto a minusculas.
            String respuesta = tbRespuesta.Text.Trim().ToLower();

            // Si la respuesta del jugador es igual a la palabra por adivinar
            if (respuesta.Equals(palabra))
            {
                // Incremento en diez puntos la puntuación.
                puntuacion += 10;
                finDelJuego("Has ganado!");
            }
            else
            {
                // Resto 5 puntos por respuesta fallida.
                puntuacion -= 5;
                finDelJuego("Has perdido!");
            }
            // Muestro las puntuaciones
            mostrarPuntuacionesJugador();

        }

        // Elimina las leras almacenadas en la lista de letras jugadas.
        private void limpiarListaLetrasJugadas()
        {
            letras.Clear();
        }

        // Comprubar si con el siguiente acierto, se ha completado la palabra, el jugador ha ganado, fin de la partida.
        private bool siHaCompletadoPalabra()
        {

            // Convierto array de caracteres a string
            String palabraEnJuego = new String(charsGionesPalabra);
            // Si la palabra con guiones es igual a la palabra secreta
            bool resultado = (palabraEnJuego.Equals(palabra)) ? true : false;

            return resultado;
        }

        // Muestra las imagenes del ahorcado
        private void dibujarParteAhorcdo(int parte)
        {

            switch (parte)
            {
                case 1: pbAhorcado.Image = Properties.Resources._1; break;
                case 2: pbAhorcado.Image = Properties.Resources._2; break;
                case 3: pbAhorcado.Image = Properties.Resources._3; break;
                case 4: pbAhorcado.Image = Properties.Resources._4; break;
                case 5: pbAhorcado.Image = Properties.Resources._5; break;
                case 6: pbAhorcado.Image = Properties.Resources._6; break;

            }


        }

        // Comprueba si la letra se encuentra en la palabra.
        private void comprobarLetra(char letra)
        {

            // Estado inicial.
            bool acierto = false;

            // Recorro cada letra de la plabra que hay que adivinar.
            for (int i = 0; i < charsPalabra.Length; i++)
            {   // Si se encuentra la letra
                if (charsPalabra[i] == letra)
                {
                    // He acertado la letra/s
                    acierto = true;
                    // Guardo la letra que acabo de encontrar en el array.
                    charsGionesPalabra[i] = letra;
                    // Muestra la palabra que hay que adivinar con la letra que se acaba de encontrar.
                    mostrarPalabraPorAdivinar();
                    // Incremento 2 puntos
                    puntuacion += 2;
                    // Incremento el numero de aciertos.
                    numeroAciertos += 1;

                }
            }


            // Si la letra no se ha encontrado
            if (acierto == false)
            {
                //Si ha fallado, le resto uno
                puntuacion -= 1;
                // Por cada letra fallida un punto menos.
                numeroFallos += 1;
                // Dibujo una parte del ahorcado
                dibujarParteAhorcdo(numeroFallos);

                // si el jugados ha llegado a los 6 fallos
                if (numeroFallos == 6)
                {
                    puntuacion -= 5; // Por cada palabra fallada cinco puntos menos.
                    // El juego ha finalizado
                    finDelJuego("Has perdido!");
                }
                else
                {
                    // Si ha fallado, le resto uno, sino no hago nada, no quiero un score con resultados negativos.
                   // puntuacion -= (puntuacion > 0) ? 1 : puntuacion;               
                }

            }

            // Si has adivinado la palabra
            if (siHaCompletadoPalabra())
            {
                // Incremento el score
                puntuacion += 10;
                // el juego ha finalizado
                finDelJuego("Has ganado!");
            }

            // Actualizo la puntuacion
            mostrarPuntuacionesJugador();

        }



        // Obtengo la letra que acaba de pulsar el jugador.
        private void buttonLetter_Click(object sender, EventArgs e)
        {
            // Obtengo el texto del boton que ha sido pulsado y lo convierto a tipo char.
            char letra = char.Parse(((Button)sender).Text.ToLower());
            // Obtengo el boton pulsado
            Button button = (Button)sender;
            // Desactivo el evento del boton
            button.Hide();
            // Comprobar si la letra existe en la palabra.
            comprobarLetra(letra);
        }


        // Muestra las puntuaciones
        private void mostrarPuntuacionesJugador()
        {
            // Muestro numero de aciertos.
            labelNumeroAciertos.Text = numeroAciertos.ToString();
            // Muestro numero de fallos
            labelNumeroFallos.Text = numeroFallos.ToString();
            // Muestro el score
            labelPuntuacion.Text = puntuacion.ToString();

        }

        // Resetea a cero las puntuaciones
        private void resetearPuntuacionesJugador()
        {
            // Pongo la variable puntuaciones a cero.
            this.numeroAciertos = 0;
            // Muestro numero de fallos
            this.numeroFallos = 0;
            // Pongo el score.
            this.puntuacion = 0;
            // Label n acierto a cero
            labelNumeroAciertos.Text = "0";
            // Numero fallos label
            labelNumeroFallos.Text = "0";
            // Reseteo label score
            labelPuntuacion.Text = "0";
        }


        // Muestro el panel de respuesta rapida
        private void btnShowPanelResolver_Click(object sender, EventArgs e)
        {
            panelResolver.Show();
            timerResolver.Start();
        }

        // Fin de la partida
        private void finDelJuego(String mensaje)
        {
            // Detengo la musica
            Task task = apagarMusicaFondoJuego();
            // Muestro al jugador la palabra secreta.
            lbPalabraGuiones.Text = palabra;
            // Muestro al jugador el siguiente mensaje
            labelFinalPartida.Text = mensaje;
            // Oculto el panel de botones/letras
            panelLetras.Hide();
            // Oculto el paner respuesta rapida.
            panelResolver.Hide();
            // Oculto pista 
            lbPista.Hide();
            // Oculto boton resolver
            pbResolver.Hide();
            //Muestro panel game over
            panelGameOver.Show();
            // Configura el valor alfa para hacer que el Panel sea semi-transparente
            panelGameOver.BackColor = System.Drawing.Color.FromArgb(128, 0, 0, 0);
            // Actualizo la puntuacion para la sesion del jugador
            SesionUsuario.Puntuacion = puntuacion;
            // Guardo la puntuacion del jugador
            ProcesarFicherosXML.ActualizarPuntuacionJugador(SesionUsuario.Id, puntuacion);

            Console.WriteLine("Actualizo puntuacion jugador " + SesionUsuario.Usuario + " id: " + SesionUsuario.Id +
                             " puntuacion: " + SesionUsuario.Puntuacion);
        }

        // Jugar otra partida
        private void buttonJugarOtraPartida_Click(object sender, EventArgs e)
        {
            // Hago visible el combobox para elegir categoria
            cbCategorias.Show();
            // Reseteo el mensaje
            labelFinalPartida.Text = "";
            // Oculto el panel de botones/letras
            panelLetras.Hide();
            // Oculto el paner respuesta rapida.
            panelResolver.Hide();
            // Oculto el panel game over
            panelGameOver.Hide();
            // Oculto el panel scores
            panelScore.Hide();
            // Quito la imagen del ahorcado
            pbAhorcado.Image = null;
            // Oculto panel 
            panelBarraHerramientas.Hide();
            // Oculto mensaje al fnalizar partda
            labelFinalPartida.Text = "";
            // Oculto el label categoria
            labelCategoria.Text = "";
            // Oculto los guiones palabra
            lbPalabraGuiones.Text = "";
            // Quito la pista anterior.
            lbPista.Text = "";
            // Muestro la pista
            lbPista.Show();

        }

        private void buttonNoJugarOtra_Click(object sender, EventArgs e)
        {

            // Oculto la ventana de login
            this.Hide();
            // Intancia
            MenuJugador menuJugador = new MenuJugador();
            // Muestro la ventana del jugador
            menuJugador.Show();
            // Detengo la musica.
            Task task = apagarMusicaFondoJuego();
        }


        private void pbMostrarPista_Click(object sender, EventArgs e)
        {
            // Obtengo la pista asociada a la palabra
            pista = damePista(palabra);
            // Muestro la pista en un label.
            lbPista.Text = pista;
            // Muestro la pista
            lbPista.Visible = true;
            // Activo el temmporizador
            timerPista.Start();
        }





        // Muestro la vengana de presentacion del juego
        private void mostrarPresentacionJuego()
        {
            pbPresentacion.Visible = true;
            // El panel ocupara toda la ventana.
            pbPresentacion.Dock = DockStyle.Fill;
            // Muestro el panel de la animacion.
            pbPresentacion.Visible = true;
            // Inicio el timer
            timer.Enabled = true;
            // Pongo la musica de fondo del juego
            ponerMusicaFondo();
        }


        // El timer acaba de terminar.
        private void timer_Tick(object sender, EventArgs e)
        {
            // Oculto presentacion
            pbPresentacion.Hide();
            // Detengo el timer
            timer.Enabled = false;

            Console.WriteLine("Temporizador ha finalizado");
        }

        private void ponerMusicaFondo()
        {
            // Volumen activo
            audioFile.Volume = 1;
            player.Init(audioFile);
            player.Play();

        }

        // Disminuye el sonido lentamente hasta apagarlo.
        private async Task apagarMusicaFondoJuego()
        {

            if (audioFile != null && player != null)
            {
                int fadeDurationMs = 2000; // Duración del fade-out en milisegundos
                int fadeIntervalMs = 100;  // Intervalo de ajuste del volumen en milisegundos

                float initialVolume = audioFile.Volume;

                for (int t = 0; t < fadeDurationMs; t += fadeIntervalMs)
                {
                    float volume = initialVolume - (float)t / fadeDurationMs;
                    if (volume < 0) volume = 0;

                    audioFile.Volume = volume;
                    await Task.Delay(fadeIntervalMs);
                }
            }


            player.Stop();

        }


        // Se cierra el juego
        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Oculta la pista
        private void timerPista_Tick(object sender, EventArgs e)
        {
            // Oculta el Label
            lbPista.Visible = false;
            // Detiene el Timer para que no siga ejecutándose
            timerPista.Stop();
        }

        // Oculta la respuesta rapida
        private void timerResolver_Tick(object sender, EventArgs e)
        {
            // Oculta el panel
            panelResolver.Visible = false;
            // Detiene el Timer para que no siga ejecutándose
            timerResolver.Stop();
        }
    }


}
