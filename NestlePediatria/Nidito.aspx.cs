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
    public partial class Nidito : System.Web.UI.Page
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

                
                if (rol != 2)
                {
                    Redireccion(rol);
                }
                
            }
           
            if (!IsPostBack) {
                CargaNidito();
            }

        }

        protected void dgNidito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEditar")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddEditNidito.aspx?idNidito=" + id);

                }
                catch (Exception)
                {
                    // lblerror.Text = "ocurrió un error al intentar Editar";
                }
            }

            if (e.CommandName == "btnEliminar")
            {
                try
                {
                    int indice = Convert.ToInt32(e.CommandArgument);
                    NiditoDAO bdArticulo = new NiditoDAO();

                    if (bdArticulo.EliminarNidito(indice))
                    {
                        lblConfirmacion.Text = "Se eliminó correctamente";
                        CargaNidito();

                    }
                    else
                        lblConfirmacion.Text = "ocurrió un error al intentar eliminar";

                }
                catch (Exception)
                {
                    lblConfirmacion.Text = "ocurrió un error al intentar eliminar";
                }

            }
        }

        protected void dgNidito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int publicado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "publicado"));
                Control ctrl = e.Row.FindControl("Image1");
                Image imagen = (Image)ctrl;

                if (publicado == 1)
                {
                    imagen.ImageUrl = "Scripts/images/ledgreen.png";
                }
                else
                {
                    imagen.ImageUrl = "Scripts/images/ledred.png";
                }
            }
        }

        protected void LinkNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditNidito.aspx");
        }


        protected void CargaNidito()
        {
           List<Models.Nidito> articuloGrid = NiditoDAO.Consulta();
            dgNidito.DataSource = articuloGrid;
            dgNidito.DataBind();


        }
    }
}