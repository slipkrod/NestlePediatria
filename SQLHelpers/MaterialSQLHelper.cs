using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class MaterialSQLHelper
    {
        public const string PARAMETRO_MATERIALID = "@materialID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_DESCRIPCION = "@descripcion";
        public const string PARAMETRO_AUTOR = "@autor";
        public const string PARAMETRO_FECHA = "@fecha";
        public const string PARAMETRO_FOTO = "@foto";
        public const string PARAMETRO_DOCUMENTO = "@documento";
        public const string PARAMETRO_PUBLICADO = "@publicado";


        public const string INSERTA_MATERIAL = "dbo.spInsertMaterial";
        public const string UPDATE_MATERIAL = "dbo.spUpdateMaterial";
        public const string DELETE_MATERIAL = "dbo.spDeleteMaterial";
        public const string CONSULTA_MATERIAL = "dbo.spConsultaMaterial";
        public const string INSERTA_MATERIAL_PAIS = "dbo.spInsertMaterialPais";
        public const string CONSULTA_UN_MATERIAL = "dbo.spConsultaUnMaterial";
        public const string CONSULTA_MATERIAL_PAIS = "dbo.spConsultaMaterialPais";
        public const string DELETE_MATERIAL_PAIS = "dbo.spDeleteMaterialPais";
    }
}
