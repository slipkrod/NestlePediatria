using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;

namespace NestlePediatria
{
    public partial class AddEditPais : System.Web.UI.Page
    {
        private int idPais;

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
            //linkNidito.Visible = false;

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
            idPais = Convert.ToInt32(Request.QueryString["idPais"]);
            if (!IsPostBack) { 
            
                if (idPais != 0)//es modificacion
                    {
                         btnGuardar.Text = "Modificar";

                        try
                        {
                            PaisDAO bdpais = new PaisDAO();
                            Pais modificarPais = bdpais.ConsultaUnPais(idPais);
                            txtNombre.Text = modificarPais.Nombre;
                            if (modificarPais.Publicado == 1) {
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Pais pais = new Pais();
            pais.Nombre = txtNombre.Text;
            if (CheckPublicado.Checked)
            {
                pais.Publicado = 1;
            }
            else
            {
                pais.Publicado = 0;
            }
            
            try 
            {
                PaisDAO paisDao = new PaisDAO();

                if (idPais != 0)//modificacion
                {

                    pais.Id = idPais;

                    if (paisDao.ModificarPais(pais))
                    {
                        lblMensaje.Text = "Se ha modificado correctamente";
                       
                    }
                    else
                    {
                        lblMensaje.Text = "Error al tratar de modificar";
                        
                    }



                }
                else {//es Alta de Pais
                    paisDao.Inserta(pais);
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Se ingreso correctamente el País";
                    txtNombre.Text = "";
                    if (CheckPublicado.Checked)
                    {
                        CheckPublicado.Checked = false;
                    }
                
                }
                
                
            }catch(Exception exe)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error Mensaje:" + exe;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paises.aspx");
        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paises.aspx");
        }
    }
}