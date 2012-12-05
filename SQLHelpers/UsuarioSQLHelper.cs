using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLHelpers
{
    public class UsuarioSQLHelper
    {
        public const string PARAMETRO_USUARIOID = "@usuarioID";
        public const string PARAMETRO_USUARIO = "@usuario";
        public const string PARAMETRO_PASSWORD = "@password";
        public const string PARAMETRO_ROL_ID = "@rolID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_HABILITADO = "@habilitado";
        public const string PARAMETRO_VERIFICACION = "@verifica";
        public const string PARAMETRO_FLAG = "@flag";

        public const string INSERTA_USUARIO = "dbo.spInsertUsuario";
        public const string CONSULTA_USUARIO = "dbo.spConsultaUsuario";
        public const string UPDATE_USUARIO_PASSWORD = "dbo.spUpdateUsuario";
        public const string DELETE_USUARIO = "dbo.spDeleteUsuario";
        public const string VERIFICA_USUARIO = "dbo.spVerificaPassword";
        public const string CONSULTA_UN_USUARIO = "dbo.spConsultaUnUsuario";
        public const string UPDATE_USUARIO = "dbo.spUdateUsuario2";
        public const string INSERTA_USUARIO_PAIS = "dbo.spInsertUsuarioPais";
        public const string DELETE_USUARIO_PAIS = "dbo.spDeleteUsuarioPais";
        public const string DELETE_USUARIO_PAIS_D = "dbo.spDeleteUsuarioPaisD";
        public const string CONSULTA_USUARIO_PAIS = "dbo.spConsultaUsuarioPais";

    }
}
