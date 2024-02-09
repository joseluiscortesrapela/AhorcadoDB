using System;

namespace Ahorcado
{
    internal class SesionUsuario
    {

        private static int id;
        private static String usuario;
        private static String contraseña;
        private static int puntuacion;
        private static String tipo;


        public static int Id { get => id; set => id = value; }
        public static string Usuario { get => usuario; set => usuario = value; }
        public static string Contraseña { get => contraseña; set => contraseña = value; }
        public static int Puntuacion { get => puntuacion; set => puntuacion = value; }
        public static string Tipo { get => tipo; set => tipo = value; }
    }
}
