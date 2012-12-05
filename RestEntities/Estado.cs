using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Estado
    {
        [DataMember]
        public int Id;
        [DataMember]
        public string Nombre;
        public int PaisId;
    }
}