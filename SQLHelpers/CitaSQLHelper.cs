using System;
using System.Collections.Generic;
using System.Text;

namespace SQLHelpers
{
    public class CitaSQLHelper
    {
        public const string PARAMETRO_PARAMETRO_ID = "@citaID";
        public const string PARAMETRO_HORA_INICIO = "@hora_inicio";
        public const string PARAMETRO_HORA_FINAL = "@hora_final";
        public const string PARAMETRO_FLAG = "@flag";
        public const string PARAMETRO_FECHA = "@fecha";
        

        public const string INSERTA_CITA = "dbo.spInsertCita";
        public const string UPDATE_CITA = "dbo.spUpdateCita";
        public const string DELETE_CITA = "dbo.spDeleteCita";
        public const string CONSULTA_CITA = "dbo.spConsultaCita";
        public const string CONSULTA_UN_CITA = "dbo.spConsultaUnCita";
    }
}
