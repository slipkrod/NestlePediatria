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
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["user"] != null)
            {
                Redireccion(int.Parse(Session["rol"].ToString()));
            }
            

            /*******************/
            ((LinkButton)Master.FindControl("LinkButton1")).Visible = false;
            /******************/
            
        }

        public void Redireccion(int RolId){
            if (RolId == 1)
            {//Administrador 
                Response.Redirect("Mostrar.aspx");
            }

            if (RolId == 2)
            { //Super Administrador
                Response.Redirect("Mostrar.aspx");
            }

            if (RolId == 3 || RolId == 4)//Menu Doctor/Asistente
            {
                Response.Redirect("MenuDoctor.aspx");
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pwd = txtPassword.Text.Trim();

            Usuario usuario = new Usuario();
            usuario.Usuarios = user;
            usuario.Password = pwd;

            UsuarioDAO usuarioDao = new UsuarioDAO();
            int verifica = usuarioDao.VerificarUsuario(usuario);

            if (verifica != 0)
            {
                Usuario DatosUser = new Usuario();
                DatosUser = usuarioDao.ConsultaUnUsuario(verifica);
                lblMensaje.Text = "Si se encuentra el usuario " + DatosUser.Nombre;

                Session.Add("user", DatosUser.Usuarios);
                Session.Add("rol", DatosUser.RolId);
                Session.Add("id", DatosUser.Id);
                Session.Add("nombre", DatosUser.Nombre);

                if (DatosUser.RolId == 3 || DatosUser.RolId == 4) {
                    int doctorId = PacienteDAO.ConsultaDoctorId(DatosUser.RolId, DatosUser.Id);
                    if (doctorId != 0)
                    {
                        Session.Add("doctorId", doctorId);
                    }
                    else {
                        Session.Add("doctorId", 0);
                    }
                    
                }

                Redireccion(DatosUser.RolId);
            }
            else {
                lblMensaje.Text = "El Usuario o la Contraseña son incorrectos";
            }
            
            
        }
    }
}