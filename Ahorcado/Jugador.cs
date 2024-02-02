using System;

namespace Ahorcado { 

    public class Jugador
    {

        private int id;
        private string nombre;
        private string contraseña;
        private int puntuacion;
        private string rol;

        public Jugador()
        {
        }

        public Jugador(int id, string nombre, string contraseña, int puntuacion, string rol)
        {
            this.id = id;
            this.nombre = nombre;
            this.contraseña = contraseña;
            this.puntuacion = puntuacion;
            this.rol = rol;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public int Puntuacion { get => puntuacion; set => puntuacion = value; }
        public string Rol { get => rol; set => rol = value; }
    }
}