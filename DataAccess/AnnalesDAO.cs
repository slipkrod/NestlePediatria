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
    public class AnnalesDAO
    {
        public AnnalesDAO() { }

        public static List<Annales> Consulta()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(AnnalesSQLHelper.CONSULTA_ANNALES, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Annales> itemsAnnales = new List<Annales>();

            Annales annales = null;

            
            while (reader.Read())
            {
                annales = new Annales();

                annales.Nombre = reader["nombre"].ToString();
                annales.Publicado = int.Parse(reader["publicado"].ToString());
                annales.Id = int.Parse(reader["id"].ToString());

                itemsAnnales.Add(annales);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsAnnales;
        }

        public static int Inserta(Annales annales, int usuarioID)
        {
           
            SqlCommand cmdAgregarAnnales = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarAnnales.CommandText = SQLHelpers.AnnalesSQLHelper.INSERTA_ANNALES;
                
            cmdAgregarAnnales.CommandType = CommandType.StoredProcedure;
            cmdAgregarAnnales.Connection = DBConnection.Open();

            SqlParameter prmAnnalesId = new SqlParameter(AnnalesSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            prmAnnalesId.Direction = ParameterDirection.Output;
            cmdAgregarAnnales.Parameters.Add(prmAnnalesId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[8];

            //Asignando Valores

            parametros[0] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = annales.Nombre;

            parametros[1] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = annales.Descripcion;

            parametros[2] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[2].Value = annales.Autor;
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[3] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(annales.Fecha);




            parametros[4] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[4].Value = annales.Foto;

            parametros[5] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = annales.Documento;

            parametros[6] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[6].Value = Convert.ToInt32(annales.Publicado);

            parametros[7] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[7].Value = usuarioID;

            

            
            //Agregando nuestros parametros al command
            cmdAgregarAnnales.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarAnnales.ExecuteReader();
            int ArticID = int.Parse(cmdAgregarAnnales.Parameters[AnnalesSQLHelper.PARAMETRO_ID].Value.ToString());
            dr.Close();
            DBConnection.Close(cmdAgregarAnnales.Connection);
            
            // Cerramos la conexion
           
            return ArticID;
        }


       

        public bool EliminarAnnales(int idAnnales)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdElimina = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdElimina.CommandText = AnnalesSQLHelper.DELETE_ANNALES;
                cmdElimina.CommandType = CommandType.StoredProcedure;
                cmdElimina.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(AnnalesSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idAnnales;

                cmdElimina.Parameters.Add(parametro);
                cmdElimina.ExecuteReader();
                DBConnection.Close(cmdElimina.Connection);
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

        public Annales ConsultarUnAnnales(int idAnnales)
        {

            Annales ann = new Annales();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = AnnalesSQLHelper.CONSULTA_UN_ANNALES;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(AnnalesSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idAnnales;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    ann.Id = drConsulta.GetInt32(0);
                    ann.Nombre = drConsulta.GetString(1);
                    ann.Descripcion = drConsulta.GetString(2);
                    ann.Autor = drConsulta.GetString(3);
                    ann.Fecha = drConsulta.GetString(4);
                    //ann.Foto = drConsulta.GetString(5);
                    //ann.Documento = drConsulta.GetString(6);
                    ann.Publicado = drConsulta.GetInt32(7);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return ann;
        }

        

        public bool ModificarAnnales(Annales annales)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = AnnalesSQLHelper.UPDATE_ANNALES;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[8];

            parametros[0] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = annales.Id;

            parametros[1] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = annales.Nombre;

            parametros[2] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = annales.Descripcion;

            parametros[3] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[3].Value = annales.Autor;
            

            parametros[4] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[4].Value = Convert.ToDateTime(annales.Fecha);

            parametros[5] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = annales.Foto;

            parametros[6] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = annales.Documento;

            parametros[7] = new SqlParameter(AnnalesSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[7].Value = Convert.ToInt32(annales.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);

            Exito = true;

            return Exito;
        }

       
    }
}
