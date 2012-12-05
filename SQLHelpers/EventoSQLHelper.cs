using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class EventoSQLHelper
    {
        public const string PARAMETRO_ID = "@eventoID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_FECHAINICIO = "@fechaInicio";
        public const string PARAMETRO_FECHAFIN= "@fechaFin";
        public const string PARAMETRO_LUGAR = "@lugar";
        public const string PARAMETRO_DIRIGIDO = "@dirigido";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_PUBLICADO = "@publicado";



        public const string INSERTA_EVENTO = "dbo.spInsertEvento";
        public const string UPDATE_EVENTO = "dbo.spUpdateEvento";
        public const string DELETE_EVENTO = "dbo.spDeleteEvento";
        public const string CONSULTA_EVENTO = "dbo.spConsultaEvento";
        public const string INSERTA_EVENTO_PAIS = "dbo.spInsertEventoPais";
        public const string CONSULTA_UN_EVENTO = "dbo.spConsultaUnEvento";
        public const string CONSULTA_EVENTO_PAIS = "dbo.spConsultaEventoPais";
        public const string DELETE_EVENTO_PAIS = "dbo.spDeleteEventoPais";
    }
}
