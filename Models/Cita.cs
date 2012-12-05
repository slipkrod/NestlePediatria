using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Cita
    {
        private int id;
        private DateTime horaInicio;
        private DateTime horaFinal;
        private string nombre;
        private int pacienteId;
       

        public int Id 
        {
            get { return id; }
            set {id = value; }
        }

        public DateTime HoraInicio
        {
            get { return horaInicio ; }
            set { horaInicio = value; }
        }

        public DateTime HoraFinal
        {
            get { return horaFinal ; }
            set { horaFinal = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int PacienteId 
        {
            get { return pacienteId; }
            set { pacienteId = value; }
        }
    }
}
