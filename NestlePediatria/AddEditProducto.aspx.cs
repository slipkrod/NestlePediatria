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
    public partial class AddEditProducto : System.Web.UI.Page
    {
        private int idProducto;

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
            

            imageProducto.Visible = false;
            idProducto = Convert.ToInt32(Request.QueryString["idProducto"]);

            if (!IsPostBack) {
                DropDawnPadecimiento.DataSource = PadecimientoDAO.ConsultaPublicado();
                DropDawnPadecimiento.DataTextField = "nombre";
                DropDawnPadecimiento.DataValueField = "id";
                // Bind the data to the control.
                DropDawnPadecimiento.DataBind();

                // Set the default selected item, if desired.
                DropDawnPadecimiento.SelectedIndex = 0;

                /******************************/

                if (idProducto != 0)//es modificacion
                {

                    btnGuardar.Text = "Modificar";
                    imageProducto.Visible = true;

                    ProductoDAO modificarProducto = new ProductoDAO();
                    Producto producto = modificarProducto.ConsultarUnProducto(idProducto);
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    //hiddenFoto.Value = producto.Foto;
                    imageProducto.HRef = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetImageProducto/" + producto.Id; ;
                    DropDawnPadecimiento.SelectedValue = producto.PadecimientoId.ToString();
                    if (producto.Publicado == 1)
                    {
                        CheckPublicado.Checked = true;
                    }

                    //Traemos la lista de los paises para ver cuales estan activos
                    List<Pais> paises = new List<Pais>();
                    paises = PaisDAO.ConsultaPublicado(int.Parse(Session["id"].ToString()));

                    ProductoDAO consulevenPais = new ProductoDAO();
                    consulevenPais.ConsultaProductoPais(idProducto, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));
                    foreach (Pais pais in paises)
                    {
                        if (pais.Publicado == 1)
                        {
                            checkPaises.Items.Add(new ListItem(pais.Nombre, pais.Id.ToString()));


                        }
                    }

                    List<int> eventopais = new List<int>();
                    ProductoDAO evendao = new ProductoDAO();
                    eventopais = evendao.ConsultaProductoPais(idProducto, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString()));



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
                else {
                    this.cargaPaises();
                }
               

               
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProducto != 0)
            { //Modificar

                Producto producto = new Producto();
                //producto.Foto = hiddenFoto.Value;
                //string ubicacion = null;
                Boolean fileOK = false;
                //String path = Server.MapPath("~/FotosProducto/");
                //string ruta = "/FotosProducto/";
                bool avanza = false;
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

                if (fileFoto.HasFile && !fileOK)
                { //subio un archivo pero la extension es incorrecta
                    Label1.Text = "No se aceptar archivos de este tipo";
                    avanza = false;
                }
                else {
                    if (fileFoto.HasFile && fileOK)
                    {//subir el archivo
                        //string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        //ubicacion = path + nombreArchivo;

                        ////ubicacion = path + DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        //fileFoto.PostedFile.SaveAs(ubicacion);
                        //producto.Foto = ruta + nombreArchivo;
                        avanza = true;
                    }
                    else {
                        avanza = true;
                    }
                }
                if (avanza)
                {
                    producto.Id = idProducto;
                    producto.Nombre = txtNombre.Text;
                    producto.Descripcion = txtDescripcion.Text;

                    HttpPostedFile ImgFile = fileFoto.PostedFile;
                    Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                    ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);

                    producto.Foto = byteImage;
                    producto.PadecimientoId = int.Parse(DropDawnPadecimiento.SelectedValue);
                    if (CheckPublicado.Checked)
                    {
                        producto.Publicado = 1;
                    }
                    else
                    {
                        producto.Publicado = 0;
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

                    ProductoDAO bdevento = new ProductoDAO();

                    if (bdevento.ModificarProducto(producto, listaPaises, int.Parse(Session["id"].ToString()), int.Parse(Session["rol"].ToString())))
                    {
                        this.lblMensaje.Text = "Se modificó correctamente el Producto ID = " + producto.Id;
                    }
                    else
                        this.lblMensaje.Text = "Ocurrió un error al tratar de modificar el Producto";
                }
                



                

            }
            else {
                /**
                Verificamos que el usuario haya subido el archivo
                */
                //string ubicacion = null;
                Boolean fileOK = false;
                //String path = Server.MapPath("~/FotosProducto/");
                //string ruta = "/FotosProducto/";

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
                        //string nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        //ubicacion = path + nombreArchivo;

                        //ubicacion = path + DateTime.Now.ToString("ddMMyyyyhhmmss") + fileFoto.FileName;
                        //fileFoto.PostedFile.SaveAs(ubicacion);
                        //Label1.Text = "File uploaded!";

                        Producto producto = new Producto();
                        producto.Nombre = txtNombre.Text;
                        producto.Descripcion = txtDescripcion.Text;
                        //producto.Foto = ruta + nombreArchivo;
                        HttpPostedFile ImgFile = fileFoto.PostedFile;
                        Byte[] byteImage = new Byte[fileFoto.PostedFile.ContentLength];
                        ImgFile.InputStream.Read(byteImage, 0, fileFoto.PostedFile.ContentLength);

                        producto.Foto = byteImage;

                        producto.PadecimientoId = int.Parse(DropDawnPadecimiento.SelectedValue);
                        if (CheckPublicado.Checked)
                        {
                            producto.Publicado = 1;
                        }
                        else
                        {
                            producto.Publicado = 0;
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


                            int productoID = ProductoDAO.Inserta(producto, listaPaises,int.Parse(Session["id"].ToString()));

                            this.lblMensaje.Text = "Se ingreso correctamente el Producto ID = " + productoID;
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
                        //Label1.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    Label1.Text = "No se aceptan archivos de este tipo.";
                }
                /*Terminamos de verificar lo del archivo*/
            }
            
        }

        public void resetControles()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";

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
            Response.Redirect("Productos.aspx");
        }
    }
}