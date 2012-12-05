using System;
using System.Collections.Generic;
using System.Text;
using DataSource;
using Models;
using System.Data;
using System.Data.SqlClient;
using SQLHelpers;

namespace DataAccess
{
    public class PacienteDAO
    {
        public PacienteDAO() { }

        public static int ConsultaDoctorId(int rol, int usuarioId) {

            SqlCommand cmdAgregarEvento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarEvento.CommandText = SQLHelpers.PacienteSQLHelper.CONSULTA_DOCTORID;

            cmdAgregarEvento.CommandType = CommandType.StoredProcedure;
            cmdAgregarEvento.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdAgregarEvento.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[2];

            parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
            parametros[0].Value = rol;

            parametros[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[1].Value = usuarioId;

            //Agregando nuestros parametros al command
            cmdAgregarEvento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarEvento.ExecuteReader();
            int eventID = int.Parse(cmdAgregarEvento.Parameters[PacienteSQLHelper.PARAMETRO_DOCTOR_ID].Value.ToString());
            dr.Close();
            dr.Dispose();
            DBConnection.Close(cmdAgregarEvento.Connection);
            
            // Cerramos la conexion

            return eventID;
        }
        public static List<Paciente> Consulta(string letra, int doctorId)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PacienteSQLHelper.CONSULTA_PACIENTE, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            if (letra != null)
            {
                //Declarando los Parametros
                SqlParameter[] parametros = new SqlParameter[2];

                //Asignando Valores
                parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametros[0].Value = doctorId;

                parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_LETRA, SqlDbType.NVarChar, 10);
                parametros[1].Value = letra;

                //Agregando nuestros parametros al command
                cmdConsulta.Parameters.AddRange(parametros);
            }
            else {
                //Declarando los Parametros
                SqlParameter[] parametros = new SqlParameter[1];

                //Asignando Valores
                parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametros[0].Value = doctorId;

               
                //Agregando nuestros parametros al command
                cmdConsulta.Parameters.AddRange(parametros);
            }
            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Paciente> itemsPaciente = new List<Paciente>();

            Paciente paciente = null;


            while (reader.Read())
            {
                paciente = new Paciente();

                paciente.Nombre = reader["nombre"].ToString();
                paciente.ID = int.Parse(reader["id"].ToString());

                itemsPaciente.Add(paciente);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsPaciente;
        }

        public void inserta(Paciente paciente) {
            SqlCommand cmdAgregarPadecimiento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarPadecimiento.CommandText = SQLHelpers.PacienteSQLHelper.INSERTA_PACIENTE;

            cmdAgregarPadecimiento.CommandType = CommandType.StoredProcedure;
            cmdAgregarPadecimiento.Connection = DBConnection.Open();


            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[23];

            //Asignando Valores
            parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[0].Value = paciente.Nombre;

            parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_SEXO, SqlDbType.NVarChar,50);
            parametros[1].Value = paciente.Sexo;

            parametros[2] = new SqlParameter(PacienteSQLHelper.PARAMETRO_FECHA_NAC, SqlDbType.DateTime);
            parametros[2].Value = paciente.FechaNac;

            parametros[3] = new SqlParameter(PacienteSQLHelper.PARAMETRO_LUGAR_NAC, SqlDbType.NVarChar, 150);
            parametros[3].Value = paciente.LugarNac;

            parametros[4] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CIUDAD_NAC, SqlDbType.NVarChar, 150);
            parametros[4].Value = paciente.CiudadNac;

            parametros[5] = new SqlParameter(PacienteSQLHelper.PARAMETRO_GRUPO_SANGUINEO, SqlDbType.NVarChar, 50);
            parametros[5].Value = paciente.GrupoSanguineo;

            parametros[6] = new SqlParameter(PacienteSQLHelper.PARAMETRO_RH, SqlDbType.NVarChar, 50);
            parametros[6].Value = paciente.RH;

