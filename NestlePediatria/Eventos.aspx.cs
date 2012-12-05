﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;


namespace NestlePediatria
{
    public partial class Eventos : System.Web.UI.Page
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
                CargaEventos();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditEvento.aspx");
        }

        protected void dgEventos_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "btnEditar")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddEditEvento.aspx?idEvento=" + id);

                }
                catch (Exception)
                {
                    // lblerror.Text = "ocurrió un error al intentar Editar";
                }
            }

            if (e.CommandName == "btnEliminar")
            {
                //try
                //{
                    int indice = Convert.ToInt32(e.CommandArgument);
                    EventoDAO bdEvento = new EventoDAO();

                    if (bdEvento.EliminarEvento(indice))
                    {
                        lblConfirmacion.Text = "Se eliminó correctamente";
                        CargaEventos();

                    }
                    else
                        lblConfirmacion.Text = "ocurrió un error al intentar eliminar";

                //}
                //catch (Exception)
                //{
                //    lblerror.Text = "ocurrió un error al intentar eliminar";
                //}

            }
        }

        protected void dgEventos_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void CargaEventos()
        {
            List<Evento> categoriaGrid = EventoDAO.Consulta(int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));
            dgEventos.DataSource = categoriaGrid;
            dgEventos.DataBind();


        }
    }
}