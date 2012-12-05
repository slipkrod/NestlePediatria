using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NestlePediatria
{
    public partial class Mostrar : System.Web.UI.Page
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
                if (rol != 1 && rol != 2)
                {
                    Redireccion(rol);
                }
                if (rol == 1) {
                    btnAnnales.Visible = false;
                    btnNidito.Visible = false;
                    btnPaises.Visible = false;
                    btnUsuarios.Visible = false;
                }
            }

        }

        protected void btnEventos_Click(object sender, EventArgs e)
        {
            Response.Redirect("Eventos.aspx");
        }

        protected void btnArticulos_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuArticulos.aspx");
        }

        protected void btnVademecum_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuVademecum.aspx");
        }

        protected void btnMealplanner_Click(object sender, EventArgs e)
        {
            Response.Redirect("Meal.aspx");
        }

        protected void btlReportes_Click(object sender, EventArgs e)
        {

        }

        protected void btnMateriales_Click(object sender, EventArgs e)
        {
            Response.Redirect("Materiales.aspx");
        }

        protected void btnNidito_Click(object sender, EventArgs e)
        {
            Response.Redirect("Nidito.aspx");
        }

        protected void btnAnnales_Click(object sender, EventArgs e)
        {
            Response.Redirect("Annales.aspx");
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