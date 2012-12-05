using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class AnnalesSQLHelper
    {
        public const string PARAMETRO_ID = "@annalesID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_AUTOR = "@autor";
        public const string PARAMETRO_FECHA = "@fecha";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_DOCUMENTO = "@documento";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_ANNALES = "dbo.spInsertAnnales";
        public const string UPDATE_ANNALES = "dbo.spUpdateAnnales";
        public const string DELETE_ANNALES = "dbo.spDeleteAnnales";
        public const string CONSULTA_ANNALES = "dbo.spConsultaAnnales";
        public const string CONSULTA_UN_ANNALES = "dbo.spConsultaUnAnnales";
        
    }
}
