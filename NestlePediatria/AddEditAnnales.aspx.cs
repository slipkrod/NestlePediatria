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
    public partial class AddEditAnnales : System.Web.UI.Page
    {
        private int idAnnales;

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
            idAnnales = Convert.ToInt32(Request.QueryString["idAnnales"]);
            if (!IsPostBack)
            {

                /**********************/
                linkDocumento.Visible = false;
                imageAnnales.Visible = false;


                if (idAnnales != 0)//es modificacion
                {

                    btnGuardar.Text = "Modificar";
                    linkdoc.Visible = true;
                    linkdoc.Text = "Documento.pdf";
                    try
                    {
                        AnnalesDAO bdAnnales = new AnnalesDAO();
                        Models.Annales modificarAnnales = bdAnnales.ConsultarUnAnnales(idAnnales);


                        txtNombre.Text = modificarAnnales.Nombre;
                        txtDescripcion.Text = modificarAnnales.Descripcion;
                        txtAutor.Text = modificarAnnales.Autor;
                        txtFecha.Text = modificarAnnales.Fecha;
                        linkdoc.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Annales/1" + modificarAnnales.Id;
                        linkdoc.Target = "blank_";


                        //Meter la info d los paises y el link de la foto del evento
                        imageAnnales.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageAnnales/" + modificarAnnales.Id;
                        //Response.Write(modificarArticulo.Foto);
                        imageAnnales.Visible = true;
                        imageAnnales.Target = "_blank";
                        //hiddenFoto.Value = modificarAnnales.Foto;
                        //HiddenDoc.Value = modificarAnnales.Documento;
                        if (modificarAnnales.Publicado == 1)
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
            if (idAnnales != 0)//es modificacion
            {
                Models.Annales annales = new Models.Annales();
                //annales.Foto = hiddenFoto.Value;
                //annales.Documento = HiddenDoc.Value;

                //string ubicacionfoto = null;
                //string ubicaciondoc = null;
                bool fileOK = false;
                bool fileOK2 = false;
                //String path = Server.MapPath("~/FotosAnnales/");
                //string rutafotos = "/FotosAnnales/";
                //String path2 = Server.MapPath("~/DocAnnales/");
                //string rutaDoc = "/DocAnnales/";
                bool avanza = false;
                bool avanza2 = false;
                /*if (fileFoto.PostedFile != null && fileFoto.PostedFile.ContentLength >0) { 
                }*/
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
                    //annales.Foto = rutafotos + nombreArchivo;
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
                    //annales.Documento = rutaDoc + nombreArchivo2;

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
                    annales.Id = idAnnales;
                    annales.Nombre = txtNombre.Text;
                    annales.Descripcion = txtDescripcion.Text;
                    annales.Autor = txtAutor.Text;
                    annales.Fecha = txtFecha.Text;

                    HttpPostedFile ImgFile = fileFoto.PostedFile;
                    Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                    ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                    annales.Foto = byteImage;

                    HttpPostedFile PdfFile = fileFoto2.PostedFile;
                    Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                    PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                    annales.Documento = bytePdf;





                    if (CheckPublicado.Checked)
                    {
                        annales.Publicado = 1;
                    }
                    else
                    {
                        annales.Publicado = 0;
                    }

                    this.lblMensaje.Visible = true;


                    try
                    {
                        AnnalesDAO daoModifica = new AnnalesDAO();


                        if (daoModifica.ModificarAnnales(annales))
                        {
                            this.lblMensaje.Text = "Se Modificó correctamente Annales con ID = " + idAnnales;
                        }
                        else
                            this.lblMensaje.Text = "Ocurrió un error al tratar de modificar Annales";
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
                //String path = Server.MapPath("~/FotosAnnales/");
                //string rutafotos = "/FotosAnnales/";
                //String path2 = Server.MapPath("~/DocAnnales/");
                //string rutaDoc = "/DocAnnales/";

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

                        Models.Annales annales = new Models.Annales();
                        annales.Nombre = txtNombre.Text;
                        annales.Descripcion = txtDescripcion.Text;
                        annales.Autor = txtAutor.Text;
                        annales.Fecha = txtFecha.Text;
                        //annales.Foto = rutafotos + nombreArchivofoto;
                        //annales.Documento = rutaDoc + nombreArchivoDoc;

                        HttpPostedFile ImgFile = fileFoto.PostedFile;
                        Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                        ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                        annales.Foto = byteImage;

                        HttpPostedFile PdfFile = fileFoto2.PostedFile;
                        Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                        PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                        annales.Documento = bytePdf;
                        if (CheckPublicado.Checked)
                        {
                            annales.Publicado = 1;
                        }
                        else
                        {
                            annales.Publicado = 0;
                        }

                        try
                        {

                            this.lblMensaje.Visible = true;



                            int NiditoID = AnnalesDAO.Inserta(annales, int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente Annales con ID = " + NiditoID;
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
            Response.Redirect("Annales.aspx");
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
            Response.Redirect("Annales.aspx");
        }
    }
}