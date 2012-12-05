using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using Models;
using DataAccess;


namespace NestlePediatria
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        public string c;
        

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
            c = Request.QueryString["fc"].ToString();

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
            

            if (!IsPostBack)
            {
                lblFecha.Text = "Citas con Fecha "+c;
                CargaCitas();
               /* string letra = null;

                DropDownPacientes.DataSource = PacienteDAO.Consulta(letra,int.Parse(Session["doctorId"].ToString()));
                DropDownPacientes.DataTextField = "nombre";
                DropDownPacientes.DataValueField = "id";
                // Bind the data to the control.
                DropDownPacientes.DataBind();
                // Set the default selected item, if desired.
                DropDownPacientes.SelectedIndex = 0;*/
            }
        }



        protected void dgCitas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "btnEliminar")
            {
                try
                {
                    
                    int indice = Convert.ToInt32(e.CommandArgument);
                    CitaDAO bdCategoria = new CitaDAO();
                    Cita cita = new Cita();
                    cita.Id = indice;
                    
                    //cita.PacienteId = DataBinder.Eval(es.Row.DataItem, "PacienteId").ToString();

                    if (bdCategoria.EliminarCita(cita, int.Parse(Session["doctorId"].ToString())) != 0)
                    {
                        Label1.Text = "Se eliminó correctamente";
                        CargaCitas();

                    }
                    else
                    {
                        Label1.Text = "ocurrió un error al intentar eliminar";
                    }

                }
                catch (Exception exc)
                {
                    Label1.Text = "ocurrió un error al intentar eliminar" + exc;
                }

            }
        }

        protected void dgCitas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
             /*   DateTime horaf;
                horaf = new DateTime();
                horaf = DateTime.ParseExact(rowsAffected[2].ToString(), "dd/MM/yyyy hh:mm:ss tt", null);*/

                
                DateTime horaIn = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "horaInicio").ToString());
                   
                Control ctr = e.Row.FindControl("lblInicio");
                Label hora_inicio = (Label)ctr;
                //hora_inicio.Text = horaIn.ToString();
                hora_inicio.Text = horaIn.ToString("HH:mm");


                DateTime horaFin = DateTime.Parse(DataBinder.Eval(e.Row.DataItem, "horaFinal").ToString());
                Control ctr2 = e.Row.FindControl("lblFinal");
                Label hora_final = (Label)ctr2;
                hora_final.Text = horaFin.ToString("HH:mm");

                Control ctr3 = e.Row.FindControl("lblEditar");
                Label liga = (Label)ctr3;
                liga.Text = "<a class='rojo' href='AddEditCita.aspx?fc=" + Request.QueryString["fc"].ToString() + "&idCita=" +int.Parse(DataBinder.Eval(e.Row.DataItem, "Id").ToString()) + "&iframe=true&amp;width=400&amp;height=240' rel='prettyPhoto[iframe]'>Editar</a>";
                
            }
        }

        protected void CargaCitas()
        {
            string fecha = Request.QueryString["fc"].ToString();
            if (fecha != null && fecha !="")
            {
                try
                {
                    List<Cita> categoriaGrid = CitaDAO.Consulta(int.Parse(Session["doctorId"].ToString()), Request.QueryString["fc"].ToString());
                    dgCitas.DataSource = categoriaGrid;
                    dgCitas.DataBind();
                }
                catch (FormatException ) {
                    Label1.Text = "No existe Fecha para la cita";
                }
                
            }
            else {
                Label1.Text = "No existe Fecha para la cita";
            }

        }

        

       

        
    }
}