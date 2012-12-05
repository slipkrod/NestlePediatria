using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class PadecimientoSQLHelper
    {
        public const string PARAMETRO_PADECIMIENTOID = "@padecimientoID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_PUBLICADO = "@publicado";

        public const string INSERTA_PADECIMIENTO = "dbo.spInsertPadecimiento";
        public const string CONSULTA_PADECIMIENTO = "dbo.spConsultaPadecimiento";
        public const string UPDATE_PADECIMIENTO = "dbo.spUpdatePadecimiento";
        public const string DELETE_PADECIMIENTO = "dbo.spDeletePadecimiento";
        public const string CONSULTA_PADECIMIENTO_ACTIVO = "dbo.spConsultaPadecimientoActivo";
        public const string CONSULTA_UN_PADECIMIENTO = "dbo.spConsultaUnPadecimiento";
    }
}
