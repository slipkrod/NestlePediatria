using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Pais
    {
        [DataMember]
        public int Id;
        [DataMember]
        public string Nombre;
    }
}