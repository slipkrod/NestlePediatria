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
    public class RolDAO
    {

        public List<Rol> Consulta()
        {
            //Creamos un objeto SQLcommand
            SqlCommand cmdConsulta = new SqlCommand(RolSQLHelper.CONSULTA_ROL, DBConnection.Open());
            cmdConsulta.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmdConsulta.ExecuteReader();

            List<Rol> itemsRoles = new List<Rol>();

            Rol rol = null;

            
            while (reader.Read())
            {
                rol = new Rol();

                rol.Id = int.Parse(reader["id"].ToString());
                rol.TipoRol = reader["tipo"].ToString();
                

                itemsRoles.Add(rol);

            }
            DBConnection.Close(cmdConsulta.Connection);

            return itemsRoles;
        }

    }
}
