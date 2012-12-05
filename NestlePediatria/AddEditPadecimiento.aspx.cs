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
    public partial class AddEditPadecimiento : System.Web.UI.Page
    {
        private int idPadecimiento;

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
            

            idPadecimiento = Convert.ToInt32(Request.QueryString["idPadecimiento"]);
            if (!IsPostBack) {
                if (idPadecimiento != 0)//es modificacion
                {
                    btnGuardar.Text = "Modificar";

                    try
                    {
                        PadecimientoDAO bdpadecimiento = new PadecimientoDAO();
                        Padecimiento modificarCategoria = bdpadecimiento.ConsultarUnPadecimiento(idPadecimiento);
                        txtNombre.Text = modificarCategoria.Nombre;
                        txtDescripcion.Text = modificarCategoria.Descripcion;
                        if (modificarCategoria.Publicado == 1)
                        {
                            CheckPublicado.Checked = true;
                        }

                    }
                    catch (Exception)
                    {
                        lblMensaje.Text = "ocurrió un error al cargar los datos";
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Padecimientos.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Padecimiento padecimiento = new Padecimiento();
            padecimiento.Nombre = txtNombre.Text;
            padecimiento.Descripcion = txtDescripcion.Text;
            if (CheckPublicado.Checked)
            {
                padecimiento.Publicado = 1;
            }
            else
            {
                padecimiento.Publicado = 0;
            }

            try
            {
                PadecimientoDAO padecimientoDao = new PadecimientoDAO();

                if (idPadecimiento != 0)//modificacion
                {

                    padecimiento.Id = idPadecimiento;

                    if (padecimientoDao.ModificarPadecimiento(padecimiento))
                    {
                        lblMensaje.Text = "Se ha modificado correctamente";

                    }
                    else
                    {
                        lblMensaje.Text = "Error al tratar de modificar";
                    }



                }
                else
                {//es Alta de Padecimiento
                    padecimientoDao.Inserta(padecimiento);

                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Se ingreso correctamente el Padecimiento";
                    limpia();

                }

                

            }
            catch (Exception exe)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error Mensaje:" + exe;
            }
        }

        public void limpia()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            if (CheckPublicado.Checked)
            {
                CheckPublicado.Checked = false;
            }
        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Padecimientos.aspx");
        }
    }
}