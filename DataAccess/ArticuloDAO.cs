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
    public class ArticuloDAO
    {
        public ArticuloDAO() { }

        public static List<Articulo> Consulta(int usuarioID, int rolID)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(ArticuloSQLHelper.CONSULTA_ARTICULO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlParameter[] parametros1 = new SqlParameter[2];
            //Asignando Valores


            parametros1[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros1[0].Value = usuarioID;

            parametros1[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
            parametros1[1].Value = rolID;
            //Agregando nuestros parametros al command
            cmdConsulta.Parameters.AddRange(parametros1);

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Articulo> itemsArticulos = new List<Articulo>();

            Articulo articulo = null;

            
            while (reader.Read())
            {
                articulo = new Articulo();

                articulo.Nombre = reader["nombre"].ToString();
                articulo.Publicado = int.Parse(reader["publicado"].ToString());
                articulo.Id = int.Parse(reader["id"].ToString());

                itemsArticulos.Add(articulo);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsArticulos;
        }

        public static int Inserta(Articulo articulo, List<Pais> pais, int usuarioID)
        {
           
            SqlCommand cmdAgregarArticulo = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarArticulo.CommandText = SQLHelpers.ArticuloSQLHelper.INSERTA_ARTICULO;
                
            cmdAgregarArticulo.CommandType = CommandType.StoredProcedure;
            cmdAgregarArticulo.Connection = DBConnection.Open();

            SqlParameter prmArticuloId = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
            prmArticuloId.Direction = ParameterDirection.Output;
            cmdAgregarArticulo.Parameters.Add(prmArticuloId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[9];

            //Asignando Valores

            parametros[0] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = articulo.Nombre;

            parametros[1] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = articulo.Descripcion;

            parametros[2] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar,150);
            parametros[2].Value = articulo.Autor;
            

            parametros[3] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(articulo.Fecha);
           

            parametros[4] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_CATEGORIAID, SqlDbType.Int);
            parametros[4].Value = Convert.ToInt32(articulo.CategoriaId);

            parametros[5] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = articulo.Foto;

            parametros[6] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = articulo.Documento;

            parametros[7] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[7].Value = Convert.ToInt32(articulo.Publicado);

            parametros[8] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[8].Value = usuarioID;

            

            
            //Agregando nuestros parametros al command
            cmdAgregarArticulo.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarArticulo.ExecuteReader();
            int ArticID = int.Parse(cmdAgregarArticulo.Parameters[ArticuloSQLHelper.PARAMETRO_ARTICULOID].Value.ToString());
            dr.Close();
            DBConnection.Close(cmdAgregarArticulo.Connection);
            InsertaArticuloPais(ArticID, pais);
            // Cerramos la conexion
           
            return ArticID;
        }


        public static void InsertaArticuloPais(int ArticuloID, List<Pais> pais)
        {




            foreach (Pais data in pais)
            {
                SqlCommand cmdAgregarEventoPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarEventoPais.CommandText = SQLHelpers.ArticuloSQLHelper.INSERTA_ARTICULO_PAIS;

                cmdAgregarEventoPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarEventoPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
                parametros1[0].Value = ArticuloID;

                parametros1[1] = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros1[1].Value = data.Id;
                //Agregando nuestros parametros al command
                cmdAgregarEventoPais.Parameters.AddRange(parametros1);


                //Ejecutamos el NonQuery
                SqlDataReader dr = cmdAgregarEventoPais.ExecuteReader();
                dr.Close();
                DBConnection.Close(cmdAgregarEventoPais.Connection);
            }

        }

        public bool EliminarArticulo(int idArticulo)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaArticulo = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaArticulo.CommandText = ArticuloSQLHelper.DELETE_ARTICULO;
                cmdEliminaArticulo.CommandType = CommandType.StoredProcedure;
                cmdEliminaArticulo.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
                parametro.Value = idArticulo;

                cmdEliminaArticulo.Parameters.Add(parametro);
                cmdEliminaArticulo.ExecuteReader();
                DBConnection.Close(cmdEliminaArticulo.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Articulo" + exc);
                return resultado;
            }
            return resultado;
        }

        /*******************************************************************************************/

        public Articulo ConsultarUnArticulo(int idArticulo)
        {

            Articulo articulo = new Articulo();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = ArticuloSQLHelper.CONSULTA_UN_ARTICULO;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
                parametro.Value = idArticulo;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    articulo.Id = drConsulta.GetInt32(0);
                    articulo.Nombre = drConsulta.GetString(1);
                    articulo.Descripcion = drConsulta.GetString(2);
                    articulo.Autor = drConsulta.GetString(3);
                    articulo.Fecha = drConsulta.GetString(4);
                    articulo.CategoriaId = drConsulta.GetInt32(5);
                    //articulo.Foto = drConsulta.GetString(6);
                    //articulo.Documento = drConsulta.GetString(7);
                    articulo.Publicado = drConsulta.GetInt32(8);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return articulo;
        }

        public List<int> ConsultaArticuloPais(int idArticulo, int usuarioID, int rolID)
        {

            int idpais;
            List<int> itemsPais = new List<int>();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = ArticuloSQLHelper.CONSULTA_ARTICULO_PAIS;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
                parametro.Value = idArticulo;
                cmdConsulta.Parameters.Add(parametro);

                // numConsulta = (int)cmdConsulta.ExecuteScalar();
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                parametros1[0].Value = usuarioID;

                parametros1[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
                parametros1[1].Value = rolID;
                //Agregando nuestros parametros al command
                cmdConsulta.Parameters.AddRange(parametros1);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    idpais = drConsulta.GetInt32(0);
                    itemsPais.Add(idpais);

                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return itemsPais;
        }

        public bool ModificarArticulo(Articulo articulo, int[] paisesElegidos,int usuarioID, int rolID)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = ArticuloSQLHelper.UPDATE_ARTICULO;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[9];

            parametros[0] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_ARTICULOID, SqlDbType.Int);
            parametros[0].Value = articulo.Id;

            parametros[1] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = articulo.Nombre;

            parametros[2] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = articulo.Descripcion;

            parametros[3] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[3].Value = articulo.Autor;
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[4] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[4].Value = Convert.ToDateTime(articulo.Fecha);
            //parametros[3].Value = Convert.ToDateTime("20/10/2012");

            parametros[5] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_CATEGORIAID, SqlDbType.Int);
            parametros[5].Value = Convert.ToInt32(articulo.CategoriaId);

            parametros[6] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = articulo.Foto;

            parametros[7] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[7].Value = articulo.Documento;

            parametros[8] = new SqlParameter(ArticuloSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[8].Value = Convert.ToInt32(articulo.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);

            List<int> paisesActivos = ConsultaArticuloPais(articulo.Id,usuarioID, rolID);
            //paisesElegidos

            //checar los paises seleccionados vs los que tengo registrados
            //los que no esten en los seleccionados y en los registrados si a esos eliminarlos
            int[] arrpaisesActivos = new int[paisesActivos.ToArray().Length];
            int contpa = 0;

            foreach (int pa in paisesActivos)
            {
                arrpaisesActivos[contpa] = pa;
                contpa++;
            }

            List<Pais> paisNuevo = new List<Pais>();
            Pais paisagr = null;
            
            foreach (int eleg1 in paisesElegidos)
            {
                bool agrega = false;
                foreach (int act2 in arrpaisesActivos)
                {
                    if (eleg1 == act2)
                    {
                        agrega = false;
                        break;
                    }
                    else
                    {
                        agrega = true;
                    }

                }
                if (agrega)
                {
                    paisagr = new Pais();
                    paisagr.Id = eleg1;
                    paisNuevo.Add(paisagr);
                }
            }

            ArticuloDAO.InsertaArticuloPais(articulo.Id, paisNuevo);

            

            foreach (int act in arrpaisesActivos)
            {
                bool elimina = false;
                foreach (int eleg in paisesElegidos)
                {
                    if (eleg == act)
                    {
                        elimina = false;
                        break;
                    }
                    else
                    {
                        elimina = true;
                    }

                }
                if (elimina) { EliminarArticuloPais(act); }
            }



            

            Exito = true;

            return Exito;
        }

        public bool EliminarArticuloPais(int idEvento)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdElimina = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdElimina.CommandText = ArticuloSQLHelper.DELETE_ARTICULO_PAIS;
                cmdElimina.CommandType = CommandType.StoredProcedure;
                cmdElimina.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idEvento;

                cmdElimina.Parameters.Add(parametro);
                SqlDataReader dr = cmdElimina.ExecuteReader();
                dr.Close();
                dr.Dispose();
                //Ejecutamos el NonQuery

                DBConnection.Close(cmdElimina.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Pais" + exc);
                return resultado;
            }
            return resultado;
        }
    }
}
