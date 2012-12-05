using System;
using System.Collections.Generic;
using System.Text;
using DataSource;
using Models;
using System.Data;
using System.Data.SqlClient;
using SQLHelpers;
using System.Security.Cryptography;

namespace DataAccess
{
    public class UsuarioDAO
    {
        
        public int Inserta(Usuario usuario) {

            SqlCommand cmdAgregarEvento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarEvento.CommandText = SQLHelpers.UsuarioSQLHelper.INSERTA_USUARIO;

            cmdAgregarEvento.CommandType = CommandType.StoredProcedure;
            cmdAgregarEvento.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(UsuarioSQLHelper.PARAMETRO_FLAG, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdAgregarEvento.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[5];

            //Asignando Valores

            string encriptada = Encriptacion.encriptar(usuario.Password);
            //Response.Write(encriptada);
            //string desencriptada = Encriptacion.desencriptar(encriptada, clave);
            //Response.Write(desencriptada);

            parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIO, SqlDbType.NVarChar,100);
            parametros[0].Value = usuario.Usuarios;

            parametros[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_PASSWORD, SqlDbType.NVarChar,100);
            parametros[1].Value = encriptada;
            
            parametros[2] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
            parametros[2].Value = usuario.RolId;

            parametros[3] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar,200);
            parametros[3].Value = usuario.Nombre;
            

            parametros[4] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_HABILITADO, SqlDbType.Int);
            parametros[4].Value = usuario.Habilitado;

            //Agregando nuestros parametros al command
            cmdAgregarEvento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarEvento.ExecuteReader();
            int usuarioID = int.Parse(cmdAgregarEvento.Parameters[UsuarioSQLHelper.PARAMETRO_FLAG].Value.ToString());
            dr.Close();
            dr.Dispose();
            DBConnection.Close(cmdAgregarEvento.Connection);
            
            // Cerramos la conexion

