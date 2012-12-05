using System;
using System.Collections.Generic;
using System.Text;

namespace SQLHelpers
{
    public class CategoriaSQLHelper
    {
        public const string PARAMETRO_CATEGORIAID = "@categoriaID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_PUBLICADO = "@publicado";

        public const string INSERTA_CATEGORIA = "dbo.spInsertCategoria";
        public const string CONSULTA_CATEGORIA = "dbo.spConsultaCategoria";
        public const string CONSULTA_CATEGORIA_ACTIVA = "dbo.spConsultaCategoriaActiva";
        public const string UPDATE_CATEGORIA = "dbo.spUpdateCategoria";
        public const string DELETE_CATEGORIA = "dbo.spDeleteCategoria";
        public const string CONSULTA_UNA_CATEGORIA = "dbo.spConsultaUnaCategoria";
    }
}