            parametros[7] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ALERGICO, SqlDbType.NVarChar, 150);
            parametros[7].Value = paciente.Alergico;

            parametros[8] = new SqlParameter(PacienteSQLHelper.PARAMETRO_MADRE, SqlDbType.NVarChar, 100);
            parametros[8].Value = paciente.Madre;

            parametros[9] = new SqlParameter(PacienteSQLHelper.PARAMETRO_OCUPACION_MADRE, SqlDbType.NVarChar, 150);
            parametros[9].Value = paciente.OcupacionMadre;

            parametros[10] = new SqlParameter(PacienteSQLHelper.PARAMETRO_PADRE, SqlDbType.NVarChar, 100);
            parametros[10].Value = paciente.Padre;

            parametros[11] = new SqlParameter(PacienteSQLHelper.PARAMETRO_OCUPACION_PADRE, SqlDbType.NVarChar, 150);
            parametros[11].Value = paciente.OcupacionPadre;

            parametros[12] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CALLE, SqlDbType.NVarChar, 50);
            parametros[12].Value = paciente.Calle;

            parametros[13] = new SqlParameter(PacienteSQLHelper.PARAMETRO_COLONIA, SqlDbType.NVarChar, 50);
            parametros[13].Value = paciente.Colonia;

            parametros[14] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CIUDAD, SqlDbType.NVarChar, 50);
            parametros[14].Value = paciente.Ciudad;

            parametros[15] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ESTADO, SqlDbType.NVarChar, 50);
            parametros[15].Value = paciente.Estado;

            parametros[16] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CP, SqlDbType.Int, 50);
            parametros[16].Value = paciente.CodigoPostal;

            parametros[17] = new SqlParameter(PacienteSQLHelper.PARAMETRO_TELEFONO, SqlDbType.NVarChar, 50);
            parametros[17].Value = paciente.Telefono;

            parametros[18] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CORREO, SqlDbType.NVarChar, 70);
            parametros[18].Value = paciente.Correo;

            parametros[19] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOMBRE_ENCARGADO, SqlDbType.NVarChar, 150);
            parametros[19].Value = paciente.NombreEncargado;

            parametros[20] = new SqlParameter(PacienteSQLHelper.PARAMETRO_TELEFONO_ENCARGADO, SqlDbType.NVarChar, 50);
            parametros[20].Value = paciente.TelefonoEncargado;

            parametros[21] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOTAS, SqlDbType.NVarChar);
            parametros[21].Value = paciente.Notas;

            parametros[22] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            parametros[22].Value = paciente.DoctorID;

            //Agregando nuestros parametros al command
            cmdAgregarPadecimiento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarPadecimiento.ExecuteReader();

            // Cerramos la conexion
            DBConnection.Close(cmdAgregarPadecimiento.Connection);
        }

        public Paciente ConsultarUnPaciente(int idPaciente, int idDoctor)
        {

            Paciente paciente = new Paciente();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = PacienteSQLHelper.CONSULTA_UN_PACIENTE;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPaciente;

                SqlParameter parametro2 = new SqlParameter();
                parametro2 = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametro2.Value = idDoctor;

                cmdConsulta.Parameters.Add(parametro);
                cmdConsulta.Parameters.Add(parametro2);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    paciente.ID = drConsulta.GetInt32(0);
                    paciente.Nombre = drConsulta.GetString(1);
                    paciente.Sexo = drConsulta.GetString(2);
                    paciente.FechaNac = drConsulta.GetString(3);
                    paciente.LugarNac = drConsulta.GetString(4);
                    paciente.CiudadNac = drConsulta.GetString(5);
                    paciente.GrupoSanguineo = drConsulta.GetString(6);
                    paciente.RH = drConsulta.GetString(7);
                    paciente.Alergico = drConsulta.GetString(8);
                    paciente.Madre = drConsulta.GetString(9);
                    paciente.OcupacionMadre = drConsulta.GetString(10);
                    paciente.Padre = drConsulta.GetString(11);
                    paciente.OcupacionPadre = drConsulta.GetString(12);
                    paciente.Calle = drConsulta.GetString(13);
                    paciente.Colonia = drConsulta.GetString(14);
                    paciente.Ciudad = drConsulta.GetString(15);
                    paciente.Estado = drConsulta.GetString(16);
                    paciente.CodigoPostal = drConsulta.GetInt32(17).ToString();
                    paciente.Telefono = drConsulta.GetString(18);
                    paciente.Correo = drConsulta.GetString(19);
                    paciente.NombreEncargado = drConsulta.GetString(20);
                    paciente.TelefonoEncargado = drConsulta.GetString(21);
                    paciente.Notas = drConsulta.GetString(22);
                    paciente.DoctorID = drConsulta.GetInt32(23);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return paciente;
        }

        public bool ModificarPaciente(Paciente paciente) {
            bool Exito = false;
            SqlCommand cmdAgregarPadecimiento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarPadecimiento.CommandText = SQLHelpers.PacienteSQLHelper.UPDATE_PACIENTE;

            cmdAgregarPadecimiento.CommandType = CommandType.StoredProcedure;
            cmdAgregarPadecimiento.Connection = DBConnection.Open();


            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[24];

            //Asignando Valores
            parametros[0] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = paciente.ID;

            parametros[1] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[1].Value = paciente.Nombre;

            parametros[2] = new SqlParameter(PacienteSQLHelper.PARAMETRO_SEXO, SqlDbType.NVarChar, 50);
            parametros[2].Value = paciente.Sexo;

            parametros[3] = new SqlParameter(PacienteSQLHelper.PARAMETRO_FECHA_NAC, SqlDbType.DateTime);
            parametros[3].Value = paciente.FechaNac;

            parametros[4] = new SqlParameter(PacienteSQLHelper.PARAMETRO_LUGAR_NAC, SqlDbType.NVarChar, 150);
            parametros[4].Value = paciente.LugarNac;

            parametros[5] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CIUDAD_NAC, SqlDbType.NVarChar, 150);
            parametros[5].Value = paciente.CiudadNac;

            parametros[6] = new SqlParameter(PacienteSQLHelper.PARAMETRO_GRUPO_SANGUINEO, SqlDbType.NVarChar, 50);
            parametros[6].Value = paciente.GrupoSanguineo;

            parametros[7] = new SqlParameter(PacienteSQLHelper.PARAMETRO_RH, SqlDbType.NVarChar, 50);
            parametros[7].Value = paciente.RH;

            parametros[8] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ALERGICO, SqlDbType.NVarChar, 150);
            parametros[8].Value = paciente.Alergico;

            parametros[9] = new SqlParameter(PacienteSQLHelper.PARAMETRO_MADRE, SqlDbType.NVarChar, 100);
            parametros[9].Value = paciente.Madre;

            parametros[10] = new SqlParameter(PacienteSQLHelper.PARAMETRO_OCUPACION_MADRE, SqlDbType.NVarChar, 150);
            parametros[10].Value = paciente.OcupacionMadre;

            parametros[11] = new SqlParameter(PacienteSQLHelper.PARAMETRO_PADRE, SqlDbType.NVarChar, 100);
            parametros[11].Value = paciente.Padre;

            parametros[12] = new SqlParameter(PacienteSQLHelper.PARAMETRO_OCUPACION_PADRE, SqlDbType.NVarChar, 150);
            parametros[12].Value = paciente.OcupacionPadre;

            parametros[13] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CALLE, SqlDbType.NVarChar, 50);
            parametros[13].Value = paciente.Calle;

            parametros[14] = new SqlParameter(PacienteSQLHelper.PARAMETRO_COLONIA, SqlDbType.NVarChar, 50);
            parametros[14].Value = paciente.Colonia;

            parametros[15] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CIUDAD, SqlDbType.NVarChar, 50);
            parametros[15].Value = paciente.Ciudad;

            parametros[16] = new SqlParameter(PacienteSQLHelper.PARAMETRO_ESTADO, SqlDbType.NVarChar, 50);
            parametros[16].Value = paciente.Estado;

            parametros[17] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CP, SqlDbType.Int, 50);
            parametros[17].Value = paciente.CodigoPostal;

            parametros[18] = new SqlParameter(PacienteSQLHelper.PARAMETRO_TELEFONO, SqlDbType.NVarChar, 50);
            parametros[18].Value = paciente.Telefono;

            parametros[19] = new SqlParameter(PacienteSQLHelper.PARAMETRO_CORREO, SqlDbType.NVarChar, 70);
            parametros[19].Value = paciente.Correo;

            parametros[20] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOMBRE_ENCARGADO, SqlDbType.NVarChar, 150);
            parametros[20].Value = paciente.NombreEncargado;

            parametros[21] = new SqlParameter(PacienteSQLHelper.PARAMETRO_TELEFONO_ENCARGADO, SqlDbType.NVarChar, 50);
            parametros[21].Value = paciente.TelefonoEncargado;

            parametros[22] = new SqlParameter(PacienteSQLHelper.PARAMETRO_NOTAS, SqlDbType.NVarChar);
            parametros[22].Value = paciente.Notas;

            parametros[23] = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
            parametros[23].Value = paciente.DoctorID;

            //Agregando nuestros parametros al command
            cmdAgregarPadecimiento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarPadecimiento.ExecuteReader();
            Exito = true;
            // Cerramos la conexion
            DBConnection.Close(cmdAgregarPadecimiento.Connection);
            

            return Exito;
        }

        public bool EliminarPaciente(int idPaciente, int idDoctor)
        {
            bool Exito = false;
            
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = PacienteSQLHelper.DELETE_PACIENTE;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PacienteSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPaciente;

                SqlParameter parametro2 = new SqlParameter();
                parametro2 = new SqlParameter(PacienteSQLHelper.PARAMETRO_DOCTOR_ID, SqlDbType.Int);
                parametro2.Value = idDoctor;

                cmdConsulta.Parameters.Add(parametro);
                cmdConsulta.Parameters.Add(parametro2);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();
                Exito = true;
                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return Exito;
        }


    }
}
