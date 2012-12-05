using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Paciente
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string fecha_nac { get; set; }
        [DataMember]
        public string lugar_nac { get; set; }
        [DataMember]
        public string ciudad_nac { get; set; }
        [DataMember]
        public string grupo_sanguineo { get; set; }
        [DataMember]
        public string rh { get; set; }
        [DataMember]
        public string alergico { get; set; }
        [DataMember]
        public string nombre_madre { get; set; }
        [DataMember]
        public string ocupacion_madre { get; set; }
        [DataMember]
        public string nombre_padre { get; set; }
        [DataMember]
        public string ocupacion_padre { get; set; }
        [DataMember]
        public string calle { get; set; }
        [DataMember]
        public string colonia { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string cp { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string nombre_encargado { get; set; }
        [DataMember]
        public string telefono_encargado { get; set; }
        [DataMember]
        public string notas { get; set; }
        [DataMember]
        public int doctor_id { get; set; }
    }
}