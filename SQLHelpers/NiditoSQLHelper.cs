using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class NiditoSQLHelper
    {
        public const string PARAMETRO_ID = "@niditoID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_AUTOR = "@autor";
        public const string PARAMETRO_FECHA = "@fecha";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_DOCUMENTO = "@documento";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_NIDITO = "dbo.spInsertNidito";
        public const string UPDATE_NIDITO = "dbo.spUpdateNidito";
        public const string DELETE_NIDITO = "dbo.spDeleteNidito";
        public const string CONSULTA_NIDITO = "dbo.spConsultaNidito";
        public const string CONSULTA_UN_NIDITO = "dbo.spConsultaUnNidito";
        public const string DELETE_NIDITO_PAIS = "dbo.spDeleteNiditoPais";
    }
}
