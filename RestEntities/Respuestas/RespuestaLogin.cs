using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities.Respuestas
{
    [DataContract]
    public class RespuestaLogin
    {
        [DataMember]
        public int Status;

        [DataMember]
        public string Message;

        [DataMember]
        public int DoctorId;

        [DataMember]
        public string DoctorName;
    }

}