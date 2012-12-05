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
    public class ProductoDAO
    {
        public ProductoDAO() { }

        public static int Inserta(Producto producto, List<Pais> pais, int usuarioID)
        {

            SqlCommand cmdAgregarProducto = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarProducto.CommandText = SQLHelpers.ProductoSQLHelper.INSERTA_PRODUCTO;

            cmdAgregarProducto.CommandType = CommandType.StoredProcedure;
            cmdAgregarProducto.Connection = DBConnection.Open();

            SqlParameter prmProductoId = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            prmProductoId.Direction = ParameterDirection.Output;
            cmdAgregarProducto.Parameters.Add(prmProductoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[6];

            //Asignando Valores


            parametros[0] = new SqlParameter(ProductoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = producto.Nombre;

            parametros[1] = new SqlParameter(ProductoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = producto.Descripcion;


            parametros[2] = new SqlParameter(ProductoSQLHelper.PARAMETRO_PADECIMIENTO_ID, SqlDbType.Int);
            parametros[2].Value = Convert.ToInt32(producto.PadecimientoId);
            

            parametros[3] = new SqlParameter(ProductoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[3].Value = producto.Foto;

            parametros[4] = new SqlParameter(ProductoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[4].Value = Convert.ToInt32(producto.Publicado);

            parametros[5] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[5].Value = usuarioID;




            //Agregando nuestros parametros al command
            cmdAgregarProducto.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarProducto.ExecuteReader();
            int productID = int.Parse(cmdAgregarProducto.Parameters[ProductoSQLHelper.PARAMETRO_ID].Value.ToString());
            dr.Close();
            DBConnection.Close(cmdAgregarProducto.Connection);
            InsertaProductoPais(productID, pais);
            // Cerramos la conexion

            return productID;
        }


        public static void InsertaProductoPais(int productoID, List<Pais> pais)
        {




            foreach (Pais data in pais)
            {
                SqlCommand cmdAgregarEventoPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarEventoPais.CommandText = SQLHelpers.ProductoSQLHelper.INSERTA_PRODUCTO_PAIS;

                cmdAgregarEventoPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarEventoPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros1[0].Value = productoID;

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

        public static List<Producto> Consulta(int usuarioID, int rolID)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(ProductoSQLHelper.CONSULTA_PRODUCTO, DBConnection.Open());
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

            List<Producto> itemsProducto = new List<Producto>();

            Producto producto = null;

            DBConnection.Open();
            while (reader.Read())
            {
                producto = new Producto();

                producto.Nombre = reader["nombre"].ToString();
                producto.Publicado = int.Parse(reader["publicado"].ToString());
                producto.Id = int.Parse(reader["id"].ToString());

                itemsProducto.Add(producto);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsProducto;
        }

        public static List<Pais> ConsultaPublicado()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(PaisSQLHelper.CONSULTA_PAIS_ACTIVO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Pais> itemsPais = new List<Pais>();

            Pais pais = null;

            DBConnection.Open();
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

        public bool EliminarProducto(int idProducto)
        {
            bool resultado = false;
            //try
            //{

                SqlCommand cmdEliminaProducto = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaProducto.CommandText = ProductoSQLHelper.DELETE_PRODUCTO;
                cmdEliminaProducto.CommandType = CommandType.StoredProcedure;
                cmdEliminaProducto.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idProducto;

                cmdEliminaProducto.Parameters.Add(parametro);
                cmdEliminaProducto.ExecuteReader();
                DBConnection.Close(cmdEliminaProducto.Connection);
                resultado = true;
            //}
            //catch (Exception exc)
            //{
            //    Console.Write("Ocurrio un error al eliminar el Producto" + exc);
            //    return resultado;
            //}
            return resultado;
        }

        /*****************************************************************************************/

        public Producto ConsultarUnProducto(int idProducto)
        {

            Producto producto = new Producto();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = ProductoSQLHelper.CONSULTA_UN_PRODUCTO;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idProducto;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    producto.Id = drConsulta.GetInt32(0);
                    producto.Nombre = drConsulta.GetString(1);
                    producto.Descripcion = drConsulta.GetString(2);
                    producto.PadecimientoId = drConsulta.GetInt32(3);
                    //producto.Foto = drConsulta.GetString(4);
                    producto.Publicado = drConsulta.GetInt32(5);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return producto;
        }

        public List<int> ConsultaProductoPais(int idProducto, int usuarioID, int rolID)
        {

            int idpais;
            List<int> itemsPais = new List<int>();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = ProductoSQLHelper.CONSULTA_PRODUCTO_PAIS;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idProducto;
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

        public bool ModificarProducto(Producto producto, int[] paisesElegidos, int usuarioID, int rolID)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = ProductoSQLHelper.UPDATE_PRODUCTO;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[6];

            parametros[0] = new SqlParameter(ProductoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = producto.Id;

            parametros[1] = new SqlParameter(ProductoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = producto.Nombre;

            parametros[2] = new SqlParameter(ProductoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = producto.Descripcion;


            parametros[3] = new SqlParameter(ProductoSQLHelper.PARAMETRO_PADECIMIENTO_ID, SqlDbType.Int);
            parametros[3].Value = Convert.ToInt32(producto.PadecimientoId);


            parametros[4] = new SqlParameter(ProductoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[4].Value = producto.Foto;

            parametros[5] = new SqlParameter(ProductoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[5].Value = Convert.ToInt32(producto.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);

            List<int> paisesActivos = ConsultaProductoPais(producto.Id, usuarioID, rolID);
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

            ProductoDAO.InsertaProductoPais(producto.Id, paisNuevo);

           
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
                if (elimina) { EliminarProductoPais(act); }
            }

            Exito = true;

            return Exito;
        }

        public bool EliminarProductoPais(int idPais)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaProducto = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaProducto.CommandText = ProductoSQLHelper.DELETE_PRODUCTO_PAIS;
                cmdEliminaProducto.CommandType = CommandType.StoredProcedure;
                cmdEliminaProducto.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPais;

                cmdEliminaProducto.Parameters.Add(parametro);
                SqlDataReader dr = cmdEliminaProducto.ExecuteReader();
                dr.Close();
                dr.Dispose();
                //Ejecutamos el NonQuery

                DBConnection.Close(cmdEliminaProducto.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Producto" + exc);
                return resultado;
            }
            return resultado;
        }
    }
}
