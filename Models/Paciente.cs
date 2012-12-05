using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Paciente
    {
        private int id;
        private string nombre;
        private string sexo;
        private string fechaNac;
        private string lugarNac;
        private string ciudadNac;
        private string grupoSanguineo;
        private string rh;
        private string alergico;
        private string madre;
        private string ocupacionMadre;
        private string padre;
        private string ocupacionPadre;
        private string calle;
        private string colonia;
        private string ciudad;
        private string estado;
        private string codigoPostal;
        private string telefono;
        private string correo;
        private string nombreEncargado;
        private string telefonoEncargado;
        private string notas;
        private int doctorId;

        public int ID 
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public string FechaNac
        {
            get { return fechaNac; }
            set { fechaNac = value; }
        }

        public string LugarNac
        {
            get { return lugarNac; }
            set { lugarNac = value; }
        }

        public string CiudadNac
        {
            get { return ciudadNac; }
            set {  ciudadNac = value; }
        }

        public string GrupoSanguineo
        {
            get { return grupoSanguineo; }
            set { grupoSanguineo = value; }
        }

        public string RH
        {
            get { return rh; }
            set { rh = value; }
        }

        public string Alergico
        {
            get { return alergico; }
            set {  alergico= value; }
        }

        public string Madre
        {
            get { return madre; }
            set { madre = value; }
        }

        public string OcupacionMadre
        {
            get { return ocupacionMadre; }
            set { ocupacionMadre = value; }
        }

        public string Padre
        {
            get { return padre; }
            set { padre = value; }
        }

        public string OcupacionPadre
        {
            get { return ocupacionPadre; }
            set { ocupacionPadre = value; }
        }

        public string Calle
        {
            get { return calle; }
            set {  calle= value; }
        }

        public string Colonia
        {
            get { return colonia; }
            set { colonia = value; }
        }

        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string CodigoPostal
        {
            get { return codigoPostal; }
            set { codigoPostal = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public string NombreEncargado
        {
            get { return nombreEncargado; }
            set { nombreEncargado = value; }
        }

        public string TelefonoEncargado
        {
            get { return telefonoEncargado; }
            set { telefonoEncargado = value; }
        }

        public string Notas
        {
            get { return notas; }
            set { notas = value; }
        }

        public int DoctorID
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
    }
}
