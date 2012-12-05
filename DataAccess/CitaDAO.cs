using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataSource;
using Models;
using System.Data;
using System.Data.SqlClient;
using SQLHelpers;

namespace DataAccess
{
    public class CitaDAO
    {
         
        public CitaDAO(){}
        public static List<Cita> Consulta(int doctorId, string fecha)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(CitaSQLHelper.CONSULTA_CITA, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            //Declarando los Parametros
                SqlParameter[] parametros = new SqlParameter[2];

                //Asignando Valores
                parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametros[0].Value = doctorId;

                parametros[1] = new SqlParameter(CitaSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
                parametros[1].Value = DateTime.Parse(fecha);

                //Agregando nuestros parametros al command
                cmdConsulta.Parameters.AddRange(parametros);
            
            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Cita> itemsCita = new List<Cita>();

            Cita cita = null;


            while (reader.Read())
            {
                cita = new Cita();

                cita.Id = int.Parse(reader["id"].ToString());
                cita.PacienteId = int.Parse(reader["paciente_id"].ToString());
                cita.HoraInicio = DateTime.Parse(reader["hora_inicio"].ToString());
                cita.HoraFinal = DateTime.Parse(reader["hora_final"].ToString());
                cita.Nombre = reader["nombre"].ToString();
                itemsCita.Add(cita);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsCita;
        }

        public int Inserta(Cita cita, int doctorId)
        {

            SqlCommand cmdAgregarCategoria = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarCategoria.CommandText = SQLHelpers.CitaSQLHelper.INSERTA_CITA;

            cmdAgregarCategoria.CommandType = CommandType.StoredProcedure;
            cmdAgregarCategoria.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(CitaSQLHelper.PARAMETRO_PARAMETRO_ID, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdAgregarCategoria.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[4];

            //Asignando Valores
            parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            parametros[0].Value = doctorId;

            parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[1].Value = cita.PacienteId;

            parametros[2] = new SqlParameter(CitaSQLHelper.PARAMETRO_HORA_INICIO, SqlDbType.DateTime);
            parametros[2].Value = cita.HoraInicio;

            parametros[3] = new SqlParameter(CitaSQLHelper.PARAMETRO_HORA_FINAL, SqlDbType.DateTime);
            parametros[3].Value = cita.HoraFinal;


            //Agregando nuestros parametros al command
            cmdAgregarCategoria.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarCategoria.ExecuteReader();

            int eventID = int.Parse(cmdAgregarCategoria.Parameters[CitaSQLHelper.PARAMETRO_PARAMETRO_ID].Value.ToString());
           

            // Cerramos la conexion
            DBConnection.Close(cmdAgregarCategoria.Connection);

            return eventID;
        }

        public int ModificarCategoria(Cita cita, int doctorId)
        {
            int Exito = 0;

            SqlCommand cmdModificarCategoria = new SqlCommand();

            cmdModificarCategoria.CommandText = SQLHelpers.CitaSQLHelper.UPDATE_CITA;
            cmdModificarCategoria.CommandType = CommandType.StoredProcedure;
            cmdModificarCategoria.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(CitaSQLHelper.PARAMETRO_FLAG, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdModificarCategoria.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[5];

            //Asignando Valores
            parametros[0] = new SqlParameter(CitaSQLHelper.PARAMETRO_PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = cita.Id;

            parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            parametros[1].Value = doctorId;

            parametros[2] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[2].Value = cita.PacienteId;

            parametros[3] = new SqlParameter(CitaSQLHelper.PARAMETRO_HORA_INICIO, SqlDbType.DateTime);
            parametros[3].Value = cita.HoraInicio;

            parametros[4] = new SqlParameter(CitaSQLHelper.PARAMETRO_HORA_FINAL, SqlDbType.DateTime);
            parametros[4].Value = cita.HoraFinal;

            cmdModificarCategoria.Parameters.AddRange(parametros);
            cmdModificarCategoria.ExecuteReader();
            Exito = int.Parse(cmdModificarCategoria.Parameters[CitaSQLHelper.PARAMETRO_FLAG].Value.ToString());
            DBConnection.Close(cmdModificarCategoria.Connection);

           

            return Exito;
        }
        
        public int EliminarCita(Cita cita,int doctorId)
        {
            int Exito = 0;
            SqlCommand cmdConsulta = new SqlCommand();
            cmdConsulta.CommandText = CitaSQLHelper.DELETE_CITA;
            cmdConsulta.CommandType = CommandType.StoredProcedure;
            cmdConsulta.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(CitaSQLHelper.PARAMETRO_FLAG, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdConsulta.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[2];

            //Asignando Valores
            parametros[0] = new SqlParameter(CitaSQLHelper.PARAMETRO_PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = cita.Id;

            parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            parametros[1].Value = doctorId;

            /*parametros[2] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[2].Value = cita.PacienteId;*/

            //Agregando nuestros parametros al command
            cmdConsulta.Parameters.AddRange(parametros);

            cmdConsulta.ExecuteReader();

            Exito = int.Parse(cmdConsulta.Parameters[CitaSQLHelper.PARAMETRO_FLAG].Value.ToString());

            DBConnection.Close(cmdConsulta.Connection);
            
            return Exito;
        }

        public Cita ConsultaUnCita(int doctorId, int idCita)
        {
            Cita cita = new Cita();

            try
            {
                SqlCommand cmdConsulta = new SqlCommand();

                cmdConsulta.CommandText = CitaSQLHelper.CONSULTA_UN_CITA;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();

                SqlParameter[] parametros = new SqlParameter[2];

                parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametros[0].Value = doctorId;

                parametros[1] = new SqlParameter(CitaSQLHelper.PARAMETRO_PARAMETRO_ID, SqlDbType.Int);
                parametros[1].Value = idCita;

                cmdConsulta.Parameters.AddRange(parametros);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    cita.Id = drConsulta.GetInt32(0);
                    cita.PacienteId = drConsulta.GetInt32(1);
                    cita.HoraInicio = drConsulta.GetDateTime(2);
                    cita.HoraFinal = drConsulta.GetDateTime(3);
                    cita.Nombre = drConsulta.GetString(4);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);

            }
            return cita;
        }
    }
}
