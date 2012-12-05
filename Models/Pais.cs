using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Pais
    {
        private int id;
        private string nombre;
        private int publicado;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre 
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Publicado
        {
            get { return publicado; }
            set { publicado = value; }
        }
    }
}
