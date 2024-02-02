﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahorcado.Conexion
{
    internal class OperacionesBaseDatos
    {


        public static bool importarBaseDatos(string rutaSql)
        {

            string script = File.ReadAllText(rutaSql);

            // Creo la conexion con la base de datos.
            MySqlConnection conexion = ConexionBaseDatos.getConexion();
            // la abro.
            conexion.Open();

            MySqlCommand comando = new MySqlCommand(script, conexion);
            comando.ExecuteNonQuery();

            Console.WriteLine("Importada base datos!");

            return true;
        }



    }
}
