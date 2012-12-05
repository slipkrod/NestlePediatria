using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Rol
    {
         private int id;
        private string tipoRol;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string TipoRol
        {
            get { return tipoRol; }
            set { tipoRol = value; }
        }
    }
}
