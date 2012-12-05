using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Usuario
    {
        private int id;
        private string usuario;
        private string password;
        private int rol_id;
        private string nombre;
        private int habilitado;

        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        public string Usuarios
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public int RolId
        {
            get { return rol_id; }
            set { rol_id = value; }
        }

        public string Nombre 
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Habilitado 
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
    }
}
