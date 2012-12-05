using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Medida
    {
        [DataMember]
        public int id;

        [DataMember]
        public int paciente_id;

        public DateTime fecha_medicion;

        [DataMember(Name="age")]
        public int edad;

        [DataMember(Name = "size")]
        public int talla;

        [DataMember(Name = "weight")]
        public int peso;
    }
}