using Ahorcado.Models;
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


        private MenuAdminModel model_admin;
        private DataGridViewRow filaTabla;
        private String nombreTabla;
        private String accionARealizar;


        public MenuAdmin()
        {
            InitializeComponent();
            // Instancio el modelo de datos
            model_admin = new MenuAdminModel();
        }

        // Muestra el nombre del usuario en el menu principal
        private void MenuAdmin_Load(object sender, EventArgs e)
        {   // Muestro el nombre del usuario
            labelNombreUsuario.Text = SesionUsuario.Usuario;
            // Cargo las categorias
            cargarCategorias();
        }

        // Fila seleccionada del dgv 
        private void dgvTablaGenerica_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Obtengo la fila que ha sido seleccionada en el dataGridView
            if (e.RowIndex >= 0)
            {
                filaTabla = dgvTablaGenerica.Rows[e.RowIndex];
                // Muestro los botones de eliminar y modificar fila.
                pbMostrarVentanEliminar.Visible = true;
                pbMostrarPanelActualizar.Visible = true;
                pbIconoMensaje.Visible = false;
                labelMensaje.Text = "";
            }

        }

        // Muestra la tabla con los jugadores
        private void lbJugadores_Click(object sender, EventArgs e)
        {
            // Cargo los jugadores en el tabla con los resultados de la consulta a la base de datos.
            dgvTablaGenerica.DataSource = model_admin.getJugadores();
            // Muestro la tablas
            panelPrincipal.Visible = true;
            // Guardo que tabla se ha utilizado
            nombreTabla = "jugadores";
            // Muestro las el numero de filas que tiene la tabla
            mostrarNumeroFilasTabla();
            // Muestro el nombre de la tabla
            mostrarTituloTablaEnUso(nombreTabla);
            // Muestra el icono de añadir usuarios
            pbMostrarPanelCrear.Image = imageList.Images[4];
            // Muestro el panel principal
            panelPrincipal.Visible = true;
            // Muestro la barra de busqueda
            panelBuscador.Visible = true;
        }

        // Muestra la tabla con las palabras  
        private void lbPalabras_Click(object sender, EventArgs e)
        {
            // Cargo las palabras en el tabla con los resultados de la consulta a la base de datos.
            dgvTablaGenerica.DataSource = model_admin.getPalabras();
            // Guardo que tabla se ha utilizado
            nombreTabla = "palabras";
            // Muestro las el numero de filas que tiene la tabla
            mostrarNumeroFilasTabla();
            // Muestra el icono de añadir palabra
            pbMostrarPanelCrear.Image = imageList.Images[5];
            // Muestro el nombre de la tabla
            mostrarTituloTablaEnUso(nombreTabla);
            // Muestro el panel principal
            panelPrincipal.Visible = true;
            // Muestro la barra de busqueda
            panelBuscador.Visible = true;
        }

        // Muestra el panel para actulizar un usuario o una palabra
        private void pbMostrarPanelActualizar_Click(object sender, EventArgs e)
        {
            // Si hay una fila seleccionada y es diferente de null.
            if (filaTabla.Cells[0].Value != null)
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
                    tbIdJugador.Text = filaTabla.Cells[0].Value.ToString();
                    // Mombre del usuario
                    tbJugador.Text = filaTabla.Cells[1].Value.ToString();
                    // Contraseña 
                    tbContraseña.Text = filaTabla.Cells[2].Value.ToString();
                    // La puntuacion
                    tbPuntuacion.Text = filaTabla.Cells[3].Value.ToString();
                    // Tipo de rol del usuario pueden ser jugador o administrador
                    cbTipoRol.Text = filaTabla.Cells[4].Value.ToString();
                    // Oculto panel
                    panelPalabras.Visible = false;
                    // Muestro el panel
                    panelJugador.Visible = true;
                    // Permito que se escriba en el campo puntuacion.
                    tbPuntuacion.Enabled = true;
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
                    tbIdPalabra.Text = filaTabla.Cells[0].Value.ToString();
                    // La palabra
                    tbPalabra.Text = filaTabla.Cells[1].Value.ToString();
                    // La pista
                    tbPista.Text = filaTabla.Cells[2].Value.ToString();
                    // Categoria de la palabra
                    cbCategorias.Text = filaTabla.Cells[3].Value.ToString();
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
                // Impido que se pueda añadir puntuacion, por defecto sera cero.
                tbPuntuacion.Enabled = false;

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
            cbCategorias.DisplayMember = "categoria";
            cbCategorias.ValueMember = "idPalabra";
            cbCategorias.DataSource = model_admin.getCategorias();
        }

        // Me dice el numero de fila que tien el datagridview
        private void mostrarNumeroFilasTabla()
        {
            // Muestro las filas que tiene la tabla
            int totalFilas = dgvTablaGenerica.RowCount;
        }

        //Muetra el nombre de la tabla que se esta utilizando
        private void mostrarTituloTablaEnUso(string nombre)
        {
            lbNombreTabla.Text = nombre;
        }

        // Elimina un registro de la tabla
        private void pbEliminar_Click(object sender, EventArgs e)
        {
            // Obtengo el identificador
            int id = (int)filaTabla.Cells[0].Value;
            // Obtengo el nombre
            String nombre = filaTabla.Cells[1].Value.ToString();
            // Mensaje que le aparecera al administrador.
            String message = "estas seguro de que quieres eliminar  " + nombre + " ?";
            // Titulo de la ventana emergente.
            String caption = "Eliminar " + nombreTabla;
            // Obtengo el resultado
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            // Si quiere eliminar 
            if (result == DialogResult.Yes)
            {
                // Para guardar el resultado de la consulta, numero de filas afectadas.
                int registroEliminado = 0;

                // Si la tabla que quiere elimianr es jugadores
                if (nombreTabla.Equals("jugadores"))
                {   // Realizo la peticion a la base de datos para eliminar al jugador por su nombre.
                    registroEliminado = model_admin.eliminarJugador(id);
                }
                else if (nombreTabla.Equals("palabras"))
                {   // Elimino  una palabra por su nombre.
                    registroEliminado = model_admin.eliminarPalabra(id);
                }

                // Si resultado de la sentencia sql es igual a uno,  quiere decir que se ha realizado con exito.
                if (registroEliminado == 1)
                {   // Muestro mensaje al administrador.       
                    mostrarMensaje("Acabas de eliminar la fila de la tabla " + nombreTabla);
                    // Actualizo el dataGridView
                    if (nombreTabla.Equals("jugadores"))
                    {   // Actualizo la el dgv con el resto de jugadores que no han sido eliminados.
                        dgvTablaGenerica.DataSource = model_admin.getJugadores();
                    }
                    else
                    {   // Actualizo el dgv con el resto de palabras de la base de datos.
                        dgvTablaGenerica.DataSource = model_admin.getPalabras();
                    }

                    // Muestro el numero de filas del dgv
                    mostrarNumeroFilasTabla();

                }
                else // Sino, no se ha podido eliminar
                {   // Muestro mensaje de error     
                    mostrarMensaje("Error, la fila no ha podido ser eliminada");
                }

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
            // Oculta mensaje de informacion
            pbIconoMensaje.Visible = false;
            labelMensaje.Text = "";

            // Muestro el numero de filas que tiene la tabla
            mostrarNumeroFilasTabla();

            // Actualizo la tabla de jugadores
            if (nombreTabla.Equals("jugadores"))
            {   // Realizo la consula a la base de datos a su tabla jugadoers
                dgvTablaGenerica.DataSource = model_admin.getJugadores();
            } // sino actualizo la tabla de palabras
            else if (nombreTabla.Equals("palabras"))
            {   // Realizo la consulta a la base de datos a tu tabla palabras
                dgvTablaGenerica.DataSource = model_admin.getPalabras();
            }

            // Muestro el panel menu principal
            panelPrincipal.Visible = true;

        }

        // Boton de accion panel jugador para crear o actulizar
        private void ButtonJugadorAceptar(object sender, EventArgs e)
        {
            //Obtengo los datos del formulario 
            // Identificador del jugador
            int idJugador = int.Parse(tbIdJugador.Text);
            // Nombre del usuario y lo convierto a minusculas
            string usuario = tbJugador.Text;
            // Contraseña del usuario
            string contraseña = tbContraseña.Text;
            // La puntuaciòn
            int puntuacion = int.Parse(tbPuntuacion.Text);
            // Tipo de rol que tiene este usurio
            string tipo = cbTipoRol.Text;

            // Si el formulario es correcto
            if (validarFormularioJugador(usuario, contraseña, tipo))
            {
                // Si quiere crer un nuevo jugador
                if (accionARealizar.Equals("crear"))
                {
                    // Creo un nuevo jugador
                    if (model_admin.registrarJugador(idJugador, usuario, contraseña, tipo) == 1)
                    {
                        // Muestro mensaje 
                        labelMensajeJugador.Text = "Acabas de crear un nuevo jugador";
                        // Actualizo la dgv con el nuevo registro que acabo de insertar en la base de datos.
                        dgvTablaGenerica.DataSource = model_admin.getJugadores();
                        // Obtengo el siguiente id de jugador por si el usuario quisiera seguire creado nuevos jugadores.
                        tbIdJugador.Text = dameSiguienteID().ToString();
                        // Limpio campos formulario
                        tbJugador.Text = "";
                        tbContraseña.Text = "";
                        cbTipoRol.Text = "";
                    }

                } // Si quiere actulizar los datos de un jugador
                else if (accionARealizar.Equals("actualizar"))
                {
                    // Actualizo los datos del jugador
                    if (model_admin.actualizarJugador(idJugador, usuario, contraseña, puntuacion, tipo) == 1)
                    {
                        labelMensajeJugador.Text = "Acabas de actualizar los datos del usuario.";
                    }
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
            int idPalabra = int.Parse(tbIdPalabra.Text);
            string palabra = tbPalabra.Text;
            string pista = tbPista.Text;
            string categoria = cbCategorias.Text;

            // Validar datos formulario
            if (validarFormularioPalabras(palabra, pista, categoria))
            {

                if (accionARealizar.Equals("crear"))
                {

                    // Si se ha insertado la palabra en la base de datos.
                    if (model_admin.registrarPalabra(idPalabra, palabra, pista, categoria) == 1)
                    {
                        // Muestro mensaje 
                        labelMensajePalabra.Text = "Acabas de crear una nueva palabra.";
                        // Actualizo la dgv con el nuevo registro que acabo de insertar en la base de datos.
                        dgvTablaGenerica.DataSource = model_admin.getPalabras();
                        // Muestro el nuevo identificador que se utilizara en el caso de seguir creando palabras.
                        tbIdPalabra.Text = dameSiguienteID().ToString();

                    }
                    else
                    {
                        labelMensajePalabra.Text = "La palabra no ha podido ser creada.";
                    }

                }
                else if (accionARealizar.Equals("actualizar"))
                {

                    // Actualizo los datos del jugador
                    if (model_admin.actualizarPalabra(idPalabra, palabra, pista, categoria) == 1)
                    {
                        labelMensajePalabra.Text = "Acabas de actualizar la palabra.";
                    }
                    else
                    {
                        labelMensajePalabra.Text = "La palabra no ha podido ser actualizada.";
                    }
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
            // Guardo la ultima fila de la tabla
            DataGridViewRow idRow = dgvTablaGenerica.Rows[dgvTablaGenerica.RowCount - 1];
            // Guardo el id
            int ultimoID = (int)idRow.Cells[0].Value;
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
                    string nombreAtiguo = filaTabla.Cells[1].Value.ToString();

                    // El usuario quiere cambiar de nombre
                    if (nombreAtiguo.ToLower() != usuario.ToLower())
                    {
                        // Compruebo si el nombre existe
                        if (model_admin.isUserExist(usuario))
                        {
                            validado = false;
                            error.SetError(tbJugador, "El usuario ya existe");
                        }
                        else
                        {
                            error.SetError(tbJugador, "");
                        }
                    }

                }

                // Si quiere crear 
                if (accionARealizar.Equals("crear"))
                {
                    if (model_admin.isUserExist(usuario))
                    {
                        validado = false;
                        error.SetError(tbJugador, "El usuario ya existe");
                    }
                    else
                    {
                        error.SetError(tbJugador, "");
                    }
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
                    string nombreAtiguo = filaTabla.Cells[1].Value.ToString();

                    // Quiere cambiar la palabra
                    if (nombreAtiguo.ToLower() != palabra.ToLower())
                    {
                        // Compruebo si la palabra existe
                        if (model_admin.isWordExist(palabra))
                        {
                            validado = false;
                            error.SetError(tbPalabra, "La palabra ya existe");
                        }
                        else
                        {
                            error.SetError(tbPalabra, "");
                        }
                    }

                }

                // Si quiere crear 
                if (accionARealizar.Equals("crear"))
                {
                    if (model_admin.isWordExist(palabra))
                    {
                        validado = false;
                        error.SetError(tbPalabra, "La palabra ya existe");
                    }
                    else
                    {
                        error.SetError(tbPalabra, "");
                    }
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

        // Cierra sesion y vuelve al menu de login
        private void pbCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        // Cierra la aplicacion
        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


        private void tbRealizarBusqueda(object sender, EventArgs e)
        {

            string texto = tbBuscar.Text;

            if (nombreTabla.Equals("jugadores"))
            {
                dgvTablaGenerica.DataSource = model_admin.buscadorJugadores( texto );
            }
            else if (nombreTabla.Equals("palabras"))
            {
                Console.WriteLine("buscar palabras por filtrado");
                dgvTablaGenerica.DataSource = model_admin.buscadorPalabras( texto );

            }


        }
    }
}
