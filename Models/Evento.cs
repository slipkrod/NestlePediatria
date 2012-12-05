using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Evento
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string fechaInicio;
        private string fechaFin;
        private string lugar;
        private string dirigido;
        private Byte[] foto;
        private int publicado;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Id 
        {
            get{return id;}
            set{id=value;}
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string FechaInicio {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public string FechaFinal
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public string Lugar
        {
            get { return lugar; }
            set { lugar = value; }
        }

        public string Dirigido
        {
            get { return dirigido; }
            set { dirigido = value; }
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
