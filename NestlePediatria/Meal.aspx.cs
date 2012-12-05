using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;

namespace NestlePediatria
{
    public partial class Meal : System.Web.UI.Page
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
            int rol = int.Parse(Session["rol"].ToString());
            if (Session["user"] != null && Session["rol"] != null)
            {


                if (rol != 1 && rol != 2)
                {
                    Redireccion(rol);
                }

                if (rol == 1)
                { //Esconder los menus del super administrador
                    linkAnnales.Visible = false;
                    linkNidito.Visible = false;
                    linkPaises.Visible = false;
                    linkUsuarios.Visible = false;
                }
            }

            if (!IsPostBack) {
                CargaPaises();
            }

        }

        protected void CargaPaises()
        {
            List<Pais> categoriaGrid = PaisDAO.ConsultaPublicado(int.Parse(Session["id"].ToString()));
            GridView1.DataSource = categoriaGrid;
            GridView1.DataBind();
            


        }

    }
}