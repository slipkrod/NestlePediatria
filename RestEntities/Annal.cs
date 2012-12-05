using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Annal
    {
        [DataMember]
        public int id;
        [DataMember]
        public string nombre;
        [DataMember]
        public string descripcion;
        [DataMember]
        public string autor;
        [DataMember]
        public string foto;
        [DataMember]
        public string documento;
    }
}