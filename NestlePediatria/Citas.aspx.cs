using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NestlePediatria
{
    public partial class Citas : System.Web.UI.Page
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


                if (rol == 1 || rol == 2)
                {
                    Redireccion(rol);
                }

            }

            if(!Page.IsPostBack){
            fechabuscar.Text =  DateTime.Today.Day.ToString() +'/'+ DateTime.Today.Month.ToString() +'/'+DateTime.Today.Year.ToString();
        }
        }

        protected void submitfecha_Click(object sender, EventArgs e)
        {
            string fc = fechabuscar.Text;
            Response.Redirect("CitasDia.aspx?fc="+fc);
        }
    }
}