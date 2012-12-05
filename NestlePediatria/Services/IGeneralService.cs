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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGeneralService" in both code and config file together.
    [ServiceContract]
    public interface IGeneralService
    {
        [WebGet(UriTemplate = "Countries", ResponseFormat = WebMessageFormat.Json)]
        List<Pais> ListaPaises();

        [WebGet(UriTemplate = "States/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Estado> ListaEstados(string id);

        [WebGet(UriTemplate = "Cities/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Ciudad> ListaCiudades(string id);

        [WebGet(UriTemplate = "Specialties", ResponseFormat = WebMessageFormat.Json)]
        List<Especialidad> ListaEspecialidades();

        [WebGet(UriTemplate = "SubSpecialties", ResponseFormat = WebMessageFormat.Json)]
        List<SubEspecialidad> ListaSubEspecialidades();

        [WebGet(UriTemplate = "MealPlanner/GetPdf/{id}")]
        Stream ObtenPdfMealPlanner(string id);

        [WebGet(UriTemplate = "Product/GetImage/{id}")]
        Stream ObtenImagenProducto(string id);

        [WebGet(UriTemplate = "Material/GetPdf/{id}")]
        Stream ObtenPdfMaterial(string id);

        [WebGet(UriTemplate = "Material/GetImage/{id}")]
        Stream ObtenImagenMaterial(string id);
    }
}
