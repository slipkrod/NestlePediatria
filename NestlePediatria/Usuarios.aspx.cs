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
    public partial class Usuarios : System.Web.UI.Page
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

                if (!IsPostBack)
                {
                    
                        this.CargaUsuarios();
                }

            
        }

        protected void dgUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEditar")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddEditUsuario.aspx?idusuario=" + id);

                }
                catch (Exception)
                {
                     lblConfirmacion.Text = "ocurrió un error al intentar Editar";
                }
            }

            if (e.CommandName == "btnEliminar")
            {
                //try
                // {
                
                /*int indice = Convert.ToInt32(e.CommandArgument);
                PaisDAO bdPais = new PaisDAO();

                if (bdPais.EliminarPais(indice))
                {
                    lblConfirmacion.Text = "Se eliminó correctamente";
                    CargaPaises();

                }
                else
                    lblConfirmacion.Text = "ocurrió un error al intentar eliminar";
                */
                

            }
        }

        protected void dgUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int publicado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "habilitado"));
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

                int rols = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RolId"));
                Control ctr3 = e.Row.FindControl("lblrol");
                Label liga = (Label)ctr3;

                if (rols == 1) {
                    liga.Text = "Administrador";
                }

                if (rols == 2) {
                    liga.Text = "Super Administrador";
                }
                
                
            }
        }

        protected void linkNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditUsuario.aspx");
        }

        protected void CargaUsuarios()
        {
            UsuarioDAO userDao = new UsuarioDAO();
            List<Usuario> UsuarioGrid = userDao.Consulta();
            dgUsuarios.DataSource = UsuarioGrid;
            dgUsuarios.DataBind();


        }

        protected void linkNuevo_Click1(object sender, EventArgs e)
        {
            Response.Redirect("AddEditUsuario.aspx");
        }
    }
}