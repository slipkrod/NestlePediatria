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
    public partial class AddEditUsuario : System.Web.UI.Page
    {
        private int idUsuario;

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
            //Pasamos el valor tipo text a la etiqueta HTML para poder validar la contraseña
            txtPassword.Attributes.Add("value", txtPassword.Text);
            txtPassword2.Attributes.Add("value", txtPassword2.Text);
            
            idUsuario = Convert.ToInt32(Request.QueryString["idUsuario"]);

            if (!IsPostBack)
            {
                cargaRoles();

                cargaPaises();
                if (idUsuario != 0) { //es Modificacion
                    txtUsuario.Enabled = false;
                    CargaDatos();
                    txtPassword.CssClass = "validate[equals[ctl00_ContentPlaceHolder1_txtPassword2]] text-input";
                    txtPassword2.CssClass = "validate[equals[ctl00_ContentPlaceHolder1_txtPassword]] text-input";

                    

                    List<int> usuariopais = new List<int>();
                    UsuarioDAO evendao = new UsuarioDAO();
                    usuariopais = evendao.ConsultaUsuarioPais(idUsuario);



                    for (int i = 0; i < checkPaises.Items.Count; i++)
                    {
                        foreach (int data in usuariopais)
                        {
                            if (int.Parse(checkPaises.Items[i].Value) == data)
                            {
                                checkPaises.Items[i].Selected = true;
                            }
                        }




                    }

                    //Terminamos de agregar los checkbox
                }
                
            }
        }

        public void cargaRoles()
        {
            RolDAO rolDao = new RolDAO();
            DropDawnRol.DataSource = rolDao.Consulta();
            DropDawnRol.DataTextField = "tipoRol";
            DropDawnRol.DataValueField = "id";
            // Bind the data to the control.
            DropDawnRol.DataBind();

            // Set the default selected item, if desired.
            DropDawnRol.SelectedIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Usuarios = txtUsuario.Text;
            usuario.Nombre = txtNombre.Text;
            usuario.Password = txtPassword.Text;
            usuario.RolId = int.Parse(DropDawnRol.SelectedValue);

            if (CheckHabilitado.Checked)
            {
                usuario.Habilitado = 1;
            }
            else
            {
                usuario.Habilitado = 0;
            }
            int estado = 0;

            /***************************************/
            //Obtenemos los países que haya seleccionado

            int[] listaPaisess;
            int contador = 0;
            for (int i = 0; i < checkPaises.Items.Count; i++)
            {

                if (checkPaises.Items[i].Selected)
                {
                    contador++;

                }

            }

            listaPaisess = new int[contador];
            contador = 0;
            int posicion = 0;
            for (int i = 0; i < checkPaises.Items.Count; i++)
            {

                if (checkPaises.Items[i].Selected)
                {
                    // contador++;
                    listaPaisess[posicion] = int.Parse(checkPaises.Items[i].Value);
                    posicion++;
                }

            }
            /**************************************/
            UsuarioDAO altaUsuario = new UsuarioDAO();

            if (idUsuario != 0) {//es modificación
                usuario.Id = idUsuario;

                if (altaUsuario.ModificarUsuario(usuario, listaPaisess))
                {


                    lblMensaje.Text = "El Usuario se modificó correctamente";
                }
                else {
                    lblMensaje.Text = "Ocurrió un Error al tratar de modificar";
                }

            } else {
               // estado = altaUsuario.Inserta(usuario);
                if (usuario.RolId == 1) { //Es Administrador

                    List<Pais> listaPaises = new List<Pais>();
                    Pais listcheckPais = null;
                    for (int i = 0; i < checkPaises.Items.Count; i++)
                    {

                        if (checkPaises.Items[i].Selected)
                        {

                            //lblMensaje.Text += checkPaises.Items[i].Text + "<br>";
                            listcheckPais = new Pais();
                            listcheckPais.Nombre = checkPaises.Items[i].Text;
                            listcheckPais.Id = int.Parse(checkPaises.Items[i].Value);
                            listaPaises.Add(listcheckPais);
                        }



                    }
                    
                    
                        estado = altaUsuario.InsertaUserPais(usuario, listaPaises);
                    
                    
                    
                }

                if (usuario.RolId == 2) { //Es Super Administrador y no le llevamos la lista de países
                    estado = altaUsuario.Inserta(usuario);
                }

                if (estado != 0)
                {
                    //se inserto correctamente
                    lblMensaje.Text = "El usuario se insertó correctamente";
                    limpiar();
                }
                if (estado == 0)
                {
                    //el usuario ya existe
                    lblMensaje.Text = "El usuario ya existe en el sistema";
                }
            }
            
        }

        public void CargaDatos() {
            UsuarioDAO modificaUser = new UsuarioDAO();
            Usuario datosUsuario = modificaUser.ConsultaUnUsuario(idUsuario);
            txtNombre.Text = datosUsuario.Nombre;

            //if (datosUsuario.Password == Encriptacion.encriptar("maris123"))
            //{
            //    lblMensaje.Text = "La contraseña es igual";
            //}
            //else {
            //    lblMensaje.Text = "se cambio la contraseña";
            //}
            //txtPassword.TextMode = texts;
            txtPassword.Text = datosUsuario.Password;

            txtUsuario.Text = datosUsuario.Usuarios;
            if (datosUsuario.Habilitado == 1) {
                CheckHabilitado.Checked = true;
            }

            DropDawnRol.SelectedValue = datosUsuario.RolId.ToString();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        public void limpiar()
        {
            txtUsuario.Text = "";
            txtPassword.Attributes.Add("value", "");
            txtPassword2.Attributes.Add("value", "");

            /*txtPassword2.Text = "";
            txtPassword.Text = "";*/
            txtNombre.Text = "";

            if (CheckHabilitado.Checked)
            {
                CheckHabilitado.Checked = false;
            }
        }

        public void cargaPaises()
        {
            List<Pais> paises = new List<Pais>();
            paises = PaisDAO.Consulta();

            foreach (Pais pais in paises)
            {
                if (pais.Publicado == 1)
                {
                    checkPaises.Items.Add(new ListItem(pais.Nombre, pais.Id.ToString()));

                }
            }

        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }
    }
}