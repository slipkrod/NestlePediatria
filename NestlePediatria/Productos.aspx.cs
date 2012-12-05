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
    public partial class Productos : System.Web.UI.Page
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
            

            if (!Page.IsPostBack)
            {
                CargaProductos();
            }
        }

        protected void dgProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEditar")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddEditProducto.aspx?idProducto=" + id);

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
                    ProductoDAO bdProducto = new ProductoDAO();

                    if (bdProducto.EliminarProducto(indice))
                    {
                        lblConfirmacion.Text = "Se eliminó correctamente";
                        CargaProductos();

                    }
                    else
                        lblConfirmacion.Text = "ocurrió un error al intentar eliminar";

                }
                catch (Exception exc)
                {
                    lblConfirmacion.Text = "ocurrió un error al intentar eliminar " + exc.ToString();
                }

            }
        }

        protected void dgProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int publicado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "publicado"));
                Control ctrl = e.Row.FindControl("Image1");
                Image imagen = (Image)ctrl;

                if (publicado == 1)
                {
                    imagen.ImageUrl = "Scripts/images/ledgreen.png";
                }
                else {
                    imagen.ImageUrl = "Scripts/images/ledred.png";
                }
            }
        }

        protected void LinkNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditProducto.aspx");
        }

        protected void CargaProductos()
        {
            List<Producto> productoGrid = ProductoDAO.Consulta(int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));
            dgProductos.DataSource = productoGrid;
            dgProductos.DataBind();


        }
    }
}