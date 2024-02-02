using Ahorcado.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Ahorcado.Utilidades
{
    public static class ProcesarFicherosXML
    {

        // Devuelve la lista de jugadores tras extraer los datos de jugadores.xml
        public static List<Jugador> dameListaJugadores()
        {

            // Donde tengo el fichero xml
            string archivoXML = @"..\..\Xml\jugadores.xml";

            // Creo un array que guardara objetos del tipo Jugador
            List<Jugador> jugadores = new List<Jugador>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(archivoXML);

                XmlNodeList jugadorNodes = xmlDoc.SelectNodes("/Jugadores/Jugador");

                foreach (XmlNode jugadorNode in jugadorNodes)
                {
                    int id = int.Parse(jugadorNode.SelectSingleNode("id").InnerText);
                    string nombre = jugadorNode.SelectSingleNode("nombre").InnerText;
                    string contraseña = jugadorNode.SelectSingleNode("contraseña").InnerText;
                    int puntuacion = int.Parse(jugadorNode.SelectSingleNode("puntuacion").InnerText);
                    string rol = jugadorNode.SelectSingleNode("rol").InnerText;


                    // Creo el jugador
                    Jugador jugador = new Jugador(id, nombre, contraseña, puntuacion, rol);

                    // Lo guardo en el array
                    jugadores.Add(jugador);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo XML: " + ex.Message);
            }


            return jugadores;
        }

        // Lee palabras.xml y devuelve un array de palabras.
        public static List<Word> dameListaPalabras()
        {
            string archivoXML = @"..\..\Xml\palabras.xml";
            // Array de objetos de tipo Word
            List<Word> palabras = new List<Word>();


            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(archivoXML);

                XmlNodeList palabraNodes = xmlDoc.SelectNodes("/WordList/Word");

                foreach (XmlNode palabraNode in palabraNodes)
                {
                    int id = int.Parse(palabraNode.SelectSingleNode("id").InnerText);
                    string word = palabraNode.SelectSingleNode("palabra").InnerText;
                    string pìsta = palabraNode.SelectSingleNode("pista").InnerText;
                    string categoria = palabraNode.SelectSingleNode("categoria").InnerText;

                    // Instancio e inicializo una nueva palabra
                    Word palabra = new Word(id, word, pìsta, categoria);

                    // Lo guardo en el array
                    palabras.Add(palabra);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el archivo XML: " + ex.Message);
            }


            return palabras;
        }

        // Exporta el contenido del dgv de jugadores a un fichero xml.
        public static void crearJugadoresXML(DataGridView dgvJugadores)
        {
            string archivoXML = @"..\..\Xml\jugadores.xml";


            try
            {
                // Verifica si el archivo XML existe y lo elimina si es necesario.
                if (File.Exists(archivoXML))
                {
                    File.Delete(archivoXML);
                }

                // Crea un nuevo documento XML.
                XmlDocument xmlDoc = new XmlDocument();
                // Agrega la declaración XML manualmente.
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDeclaration);
                // Crea el elemento raíz del documento.
                XmlElement rootElement = xmlDoc.CreateElement("Jugadores");
                xmlDoc.AppendChild(rootElement);

                // Recorre las filas del DataGridView y agrega cada jugador como un elemento al documento XML.
                foreach (DataGridViewRow fila in dgvJugadores.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        XmlElement jugadorElement = xmlDoc.CreateElement("Jugador");

                        // Crea elementos para cada columna y agrega sus valores.
                        XmlElement idElement = xmlDoc.CreateElement("id");
                        idElement.InnerText = fila.Cells[0].Value.ToString();
                        jugadorElement.AppendChild(idElement);

                        XmlElement nombreElement = xmlDoc.CreateElement("nombre");
                        nombreElement.InnerText = fila.Cells["nombre"].Value.ToString();
                        jugadorElement.AppendChild(nombreElement);

                        XmlElement contraseñaElement = xmlDoc.CreateElement("contraseña");
                        contraseñaElement.InnerText = fila.Cells["contraseña"].Value.ToString();
                        jugadorElement.AppendChild(contraseñaElement);

                        XmlElement puntuacionElement = xmlDoc.CreateElement("puntuacion");
                        puntuacionElement.InnerText = fila.Cells["puntuacion"].Value.ToString();
                        jugadorElement.AppendChild(puntuacionElement);

                        XmlElement rolElement = xmlDoc.CreateElement("rol");
                        rolElement.InnerText = fila.Cells["rol"].Value.ToString();
                        jugadorElement.AppendChild(rolElement);

                        rootElement.AppendChild(jugadorElement);
                    }
                }

                // Guarda el documento XML en el archivo.
                xmlDoc.Save(archivoXML);

                Console.WriteLine("Datos guardados en " + archivoXML);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar los datos en el archivo XML: " + ex.Message);
            }
        }

        // Exporta el contenido del dgv de palabras a un fichero xml.
        public static void crearPalabrasXML(DataGridView dgvPalabras)
        {
            string archivoXML = @"..\..\Xml\palabras.xml";

            try
            {
                // Verifica si el archivo XML existe y lo elimina si es necesario.
                if (File.Exists(archivoXML))
                {
                    File.Delete(archivoXML);
                }

                // Crea un nuevo documento XML.
                XmlDocument xmlDoc = new XmlDocument();
                // Agrega la declaración XML manualmente.
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlDeclaration);
                // Crea el elemento raíz del documento.
                XmlElement rootElement = xmlDoc.CreateElement("WordList");
                xmlDoc.AppendChild(rootElement);

                // Recorre las filas del DataGridView y agrega cada jugador como un elemento al documento XML.
                foreach (DataGridViewRow fila in dgvPalabras.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        XmlElement wordElement = xmlDoc.CreateElement("Word");

                        // Crea elementos para cada columna y agrega sus valores.
                        XmlElement idElement = xmlDoc.CreateElement("id");
                        idElement.InnerText = fila.Cells[0].Value.ToString();
                        wordElement.AppendChild(idElement);

                        XmlElement palabraElement = xmlDoc.CreateElement("palabra");
                        palabraElement.InnerText = fila.Cells["palabra"].Value.ToString();
                        wordElement.AppendChild(palabraElement);

                        XmlElement pistaElement = xmlDoc.CreateElement("pista");
                        pistaElement.InnerText = fila.Cells["pista"].Value.ToString();
                        wordElement.AppendChild(pistaElement);

                        XmlElement categoriaElement = xmlDoc.CreateElement("categoria");
                        categoriaElement.InnerText = fila.Cells["categoria"].Value.ToString();
                        wordElement.AppendChild(categoriaElement);
                        // Añade nodo del tipo Word 
                        rootElement.AppendChild(wordElement);
                    }
                }

                // Guarda el documento XML en el archivo.
                xmlDoc.Save(archivoXML);

                Console.WriteLine("Datos guardados en " + archivoXML);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar los datos en el archivo XML: " + ex.Message);
            }
        }

        // Actualiza l apuntuacion del jugador
        public static void ActualizarPuntuacionJugador(int idJugador, int nuevaPuntuacion)
        {

            string archivoXML = @"..\..\Xml\jugadores.xml";
            // Cargar el documento XML
            XDocument xdoc = XDocument.Load(archivoXML);

            // Buscar el jugador por su ID
            var jugador = xdoc.Descendants("Jugador")
                .Where(j => (int)j.Element("id") == idJugador)
                .FirstOrDefault();

            if (jugador != null)
            {
                // Actualizar la puntuación del jugador
                jugador.Element("puntuacion").Value = nuevaPuntuacion.ToString();
                // Guardar el archivo XML con los cambios
                xdoc.Save(archivoXML);
            }
            else
            {
                Console.WriteLine("Usuario no encontrado en jugadores.xml");
            }



        }

        public static bool AgregarJugador(int id, string nombre, string contraseña)
        {
            bool creado = false;

            string archivoXML = @"..\..\Xml\jugadores.xml";

            // Carga el archivo XML existente en un objeto XDocument
            XDocument xmlDocument = XDocument.Load(archivoXML);

            // Crea un nuevo elemento Jugador
            XElement nuevoJugador = new XElement("Jugador",
                                    new XElement("id", id),
                                    new XElement("nombre", nombre),
                                    new XElement("contraseña", contraseña),
                                    new XElement("puntuacion", "0"),
                                    new XElement("rol", "Jugador")
            );

            // Agrega el nuevo jugador al elemento Jugadores
            xmlDocument.Element("Jugadores").Add(nuevoJugador);

            // Guarda el documento XML de nuevo en el archivo
            xmlDocument.Save(archivoXML);

            creado = true;

            Console.WriteLine("Acabas de registra un nuevo jugador en el fichero jugadores.xml");

            return creado;


        }

    }
}
