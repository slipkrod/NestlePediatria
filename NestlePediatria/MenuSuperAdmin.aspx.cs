using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NestlePediatria
{
    public partial class MenuSuperAdmin : System.Web.UI.Page
    {
        public void Redireccion(int RolId)
        {
            if (RolId == 1)
            {//Administrador 
                Response.Redirect("Mostrar.aspx");
            }

            if (RolId == 2)
            { //Super Administrador
                Response.Redirect("MenuSuperAdmin.aspx");
            }

            if (RolId == 3 || RolId == 4)//Menu Doctor/Asistente
            {
                Response.Redirect("MenuDoctor.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (Session["user"] != null && Session["rol"] != null)
            {

                int rol = int.Parse(Session["rol"].ToString());
                if (rol != 2)
                {
                    Redireccion(rol);
                }
            }
        }

        protected void btnPaises_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paises.aspx");
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }
    }
}