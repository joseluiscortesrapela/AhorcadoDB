using Ahorcado.Models;
using Ahorcado.Utilidades;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ahorcado
{
    public partial class MenuAdmin : Form
    {

        private DataGridViewRow fila;
        private String nombreTabla;
        private String accionARealizar;



        public MenuAdmin()
        {
            InitializeComponent();
        }

        // Muestra el nombre del usuario en el menu principal
        private void MenuAdmin_Load(object sender, EventArgs e)
        {   // Muestro el nombre del usuario
            labelNombreUsuario.Text = SesionUsuario.Usuario;
            // Cargo el dgv de jugadores con los datos extraidos del fichero xml.
            importarJugadoresDesdeXMLAlDGV();
            // Cargo el dgv de palabras con los datos extraidos del fichero xml.
            importarPalabrasDesdeXMLAlDGV();
            // Cargo las categorias
            cargarCategorias();
        }

        // Importo los todos los jugadores desde el ficehro xml y los guardo en el dgv
        private void importarJugadoresDesdeXMLAlDGV()
        {
            // Obtengo la lista de jugadores y los guardo en el dgv
            List<Jugador> jugadores = ProcesarFicherosXML.dameListaJugadores();

            // Recorro la lista de jugadores
            foreach (Jugador jugador in jugadores)
            {   // Añado jugador al dgv
                dgvJugadores.Rows.Add(jugador.Id, jugador.Nombre, jugador.Contraseña, jugador.Puntuacion, jugador.Rol);
            }

        }

        // Importo los todos las palabas desde el ficehro xml y los guardo en el dgv
        private void importarPalabrasDesdeXMLAlDGV()
        {
            // Obtengo la lista de jugadores y los guardo en el dgv
            List<Word> palabras = ProcesarFicherosXML.dameListaPalabras();

            // Recorro la lista de palabras
            foreach (Word palabra in palabras)
            {   // Añado palabra al dgv
                dgvPalabras.Rows.Add(palabra.Id, palabra.Palabra, palabra.Pista, palabra.Categoria);
            }

        }


        // Muestra la tabla con los jugadores
        private void lbJugadores_Click(object sender, EventArgs e)
        {
            // Guardo que tabla se ha utilizado
            nombreTabla = "jugadores";
            // Muestro el nombre de la tabla
            mostrarTituloTablaEnUso(nombreTabla);
            // Muestra el icono de añadir usuarios
            pbMostrarPanelCrear.Image = imageList.Images[4];
            // Oculto dgv de las palabras
            dgvPalabras.Visible = false;
            // Muestro el dgv de los jugadores. 
            dgvJugadores.Visible = true;
            // Muestro el panel principal
            panelPrincipal.Visible = true;

        }

        // Muestra la tabla con las palabras  
        private void lbPalabras_Click(object sender, EventArgs e)
        {

            // Guardo que tabla se ha utilizado
            nombreTabla = "palabras";
            // Muestro el nombre de la tabla
            mostrarTituloTablaEnUso(nombreTabla);
            // Muestra el icono de añadir palabra
            pbMostrarPanelCrear.Image = imageList.Images[5];
            // Oculto el dgv de los jugadores. 
            dgvJugadores.Visible = false;
            // Muestro el dgv de las palabras
            dgvPalabras.Visible = true;
            // Muestro el panel principal
            panelPrincipal.Visible = true;

        }

        // Muestra el panel para actulizar un usuario o una palabra
        private void pbMostrarPanelActualizar_Click(object sender, EventArgs e)
        {

            // Si se ha seleccionado una fila
            if (fila != null)
            {
                // Accion que quiero realizar.
                accionARealizar = "actualizar";
                // Oculto el panel 
                panelPrincipal.Visible = false;
                // Reseteo los valores o error de tipo provider que pudiera tener el formulario.
                limpiarFormulario();

                // Muestro la tabla para crear jugadores
                if (nombreTabla.Equals("jugadores"))
                {
                    // Doy nombre al titulo del formulario
                    labelNombrePanelJugador.Text = "Formulario actualizar usuario";
                    // Cambio la imagen
                    iconoFormularioJugador.Image = imageList.Images[1];
                    // Muestro el panel
                    panelJugador.Visible = true;
                    // Relleno el formulario con los datos del jugador.
                    // Id del usuario
                    tbIdJugador.Text = fila.Cells["idJugador"].Value.ToString();
                    // El nombre del usuario
                    tbJugador.Text = fila.Cells["nombre"].Value.ToString();
                    // La contraseña del uusaurio
                    tbContraseña.Text = fila.Cells["contraseña"].Value.ToString();
                    // La puntuacion 
                    tbPuntuacion.Text = fila.Cells["puntuacion"].Value.ToString();
                    // Tipo de rol del usuario pueden ser jugador o administrador
                    cbTipoRol.Text = fila.Cells["rol"].Value.ToString();
                    // Oculto panel
                    panelPalabras.Visible = false;
                    // Muestro el panel
                    panelJugador.Visible = true;
                }
                else if (nombreTabla.Equals("palabras"))
                {
                    // Doy nombre al titulo del formulario
                    labelNombrePanelPalabra.Text = "Formulario actualizar palabra";
                    // Cambio la imagen
                    iconoFormularioPalabra.Image = imageList.Images[3];
                    // Muestro el panel
                    panelJugador.Visible = true;
                    // Relleno el formulario con los datos.
                    // Id 
                    tbIdPalabra.Text = fila.Cells["idPalabra"].Value.ToString();
                    // La palabra
                    tbPalabra.Text = fila.Cells["palabra"].Value.ToString();
                    // La pista
                    tbPista.Text = fila.Cells["pista"].Value.ToString();
                    // Categoria de la palabra
                    cbCategorias.Text = fila.Cells["categoria"].Value.ToString();
                    // Oculto panel
                    panelJugador.Visible = false;
                    // Muestro el panel
                    panelPalabras.Visible = true;
                }

            }
            else
            {   // Mensaje que quiero mostrar
                labelMensaje.Text = "Antes de actualizar, sellecione una fila de la tabla " + nombreTabla;
                // Muestro el mensaje 
                labelMensaje.Visible = true;
                // Muestro el icono
                pbIconoMensaje.Visible = true;
            }

        }

        //Muestra el panel para crear un usuario o palabra
        private void pbMostrarPanelCrear_Click(object sender, EventArgs e)
        {
            // Accion que se quiere realizar.
            accionARealizar = "crear";
            // Oculto el panel 
            panelPrincipal.Visible = false;
            // Limpio el resto de campos del formulario
            limpiarFormulario();

            // Muestro la tabla para crear jugadores
            if (nombreTabla.Equals("jugadores"))
            {   // Titulo 
                labelNombrePanelJugador.Text = "Formulario crear usuario";
                // Cambio la imagen
                iconoFormularioJugador.Image = imageList.Images[0];
                // Obtengo el siguiente id 
                tbIdJugador.Text = dameSiguienteID().ToString();
                // Oculto panel
                panelPalabras.Visible = false;
                // Muestro el panel
                panelJugador.Visible = true;

            } // Muestro el panel palabras para crear y actualizarlas.
            else if (nombreTabla.Equals("palabras"))
            {
                labelNombrePanelPalabra.Text = "Formulario crear palabra";
                // Cambio la imagen
                iconoFormularioPalabra.Image = imageList.Images[2];
                // Obtengo el siguiente id 
                tbIdPalabra.Text = dameSiguienteID().ToString();
                // Oculto el panel
                panelJugador.Visible = false;
                // Muestro el panel palabras y categorias
                panelPalabras.Visible = true;
            }

        }

        // Carga las categorias 
        private void cargarCategorias()
        {
            // Cargo las categorias 
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

        // Me dice el numero de fila que tien el datagridview


        //Muetra el nombre de la tabla que se esta utilizando
        private void mostrarTituloTablaEnUso(string nombre)
        {
            lbNombreTabla.Text = nombre;
        }

        // Elimina un registro de la tabla
        private void pbEliminar_Click(object sender, EventArgs e)
        {
            // Obtengo el identificador
            int id = (int)fila.Cells[0].Value;
            // Obtengo el nombre
            String nombre = fila.Cells[1].Value.ToString();
            // Mensaje que le aparecera al administrador.
            String message = "estas seguro de que quieres eliminar  " + nombre + " ?";
            // Titulo de la ventana emergente.
            String caption = "Eliminar " + nombreTabla;
            // Obtengo el resultado
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            // Si quiere eliminar 
            if (result == DialogResult.Yes)
            {

                if (nombreTabla.Equals("jugadores"))
                {
                    // Elimina al jugador
                    dgvJugadores.Rows.Remove(fila);
                }
                else
                {   // Elimina la palabra
                    dgvPalabras.Rows.Remove(fila);
                }

                // Muestro mensaje al administrador.       
                mostrarMensaje("Acabas de eliminar la fila de la tabla " + nombreTabla);
                // Ocultar botones de accion editar y eliminar.
                ocultarBotonesActualizarYEliminar();

            }

        }

        // Muestra mensaje
        private void mostrarMensaje(string mensaje)
        {
            labelMensaje.Visible = true;
            pbIconoMensaje.Visible = true;
            labelMensaje.Text = mensaje;
        }

        // Regresa al menu principal
        private void volverMenuPrincipal(object sender, EventArgs e)
        {
            // Oculto el panel del usuario
            panelJugador.Visible = false;
            // Oculto el panel de las palabras y sus categorias
            panelPalabras.Visible = false;
            // Mostrar panel menu
            panelVerticalMenu.Visible = true;

            // Actualizo la tabla de jugadores
            if (nombreTabla.Equals("jugadores"))
            {
                //dgvTablaGenerica = dgvJugadores;
            } // sino actualizo la tabla de palabras
            else if (nombreTabla.Equals("palabras"))
            {   // Realizo la consulta a la base de datos a tu tabla palabras
                // dgvTablaGenerica.DataSource = model_administrador.getPalabras();
            }

            Console.WriteLine("volver menu principal");

            // Muestro el panel menu principal
            panelPrincipal.Visible = true;
        }


        // Boton de accion panel jugador para crear o actulizar
        private void ButtonJugadorAceptar(object sender, EventArgs e)
        {
            //Obtengo los datos del formulario 
            // Identificador del jugador
            int id = int.Parse(tbIdJugador.Text);
            // El nombre del usuario 
            string usuario = tbJugador.Text;
            // La contraseña del usuario
            string contraseña = tbContraseña.Text;
            // La puntuacion
            string puntuacion = tbPuntuacion.Text;
            // Tipo de rol que tiene este usurio
            string rol = cbTipoRol.Text;

            // Si el formulario es correcto
            if (validarFormularioJugador(usuario, contraseña, rol))
            {
                // Si quiere crer un nuevo jugador
                if (accionARealizar.Equals("crear"))
                {
                    // Añado un nuevo jugador al dgv
                    dgvJugadores.Rows.Add(id, usuario, contraseña, puntuacion, rol);
                    // Reseteo formulario 
                    limpiarFormulario();
                    // Muestro el nuevo identificador que se utilizara en el caso de seguir creando usuarios.
                    tbIdJugador.Text = dameSiguienteID().ToString();
                    // Muestro mensaje 
                    labelMensajeJugador.Text = "Acabas de crear un nuevo usuario.";
                    Console.WriteLine("Jugdor creado");

                } // Si quiere actulizar los datos de un jugador
                else if (accionARealizar.Equals("actualizar"))
                {
                    // Actualizo la fila del jugador
                    fila.Cells["nombre"].Value = usuario;
                    fila.Cells["contraseña"].Value = contraseña;
                    fila.Cells["rol"].Value = rol;
                    // Muestro mensaje
                    labelMensajeJugador.Text = "Acabas de actualizar los datos del usuario.";
                    Console.WriteLine("jugador actulizado");
                }
            }

            // Oculto los botones de accion update y edit
            ocultarBotonesActualizarYEliminar();

        }

        // Boton de accion panel palabras para crear o actualizar
        private void buttonPalabraAceptar(object sender, EventArgs e)
        {
            // Recogo los datos del formulario
            // Id 
            int id = int.Parse(tbIdPalabra.Text);
            string palabra = tbPalabra.Text;
            string pista = tbPista.Text;
            string categoria = cbCategorias.Text;

            // Validar datos formulario
            if (validarFormularioPalabras(palabra, pista, categoria))
            {
                // Si quiere crear nueva palabra
                if (accionARealizar.Equals("crear"))
                {
                    // Añado una nueva palabra al dgv
                    dgvPalabras.Rows.Add(id, palabra, pista, categoria);
                    // Reseteo el formulario a cero.
                    limpiarFormulario();
                    // Muestro el nuevo identificador que se utilizara en el caso de seguir creando palabras.
                    tbIdPalabra.Text = dameSiguienteID().ToString();
                    // Muestro mensaje 
                    labelMensajePalabra.Text = "Acabas de crear una nueva palabra.";
                    Console.WriteLine("Creo una palabra");

                }
                else if (accionARealizar.Equals("actualizar"))
                {
                    // Actualizo las celdas de la fila con los nuevos valores.
                    fila.Cells["palabra"].Value = palabra;
                    fila.Cells["pista"].Value = pista;
                    fila.Cells["categoria"].Value = categoria;

                    // Actualizo los datos del jugador
                    labelMensajePalabra.Text = "Acabas de actualizar la palabra.";
                    Console.WriteLine("Actualizo palabra");
                }

            }
        }

        // Oculta los botones edit y update
        private void ocultarBotonesActualizarYEliminar()
        {
            pbMostrarPanelActualizar.Visible = false;
            pbMostrarVentanEliminar.Visible = false;
        }

        // Devuelve el ultimo id de la tabla
        private int dameSiguienteID()
        {

            int ultimoID = 0;

            // Si la tabla seleccionada es jugadores
            if (nombreTabla == "jugadores")
            {
                // Sino esta vacio el dgv
                if (dgvJugadores.Rows.Count > 0)
                {   // Obtengo la ultima fila
                    fila = dgvJugadores.Rows[dgvJugadores.Rows.Count - 1];
                    // Obtengo el valor de la primera columna que corresponde a los identificadores de ambas tablas.
                    ultimoID = (int)fila.Cells[0].Value;
                }
                else
                {
                    ultimoID = 0;
                }

            }

            if (nombreTabla == "palabras")
            {
                // Si no esta vacio el dgv
                if (dgvPalabras.Rows.Count > 0)
                {
                    fila = dgvPalabras.Rows[dgvPalabras.Rows.Count - 1];
                    // Obtengo el valor de la primera columna que corresponde a los identificadores de ambas tablas.
                    ultimoID = (int)fila.Cells[0].Value;
                }
                else
                {
                    ultimoID = 0;
                }

            }

            // Incremento en uno 
            ultimoID++;
            // Devuelvo su valor
            return ultimoID;

        }

        // Realiza la validacion de los campos del formulario del usuario/jugador
        private bool validarFormularioJugador(string usuario, string contraseña, string tipoRol)
        {
            bool validado = true;

            if (usuario.Trim().Length == 0)
            {
                validado = false;
                error.SetError(tbJugador, "El campo usuario esta vacio");
            }
            else
            {
                error.SetError(tbJugador, "");

                // Si quiere actualizar
                if (accionARealizar.Equals("actualizar"))
                {
                    // Nombre que tenia el usuarios
                    string nombreAtiguo = fila.Cells[1].Value.ToString();

                    // El usuario quiere cambiar de nombre
                    if (nombreAtiguo.ToLower() != usuario.ToLower())
                    {
                        // Compruebo si el nombre existe
                        /*
                        if (true)
                        {
                            validado = false;
                            error.SetError(tbJugador, "El usuario ya existe");
                        }
                        else
                        {
                            error.SetError(tbJugador, "");
                        }
                        */
                    }

                }

                // Si quiere crear 
                if (accionARealizar.Equals("crear"))
                {
                    /*
                    if (model_administrador.isUserExist(usuario))
                    {
                        validado = false;
                        error.SetError(tbJugador, "El usuario ya existe");
                    }
                    else
                    {
                        error.SetError(tbJugador, "");
                    }
                    */
                }

            }

            if (contraseña.Trim().Length == 0)
            {
                validado = false;
                error.SetError(tbContraseña, "El campo contraseña esta vacio");
            }
            else
            {
                error.SetError(tbContraseña, "");
            }


            if (tipoRol.Trim().Length == 0)
            {
                validado = false;
                error.SetError(cbTipoRol, "El campo tipo usuario estas vacio.");
            }
            else
            {
                error.SetError(cbTipoRol, "");
            }


            return validado;
        }

        // Realiza la validacion de los campos del formulario palabras y categorias
        private bool validarFormularioPalabras(string palabra, string pista, string categoria)
        {
            bool validado = true;
            // Si la palabra no esta vacia
            if (palabra.Trim().Length == 0)
            {
                validado = false;
                error.SetError(tbPalabra, "El campo palabra esta vacio");
            }
            else
            {
                error.SetError(tbPalabra, "");

                // Si quiere actualizar
                if (accionARealizar.Equals("actualizar"))
                {
                    // Nombre que tenia 
                    string nombreAtiguo = fila.Cells[1].Value.ToString();

                    // Quiere cambiar la palabra
                    if (nombreAtiguo.ToLower() != palabra.ToLower())
                    {
                        /*
                        // Compruebo si la palabra existe
                        if (model_administrador.isWordExist(palabra))
                        {
                            validado = false;
                            error.SetError(tbPalabra, "La palabra ya existe");
                        }
                        else
                        {
                            error.SetError(tbPalabra, "");
                        }
                        */
                    }

                }

                // Si quiere crear 
                if (accionARealizar.Equals("crear"))
                {
                    /*
                    if (model_administrador.isWordExist(palabra))
                    {
                        validado = false;
                        error.SetError(tbPalabra, "La palabra ya existe");
                    }
                    else
                    {
                        error.SetError(tbPalabra, "");
                    }
                    */
                }

            }
            // Si la pista no esta vacia
            if (pista.Trim().Length == 0)
            {
                validado = false;
                error.SetError(tbPista, "El pista esta vacio");
            }
            else
            {
                error.SetError(tbPista, "");
            }

            // Si la categoria no esta vaica
            if (categoria.Length == 0)
            {
                validado = false;
                error.SetError(cbCategorias, "El campo categoria estas vacio.");
            }
            else
            {
                error.SetError(cbCategorias, "");
            }


            return validado;
        }

        // Resetea los campos del formulario
        private void limpiarFormulario()
        {
            // Oculto panel
            panelVerticalMenu.Visible = false;

            if (nombreTabla.Equals("jugadores"))
            {
                // Campos formulario jugadores
                tbJugador.Text = "";
                tbContraseña.Text = "";
                tbPuntuacion.Text = "0";
                cbTipoRol.Text = "";
                // Mensaje
                labelMensajeJugador.Text = "";
                // Errores
                error.SetError(tbJugador, "");
                error.SetError(tbContraseña, "");
                error.SetError(cbTipoRol, "");

            }
            else
            {
                // Campos form palabras
                tbPalabra.Text = "";
                tbPista.Text = "";
                tbPuntuacion.Text = "0";
                cbCategorias.Text = "";
                // Mensaje
                labelMensajePalabra.Text = "";
                // Errores
                error.SetError(tbPalabra, "");
                error.SetError(tbPista, "");
                error.SetError(cbCategorias, "");
            }


        }

        // Se cierra el programa
        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        // Obtengo la fila que ha sido seleccionada en el dgv jugadores.
        private void dgvJugadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si ha seleccionado una fila 
            if (e.RowIndex >= 0)
            {
                // Obtengo la fila que ha sido seleccionada en el dataGridView
                fila = dgvJugadores.Rows[e.RowIndex];
                // Muestro los botones editar y eliminar
                showBotonesAccion();
            }

            Console.WriteLine("Fila seleccionada en el dgv jugadores");

        }

        // Obtengo la fila que ha sido seleccionada en el dgv palabras.
        private void dgvPalabras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si ha seleccionado una fila 
            if (e.RowIndex >= 0)
            {
                // Obtengo la fila que ha sido seleccionada en el dataGridView
                fila = dgvPalabras.Rows[e.RowIndex];
                // Muestro los botones editar y eliminar
                showBotonesAccion();
            }

            Console.WriteLine("Fila seleccionada en el dgv palabras");
        }

        // Muestra los botones editar y eliminar del datagridview
        private void showBotonesAccion()
        {
            // Muestro los botones de eliminar y modificar fila.
            pbMostrarVentanEliminar.Visible = true;
            pbMostrarPanelActualizar.Visible = true;
            pbIconoMensaje.Visible = false;
            labelMensaje.Text = "";
        }

        private void pbGuardar_Click(object sender, EventArgs e)
        {

            // Guardo el contenido del dgv jugadores y creo su fichero xml.
            ProcesarFicherosXML.crearJugadoresXML(dgvJugadores);
            // Guardo el contenido del dgv palabras y creo su fichero xml.
            ProcesarFicherosXML.crearPalabrasXML(dgvPalabras);
            // Muestro mensaje
            labelMensaje.Text = "Exportación realizada en la carpeta Ahorcado/Xml";
            // Muestro el label que estara oculto
            labelMensaje.Visible = true;
            // Muestro icono
            pbIconoMensaje.Visible = true;
        }

        // Salgo de la ventana de administracion y abro la ventana de login
        private void pbSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
