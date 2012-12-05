using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataSource
{
    public class DBConnection
    {
        public DBConnection()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static SqlConnection Open()
        {
            //creamos el objeto conexion
            SqlConnection conGestionEmpleados = new SqlConnection();
            try
            {

                conGestionEmpleados.ConnectionString = ConfigurationManager.ConnectionStrings["conecta"].ConnectionString;
                conGestionEmpleados.Open();
                // devolvemos el objeto de conexion Abierto

            }
            catch (SqlException sqlexc)
            {
                sqlexc.ToString();
            }
            return conGestionEmpleados;
        }

        public static bool Close(SqlConnection connection)
        {

            // devolvemos el objeto de conexion Cerrado
            connection.Close();
            
            return true;
        }
    }
}
