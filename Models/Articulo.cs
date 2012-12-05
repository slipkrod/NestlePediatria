using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Articulo
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string autor;
        private string fecha;
        private int categoria_id;
        private Byte[] foto;
        private Byte[] documento;
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

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public int CategoriaId
        {
            get { return categoria_id; }
            set { categoria_id = value; }
        }

        public Byte[] Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        public Byte[] Documento
        {
            get { return documento; }
            set { documento = value; }
        }

        public int Publicado
        {
            get { return publicado; }
            set { publicado = value; }
        }
    }
}