            return usuarioID;
        
        }

        public int InsertaUserPais(Usuario usuario, List<Pais> pais)
        {
            int IdUsuario = usuario.Id;

            if (usuario.Id == 0)
            { //Hacemos el Alta de Usuario
                IdUsuario = this.Inserta(usuario);
            }
            
            
            if (IdUsuario != 0 && usuario.RolId == 1) { //Si existe el usuario y el Rol es Administrador agregamos los países
            foreach (Pais data in pais)
            {
                SqlCommand cmdAgregarEventoPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarEventoPais.CommandText = SQLHelpers.UsuarioSQLHelper.INSERTA_USUARIO_PAIS;

                cmdAgregarEventoPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarEventoPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                parametros1[0].Value = IdUsuario;

                parametros1[1] = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros1[1].Value = data.Id;
                //Agregando nuestros parametros al command
                cmdAgregarEventoPais.Parameters.AddRange(parametros1);


                //Ejecutamos el NonQuery
                SqlDataReader dr = cmdAgregarEventoPais.ExecuteReader();
                dr.Close();
                dr.Dispose();
                DBConnection.Close(cmdAgregarEventoPais.Connection);
            }
        }

            return IdUsuario;

        }

        public List<Usuario> Consulta()
        {
            List<Usuario> itemsUsuarios = new List<Usuario>();
            
            try {
                //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(UsuarioSQLHelper.CONSULTA_USUARIO, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            

            Usuario usuario = null;

            DBConnection.Open();
            while (reader.Read())
            {
                usuario = new Usuario();

                usuario.Id = int.Parse(reader["id"].ToString());
                usuario.RolId = int.Parse(reader["rol_id"].ToString());
                usuario.Usuarios = reader["usuario"].ToString();
                usuario.Nombre = reader["nombre"].ToString();
                usuario.Habilitado = int.Parse(reader["habilitado"].ToString());


                itemsUsuarios.Add(usuario);

            }
            DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception esc) { }
            return itemsUsuarios;
        }


        public Usuario ConsultaUnUsuario(int idUsuario)
        {
            Usuario usuario = new Usuario();

            try
            {
                SqlCommand cmdConsultaPais = new SqlCommand();

                cmdConsultaPais.CommandText = UsuarioSQLHelper.CONSULTA_UN_USUARIO;
                cmdConsultaPais.CommandType = CommandType.StoredProcedure;
                cmdConsultaPais.Connection = DBConnection.Open();

                SqlParameter[] parametros = new SqlParameter[1];

                parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                parametros[0].Value = idUsuario;

                cmdConsultaPais.Parameters.AddRange(parametros);

                SqlDataReader drConsulta = cmdConsultaPais.ExecuteReader();
                
                while (drConsulta.Read())
                {
                    usuario.Id = drConsulta.GetInt32(0);
                    usuario.Usuarios = drConsulta.GetString(1);
                    usuario.Password = drConsulta.GetString(2);
                    usuario.RolId = drConsulta.GetInt32(3);
                    usuario.Nombre = drConsulta.GetString(4);
                    usuario.Habilitado = drConsulta.GetInt32(5);

                }

                DBConnection.Close(cmdConsultaPais.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);

            }
            return usuario;
        }

        public List<int> ConsultaUsuarioPais(int idAUsuario)
        {

            int idpais;
            List<int> itemsPais = new List<int>();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = UsuarioSQLHelper.CONSULTA_USUARIO_PAIS;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                parametro.Value = idAUsuario;
                cmdConsulta.Parameters.Add(parametro);

                // numConsulta = (int)cmdConsulta.ExecuteScalar();

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

        public bool ModificarUsuario(Usuario usuario, int[] paisesElegidos)
        {
            bool Exito = false;
            try
            {
                SqlCommand cmdModificarPais = new SqlCommand();
                if (usuario.Password != null && usuario.Password != "")
                {
                    string encriptada = Encriptacion.encriptar(usuario.Password);
                    cmdModificarPais.CommandText = UsuarioSQLHelper.UPDATE_USUARIO_PASSWORD;

                    SqlParameter[] parametros = new SqlParameter[5];

                    parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                    parametros[0].Value = usuario.Id;

                    
                    parametros[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_PASSWORD, SqlDbType.NVarChar, 100);
                    parametros[1].Value = encriptada;

                    parametros[2] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
                    parametros[2].Value = usuario.RolId;

                    parametros[3] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 200);
                    parametros[3].Value = usuario.Nombre;


                    parametros[4] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_HABILITADO, SqlDbType.Int);
                    parametros[4].Value = usuario.Habilitado;

                    cmdModificarPais.Parameters.AddRange(parametros);
                }
                else
                {
                    cmdModificarPais.CommandText = UsuarioSQLHelper.UPDATE_USUARIO;
                    SqlParameter[] parametros = new SqlParameter[4];

                    parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                    parametros[0].Value = usuario.Id;

                    parametros[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_ROL_ID, SqlDbType.Int);
                    parametros[1].Value = usuario.RolId;

                    parametros[2] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 200);
                    parametros[2].Value = usuario.Nombre;


                    parametros[3] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_HABILITADO, SqlDbType.Int);
                    parametros[3].Value = usuario.Habilitado;

                    cmdModificarPais.Parameters.AddRange(parametros);
                }
                cmdModificarPais.CommandType = CommandType.StoredProcedure;
                cmdModificarPais.Connection = DBConnection.Open();

                
                cmdModificarPais.ExecuteReader();

                DBConnection.Close(cmdModificarPais.Connection);
                if (usuario.RolId == 2)
                {
                    this.DeleteUserPais(usuario.Id);
                }
                else {//Si es Administrador actualizar Países

                    List<int> paisesActivos = ConsultaUsuarioPais(usuario.Id);
                    //paisesElegidos
                    if (paisesActivos.Count == 0)
                    {//era Super Administrador y no tiene paises activos
                        List<Pais> paisNuevo = new List<Pais>();
                        Pais paisagr = null;
                        foreach (int eleg1 in paisesElegidos)
                        {
                            
                            
                            
                                paisagr = new Pais();
                                paisagr.Id = eleg1;
                                paisNuevo.Add(paisagr);
                           
                        }

                        InsertaUserPais(usuario, paisNuevo);

                    }
                    else {
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

                        InsertaUserPais(usuario, paisNuevo);




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
                            if (elimina) { EliminarUsuarioPais(act); }
                        }
                    }
                    
                
                }
                Exito = true;
            }
            catch (Exception exc)
            {
                Console.Write(exc);
                return false;
            }

            return Exito;

        }


        public int VerificarUsuario(Usuario usuario)
        {
            int verifica = 0;
            // Creamos el objeto command
            SqlCommand cmdVerificaPassword = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdVerificaPassword.CommandText = SQLHelpers.UsuarioSQLHelper.VERIFICA_USUARIO;
            cmdVerificaPassword.CommandType = CommandType.StoredProcedure;
            cmdVerificaPassword.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(UsuarioSQLHelper.PARAMETRO_VERIFICACION, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdVerificaPassword.Parameters.Add(prmEventoId);

            string encriptada = Encriptacion.encriptar(usuario.Password);

            SqlParameter[] parametros = new SqlParameter[2];

            //Asignando Valores

            parametros[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIO, SqlDbType.NVarChar, 100);
            parametros[0].Value = usuario.Usuarios;

            parametros[1] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_PASSWORD, SqlDbType.NVarChar, 100);
            parametros[1].Value = encriptada;

            //Agregando nuestros parametros al command
            cmdVerificaPassword.Parameters.AddRange(parametros);

            // Creacion del SqlDataReader
            SqlDataReader drConsulta = cmdVerificaPassword.ExecuteReader();
            verifica = int.Parse(cmdVerificaPassword.Parameters[UsuarioSQLHelper.PARAMETRO_VERIFICACION].Value.ToString());

            drConsulta.Close();
            drConsulta.Dispose();
            DBConnection.Close(cmdVerificaPassword.Connection);
            return verifica;

        }

        public int DeleteUserPais(int IdUsuario)//Elimina a todos los países aspciados a un usuario
        {
            

                SqlCommand cmdAgregarEventoPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarEventoPais.CommandText = SQLHelpers.UsuarioSQLHelper.DELETE_USUARIO_PAIS;

                cmdAgregarEventoPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarEventoPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[1];
                //Asignando Valores


                parametros1[0] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
                parametros1[0].Value = IdUsuario;

                
                //Agregando nuestros parametros al command
                cmdAgregarEventoPais.Parameters.AddRange(parametros1);


                //Ejecutamos el NonQuery
                SqlDataReader dr = cmdAgregarEventoPais.ExecuteReader();
                dr.Close();
                dr.Dispose();
                DBConnection.Close(cmdAgregarEventoPais.Connection);
            

            return IdUsuario;

        }

//Elimina los Países que ya no tenga el usuario
        public bool EliminarUsuarioPais(int idPais)
        {
            
            bool resultado = false;
            try
            {

                SqlCommand cmdElimina = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdElimina.CommandText = UsuarioSQLHelper.DELETE_USUARIO_PAIS_D;
                cmdElimina.CommandType = CommandType.StoredProcedure;
                cmdElimina.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPais;

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
