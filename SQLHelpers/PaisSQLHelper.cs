using System;
using System.Collections.Generic;
using System.Text;

namespace SQLHelpers
{
    public class PaisSQLHelper
    {
        public const string PARAMETRO_ID = "@paisID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_PAIS = "dbo.spInsertPais";
        public const string UPDATE_PAIS = "dbo.spUpdatePais";
        public const string DELETE_PAIS = "dbo.spDeletePais";
        public const string CONSULTA_PAIS = "dbo.spConsultaPais";
        public const string CONSULTA_PAIS_ACTIVO = "spConsultaPaisActivo";
        public const string CONSULTA_UN_PAIS = "spConsultaUnPais";
    }
}
