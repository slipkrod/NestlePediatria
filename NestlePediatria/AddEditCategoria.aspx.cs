using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;

namespace NestlePediatria
{
    public partial class AddEditCategoria : System.Web.UI.Page
    {
        private int idCategoria;

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
            

            idCategoria = Convert.ToInt32(Request.QueryString["idCategoria"]);
            if (!IsPostBack) {
                if (idCategoria != 0)//es modificacion
                {
                    btnGuardar.Text = "Modificar";

                    try
                    {
                        CategoriaDAO bdcategoria = new CategoriaDAO();
                        Categoria modificarCategoria = bdcategoria.ConsultarUnaCategoria(idCategoria);
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
            Response.Redirect("Categorias.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Nombre = txtNombre.Text;
            categoria.Descripcion = txtDescripcion.Text;
            if (CheckPublicado.Checked)
            {
                categoria.Publicado = 1;
            }
            else
            {
                categoria.Publicado = 0;
            }

            try
            {
                CategoriaDAO categoriaDao = new CategoriaDAO();

                if (idCategoria != 0)//modificacion
                {

                    categoria.Id = idCategoria;

                    if (categoriaDao.ModificarCategoria(categoria))
                    {
                        lblMensaje.Text = "Se ha modificado correctamente";

                    }
                    else
                    {
                        lblMensaje.Text = "Error al tratar de modificar";
                    }



                }
                else
                {//es Alta de Categoria
                    categoriaDao.Inserta(categoria);
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Se ingreso correctamente la Categoria";
                    limpia();

                }
                

            }catch(Exception exe)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error Mensaje:" + exe;
            }
        }

        public void limpia() {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            if (CheckPublicado.Checked) {
                CheckPublicado.Checked = false;
            }
        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Categorias.aspx");
        }
    }
}