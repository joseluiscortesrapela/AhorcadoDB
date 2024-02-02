using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahorcado.Models
{
    public class Word
    {
        private int id;
        private string palabra;
        private string pista;
        private string categoria;

        public Word(int id, string palabra, string pista, string categoria)
        {
            this.id = id;
            this.palabra = palabra;
            this.pista = pista;
            this.categoria = categoria;
        }

        public int Id { get => id; set => id = value; }
        public string Palabra { get => palabra; set => palabra = value; }
        public string Pista { get => pista; set => pista = value; }
        public string Categoria { get => categoria; set => categoria = value; }
    }
}
