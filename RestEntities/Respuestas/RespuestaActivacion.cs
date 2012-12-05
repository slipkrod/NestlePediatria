using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities.Respuestas
{
    [DataContract]
    public class RespuestaActivacion
    {
        [DataMember]
        public bool Activated;

        [DataMember]
        public string Message;
    }
}