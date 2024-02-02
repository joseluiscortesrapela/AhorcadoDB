using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ahorcado.Conexion
{
    class ConexionBaseDatos
    {


        // Establece la conezion a la bsae de datos
        public static MySqlConnection getConexion()
        {
            // Server name
            String server = "localhost";
            // Database name
            String dataBase = "ahorcado";
            // User name
            String user = "root";
            // Password for this user.
            String password = "1234";

            // Tados de la conexion
            String parameters = "server=" + server + ";database=" + dataBase + "; uid=" + user + "; pwd=" + password;

            MySqlConnection conexion = new MySqlConnection(parameters);

            return conexion;
        }







    }
}
