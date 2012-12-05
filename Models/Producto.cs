using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Producto
    {
        private int id;
        private string nombre;
        private string descripcion;
        private int padecimiento_id;
        private Byte[] foto;
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

        public int PadecimientoId 
        {
            get { return padecimiento_id; }
            set { padecimiento_id = value;}
        }

        public Byte[] Foto 
        {
            get { return foto; }
            set { foto = value; }
        }
        public int Publicado
        {
            get { return publicado; }
            set { publicado = value; }
        }
    }
}
