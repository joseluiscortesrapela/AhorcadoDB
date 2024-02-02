using Ahorcado.Conexion;
using Ahorcado.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ahorcado
{
    public partial class Login : Form
    {
        private LoginModel loginModel;
        //  private List<Jugador> jugadores;  // Array de jugadores


        public Login()
        {
            InitializeComponent();
            // Intencio el modelo de datos que controlara las peticiones  con la base de datos.
            loginModel = new LoginModel();
        }


        // Login usuario
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Obtengo nombre 
            string usuario = tbNombre.Text;
            // Obtengo la contraseña
            string contraseña = tbContraseña.Text;

            // Si el formulario es valido    
            if (siValidarFormularioLogin(usuario, contraseña))
            {
                // Realizo la consulta al modelo y obtengo la respuesta.
                bool existe = loginModel.login(usuario, contraseña);

                // Si usuario existe.
                if (existe)
                {
                    // Oculto la ventana de login
                    this.Hide();
                    // Obtengo el usuario logeado
                    string tipoUsuario = SesionUsuario.Tipo;

                    // Si quien se logia es un jugador
                    if (tipoUsuario.Equals("Jugador"))
                    {
                        // Intancia
                        MenuJugador menuJugador = new MenuJugador();
                        // Muestro la ventana del jugador
                        menuJugador.Show();
                    } // Si quien se logea es un administrador
                    else if (tipoUsuario.Equals("Administrador"))
                    {
                        // Intancio
                        MenuAdmin menuAdmin = new MenuAdmin();
                        // Muestro la ventana del alministrador
                        menuAdmin.Show();
                    }
                }
                else
                {
                    labelMensajeLogin.Text = "Usuario no encontrado";
                }

                Console.WriteLine("LOGIN: el usuario: " + usuario + " con contraseña: " + contraseña + " ¿Existe? " + existe);

            }


        }


        // Registra un nuevo jugador
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Obtengo nombre y le quito los espacios en blanco a derecha e izquierda
            string nombre = tbUsuario.Text.Trim();
            // Obtengo la contraseña
            string contraseña = tbPassword.Text;

            // Si datos formulario son correctos
            if (siValidarFormularioRegistro(nombre, contraseña) ) {
                // Guardo un nuevo usuario en la base de datos.
                loginModel.registrarJugador(nombre, contraseña);
            }
        }


        // Valida los campos del formulario de login
        private bool siValidarFormularioLogin(string nombre, string contraseña)
        {

            bool valido = true;

            // Si el nombre de usuario no esta vacio
            if (nombre.Length == 0)
            {
                valido = false;
                error.SetError(tbNombre, "El nombre del usuario no puede estar vacio.");
            }
            else
            {
                error.SetError(tbNombre, "");
            }

            // Si el campo contraseña no esta vacio
            if (contraseña.Length == 0)
            {
                valido = false;
                error.SetError(tbContraseña, "La contraseña no puede estar vacia.");
            }
            else
            {
                error.SetError(tbContraseña, "");
            }


            return valido;


        }


        // Valida los campos del formulario de registro
        private bool siValidarFormularioRegistro(string nombre, string contraseña)
        {

            bool valido = true;

            // Si el nombre de usuario no esta vacio
            if (nombre.Length == 0 || string.IsNullOrWhiteSpace(nombre))
            {
                valido = false;
                error.SetError(tbUsuario, "El nombre del usuario no puede estar vacio.");
            }
            else
            {
                error.SetError(tbUsuario, "");
            }

            // Si el campo contraseña no esta vacio
            if (contraseña.Length == 0)
            {
                valido = false;
                error.SetError(tbPassword, "La contraseña no puede estar vacia.");
            }
            else
            {
                error.SetError(tbPassword, "");
            }


            return valido;


        }


        // Muestro panel para registrar un nuevo usuario
        private void lbMostrarPanelRegistro_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panelRegistro.Visible = true;
        }


        // Vuelvo a la vengan de login
        private void lbVolverLogin_Click(object sender, EventArgs e)
        {
            panelRegistro.Visible = false;
            panelLogin.Visible = true;
        }


        // Se cierra el programa
        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Login_Load(object sender, EventArgs e)
        {
            // string ruta = "Sql\ahorcado.sql";
            //OperacionesBaseDatos.importarBaseDatos(ruta);
        }
    } // Final clase Login



} // Final nameespace
