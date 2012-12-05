using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;
using System.IO;

namespace NestlePediatria
{
    public partial class AddEditEvento : System.Web.UI.Page
    {
        private int idEvento;

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


            imageEvento.Visible = false;
                idEvento = Convert.ToInt32(Request.QueryString["idEvento"]);

                if (!IsPostBack)
                {
                    if (idEvento != 0)//es modificacion
                    {
                       
                       Guardar.Text = "Modificar";
                       imageEvento.Visible = true;
                        try
                        {
                            
                            EventoDAO bdevento = new EventoDAO();
                            Evento modificarEvento = bdevento.ConsultarUnEvento(idEvento);
                            txtNombre.Text = modificarEvento.Nombre;
                            txtDescripcion.Text = modificarEvento.Descripcion;
                            txtFechaInicio.Text = modificarEvento.FechaInicio;
                            txtFechaFin.Text = modificarEvento.FechaFinal;
                            txtLugar.Text = modificarEvento.Lugar;
                            txtDirigido.Text = modificarEvento.Dirigido;
                            imageEvento.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageEvento/" + modificarEvento.Id;
                            imageEvento.Target = "_blank";
                            //Meter la info d los paises y el link de la foto del evento
                           
                            //imagenEvento.Visible = true;
                            //hiddenFoto.Value = modificarEvento.Foto;

                            if (modificarEvento.Publicado == 1)
                            {
                                CheckPublicado.Checked = true;
                            }

                            //Traemos la lista de los paises para ver cuales estan activos
                            List<Pais> paises = new List<Pais>();
                            paises = PaisDAO.ConsultaPublicado(int.Parse(Session["id"].ToString()));

                            EventoDAO consulevenPais = new EventoDAO();
                            consulevenPais.ConsultaEventoPais(idEvento, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));
                            foreach (Pais pais in paises)
                            {
                                if (pais.Publicado == 1)
                                {
                                    checkPaises.Items.Add(new ListItem(pais.Nombre, pais.Id.ToString()));
                                    
                                    
                                }
                            }

                            List<int> eventopais = new List<int>();
                            EventoDAO evendao = new EventoDAO();
                            eventopais = evendao.ConsultaEventoPais(idEvento, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));



                            for (int i = 0; i < checkPaises.Items.Count; i++)
                            {
                                foreach (int data in eventopais)
                                {
                                    if (int.Parse(checkPaises.Items[i].Value) == data)
                                    {
                                        checkPaises.Items[i].Selected = true;
                                    }
                                }




                            }

                        }
                        catch (Exception)
                        {
                            lblMensaje.Text = "ocurrió un error al cargar los datos";
                        }
                    }
                    else {
                        this.cargaPaises();
                    }
                    
                }
                
            
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Eventos.aspx");
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            //string ubicacion = null;
            Boolean fileOK = false;
           // String path = Server.MapPath("~/FotosEvento/");
            //string ruta = "/FotosEvento/";



            HttpPostedFile ImgFile = fileFoto.PostedFile;
            Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
            ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);

            /**
             Verificamos que el usuario haya subido el archivo
             */
            if (idEvento != 0)//es modificacion
            {
                Evento evento = new Evento();
                evento.Id = idEvento;
                evento.Nombre = txtNombre.Text;
                evento.Descripcion = txtDescripcion.Text;
                evento.FechaInicio = txtFechaInicio.Text;
                evento.FechaFinal = txtFechaFin.Text;
                evento.Lugar = txtLugar.Text;
                evento.Dirigido = txtDirigido.Text;
                //evento.Foto = ubicacion;
                if (CheckPublicado.Checked)
                {
                    evento.Publicado = 1;
                }
                else
                {
                    evento.Publicado = 0;
                }

                if (fileFoto.HasFile)
                {
                    String fileExtension = System.IO.Path.GetExtension(fileFoto.FileName).ToLower();
                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }

                    if (fileFoto.HasFile && fileOK)
                    {
                        evento.Foto = byteImage;
                        /*string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        ubicacion = path + nombreArchivo;

                        fileFoto.PostedFile.SaveAs(ubicacion);
                        Label1.Text = "File uploaded!";*/
                        //evento.Foto = ruta + nombreArchivo;
                    }
                    else {
                        //evento.Foto = hiddenFoto.Value;
                    }
                }
                else {
                   // evento.Foto = hiddenFoto.Value;
                }

                int[] listaPaises;
                int contador = 0;
                for (int i = 0; i < checkPaises.Items.Count; i++)
                {

                    if (checkPaises.Items[i].Selected)
                    {
                        contador++;
                        
                    }

                }

                listaPaises = new int[contador];
                contador = 0;
                int posicion = 0;
                for (int i = 0; i < checkPaises.Items.Count; i++)
                {

                    if (checkPaises.Items[i].Selected)
                    {
                       // contador++;
                        listaPaises[posicion] = int.Parse(checkPaises.Items[i].Value);
                        posicion++;
                    }

                }

                EventoDAO bdevento = new EventoDAO();

                if (bdevento.ModificarEvento(evento, listaPaises, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString())))
                {
                    this.lblMensaje.Text = "Se modificó correctamente el Evento ID = " + evento.Id;
                }else
                    this.lblMensaje.Text = "Ocurrió un error al tratar de modificar el Evento";




            }
            else
            {//Si es Alta de Evento



                if (fileFoto.HasFile)
                {
                    String fileExtension =
                        System.IO.Path.GetExtension(fileFoto.FileName).ToLower();
                    String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (fileExtension == allowedExtensions[i])
                        {
                            fileOK = true;
                        }
                    }
                }

                if (fileOK)
                {
                    try
                    {
                       /* string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        ubicacion = path + nombreArchivo;*/


                        //fileFoto.PostedFile.SaveAs(ubicacion);
                        //Label1.Text = "File uploaded!";

                        Evento evento = new Evento();
                        evento.Nombre = txtNombre.Text;
                        evento.Descripcion = txtDescripcion.Text;
                        evento.FechaInicio = txtFechaInicio.Text;
                        evento.FechaFinal = txtFechaFin.Text;
                        evento.Lugar = txtLugar.Text;
                        evento.Dirigido = txtDirigido.Text;
                        //evento.Foto = ruta + nombreArchivo;
                        evento.Foto = byteImage;
                        if (CheckPublicado.Checked)
                        {
                            evento.Publicado = 1;
                        }
                        else
                        {
                            evento.Publicado = 0;
                        }

                        try
                        {

                            this.lblMensaje.Visible = true;
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


                            int eventoID = EventoDAO.Inserta(evento, listaPaises, int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente el Evento ID = " + eventoID;
                            this.resetControles();
                            //checkPaises.Items.Clear();
                            //this.cargaPaises();

                        }
                        catch (Exception exe)
                        {
                            this.lblMensaje.Visible = true;
                            this.lblMensaje.Text = "Error Mensaje:" + exe;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        Label1.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    Label1.Text = "Cannot accept files of this type.";
                }
                /*Terminamos de verificar lo del archivo*/
            } 
           
            
        }

        public void cargaPaises() {
            List<Pais> paises = new List<Pais>();
            paises = PaisDAO.ConsultaPublicado(int.Parse(Session["id"].ToString()));

            foreach (Pais pais in paises)
            {
                if (pais.Publicado == 1)
                {
                    checkPaises.Items.Add(new ListItem(pais.Nombre, pais.Id.ToString()));
                    
                }
            }

        }

        public void resetControles() {
            txtNombre.Text = "";
            txtLugar.Text = "";
            txtDirigido.Text = "";
            txtDescripcion.Text = "";
            txtFechaFin.Text = "";
            txtFechaInicio.Text = "";

            for (int i = 0; i < checkPaises.Items.Count; i++)
            {

                if (checkPaises.Items[i].Selected)
                {
                    checkPaises.Items[i].Selected = false;                    
                }



            }

            if (CheckPublicado.Checked) {
                CheckPublicado.Checked = false;
            }
            


        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Eventos.aspx");
        }

        
    }
}