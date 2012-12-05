using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Evento
    {
        [DataMember]
        public int id;
        [DataMember]
        public string nombre;
        [DataMember]
        public string descripcion;
        [DataMember]
        public string fechaInicio;
        [DataMember]
        public string fechaFin;
        [DataMember]
        public string lugar;
        [DataMember]
        public string dirigido;
        [DataMember]
        public string foto;
        
    }
}