using RestEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Rest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IArticleService" in both code and config file together.
    [ServiceContract]
    public interface IArticleService
    {
        [WebGet(UriTemplate = "Categories/{pais_id}", ResponseFormat = WebMessageFormat.Json)]
        List<Categoria> ListaCategorias(string pais_id);

        [WebGet(UriTemplate = "List/{pais_id}/{cat_id}", ResponseFormat = WebMessageFormat.Json)]
        List<Categoria> ListaArticulos(string pais_id, string cat_id);

        [WebGet(UriTemplate = "Annales", ResponseFormat = WebMessageFormat.Json)]
        List<Annal> ListaAnnales();

        [WebGet(UriTemplate = "Nidito", ResponseFormat = WebMessageFormat.Json)]
        List<Nidito> ListaNidito();

        [WebGet(UriTemplate = "Article/{idArticulo}", ResponseFormat = WebMessageFormat.Json)]
        Articulo DetallesArticulo(string idArticulo);

        [WebGet(UriTemplate = "Nidito/{idNidito}", ResponseFormat = WebMessageFormat.Json)]
        Nidito DetallesNidito(string idNidito);

        [WebGet(UriTemplate = "Annal/{idAnnal}", ResponseFormat = WebMessageFormat.Json)]
        Annal DetallesAnnal(string idAnnal);

        [WebGet(UriTemplate = "GetImage/{list}/{articleId}")]
        Stream ObtenImagen(string list, string articleId);

        [WebGet(UriTemplate = "GetPdf/{list}/{articleId}")]
        Stream ObtenPdf(string list, string articleId);
    }
}
