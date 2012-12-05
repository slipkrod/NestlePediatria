using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Models;

namespace NestlePediatria
{
    public partial class Pacientes : System.Web.UI.Page
    {
        private string letra;
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

            if (!IsPostBack)
            {
                this.CargaPacientes();
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddEditPaciente.aspx");
        }

        protected void dgPacientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEditar")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddEditPaciente.aspx?idPaciente=" + id);

                }
                catch (Exception)
                {
                    lblConfirmacion.Text = "ocurrió un error al intentar Editar";
                }
            }

            if (e.CommandName == "btnEliminar")
            {

                int indice = Convert.ToInt32(e.CommandArgument);
                PacienteDAO bdPais = new PacienteDAO();

                if (bdPais.EliminarPaciente(indice, int.Parse(Session["doctorId"].ToString())))
                {
                    lblConfirmacion.Text = "Se eliminó correctamente";
                    CargaPacientes();

                }
                else
                    lblConfirmacion.Text = "ocurrió un error al intentar eliminar";



            }
        }



        protected void CargaPacientes()
        {
            letra = null;
            if (Request.QueryString["case"] != null)
            {
                int indice = int.Parse(Request.QueryString["case"]);
                switch (indice)
                {
                    case 1: letra = "A"; break;
                    case 2: letra = "B"; break;
                    case 3: letra = "C"; break;
                    case 4: letra = "D"; break;
                    case 5: letra = "E"; break;
                    case 6: letra = "F"; break;
                    case 7: letra = "G"; break;
                    case 8: letra = "H"; break;
                    case 9: letra = "I"; break;
                    case 10: letra = "J"; break;
                    case 11: letra = "K"; break;
                    case 12: letra = "L"; break;
                    case 13: letra = "M"; break;
                    case 14: letra = "N"; break;
                    case 15: letra = "O"; break;
                    case 16: letra = "P"; break;
                    case 17: letra = "Q"; break;
                    case 18: letra = "R"; break;
                    case 19: letra = "S"; break;
                    case 20: letra = "T"; break;
                    case 21: letra = "U"; break;
                    case 22: letra = "V"; break;
                    case 23: letra = "W"; break;
                    case 24: letra = "X"; break;
                    case 25: letra = "Y"; break;
                    case 26: letra = "Z"; break;
                    default: letra = null; break;
                }
            }



            List<Paciente> padecimientoGrid = PacienteDAO.Consulta(letra, int.Parse(Session["doctorId"].ToString()));
            dgPacientes.DataSource = padecimientoGrid;
            dgPacientes.DataBind();


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=1");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=2");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=3");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=4");
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=5");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=6");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=7");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=8");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=9");
        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=10");
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=11");
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=12");
        }

        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=13");
        }

        protected void LinkButton15_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=14");
        }

        protected void LinkButton16_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=15");
        }

        protected void LinkButton17_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=16");
        }

        protected void LinkButton18_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=17");
        }

        protected void LinkButton19_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=18");
        }

        protected void LinkButton20_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=19");
        }

        protected void LinkButton21_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=20");
        }

        protected void LinkButton22_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=21");
        }

        protected void LinkButton23_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=22");
        }

        protected void LinkButton24_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=23");
        }

        protected void LinkButton25_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=24");
        }

        protected void LinkButton26_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=25");
        }

        protected void LinkButton27_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx?case=26");
        }

        protected void LinkButton28_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }


    }
}