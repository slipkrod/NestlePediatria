using System;
using System.Collections.Generic;
using System.Text;

namespace SQLHelpers
{
    public class PacienteSQLHelper
    {
        public const string PARAMETRO_ID = "@pacienteID";
        public const string PARAMETRO_NOMBRE = "@nombre";
        public const string PARAMETRO_SEXO = "@sexo";
        public const string PARAMETRO_FECHA_NAC = "@fechaNac";
        public const string PARAMETRO_LUGAR_NAC = "@lugarNAc";
        public const string PARAMETRO_CIUDAD_NAC = "@ciudadNac";
        public const string PARAMETRO_GRUPO_SANGUINEO = "@grupo";
        public const string PARAMETRO_RH = "@rh";
        public const string PARAMETRO_ALERGICO = "@alergico";
        public const string PARAMETRO_MADRE = "@madre";
        public const string PARAMETRO_OCUPACION_MADRE = "@ocupacion_madre";
        public const string PARAMETRO_PADRE = "@padre";
        public const string PARAMETRO_OCUPACION_PADRE = "@ocupacion_padre";
        public const string PARAMETRO_CALLE = "@calle";
        public const string PARAMETRO_COLONIA = "@colonia";
        public const string PARAMETRO_CIUDAD = "@ciudad";
        public const string PARAMETRO_ESTADO = "@estado";
        public const string PARAMETRO_CP = "@cp";
        public const string PARAMETRO_TELEFONO = "@telefono";
        public const string PARAMETRO_CORREO = "@correo";
        public const string PARAMETRO_NOMBRE_ENCARGADO = "@encargado";
        public const string PARAMETRO_TELEFONO_ENCARGADO = "@telefono_encargado";
        public const string PARAMETRO_NOTAS = "@notas";
        public const string PARAMETRO_DOCTOR_ID = "@doctorID";
        public const string PARAMETRO_LETRA = "@letra";
        

        public const string INSERTA_PACIENTE = "dbo.spInsertPaciente";
        public const string UPDATE_PACIENTE = "dbo.spUpdatePaciente";
        public const string DELETE_PACIENTE = "dbo.spDeletePaciente";
        public const string CONSULTA_PACIENTE = "dbo.spConsultaPaciente";
        public const string CONSULTA_UN_PACIENTE = "dbo.spConsultaUnPaciente";
        public const string CONSULTA_DOCTORID = "dbo.spConsultaDoctorId";
    }
}
