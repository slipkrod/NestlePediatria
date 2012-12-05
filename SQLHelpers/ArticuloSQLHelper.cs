using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class ArticuloSQLHelper
    {
        public const string PARAMETRO_ARTICULOID = "@articuloID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_AUTOR = "@autor";
        public const string PARAMETRO_FECHA = "@fecha";
        public const string PARAMETRO_CATEGORIAID = "@categoriaID";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_DOCUMENTO = "@documento";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_ARTICULO = "dbo.spInsertArticulo";
        public const string UPDATE_ARTICULO = "dbo.spUpdateArticulo";
        public const string DELETE_ARTICULO = "dbo.spDeleteArticulo";
        public const string CONSULTA_ARTICULO = "dbo.spConsultaArticulo";
        public const string INSERTA_ARTICULO_PAIS = "dbo.spInsertArticuloPais";
        public const string CONSULTA_UN_ARTICULO = "dbo.spConsultaUnArticulo";
        public const string CONSULTA_ARTICULO_PAIS = "dbo.spConsultaArticuloPais";
        public const string DELETE_ARTICULO_PAIS = "dbo.spDeleteArticuloPais";
    }
}
