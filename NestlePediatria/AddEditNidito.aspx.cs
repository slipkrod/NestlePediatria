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
    public partial class AddEditNidito : System.Web.UI.Page
    {
        private int idNidito;

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


                if (rol != 2)
                {
                    Redireccion(rol);
                }
                
            }

            /*******************/
            ((Label)Master.FindControl("lblUser")).Text = Session["nombre"].ToString();
            /******************/
            idNidito = Convert.ToInt32(Request.QueryString["idNidito"]);
            if (!IsPostBack)
            {

                /**********************/
                linkDocumento.Visible = false;
               // imagenNidito.Visible = false;


                if (idNidito != 0)//es modificacion
                {

                    btnGuardar.Text = "Modificar";
                    linkdoc.Visible = true;
                    linkdoc.Text = "Documento.pdf";
                    try
                    {
                        NiditoDAO bdNidito = new NiditoDAO();
                        Models.Nidito modificarNidito = bdNidito.ConsultarUnNidito(idNidito);


                        txtNombre.Text = modificarNidito.Nombre;
                        txtDescripcion.Text = modificarNidito.Descripcion;
                        txtAutor.Text = modificarNidito.Autor;
                        txtFecha.Text = modificarNidito.Fecha;
                        linkdoc.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Nidito/"+modificarNidito.Id;
                        linkdoc.Target = "blank_";


                        //Meter la info d los paises y el link de la foto del evento
                        imageNidito.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageNidito/" + modificarNidito.Id;
                        //Response.Write(modificarArticulo.Foto);
                        imageNidito.Visible = true;
                        //hiddenFoto.Value = modificarNidito.Foto;
                        //HiddenDoc.Value = modificarNidito.Documento;
                        if (modificarNidito.Publicado == 1)
                        {
                            CheckPublicado.Checked = true;
                        }


                    }
                    catch (Exception) { }
                }
                else
                {

                    
                }





            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idNidito != 0)//es modificacion
            {
                Models.Nidito nidito = new Models.Nidito();
                //nidito.Foto = hiddenFoto.Value;
                //nidito.Documento = HiddenDoc.Value;

                //string ubicacionfoto = null;
                //string ubicaciondoc = null;
                bool fileOK = false;
                bool fileOK2 = false;
                //String path = Server.MapPath("~/FotosNidito/");
                //string rutafotos = "/FotosNidito/";
                //String path2 = Server.MapPath("~/DocNidito/");
                //string rutaDoc = "/DocNidito/";
                bool avanza = false;
                bool avanza2 = false;
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

                if (fileFoto2.HasFile)
                {
                    String fileExtension2 = System.IO.Path.GetExtension(fileFoto2.FileName).ToLower();
                    String[] allowedExtensions2 = { ".pdf" };
                    for (int i = 0; i < allowedExtensions2.Length; i++)
                    {
                        if (fileExtension2 == allowedExtensions2[i])
                        {
                            fileOK2 = true;
                        }
                    }
                }

                if (fileFoto.HasFile && fileOK)
                {
                    //subir archivo
                    //string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                    //ubicacionfoto = path + nombreArchivo;
                    //fileFoto.PostedFile.SaveAs(ubicacionfoto);
                    //nidito.Foto = rutafotos + nombreArchivo;
                    avanza = true;
                }
                else
                {
                    if (fileFoto.HasFile && !fileOK)
                    {
                        avanza = false;
                    }
                    else
                    {
                        avanza = true;
                    }
                }

                if (fileFoto2.HasFile && fileOK2)
                {
                    //subir archivo
                    //string nombreArchivo2 = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto2.FileName;
                    //ubicaciondoc = path2 + nombreArchivo2;
                    //fileFoto2.PostedFile.SaveAs(ubicaciondoc);
                    //nidito.Documento = rutaDoc + nombreArchivo2;

                    avanza2 = true;
                }
                else
                {
                    if (fileFoto2.HasFile && !fileOK2)
                    {
                        avanza2 = false;
                    }
                    else
                    {
                        avanza2 = true;
                    }
                }



                if (avanza && avanza2)
                {
                    //proceder a hacer la modificacion
                    nidito.Id = idNidito;
                    nidito.Nombre = txtNombre.Text;
                    nidito.Descripcion = txtDescripcion.Text;
                    nidito.Autor = txtAutor.Text;
                    nidito.Fecha = txtFecha.Text;

                    HttpPostedFile ImgFile = fileFoto.PostedFile;
                    Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                    ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                    nidito.Foto = byteImage;

                    HttpPostedFile PdfFile = fileFoto2.PostedFile;
                    Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                    PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                    nidito.Documento = bytePdf;





                    if (CheckPublicado.Checked)
                    {
                        nidito.Publicado = 1;
                    }
                    else
                    {
                        nidito.Publicado = 0;
                    }

                    this.lblMensaje.Visible = true;
                    

                    try
                    {
                        NiditoDAO daoModifica = new NiditoDAO();


                        if (daoModifica.ModificarNidito(nidito))
                        {
                            this.lblMensaje.Text = "Se Modificó correctamente Nidito con ID = " + idNidito;
                        }
                        else
                            this.lblMensaje.Text = "Ocurrió un error al tratar de modificar Nidito";
                    }
                    catch (Exception exc)
                    {
                        Response.Write("Ocurrió un error " + exc);
                    }
                }
                else
                {
                    if (!avanza)
                    {
                        Label1.Text = "No se aceptan archivos de este tipo";
                    }
                    if (!avanza2)
                    {
                        Label2.Text = "No se aceptan archivos de este tipo";
                    }
                }


            }
            else
            {
                /**
                Verificamos que el usuario haya subido el archivo
                */
                //string ubicacionfoto = null;
                //string ubicaciondoc = null;
                Boolean fileOK = false;
                Boolean fileOK2 = false;
                //String path = Server.MapPath("~/FotosNidito/");
                //string rutafotos = "/FotosNidito/";
                //String path2 = Server.MapPath("~/DocNidito/");
                //string rutaDoc = "/DocNidito/";

                if (fileFoto.HasFile && fileFoto2.HasFile)
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

                    String fileExtension2 = System.IO.Path.GetExtension(fileFoto2.FileName).ToLower();
                    String[] allowedExtensions2 = { ".pdf" };
                    for (int ii = 0; ii < allowedExtensions2.Length; ii++)
                    {
                        if (fileExtension2 == allowedExtensions2[ii])
                        {
                            fileOK2 = true;
                        }
                    }
                }

                if (fileOK && fileOK2)
                {
                    try
                    {
                        //string nombreArchivofoto = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        //ubicacionfoto = path + nombreArchivofoto;

                        
                        //fileFoto.PostedFile.SaveAs(ubicacionfoto);
                        

                        //string nombreArchivoDoc = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto2.FileName;
                        //ubicaciondoc = path2 + nombreArchivoDoc;

                        //fileFoto2.PostedFile.SaveAs(ubicaciondoc);
                        //Label2.Text = "File uploaded!";

                        Models.Nidito nidito = new Models.Nidito();
                        nidito.Nombre = txtNombre.Text;
                        nidito.Descripcion = txtDescripcion.Text;
                        nidito.Autor = txtAutor.Text;
                        nidito.Fecha = txtFecha.Text;
                        //nidito.Foto = rutafotos + nombreArchivofoto;
                        //nidito.Documento = rutaDoc + nombreArchivoDoc;

                        HttpPostedFile ImgFile = fileFoto.PostedFile;
                        Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                        ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                        nidito.Foto = byteImage;

                        HttpPostedFile PdfFile = fileFoto2.PostedFile;
                        Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                        PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                        nidito.Documento = bytePdf;


                        if (CheckPublicado.Checked)
                        {
                            nidito.Publicado = 1;
                        }
                        else
                        {
                            nidito.Publicado = 0;
                        }

                        try
                        {

                            this.lblMensaje.Visible = true;
                            


                            int NiditoID = NiditoDAO.Inserta(nidito, int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente Nidito con ID = " + NiditoID;
                            this.resetControles();
                            

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
                        Label2.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    Label1.Text = "Cannot accept files of this type.";
                    Label2.Text = "Cannot accept files of this type.";
                }
                /*Terminamos de verificar lo del archivo*/
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Nidito.aspx");
        }



        public void resetControles()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtAutor.Text = "";
            txtFecha.Text = "";


            if (CheckPublicado.Checked)
            {
                CheckPublicado.Checked = false;
            }



        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Nidito.aspx");
        }
    }
}