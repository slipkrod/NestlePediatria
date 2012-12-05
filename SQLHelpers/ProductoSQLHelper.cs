using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class ProductoSQLHelper
    {
        public const string PARAMETRO_ID = "@productoID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_PADECIMIENTO_ID = "@padecimientoID";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_PRODUCTO = "dbo.spInsertProducto";
        public const string INSERTA_PRODUCTO_PAIS = "dbo.spInsertProductoPais";
        public const string UPDATE_PRODUCTO = "dbo.spUpdateProducto";
        public const string DELETE_PRODUCTO = "dbo.spDeleteProducto";
        public const string CONSULTA_PRODUCTO = "dbo.spConsultaProducto";
        public const string CONSULTA_PRODUCTO_ACTIVO = "spConsultaProductoActivo";

        public const string CONSULTA_UN_PRODUCTO = "dbo.spConsultaUnProducto";
        public const string CONSULTA_PRODUCTO_PAIS = "dbo.spConsultaProductoPais";
        public const string DELETE_PRODUCTO_PAIS = "dbo.spDeleteProductoPais";
    }
}
