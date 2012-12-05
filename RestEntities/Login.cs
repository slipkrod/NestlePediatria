using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestEntities
{
    [DataContract]
    public class Login
    {
        [DataMember(Name="usuario")]
        public string Usuario;
        
        [DataMember(Name="password")]
        public string Password;
    }
}