using System.ServiceModel;
using System.ServiceModel.Web;
using RestEntities;
using RestEntities.Respuestas;
using System.Collections.Generic;

namespace Rest.Services
{

    [ServiceContract]
    public interface IDoctorService
    {
        [OperationContract]
        [WebInvoke(
            UriTemplate = "Register",
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        RespuestaRegistro Register(Doctor doctor);

        [OperationContract]
        [WebGet(
            UriTemplate = "GetInfo/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        Doctor GetInfo(string id);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "Update",
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        RespuestaRegistro Update(Doctor doctor);

        [OperationContract]
        [WebGet(
            UriTemplate = "Activate/{codigoStr}/{pais}",
            ResponseFormat = WebMessageFormat.Json)]
        RespuestaActivacion Activate(string codigoStr, string pais);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "Login",
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        RespuestaLogin Login(Login login);

        [OperationContract]
        [WebGet(
            UriTemplate = "LoginG/{mail}/{password}",
            ResponseFormat = WebMessageFormat.Json)]
        RespuestaRegistro LoginG(string mail, string password);

        [OperationContract]
        [WebGet(
            UriTemplate = "ContactList/{id}", 
            ResponseFormat = WebMessageFormat.Json)]
        List<Paciente> ObtenPacientes(string id);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "AddContact",
            Method = "POST", 
            ResponseFormat = WebMessageFormat.Json)]
        RespuestaRegistroContacto AgregaContacto(Paciente paciente);

        [OperationContract]
        [WebInvoke(
            UriTemplate = "UpdateContact",
            Method = "POST",
            ResponseFormat = WebMessageFormat.Json)]
        int ActualizaContacto(Paciente paciente);

        //[OperationContract]
        //[WebInvoke(
        //    UriTemplate = "AddMeasure",
        //    Method = "POST",
        //    ResponseFormat = WebMessageFormat.Json)]
        //RespuestaRegistroMedida AgregaMedida(Medida medida);

        //[OperationContract]
        //[WebGet(
        //    UriTemplate = "MeasureHistory/{paciente_id}",
        //    ResponseFormat = WebMessageFormat.Json)]
        //List<Medida> ListaMedidas(string paciente_id);
    }
}