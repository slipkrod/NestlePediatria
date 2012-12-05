using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Doctor
    {
        [DataMember]
        public int id;
        [DataMember]
        public string usuario;
        [DataMember]
        public string password;
        [DataMember]
        public string nombre;
        [DataMember]
        public string apellido;
        [DataMember]
        public string genero;
        [DataMember]
        public string fecha_nac;
        [DataMember]
        public int pais_id;
        [DataMember]
        public int estado_id;
        [DataMember]
        public int? ciudad_id;
        [DataMember]
        public string telefono;
        [DataMember]
        public int especialidad_id;
        [DataMember]
        public int subespecialidad_id;
        [DataMember]
        public string residente;
        [DataMember]
        public int anio_residencia;
        [DataMember]
        public string consulta_institucion;
        [DataMember]
        public string institucion;
        [DataMember]
        public string consulta_privada;
    }
}