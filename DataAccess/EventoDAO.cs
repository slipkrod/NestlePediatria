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
    public class EventoDAO
    {
        public EventoDAO() { }

        public static int Inserta(Evento evento,List<Pais> pais,int usuarioID)
        {
           
            SqlCommand cmdAgregarEvento = new SqlCommand();
            // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
            cmdAgregarEvento.CommandText = SQLHelpers.EventoSQLHelper.INSERTA_EVENTO;
                
            cmdAgregarEvento.CommandType = CommandType.StoredProcedure;
            cmdAgregarEvento.Connection = DBConnection.Open();

            SqlParameter prmEventoId = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            prmEventoId.Direction = ParameterDirection.Output;
            cmdAgregarEvento.Parameters.Add(prmEventoId);

            //Declarando los Parametros
            SqlParameter[] parametros = new SqlParameter[9];

            //Asignando Valores
            //parametros[0] = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            //parametros[0].Value = ParameterDirection.Output;

            parametros[0] = new SqlParameter(EventoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[0].Value = evento.Nombre;

            parametros[1] = new SqlParameter(EventoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[1].Value = evento.Descripcion;

            parametros[2] = new SqlParameter(EventoSQLHelper.PARAMETRO_FECHAINICIO, SqlDbType.DateTime);
            parametros[2].Value = Convert.ToDateTime(evento.FechaInicio);
            //parametros[2].Value = Convert.ToDateTime("17/10/2012");

            parametros[3] = new SqlParameter(EventoSQLHelper.PARAMETRO_FECHAFIN, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(evento.FechaFinal);
            //parametros[3].Value = Convert.ToDateTime("20/10/2012");

            parametros[4] = new SqlParameter(EventoSQLHelper.PARAMETRO_LUGAR, SqlDbType.NVarChar, 150);
            parametros[4].Value = evento.Lugar;

            parametros[5] = new SqlParameter(EventoSQLHelper.PARAMETRO_DIRIGIDO, SqlDbType.NVarChar,150);
            parametros[5].Value = evento.Dirigido;

            parametros[6] = new SqlParameter(EventoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[6].Value = evento.Foto;

            parametros[7] = new SqlParameter(EventoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[7].Value = Convert.ToInt32(evento.Publicado);

            parametros[8] = new SqlParameter(UsuarioSQLHelper.PARAMETRO_USUARIOID, SqlDbType.Int);
            parametros[8].Value = usuarioID;
            

            
            //Agregando nuestros parametros al command
            cmdAgregarEvento.Parameters.AddRange(parametros);


            //Ejecutamos el NonQuery

            SqlDataReader dr = cmdAgregarEvento.ExecuteReader();
            int eventID = int.Parse(cmdAgregarEvento.Parameters[EventoSQLHelper.PARAMETRO_ID].Value.ToString());
            dr.Close();
            dr.Dispose();
            DBConnection.Close(cmdAgregarEvento.Connection);
            InsertaEventoPais(eventID, pais);
            // Cerramos la conexion
           
            return eventID;
        }


        public static void InsertaEventoPais(int eventoID, List<Pais> pais) {
            

            

            foreach (Pais data in pais)
            {
                SqlCommand cmdAgregarEventoPais = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdAgregarEventoPais.CommandText = SQLHelpers.EventoSQLHelper.INSERTA_EVENTO_PAIS;

                cmdAgregarEventoPais.CommandType = CommandType.StoredProcedure;
                cmdAgregarEventoPais.Connection = DBConnection.Open();

                //Declarando los Parametros
                SqlParameter[] parametros1 = new SqlParameter[2];
                //Asignando Valores


                parametros1[0] = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametros1[0].Value = eventoID;

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

        public static List<Evento> Consulta(int usuarioID, int rolID)
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(EventoSQLHelper.CONSULTA_EVENTO, DBConnection.Open());
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

            List<Evento> itemsEventos = new List<Evento>();

            Evento evento = null;

            DBConnection.Open();
            while (reader.Read())
            {
                evento = new Evento();

                evento.Nombre = reader["nombre"].ToString();
                evento.Publicado = int.Parse(reader["publicado"].ToString());
                evento.Id = int.Parse(reader["id"].ToString());

                itemsEventos.Add(evento);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsEventos;
        }

        public bool EliminarEvento(int idEvento)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaEvento = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaEvento.CommandText = EventoSQLHelper.DELETE_EVENTO;
                cmdEliminaEvento.CommandType = CommandType.StoredProcedure;
                cmdEliminaEvento.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idEvento;

                cmdEliminaEvento.Parameters.Add(parametro);
                cmdEliminaEvento.ExecuteReader();
                DBConnection.Close(cmdEliminaEvento.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Evento" + exc);
                return resultado;
            }
            return resultado;
        }

        public Evento ConsultarUnEvento(int idEvento)
        {

            Evento evento = new Evento();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = EventoSQLHelper.CONSULTA_UN_EVENTO;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idEvento;
                cmdConsulta.Parameters.Add(parametro);

                SqlDataReader drConsulta = cmdConsulta.ExecuteReader();

                while (drConsulta.Read())
                {
                    evento.Id = drConsulta.GetInt32(0);
                    evento.Nombre = drConsulta.GetString(1);
                    evento.Descripcion = drConsulta.GetString(2);
                    evento.FechaInicio = drConsulta.GetString(3);
                    evento.FechaFinal = drConsulta.GetString(4);
                    evento.Lugar = drConsulta.GetString(5);
                    evento.Dirigido = drConsulta.GetString(6);
                    //evento.Foto = (Byte[])drConsulta.GetSqlByte(7);
                    evento.Publicado = drConsulta.GetInt32(8);


                }
                evento.Foto = ConsultaImagen(evento.Id);

                DBConnection.Close(cmdConsulta.Connection);
            }
            catch (Exception exc)
            {
                Console.Write(exc);
            }
            return evento;
        }

        public List<int> ConsultaEventoPais(int idEvento, int usuarioID, int rolID)
        {

            int idpais;
            List<int> itemsPais = new List<int>();
            try
            {
                SqlCommand cmdConsulta = new SqlCommand();
                cmdConsulta.CommandText = EventoSQLHelper.CONSULTA_EVENTO_PAIS;
                cmdConsulta.CommandType = CommandType.StoredProcedure;
                cmdConsulta.Connection = DBConnection.Open();


                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idEvento;
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

        public bool ModificarEvento(Evento evento, int[] paisesElegidos, int usuarioID, int rolID)
        {
            bool Exito = false;

            SqlCommand cmdModificar = new SqlCommand();

            cmdModificar.CommandText = EventoSQLHelper.UPDATE_EVENTO;
            cmdModificar.CommandType = CommandType.StoredProcedure;
            cmdModificar.Connection = DBConnection.Open();

            SqlParameter[] parametros = new SqlParameter[9];

            parametros[0] = new SqlParameter(EventoSQLHelper.PARAMETRO_ID, SqlDbType.Int);
            parametros[0].Value = evento.Id;

            parametros[1] = new SqlParameter(EventoSQLHelper.PARAMETRO_NOMBRE, SqlDbType.NVarChar, 100);
            parametros[1].Value = evento.Nombre;

            parametros[2] = new SqlParameter(EventoSQLHelper.PARAMETRO_DESCRIPCION, SqlDbType.NVarChar);
            parametros[2].Value = evento.Descripcion;

            parametros[3] = new SqlParameter(EventoSQLHelper.PARAMETRO_FECHAINICIO, SqlDbType.DateTime);
            parametros[3].Value = Convert.ToDateTime(evento.FechaInicio);
            

            parametros[4] = new SqlParameter(EventoSQLHelper.PARAMETRO_FECHAFIN, SqlDbType.DateTime);
            parametros[4].Value = Convert.ToDateTime(evento.FechaFinal);
           

            parametros[5] = new SqlParameter(EventoSQLHelper.PARAMETRO_LUGAR, SqlDbType.NVarChar, 150);
            parametros[5].Value = evento.Lugar;

            parametros[6] = new SqlParameter(EventoSQLHelper.PARAMETRO_DIRIGIDO, SqlDbType.NVarChar, 150);
            parametros[6].Value = evento.Dirigido;

            parametros[7] = new SqlParameter(EventoSQLHelper.PARAMETRO_FOTO, SqlDbType.VarBinary, 7000);
            parametros[7].Value = evento.Foto;

            parametros[8] = new SqlParameter(EventoSQLHelper.PARAMETRO_PUBLICADO, SqlDbType.Int);
            parametros[8].Value = Convert.ToInt32(evento.Publicado);

            cmdModificar.Parameters.AddRange(parametros);
            cmdModificar.ExecuteReader();

            DBConnection.Close(cmdModificar.Connection);
            
            List<int> paisesActivos = ConsultaEventoPais(evento.Id,usuarioID, rolID);
            //paisesElegidos

            //checar los paises seleccionados vs los que tengo registrados
            //los que no esten en los seleccionados y en los registrados si a esos eliminarlos
            int[] arrpaisesActivos = new int[paisesActivos.ToArray().Length];
            int contpa = 0;

            foreach(int pa in paisesActivos){
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
                if (agrega) {
                    paisagr = new Pais();
                    paisagr.Id = eleg1;
                    paisNuevo.Add(paisagr);
                }
            }

            EventoDAO.InsertaEventoPais(evento.Id, paisNuevo);

           

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
                    else {
                        elimina = true;
                    }

                }
                if (elimina) { EliminarEventoPais(act); }
            }
            
            
         
           
            //los que no esten en los registrados pero si en los seleccionados registrarlos ocmo uno nuevo

            Exito = true;

            return Exito;
        }

        public bool EliminarEventoPais(int idPais)
        {
            bool resultado = false;
            try
            {

                SqlCommand cmdEliminaEvento = new SqlCommand();
                // Indicamos sus parametro de CommandTExt y la Conexion. del Objeto Command
                cmdEliminaEvento.CommandText = EventoSQLHelper.DELETE_EVENTO_PAIS;
                cmdEliminaEvento.CommandType = CommandType.StoredProcedure;
                cmdEliminaEvento.Connection = DBConnection.Open();

                //Declaramos el parametro
                SqlParameter parametro = new SqlParameter();
                parametro = new SqlParameter(PaisSQLHelper.PARAMETRO_ID, SqlDbType.Int);
                parametro.Value = idPais;

                cmdEliminaEvento.Parameters.Add(parametro);
                SqlDataReader dr = cmdEliminaEvento.ExecuteReader();
                dr.Close();
                dr.Dispose();
                //Ejecutamos el NonQuery
                
                DBConnection.Close(cmdEliminaEvento.Connection);
                resultado = true;
            }
            catch (Exception exc)
            {
                Console.Write("Ocurrio un error al eliminar el Evento" + exc);
                return resultado;
            }
            return resultado;
        }

        public Byte[] ConsultaImagen(int imagenId) {
            //Mostramos la ImagenFile por pantalla
            string sql = "Select foto From evento Where id = " + imagenId;
            SqlCommand SqlCom = new SqlCommand(sql, DBConnection.Open());

            Byte[] byteImage = (Byte[])SqlCom.ExecuteScalar();
            //byte[] byteImage = (byte[])SqlCom.Ex();
            //SqlConn.Close();
            return byteImage;
        }
    }
}
