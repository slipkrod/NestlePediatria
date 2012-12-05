using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using DataAccess;

namespace NestlePediatria
{
    public partial class AddEditArticulo : System.Web.UI.Page
    {
        private int idArticulo;

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
            idArticulo = Convert.ToInt32(Request.QueryString["idArticulo"]);
            if (!IsPostBack)
            {
                // Specify the data source and field names for the Text 
                // and Value properties of the items (ListItem objects) 
                // in the DropDownList control.
                DropDawnCategoria.DataSource = CategoriaDAO.ConsultaPublicado();
                DropDawnCategoria.DataTextField = "nombre";
                DropDawnCategoria.DataValueField = "id";
                // Bind the data to the control.
                DropDawnCategoria.DataBind();

                // Set the default selected item, if desired.
                DropDawnCategoria.SelectedIndex = 0;

                
                /**********************/
                linkDocumento.Visible = false;
                imageArticulo.Visible = false;
                

                if (idArticulo != 0)//es modificacion
                {

                    btnGuardar.Text = "Modificar";
                    linkdoc.Visible = true;
                    linkdoc.Text = "Documento.pdf";
                    try
                    {
                        ArticuloDAO bdArticulo = new ArticuloDAO();
                        Articulo modificarArticulo = bdArticulo.ConsultarUnArticulo(idArticulo);


                        txtNombre.Text = modificarArticulo.Nombre;
                        txtDescripcion.Text = modificarArticulo.Descripcion;
                        txtAutor.Text = modificarArticulo.Autor;
                        txtFecha.Text = modificarArticulo.Fecha;
                        linkdoc.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Article/" + modificarArticulo.Id;
                        linkdoc.Target ="blank_";
                        DropDawnCategoria.SelectedValue = modificarArticulo.CategoriaId.ToString();

                        //Meter la info d los paises y el link de la foto del evento
                        imageArticulo.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageArticulo/" + modificarArticulo.Id;
                        imageArticulo.Target = "_blank";
                        //Response.Write(modificarArticulo.Foto);
                        imageArticulo.Visible = true;
                        //hiddenFoto.Value = modificarArticulo.Foto;
                        //HiddenDoc.Value = modificarArticulo.Documento;
                        if (modificarArticulo.Publicado == 1)
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

                        List<int> articulopais = new List<int>();
                        ArticuloDAO evendao = new ArticuloDAO();
                        articulopais = evendao.ConsultaArticuloPais(idArticulo, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));



                        for (int i = 0; i < checkPaises.Items.Count; i++)
                        {
                            foreach (int data in articulopais)
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
                else {

                    this.cargaPaises();
                }
                
                
                
              

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx");
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idArticulo != 0)//es modificacion
            {
                Articulo articulo = new Articulo();
                //articulo.Foto = hiddenFoto.Value;
                //articulo.Documento = HiddenDoc.Value;

               // string ubicacionfoto = null;
                //string ubicaciondoc = null;
                bool fileOK = false;
                bool fileOK2 = false;
                //String path = Server.MapPath("~/FotosArticulo/");
                //string rutafotos = "/FotosArticulo/";
               // String path2 = Server.MapPath("~/DocArticulo/");
               // string rutaDoc = "/DocArticulo/";
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

                if (fileFoto.HasFile && fileOK) { 
                //subir archivo
                    //string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                    //ubicacionfoto = path + nombreArchivo;
                    //fileFoto.PostedFile.SaveAs(ubicacionfoto);
                    //articulo.Foto = rutafotos + nombreArchivo;
                    avanza = true;
                }else{
                    if (fileFoto.HasFile && !fileOK)
                    {
                        avanza = false;
                    }
                    else {
                        avanza = true;
                    }
                }

                if (fileFoto2.HasFile && fileOK2)
                {
                    //subir archivo
                   // string nombreArchivo2 = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto2.FileName;
                    //ubicaciondoc = path2 + nombreArchivo2;
                    //fileFoto2.PostedFile.SaveAs(ubicaciondoc);
                    //articulo.Documento = rutaDoc + nombreArchivo2;

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
                    articulo.Id = idArticulo;
                    articulo.Nombre = txtNombre.Text;
                    articulo.Descripcion = txtDescripcion.Text;
                    articulo.Autor = txtAutor.Text;
                    articulo.Fecha = txtFecha.Text;
                    articulo.CategoriaId = int.Parse(DropDawnCategoria.SelectedValue);

                    HttpPostedFile ImgFile = fileFoto.PostedFile;
                    Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                    ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);
                    articulo.Foto = byteImage;

                    HttpPostedFile PdfFile = fileFoto2.PostedFile;
                    Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                    PdfFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);
                    articulo.Documento = bytePdf;


                    if (CheckPublicado.Checked)
                    {
                        articulo.Publicado = 1;
                    }
                    else
                    {
                        articulo.Publicado = 0;
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
                        ArticuloDAO daoModifica = new ArticuloDAO();


                        if (daoModifica.ModificarArticulo(articulo, listaPaises, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString())))
                        {
                            this.lblMensaje.Text = "Se Modificó correctamente el Ártículo ID = " + idArticulo;
                        }
                        else
                            this.lblMensaje.Text = "Ocurrió un error al tratar de modificar el Artículo";
                    }
                    catch (Exception exc)
                    {
                        Response.Write("Ocurrió un error " + exc);
                    }
                }
                else {
                    if (!avanza) {
                        Label1.Text = "No se aceptan archivos de este tipo";
                    }
                    if (!avanza2) {
                        Label2.Text = "No se aceptan archivos de este tipo";
                    }
                }
               

            }
            else {
                /**
                Verificamos que el usuario haya subido el archivo
                */
                /*string ubicacionfoto = null;
                string ubicaciondoc = null;*/
                Boolean fileOK = false;
                Boolean fileOK2 = false;
                /*String path = Server.MapPath("~/FotosArticulo/");
                string rutafotos = "/FotosArticulo/";
                String path2 = Server.MapPath("~/DocArticulo/");
                string rutaDoc = "/DocArticulo/";*/

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
                        /*
                        string nombreArchivofoto = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        ubicacionfoto = path + nombreArchivofoto;

                        //ubicacion = path + DateTime.Now.ToString("ddMMyyyy") + fileFoto.FileName;
                        fileFoto.PostedFile.SaveAs(ubicacionfoto);
                        Label1.Text = "File uploaded!";

                        string nombreArchivoDoc = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        ubicaciondoc = path2 + nombreArchivoDoc;

                        fileFoto2.PostedFile.SaveAs(ubicaciondoc);
                        Label2.Text = "File uploaded!";*/
                        HttpPostedFile ImgFile = fileFoto.PostedFile;
                        Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                        ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);

                        HttpPostedFile DocFile = fileFoto2.PostedFile;
                        Byte[] bytePdf = new Byte[fileFoto2.PostedFile.ContentLength];
                        DocFile.InputStream.Read(bytePdf, 0, fileFoto2.PostedFile.ContentLength);

                        Articulo articulo = new Articulo();
                        articulo.Nombre = txtNombre.Text;
                        articulo.Descripcion = txtDescripcion.Text;
                        articulo.Autor = txtAutor.Text;
                        articulo.Fecha = txtFecha.Text;
                        articulo.CategoriaId = int.Parse(DropDawnCategoria.SelectedValue);
                        articulo.Foto = byteImage;
                        articulo.Documento = bytePdf;


                        if (CheckPublicado.Checked)
                        {
                            articulo.Publicado = 1;
                        }
                        else
                        {
                            articulo.Publicado = 0;
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


                            int articuloID = ArticuloDAO.Inserta(articulo, listaPaises, int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente el Artículo ID = " + articuloID;
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

        protected void linkCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulos.aspx");
        }
    }
}