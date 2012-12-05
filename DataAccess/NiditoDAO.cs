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
    public class NiditoDAO
    {
        public NiditoDAO() { }

        public static List<Nidito> Consulta()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(NiditoSQLHelper.CONSULTA_NIDITO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Nidito> itemsNidito = new List<Nidito>();

            Nidito nidito = null;

            
            while (reader.Read())
            {
                nidito = new Nidito();

                nidito.Nombre = reader["nombre"].ToString();
                nidito.Publicado = int.Parse(reader["publicado"].ToString());
                nidito.Id = int.Parse(reader["id"].ToString());

                itemsNidito.Add(nidito);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsNidito;
        }

        public static int Inserta(Nidito nidito, int usuarioID)
        {
           
            SqlCommand cmdAgregarNidito = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarNidito.CommandText = SQLHelpers.NiditoSQLHelper.INSERTA_NIDITO;
                
            cmdAgregarNidito.CommandType = CommandType.StoredProcedure;
            cmdAgregarNidito.Connection = DBConnection.Open();

            SqlParameter prmNiditoId = new SqlParameter(NiditoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            prmNiditoId.Direction = ParameterDirection.Output;
            cmdAgregarNidito.Parameters.Add(prmNiditoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[8];

            //Asignando Valores

            parametros[0] = new SqlParameter(NiditoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = nidito.Nombre;

            parametros[1] = new SqlParameter(NiditoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = nidito.Descripcion;

            parametros[2] = new SqlParameter(NiditoSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[2].Value = nidito.Autor;
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[3] = new SqlParameter(NiditoSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(nidito.Fecha);

            parametros[4] = new SqlParameter(NiditoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[4].Value = nidito.Foto;

            parametros[5] = new SqlParameter(NiditoSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = nidito.Documento;

            parametros[6] = new SqlParameter(NiditoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[6].Value = Convert.ToInt32(nidito.Publicado);

            parametros[7] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[7].Value = usuarioID;

            

            
            //Agregando nuestros parametros al command
            cmdAgregarNidito.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarNidito.ExecuteReader();
            int ArticID = int.Parse(cmdAgregarNidito.Parameters[NiditoSQLHelper.PARAMETRO_ID].Value.ToString());
            dr.Close();
            DBConnection.Close(cmdAgregarNidito.Connection);
            
            // Cerramos la conexion
           
            return ArticID;
        }


       

        public bool EliminarNidito(int idNidito)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaNidito = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaNidito.CommandText = NiditoSQLHelper.DELETE_NIDITO;
                cmdEliminaNidito.CommandType = CommandType.StoredProcedure;
                cmdEliminaNidito.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(NiditoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idNidito;

                cmdEliminaNidito.Parameters.Add(parametro);
                cmdEliminaNidito.ExecuteReader();
                DBConnection.Close(cmdEliminaNidito.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar " + exc);
                return resultado;
            }
            return resultado;
        }

        /*******************************************************************************************/

        public Nidito ConsultarUnNidito(int idNidito)
        {

            Nidito nidito = new Nidito();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = NiditoSQLHelper.CONSULTA_UN_NIDITO;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(NiditoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idNidito;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    nidito.Id = drConsulta.GetInt32(0);
                    nidito.Nombre = drConsulta.GetString(1);
                    nidito.Descripcion = drConsulta.GetString(2);
                    nidito.Autor = drConsulta.GetString(3);
                    nidito.Fecha = drConsulta.GetString(4);
                    //nidito.Foto = drConsulta.GetString(5);
                   // nidito.Documento = drConsulta.GetString(6);
                    nidito.Publicado = drConsulta.GetInt32(7);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return nidito;
        }



        public bool ModificarNidito(Nidito nidito)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = NiditoSQLHelper.UPDATE_NIDITO;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[8];

            parametros[0] = new SqlParameter(NiditoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = nidito.Id;

            parametros[1] = new SqlParameter(NiditoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = nidito.Nombre;

            parametros[2] = new SqlParameter(NiditoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = nidito.Descripcion;

            parametros[3] = new SqlParameter(NiditoSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[3].Value = nidito.Autor;


            parametros[4] = new SqlParameter(NiditoSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[4].Value = Convert.ToDateTime(nidito.Fecha);

            parametros[5] = new SqlParameter(NiditoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = nidito.Foto;

            parametros[6] = new SqlParameter(NiditoSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = nidito.Documento;

            parametros[7] = new SqlParameter(NiditoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[7].Value = Convert.ToInt32(nidito.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);

            Exito = true;

            return Exito;
        }
    }
}
