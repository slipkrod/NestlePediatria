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
    public class MaterialDAO
    {
        public MaterialDAO() { }

        public static List<Material> Consulta(int usuarioID, int rolID)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(MaterialSQLHelper.CONSULTA_MATERIAL, DBConnection.Open());
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

            List<Material> itemsMateriales = new List<Material>();

            Material material = null;

            
            while (reader.Read())
            {
                material = new Material();

                material.Nombre = reader["nombre"].ToString();
                material.Publicado = int.Parse(reader["publicado"].ToString());
                material.Id = int.Parse(reader["id"].ToString());

                itemsMateriales.Add(material);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsMateriales;
        }

        public static int Inserta(Material material, List<Pais> pais, int usuarioID)
        {
           
            SqlCommand cmdAgregarMaterial = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarMaterial.CommandText = SQLHelpers.MaterialSQLHelper.INSERTA_MATERIAL;
                
            cmdAgregarMaterial.CommandType = CommandType.StoredProcedure;
            cmdAgregarMaterial.Connection = DBConnection.Open();

            SqlParameter prmMaterialId = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
            prmMaterialId.Direction = ParameterDirection.Output;
            cmdAgregarMaterial.Parameters.Add(prmMaterialId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[8];

            //Asignando Valores

            parametros[0] = new SqlParameter(MaterialSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = material.Nombre;

            parametros[1] = new SqlParameter(MaterialSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = material.Descripcion;

            parametros[2] = new SqlParameter(MaterialSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[2].Value = material.Autor;
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[3] = new SqlParameter(MaterialSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(material.Fecha);




            parametros[4] = new SqlParameter(MaterialSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[4].Value = material.Foto;

            parametros[5] = new SqlParameter(MaterialSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = material.Documento;

            parametros[6] = new SqlParameter(MaterialSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[6].Value = Convert.ToInt32(material.Publicado);

            parametros[7] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[7].Value = usuarioID;

            

            
            //Agregando nuestros parametros al command
            cmdAgregarMaterial.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarMaterial.ExecuteReader();
            int ArticID = int.Parse(cmdAgregarMaterial.Parameters[MaterialSQLHelper.PARAMETRO_MATERIALID].Value.ToString());
            dr.Close();
            DBConnection.Close(cmdAgregarMaterial.Connection);
            InsertaMaterialPais(ArticID, pais);
            // Cerramos la conexion
           
            return ArticID;
        }


        public static void InsertaMaterialPais(int MaterialID, List<Pais> pais)
        {




            foreach (Pais data in pais)
            {
                SqlCommand cmdAgregarMaterialPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarMaterialPais.CommandText = SQLHelpers.MaterialSQLHelper.INSERTA_MATERIAL_PAIS;

                cmdAgregarMaterialPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarMaterialPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
                parametros1[0].Value = MaterialID;

                parametros1[1] = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros1[1].Value = data.Id;
                //Agregando nuestros parametros al command
                cmdAgregarMaterialPais.Parameters.AddRange(parametros1);


                //Ejecutamos el NonQuery
                SqlDataReader dr = cmdAgregarMaterialPais.ExecuteReader();
                dr.Close();
                DBConnection.Close(cmdAgregarMaterialPais.Connection);
            }

        }

        public bool EliminarMaterial(int idMaterial)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaMaterial = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaMaterial.CommandText = MaterialSQLHelper.DELETE_MATERIAL;
                cmdEliminaMaterial.CommandType = CommandType.StoredProcedure;
                cmdEliminaMaterial.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
                parametro.Value = idMaterial;

                cmdEliminaMaterial.Parameters.Add(parametro);
                cmdEliminaMaterial.ExecuteReader();
                DBConnection.Close(cmdEliminaMaterial.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Material" + exc);
                return resultado;
            }
            return resultado;
        }

        /*******************************************************************************************/

        public Material ConsultarUnMaterial(int idMaterial)
        {

            Material material = new Material();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = MaterialSQLHelper.CONSULTA_UN_MATERIAL;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
                parametro.Value = idMaterial;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    material.Id = drConsulta.GetInt32(0);
                    material.Nombre = drConsulta.GetString(1);
                    material.Descripcion = drConsulta.GetString(2);
                    material.Autor = drConsulta.GetString(3);
                    material.Fecha = drConsulta.GetString(4);
                    //material.Foto = drConsulta.GetString(5);
                   // material.Documento = drConsulta.GetString(6);
                    material.Publicado = drConsulta.GetInt32(7);


                }

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return material;
        }

        public List<int> ConsultaMaterialPais(int idMaterial, int usuarioID, int rolID)
        {

            int idpais;
            List<int> itemsPais = new List<int>();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = MaterialSQLHelper.CONSULTA_MATERIAL_PAIS;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
                parametro.Value = idMaterial;
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

        public bool ModificarMaterial(Material material, int[] paisesElegidos, int usuarioID, int rolID)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = MaterialSQLHelper.UPDATE_MATERIAL;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[8];

            parametros[0] = new SqlParameter(MaterialSQLHelper.PARAMETRO_MATERIALID, SqlDbType.Int);
            parametros[0].Value = material.Id;

            parametros[1] = new SqlParameter(MaterialSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = material.Nombre;

            parametros[2] = new SqlParameter(MaterialSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = material.Descripcion;

            parametros[3] = new SqlParameter(MaterialSQLHelper.PARAMETRO_AUTOR, SqlDbType.NVarChar, 150);
            parametros[3].Value = material.Autor;
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[4] = new SqlParameter(MaterialSQLHelper.PARAMETRO_FECHA, SqlDbType.DateTime);
            parametros[4].Value = Convert.ToDateTime(material.Fecha);

            parametros[5] = new SqlParameter(MaterialSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[5].Value = material.Foto;

            parametros[6] = new SqlParameter(MaterialSQLHelper.PARAMETRO_DOCUMENTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = material.Documento;

            parametros[7] = new SqlParameter(MaterialSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[7].Value = Convert.ToInt32(material.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);

            List<int> paisesActivos = ConsultaMaterialPais(material.Id,usuarioID, rolID);
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

            MaterialDAO.InsertaMaterialPais(material.Id, paisNuevo);

            

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
                if (elimina) { EliminarMaterialPais(act); }
            }
            
            //los que no esten en los registrados pero si en los seleccionados registrarlos ocmo uno nuevo

            Exito = true;

            return Exito;
        }

        public bool EliminarMaterialPais(int idEvento)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdElimina = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdElimina.CommandText = MaterialSQLHelper.DELETE_MATERIAL_PAIS;
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
