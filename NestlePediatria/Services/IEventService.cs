using RestEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace Rest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEventService" in both code and config file together.
    [ServiceContract]
    public interface IEventService
    {
        [WebGet(UriTemplate = "List/{pais_id}", ResponseFormat = WebMessageFormat.Json)]
        List<Evento> ListaEstados(string pais_id);

        [WebGet(UriTemplate = "GetImage/{id}")]
        Stream ObtenImagen(string id);
    }
}
