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
    public class PadecimientoDAO
    {
        public PadecimientoDAO() { }

        public void Inserta(Padecimiento padecimiento) {
            SqlCommand cmdAgregarPadecimiento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarPadecimiento.CommandText = SQLHelpers.PadecimientoSQLHelper.INSERTA_PADECIMIENTO;

            cmdAgregarPadecimiento.CommandType = CommandType.StoredProcedure;
            cmdAgregarPadecimiento.Connection = DBConnection.Open();


            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[3];

            //Asignando Valores
            parametros[0] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[0].Value = padecimiento.Nombre;

            parametros[1] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = padecimiento.Descripcion;

            parametros[2] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[2].Value = Convert.ToInt32(padecimiento.Publicado);


            //Agregando nuestros parametros al command
            cmdAgregarPadecimiento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarPadecimiento.ExecuteReader();

            // Cerramos la conexion
            DBConnection.Close(cmdAgregarPadecimiento.Connection);
        }

        public static List<Padecimiento> ConsultaPublicado()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PadecimientoSQLHelper.CONSULTA_PADECIMIENTO_ACTIVO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Padecimiento> itemsPadecimientos = new List<Padecimiento>();

            Padecimiento padecimiento = null;

            DBConnection.Open();
            while (reader.Read())
            {
                padecimiento = new Padecimiento();

                padecimiento.Nombre = reader["nombre"].ToString();
                padecimiento.Publicado = int.Parse(reader["publicado"].ToString());
                padecimiento.Id = int.Parse(reader["id"].ToString());

                itemsPadecimientos.Add(padecimiento);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsPadecimientos;
        }

        public static List<Padecimiento> Consulta()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PadecimientoSQLHelper.CONSULTA_PADECIMIENTO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Padecimiento> itemsPadecimientos = new List<Padecimiento>();

            Padecimiento padecimiento = null;

            DBConnection.Open();
            while (reader.Read())
            {
                padecimiento = new Padecimiento();

                padecimiento.Nombre = reader["nombre"].ToString();
                padecimiento.Publicado = int.Parse(reader["publicado"].ToString());
                padecimiento.Id = int.Parse(reader["id"].ToString());

                itemsPadecimientos.Add(padecimiento);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsPadecimientos;
        }

        public Padecimiento ConsultarUnPadecimiento(int idPadecimiento)
        {

            Padecimiento padecimient = new Padecimiento();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = PadecimientoSQLHelper.CONSULTA_UN_PADECIMIENTO;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_PADECIMIENTOID, SqlDbType.Int);
                parametro.Value = idPadecimiento;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    padecimient.Id = drConsulta.GetInt32(0);
                    padecimient.Nombre = drConsulta.GetString(1);
                    padecimient.Descripcion = drConsulta.GetString(2);
                    padecimient.Publicado = drConsulta.GetInt32(3);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return padecimient;
        }

        public bool ModificarPadecimiento(Padecimiento padecimiento)
        {
            bool Exito = false;

            SqlCommand cmdModificarPadecimiento = new SqlCommand();

            cmdModificarPadecimiento.CommandText = PadecimientoSQLHelper.UPDATE_PADECIMIENTO;
            cmdModificarPadecimiento.CommandType = CommandType.StoredProcedure;
            cmdModificarPadecimiento.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[4];

            parametros[0] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_PADECIMIENTOID, SqlDbType.Int);
            parametros[0].Value = padecimiento.Id;

            parametros[1] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[1].Value = padecimiento.Nombre;

            parametros[2] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = padecimiento.Descripcion;

            parametros[3] = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[3].Value = padecimiento.Publicado;

            cmdModificarPadecimiento.Parameters.AddRange(parametros);
            cmdModificarPadecimiento.ExecuteReader();

            DBConnection.Close(cmdModificarPadecimiento.Connection);

            Exito = true;

            return Exito;
        }

        public bool EliminarPadecimiento(int idPadecimiento)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaPadecimiento = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaPadecimiento.CommandText = PadecimientoSQLHelper.DELETE_PADECIMIENTO;
                cmdEliminaPadecimiento.CommandType = CommandType.StoredProcedure;
                cmdEliminaPadecimiento.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PadecimientoSQLHelper.PARAMETRO_PADECIMIENTOID, SqlDbType.Int);
                parametro.Value = idPadecimiento;

                cmdEliminaPadecimiento.Parameters.Add(parametro);
                cmdEliminaPadecimiento.ExecuteReader();
                DBConnection.Close(cmdEliminaPadecimiento.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el padecimiento" + exc);
                return resultado;
            }
            return resultado;
        }


    }
}
