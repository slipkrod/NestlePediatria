using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Categoria
    {
        [DataMember]
        public int id;
        [DataMember]
        public string Nombre;
        [DataMember]
        public List<Articulo> Articulos;
    }
}