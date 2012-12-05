using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;

namespace NestlePediatria
{
    public partial class AddEditCita : System.Web.UI.Page
    {
        private int idCita;

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

           string fecha = Request.QueryString["fc"].ToString();
           
           //lblMensaje.Text = "<a href='AddEditCita.aspx?fc=23/11/2012&iframe=true&amp;width=500&amp;height=500' rel='prettyPhoto[iframe]'>My site</a>";
           idCita = Convert.ToInt32(Request.QueryString["idCita"]);
           
           if (!IsPostBack)
           {
               string letra = null;

               DropDownPacientes.DataSource = PacienteDAO.Consulta(letra, int.Parse(Session["doctorId"].ToString()));
               DropDownPacientes.DataTextField = "nombre";
               DropDownPacientes.DataValueField = "id";
               // Bind the data to the control.
               DropDownPacientes.DataBind();
               // Set the default selected item, if desired.
               DropDownPacientes.SelectedIndex = 0;

               if (idCita != 0)
               { //es modificación
                   Button1.Text = "Modificar";
                   DropDownPacientes.Enabled = false;
                   
                   CitaDAO modificar = new CitaDAO();
                   Cita cita = new Cita();
                   cita = modificar.ConsultaUnCita(int.Parse(Session["doctorId"].ToString()), idCita);
                   DropDownPacientes.SelectedValue = cita.PacienteId.ToString();
                   horatxt.Text = cita.HoraInicio.ToString("HH:mm");
                   horatxt2.Text = cita.HoraFinal.ToString("HH:mm");

               }
           }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["fc"].ToString() != null)
            {

                try
                {
                    Cita cita = new Cita();

                    DateTime horaInicio = DateTime.Parse(Request.QueryString["fc"].ToString() + " " + horatxt.Text);
                    DateTime horaFinal = DateTime.Parse(Request.QueryString["fc"].ToString() + " " + horatxt2.Text);
                    cita.PacienteId = int.Parse(DropDownPacientes.SelectedValue);
                    cita.HoraInicio = horaInicio;
                    cita.HoraFinal = horaFinal;
                    CitaDAO agregacita = new CitaDAO();
                    if (idCita != 0)//Modificacion
                    {
                        cita.Id = idCita;
                        int idModificacion = agregacita.ModificarCategoria(cita, int.Parse(Session["doctorId"].ToString()));
                        if (idModificacion != 0)
                        {
                            Label1.Text = "Se Modificó la cita correctamente";
                        }
                        else {
                            Label1.Text = "Ocurrió un error al agendar la cita";
                        }
                    }
                    else {
                        int idCitas = agregacita.Inserta(cita, int.Parse(Session["doctorId"].ToString()));
                        if (idCitas != 0)
                        {
                            limpia();
                            Label1.Text = "Se Agendó la cita correctamente";

                        }
                        else
                        {
                            Label1.Text = "Ocurrió un error al agendar la cita";
                        }
                    }


                    
                    // Response.Write(horaInicio);
                    // Response.Write(horaFinal);
                }
                catch (FormatException)
                {
                    Label1.Text = "NO EXISTE FECHA PARA LA CITA";
                }
                catch (SqlException excSql)
                {
                    Label1.Text = "Ocurrió un error al ingresar la cita" + excSql;
                }

            }
            else
            {
                Label1.Text = "No se puede agregar la cita";
            }
            // Response.Write(horatxt.Text + horatxt2.Text + Request.QueryString["fc"].ToString());
        }

        protected void limpia()
        {
            horatxt.Text = "";
            horatxt2.Text = "";
        }
    }
}