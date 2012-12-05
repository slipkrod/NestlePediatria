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
    public class PaisDAO
    {
        public PaisDAO() { }

        public void Inserta(Pais pais)
        {
            SqlCommand cmdAgregarPais = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarPais.CommandText = SQLHelpers.PaisSQLHelper.INSERTA_PAIS;
                
            cmdAgregarPais.CommandType = CommandType.StoredProcedure;
            cmdAgregarPais.Connection = DBConnection.Open();


            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[2];

            //Asignando Valores
            parametros[0] = new SqlParameter(PaisSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = pais.Nombre;

            parametros[1] = new SqlParameter(PaisSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[1].Value = Convert.ToInt32(pais.Publicado);

            
            //Agregando nuestros parametros al command
            cmdAgregarPais.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarPais.ExecuteReader();

            // Cerramos la conexion
            DBConnection.Close(cmdAgregarPais.Connection);
        }

        public static List<Pais> Consulta() 
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PaisSQLHelper.CONSULTA_PAIS, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Pais> itemsPais = new List<Pais>();

            Pais pais = null;

           
            while (reader.Read())
            {
                pais = new Pais();

                pais.Nombre = reader["nombre"].ToString();
                pais.Publicado = int.Parse(reader["publicado"].ToString());
                pais.Id = int.Parse(reader["id"].ToString());
                
                itemsPais.Add(pais);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsPais;
        }

        public static List<Pais> ConsultaPublicado(int usuarioID)
        {
            
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PaisSQLHelper.CONSULTA_PAIS_ACTIVO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[1];

            //Asignando Valores
            parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[0].Value = usuarioID;

            //Agregando nuestros parametros al command
            cmdConsulta.Parameters.AddRange(parametros);

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Pais> itemsPais = new List<Pais>();

            Pais pais = null;

            
            while (reader.Read())
            {
                pais = new Pais();

                pais.Nombre = reader["nombre"].ToString();
                pais.Publicado = int.Parse(reader["publicado"].ToString());
                pais.Id = int.Parse(reader["id"].ToString());

                itemsPais.Add(pais);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsPais;
        }

        public Pais ConsultaUnPais(int idPais)
        {
            Pais pais = new Pais();

            try
            {
                SqlCommand cmdConsultaPais = new SqlCommand();

                cmdConsultaPais.CommandText = PaisSQLHelper.CONSULTA_UN_PAIS;
                cmdConsultaPais.CommandType = CommandType.StoredProcedure;
                cmdConsultaPais.Connection = DBConnection.Open();

                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros[0].Value = idPais;

                cmdConsultaPais.Parameters.AddRange(parametros);

                SqlDataReader drConsulta = cmdConsultaPais.ExecuteReader();

                while (drConsulta.Read())
                {
                    pais.Id = drConsulta.GetInt32(0);
                    pais.Nombre = drConsulta.GetString(1);
                    pais.Publicado = drConsulta.GetInt32(2);
                    

                }

                DBConnection.Close(cmdConsultaPais.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
               
            }
            return pais;
        }

        public bool ModificarPais(Pais pais)
        {
            bool Exito = false;
            try
            {
                SqlCommand cmdModificarPais = new SqlCommand();

                cmdModificarPais.CommandText = PaisSQLHelper.UPDATE_PAIS;
                cmdModificarPais.CommandType = CommandType.StoredProcedure;
                cmdModificarPais.Connection = DBConnection.Open();

                SqlParameter[] parametros = new SqlParameter[3];

                parametros[0] = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros[0].Value = pais.Id;

                parametros[1] = new SqlParameter(PaisSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
                parametros[1].Value = pais.Nombre;

                parametros[2] = new SqlParameter(PaisSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
                parametros[2].Value = Convert.ToInt32(pais.Publicado);

                cmdModificarPais.Parameters.AddRange(parametros);
                cmdModificarPais.ExecuteReader();

                DBConnection.Close(cmdModificarPais.Connection);
                Exito = true;
            }
            catch (Exception exc)
            {
                Console.Write(exc);
                return false;
            }

            return Exito;

        }

        public bool EliminarPais(int idPais)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaPais.CommandText = PaisSQLHelper.DELETE_PAIS;
                cmdEliminaPais.CommandType = CommandType.StoredProcedure;
                cmdEliminaPais.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPais;

                cmdEliminaPais.Parameters.Add(parametro);
                cmdEliminaPais.ExecuteReader();
                DBConnection.Close(cmdEliminaPais.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el pais" + exc);
                return resultado;
            }
            return resultado;
        }

        
    }
}
