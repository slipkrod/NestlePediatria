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
    public partial class AddEditPaciente : System.Web.UI.Page
    {
        private int Idpaciente;
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


            Idpaciente = Convert.ToInt32(Request.QueryString["idPaciente"]);
            if (!IsPostBack)
            {
                if (Idpaciente != 0)//es modificacion
                {
                    btnGuardar.Text = "Modificar";

                    try
                    {
                        PacienteDAO bdpaciente = new PacienteDAO();
                        Paciente modificarCategoria = bdpaciente.ConsultarUnPaciente(Idpaciente, int.Parse(Session["doctorId"].ToString()));
                        txtNombre.Text = modificarCategoria.Nombre;
                        DropDownSexo.SelectedValue = modificarCategoria.Sexo;
                        txtFechaNac.Text = modificarCategoria.FechaNac;
                        txtLugarNac.Text = modificarCategoria.LugarNac;
                        txtCiudadNac.Text = modificarCategoria.CiudadNac;
                        TxtGrupoSanguineo.Text = modificarCategoria.GrupoSanguineo;
                        DropDawnRh.SelectedValue = modificarCategoria.RH;
                        txtAlergico.Text = modificarCategoria.Alergico;
                        txtNombreMadre.Text = modificarCategoria.Madre;
                        txtOcupacionMadre.Text = modificarCategoria.OcupacionMadre;
                        txtNombrePadre.Text = modificarCategoria.Padre;
                        txtOcupacionPadre.Text = modificarCategoria.OcupacionPadre;
                        TxtCalle.Text = modificarCategoria.Calle;
                        txtColonia.Text = modificarCategoria.Colonia;
                        txtCiudad.Text = modificarCategoria.Ciudad;
                        txtEstado.Text = modificarCategoria.Estado;
                        txtCp.Text = modificarCategoria.CodigoPostal;
                        txtTelefono.Text = modificarCategoria.Telefono;
                        txtCorreo.Text = modificarCategoria.Correo;
                        txtEncargado.Text = modificarCategoria.NombreEncargado;
                        txtTelContacto.Text = modificarCategoria.TelefonoEncargado;
                        txtNotas.Text = modificarCategoria.Notas;

                        
                        

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
            Paciente paciente = new Paciente();
            paciente.Nombre = txtNombre.Text;
            paciente.Sexo = DropDownSexo.SelectedValue;
            paciente.FechaNac = txtFechaNac.Text;
            paciente.LugarNac = txtLugarNac.Text;
            paciente.CiudadNac = txtCiudadNac.Text;
            paciente.GrupoSanguineo = TxtGrupoSanguineo.Text;
            paciente.RH = DropDawnRh.SelectedValue;
            paciente.Alergico = txtAlergico.Text;
            paciente.Madre = txtNombreMadre.Text;
            paciente.OcupacionMadre = txtOcupacionMadre.Text;
            paciente.Padre = txtNombrePadre.Text;
            paciente.OcupacionPadre = txtOcupacionPadre.Text;
            paciente.Calle = TxtCalle.Text;
            paciente.Colonia = txtColonia.Text;
            paciente.Ciudad = txtCiudad.Text;
            paciente.Estado = txtEstado.Text;
            paciente.CodigoPostal = txtCp.Text;
            paciente.Telefono = txtTelefono.Text;
            paciente.Correo = txtCorreo.Text;
            paciente.NombreEncargado = txtEncargado.Text;
            paciente.TelefonoEncargado = txtTelContacto.Text;
            paciente.Notas = txtNotas.Text;
            paciente.DoctorID = int.Parse(Session["doctorId"].ToString());

            try
            {
                PacienteDAO pacienteDao = new PacienteDAO();

                if (Idpaciente != 0)//modificacion
                {

                    paciente.ID = Idpaciente;

                    if (pacienteDao.ModificarPaciente(paciente))
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
                    pacienteDao.inserta(paciente);
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = "Se ingreso correctamente la Categoria";
                    limpia();

                }


            }
            catch (Exception exe)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Error Mensaje:" + exe;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }

        public void limpia()
        {
            txtNombre.Text = "";
            txtAlergico.Text = "";
           
        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }
    }
}