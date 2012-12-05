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
    public partial class AddEditMaterial : System.Web.UI.Page
    {
        private int idMaterial;

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

            /*******************/
            ((Label)Master.FindControl("lblUser")).Text = Session["nombre"].ToString();
            /******************/
            idMaterial = Convert.ToInt32(Request.QueryString["idMaterial"]);
            if (!IsPostBack)
            {

                /**********************/
                linkDocumento.Visible = false;
                imageMaterial.Visible = false;


                if (idMaterial != 0)//es modificacion
                {

                    btnGuardar.Text = "Modificar";
                    linkdoc.Visible = true;
                    linkdoc.Text = "Documento.pdf";
                    imageMaterial.Visible = true;
                    try
                    {
                        MaterialDAO bdMaterial = new MaterialDAO();
                        Material modificarMaterial = bdMaterial.ConsultarUnMaterial(idMaterial);


                        txtNombre.Text = modificarMaterial.Nombre;
                        txtDescripcion.Text = modificarMaterial.Descripcion;
                        txtAutor.Text = modificarMaterial.Autor;
                        txtFecha.Text = modificarMaterial.Fecha;
                        linkdoc.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Material/"+modificarMaterial.Id;
                        linkdoc.Target = "blank_";
                        

                        //Meter la info d los paises y el link de la foto del evento
                        imageMaterial.Target = "blank_";
                        imageMaterial.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageMaterial/" + modificarMaterial.Id;
                        //Response.Write(modificarArticulo.Foto);
                        
                        //hiddenFoto.Value = modificarMaterial.Foto;
                        //HiddenDoc.Value = modificarMaterial.Documento;
                        if (modificarMaterial.Publicado == 1)
                        {
                            CheckPublicado.Checked = true;
                        }


                        //Traemos la lista de los paises para ver cuales estan activos
                        List<Pais> paises = new List<Pais>();
                        paises = PaisDAO.ConsultaPublicado(int.Parse(Session["id"].ToString()));

                        //ArticuloDAO consulartPais = new ArticuloDAO();
                        //consulartPais.ConsultaArticuloPais(idArticulo);
                        foreach (Pais pais in paises)
                        {
                            if (pais.Publicado == 1)
                            {
                                checkPaises.Items.Add(new ListItem(pais.Nombre, pais.Id.ToString()));


                            }
                        }

                        List<int> materialpais = new List<int>();
                        MaterialDAO matdao = new MaterialDAO();
                        materialpais = matdao.ConsultaMaterialPais(idMaterial, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));



                        for (int i = 0; i < checkPaises.Items.Count; i++)
                        {
                            foreach (int data in materialpais)
                            {
                                if (int.Parse(checkPaises.Items[i].Value) == data)
                                {
                                    checkPaises.Items[i].Selected = true;
                                }
                            }




                        }

                    }
                    catch (Exception) { }
                }
                else
                {

                    this.cargaPaises();
                }





            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idMaterial != 0)//es modificacion
            {
                Material material = new Material();
                //material.Foto = hiddenFoto.Value;
                //material.Documento = HiddenDoc.Value;

                //string ubicacionfoto = null;
                //string ubicaciondoc = null;
                bool fileOK = false;
                bool fileOK2 = false;
                //String path = Server.MapPath("~/FotosMateriales/");
                //string rutafotos = "/FotosMateriales/";
                //String path2 = Server.MapPath("~/DocMateriales/");
                //string rutaDoc = "/DocMateriales/";
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
                    //material.Foto = rutafotos + nombreArchivo;
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
                    //material.Documento = rutaDoc + nombreArchivo2;

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
                    material.Id = idMaterial;
                    material.Nombre = txtNombre.Text;
                    material.Descripcion = txtDescripcion.Text;
                    material.Autor = txtAutor.Text;
                    material.Fecha = txtFecha.Text;

                    HttpPostedFile ImgFile = fileFoto.PostedFile;
                    Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                    ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                    material.Foto = byteImage;

                    HttpPostedFile PdfFile = fileFoto2.PostedFile;
                    Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                    PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                    material.Documento = bytePdf;





                    if (CheckPublicado.Checked)
                    {
                        material.Publicado = 1;
                    }
                    else
                    {
                        material.Publicado = 0;
                    }

                    this.lblMensaje.Visible = true;
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

                    try
                    {
                        MaterialDAO daoModifica = new MaterialDAO();


                        if (daoModifica.ModificarMaterial(material, listaPaises, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString())))
                        {
                            this.lblMensaje.Text = "Se Modificó correctamente el Material ID = " + idMaterial;
                        }
                        else
                            this.lblMensaje.Text = "Ocurrió un error al tratar de modificar el Material";
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
                //String path = Server.MapPath("~/FotosMateriales/");
                //string rutafotos = "/FotosMateriales/";
                //String path2 = Server.MapPath("~/DocMateriales/");
                //string rutaDoc = "/DocMateriales/";

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

                        ////ubicacion = path + DateTime.Now.ToString("ddMMyyyy") + fileFoto.FileName;
                        //fileFoto.PostedFile.SaveAs(ubicacionfoto);
                        ////Label1.Text = "File uploaded!";

                        //string nombreArchivoDoc = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto2.FileName;
                        //ubicaciondoc = path2 + nombreArchivoDoc;

                        //fileFoto2.PostedFile.SaveAs(ubicaciondoc);
                        //Label2.Text = "File uploaded!";

                        Material material = new Material();
                        material.Nombre = txtNombre.Text;
                        material.Descripcion = txtDescripcion.Text;
                        material.Autor = txtAutor.Text;
                        material.Fecha = txtFecha.Text;
                        //material.Foto = rutafotos + nombreArchivofoto;
                        //material.Documento = rutaDoc + nombreArchivoDoc;
                        HttpPostedFile ImgFile = fileFoto.PostedFile;
                        Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                        ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                        material.Foto = byteImage;

                        HttpPostedFile PdfFile = fileFoto2.PostedFile;
                        Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                        PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                        material.Documento = bytePdf;


                        if (CheckPublicado.Checked)
                        {
                            material.Publicado = 1;
                        }
                        else
                        {
                            material.Publicado = 0;
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


                            int MaterialID = MaterialDAO.Inserta(material, listaPaises, int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente el Material ID = " + MaterialID;
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
            Response.Redirect("Materiales.aspx");
        }

        public void cargaPaises()
        {
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

        public void resetControles()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtAutor.Text = "";
            txtFecha.Text = "";


            for (int i = 0; i < checkPaises.Items.Count; i++)
            {

                if (checkPaises.Items[i].Selected)
                {
                    checkPaises.Items[i].Selected = false;
                }



            }

            if (CheckPublicado.Checked)
            {
                CheckPublicado.Checked = false;
            }



        }

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Materiales.aspx");
        }
    }
}