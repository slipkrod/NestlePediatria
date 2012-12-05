using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities.Respuestas
{
    [DataContract]
    public class RespuestaRegistroMedida
    {
        [DataMember]
        public int Status;

        [DataMember]
        public string Message;

        [DataMember]
        public int MedidaId;
    }

}