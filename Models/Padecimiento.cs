using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Padecimiento
    {
        private int id;
        private string nombre;
        private string descripcion;
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

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public int Publicado
        {
            get { return publicado; }
            set { publicado = value; }
        }
        
    }
}
