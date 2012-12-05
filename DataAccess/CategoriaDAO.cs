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
    public class CategoriaDAO
    {

        public CategoriaDAO() { }

        public void Inserta(Categoria categoria) {

        SqlCommand cmdAgregarCategoria = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarCategoria.CommandText = SQLHelpers.CategoriaSQLHelper.INSERTA_CATEGORIA;
                
            cmdAgregarCategoria.CommandType = CommandType.StoredProcedure;
            cmdAgregarCategoria.Connection = DBConnection.Open();


            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[3];

            //Asignando Valores
            parametros[0] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[0].Value = categoria.Nombre;

            parametros[1] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = categoria.Descripcion;

            parametros[2] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[2].Value = Convert.ToInt32(categoria.Publicado);

            
            //Agregando nuestros parametros al command
            cmdAgregarCategoria.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery
            cmdAgregarCategoria.ExecuteReader();

            // Cerramos la conexion
            DBConnection.Close(cmdAgregarCategoria.Connection);
        }

        public static List<Categoria> Consulta()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(CategoriaSQLHelper.CONSULTA_CATEGORIA, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Categoria> itemsCategorias = new List<Categoria>();

            Categoria categoria = null;

            DBConnection.Open();
            while (reader.Read())
            {
                categoria = new Categoria();

                categoria.Nombre = reader["nombre"].ToString();
                categoria.Publicado = int.Parse(reader["publicado"].ToString());
                categoria.Id = int.Parse(reader["id"].ToString());

                itemsCategorias.Add(categoria);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsCategorias;
        }

        public static List<Categoria> ConsultaPublicado()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(CategoriaSQLHelper.CONSULTA_CATEGORIA_ACTIVA, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Categoria> itemsCategorias = new List<Categoria>();

            Categoria categoria = null;

            DBConnection.Open();
            while (reader.Read())
            {
                categoria = new Categoria();

                categoria.Nombre = reader["nombre"].ToString();
                categoria.Publicado = int.Parse(reader["publicado"].ToString());
                categoria.Id = int.Parse(reader["id"].ToString());

                itemsCategorias.Add(categoria);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsCategorias;
        }

        public Categoria ConsultarUnaCategoria(int idCategoria)
        {

            Categoria categoria = new Categoria();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = CategoriaSQLHelper.CONSULTA_UNA_CATEGORIA;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(CategoriaSQLHelper.PARAMETRO_CATEGORIAID, SqlDbType.Int);
                parametro.Value = idCategoria;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    categoria.Id = drConsulta.GetInt32(0);
                    categoria.Nombre = drConsulta.GetString(1);
                    categoria.Descripcion = drConsulta.GetString(2);
                    categoria.Publicado = drConsulta.GetInt32(3);
                    

                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return categoria;
        }

        public bool ModificarCategoria(Categoria categoria)
        {
            bool Exito = false;

            SqlCommand cmdModificarCategoria = new SqlCommand();

            cmdModificarCategoria.CommandText = CategoriaSQLHelper.UPDATE_CATEGORIA;
            cmdModificarCategoria.CommandType = CommandType.StoredProcedure;
            cmdModificarCategoria.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[4];

            parametros[0] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_CATEGORIAID, SqlDbType.Int);
            parametros[0].Value = categoria.Id;

            parametros[1] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 150);
            parametros[1].Value = categoria.Nombre;

            parametros[2] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = categoria.Descripcion;

            parametros[3] = new SqlParameter(CategoriaSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[3].Value = categoria.Publicado;

            cmdModificarCategoria.Parameters.AddRange(parametros);
            cmdModificarCategoria.ExecuteReader();

            DBConnection.Close(cmdModificarCategoria.Connection);

            Exito = true;

            return Exito;
        }

        public bool EliminarCategoria(int idCategoria)
        {
            bool Exito = false;
            SqlCommand cmdConsulta = new SqlCommand();
            cmdConsulta.CommandText = CategoriaSQLHelper.DELETE_CATEGORIA;
            cmdConsulta.CommandType = CommandType.StoredProcedure;
            cmdConsulta.Connection = DBConnection.Open();


            SqlParameter parametro = new SqlParameter();
            parametro = new SqlParameter(CategoriaSQLHelper.PARAMETRO_CATEGORIAID, SqlDbType.Int);
            parametro.Value = idCategoria;
            cmdConsulta.Parameters.Add(parametro);
            cmdConsulta.ExecuteReader();
            DBConnection.Close(cmdConsulta.Connection);
            Exito = true;
            return Exito;
        }
    }

     
}
