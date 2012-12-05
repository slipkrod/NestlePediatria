using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Configuration;
using System.Data;


namespace NestlePediatria
{
    public partial class Mealadd : System.Web.UI.Page
    {
        public string connString = ConfigurationManager.ConnectionStrings["conecta"].ConnectionString;


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
            

        
                string paisnom = Request.QueryString["pais"];
                int idpais = Convert.ToInt32(Request.QueryString["id"]);
                pais.Text = paisnom.ToString();

                if (!Page.IsPostBack)
                {
                    /////////////////
                    int etapaid = int.Parse(lb1.Text);
                    int verf = verificar(idpais, etapaid);
                    if (verf == 1)
                    {
                        string[] carga1 = cargar(idpais, etapaid);
                        TextBox1.Text = carga1[0];
                        file01.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Meal/" + carga1[1];
                        file01.Text = "file01.pdf";
                    }
                    /////////////////
                    /////////////////
                    etapaid = int.Parse(lb2.Text);
                    verf = verificar(idpais, etapaid);
                    if (verf == 1)
                    {
                        string[] carga2 = cargar(idpais, etapaid);
                        TextBox2.Text = carga2[0];
                        file02.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Meal/" + carga2[1];
                        file02.Text = "file02.pdf";
                    }
                    /////////////////
                    /////////////////
                    etapaid = int.Parse(lb3.Text);
                    verf = verificar(idpais, etapaid);
                    if (verf == 1)
                    {
                        string[] carga3 = cargar(idpais, etapaid);
                        TextBox3.Text = carga3[0];
                        file03.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Meal/" + carga3[1];
                        file03.Text = "file03.pdf";
                    }
                    /////////////////
                    /////////////////
                    etapaid = int.Parse(lb4.Text);
                    verf = verificar(idpais, etapaid);
                    if (verf == 1)
                    {
                        string[] carga4 = cargar(idpais, etapaid);
                        TextBox4.Text = carga4[0];
                        file04.NavigateUrl = "http://pediatriaservices.azurewebsites.net/Servicios/ArticleService.svc/GetPdf/Meal/" + carga4[1];
                        file04.Text = "file04.pdf";
                    }
                    /////////////////
                }
            
           
        }

        bool insertar(int idpais, string name, int etapaid, Byte[] ruta) 
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connString);
                conn.Open();

                using (SqlCommand cmd = new SqlCommand())
                {


                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO etapa_pais (etapa_id,pais_id,descripcion,ruta_pdf,usuario_id,fecha_add) Values (@etapaid,@idpais, @var,@ruta,@usuarioID,CURRENT_TIMESTAMP)";
                    cmd.Parameters.AddWithValue("@var", name);
                    cmd.Parameters.AddWithValue("@idpais", idpais);
                    cmd.Parameters.AddWithValue("@etapaid", etapaid);
                    cmd.Parameters.AddWithValue("@ruta", ruta);
                    cmd.Parameters.AddWithValue("@usuarioID", int.Parse(Session["id"].ToString()));
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        //vientos
                    }
                    else
                    {

                        //changos
                    }
                }
            }
            catch (Exception ex)
            {

                labalol.Text = ex.ToString();
                //error sql
            }
            finally
            {

                conn.Close();
                labalol.Text = "Etapa insertada correctamente";

            }

            return true;
        }


        bool reemplazar(int idpais, string name, int etapaid, Byte[] ruta)
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(connString);
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE  etapa_pais SET pais_id=@idpais,etapa_id=@etapaid,descripcion=@name,ruta_pdf=@ruta WHERE pais_id=@idpais AND etapa_id = @etapaid ";
                    cmd.Parameters.AddWithValue("@idpais", idpais);
                    cmd.Parameters.AddWithValue("@etapaid", etapaid);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@ruta", ruta);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        labalol.Text = "Etapa cambiada";
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                labalol.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }

            return true;
        }



        int verificar(int idpais, int etapaid)
        {

            SqlConnection conn = null;
                conn = new SqlConnection(connString);
                conn.Open();

               SqlCommand cmd = new SqlCommand();

                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(*) FROM etapa_pais WHERE pais_id=@idpais AND etapa_id = @etapaid ";
                    cmd.Parameters.AddWithValue("@idpais", idpais);
                    cmd.Parameters.AddWithValue("@etapaid", etapaid);
                    int rowsAffected = (int)cmd.ExecuteScalar();
            
                    return rowsAffected;
            
        }

        string[] cargar(int idpais, int etapaid) 
        {

           

            SqlConnection conn = null;
            conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT id,etapa_id,pais_id,descripcion,ruta_pdf FROM etapa_pais WHERE pais_id=@idpais AND etapa_id = @etapaid ";
            cmd.Parameters.AddWithValue("@idpais", idpais);
            cmd.Parameters.AddWithValue("@etapaid", etapaid);
           SqlDataReader rowsAffected;
            rowsAffected = cmd.ExecuteReader();

            List<string> cargador = new List<string>();
            

            while (rowsAffected.Read()) 
            {


                cargador.Add(rowsAffected["descripcion"].ToString());
                cargador.Add(rowsAffected["id"].ToString());
                //cargador.Add(rowsAffected["id"].ToString());
            
            }
            
            return cargador.ToArray();
           
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

           

            //string ubicacion = null;
            Boolean fileOK = false;
            //String path = Server.MapPath("~/PDF/");
            //string nombreArchivo = "";
            //string carpeta = "/PDF/";

            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            //if (fileOK)
            //{
            //        //nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + FileUpload1.FileName;
            //        //ubicacion = path + nombreArchivo;
            //        //FileUpload1.PostedFile.SaveAs(ubicacion);
                    
            //}
   

                    ////////////////////////

                     int idpais = Convert.ToInt32(Request.QueryString["id"]);
                     string name = TextBox1.Text;
                     int etapaid = int.Parse(lb1.Text);
                     //string ruta = carpeta + nombreArchivo;

                     HttpPostedFile ImgFile = FileUpload1.PostedFile;
                     Byte[] byteImage = new Byte[FileUpload1.PostedFile.ContentLength];
                     ImgFile.InputStream.Read(byteImage, 0, FileUpload1.PostedFile.ContentLength);
                     Byte[] ruta = byteImage;
                    
            int verf =  verificar(idpais,etapaid);
            if (verf == 0)
            {


                bool insertado = insertar(idpais,name,etapaid,ruta);

               
             
            }
            else 
            {
                //if (!FileUpload1.HasFile)
                //{
                //    string[] carga1 = cargar(idpais, etapaid);
                //    ruta = carga1[1];
                //}
                bool reemplazado = reemplazar(idpais, name, etapaid, ruta);
            }
        }









        protected void Button2_Click(object sender, EventArgs e)
        {

            //string ubicacion = null;
            Boolean fileOK = false;
            //String path = Server.MapPath("~/PDF/");
            //string carpeta = "/PDF/";
            //string nombreArchivo = "";
            if (FileUpload2.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            //if (fileOK)
            //{
            //   nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + FileUpload2.FileName;
            //    ubicacion = path + nombreArchivo;
            //    FileUpload2.PostedFile.SaveAs(ubicacion);
            //}


            ////////////////////////

            int idpais = Convert.ToInt32(Request.QueryString["id"]);
            string name = TextBox2.Text;
            int etapaid = int.Parse(lb2.Text);

            HttpPostedFile ImgFile = FileUpload2.PostedFile;
            Byte[] byteImage = new Byte[FileUpload2.PostedFile.ContentLength];
            ImgFile.InputStream.Read(byteImage, 0, FileUpload2.PostedFile.ContentLength);
            Byte[] ruta = byteImage;

            

            int verf = verificar(idpais, etapaid);
            if (verf == 0)
            {


                bool insertado = insertar(idpais,name, etapaid, ruta);

            }
            else
            {
                //if(!FileUpload2.HasFile)
                //{
                //    string[] carga2 = cargar(idpais, etapaid);
                //    ruta = carga2[1];
                //}
                bool reemplazado = reemplazar(idpais, name, etapaid, ruta);
            }

        }






        protected void Button3_Click(object sender, EventArgs e) 
        {
            //string ubicacion = null;
            Boolean fileOK = false;
            //String path = Server.MapPath("~/PDF/");
            //string carpeta = "/PDF/";
            //string nombreArchivo = "";
            if (FileUpload3.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload3.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            //if (fileOK)
            //{
            //    nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + FileUpload3.FileName;
            //    ubicacion = path + nombreArchivo;
            //    FileUpload3.PostedFile.SaveAs(ubicacion);
            //}


            ////////////////////////

            int idpais = Convert.ToInt32(Request.QueryString["id"]);
            string name = TextBox3.Text;
            int etapaid = int.Parse(lb3.Text);
            HttpPostedFile ImgFile = FileUpload3.PostedFile;
            Byte[] byteImage = new Byte[FileUpload3.PostedFile.ContentLength];
            ImgFile.InputStream.Read(byteImage, 0, FileUpload3.PostedFile.ContentLength);
            Byte[] ruta = byteImage;



            int verf = verificar(idpais, etapaid);
            if (verf == 0)
            {


                bool insertado = insertar(idpais, name, etapaid, ruta);

            }
            else
            {
                //if (!FileUpload3.HasFile)
                //{
                //    string[] carga3 = cargar(idpais, etapaid);
                //    ruta = carga3[1];
                //}
                bool reemplazado = reemplazar(idpais, name, etapaid, ruta);
            }
        }






        protected void Button4_Click(object sender, EventArgs e) 
        {
            //string ubicacion = null;
            Boolean fileOK = false;
            //String path = Server.MapPath("~/PDF/");
            //string carpeta = "/PDF/";
            //string nombreArchivo = "";
            if (FileUpload4.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload4.FileName).ToLower();
                String[] allowedExtensions = { ".pdf" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            //if (fileOK)
            //{
            //    nombreArchivo = DateTime.Now.ToString("ddMMyyyyhhmmss") + FileUpload4.FileName;
            //    ubicacion = path + nombreArchivo;
            //    FileUpload4.PostedFile.SaveAs(ubicacion);
            //}


            ////////////////////////

            int idpais = Convert.ToInt32(Request.QueryString["id"]);
            string name = TextBox4.Text;
            int etapaid = int.Parse(lb4.Text);

            HttpPostedFile ImgFile = FileUpload4.PostedFile;
            Byte[] byteImage = new Byte[FileUpload4.PostedFile.ContentLength];
            ImgFile.InputStream.Read(byteImage, 0, FileUpload4.PostedFile.ContentLength);
            Byte[] ruta = byteImage;

            int verf = verificar(idpais, etapaid);
            if (verf == 0)
            {


                bool insertado = insertar(idpais, name, etapaid, ruta);

            }
            else
            {
                //if (!FileUpload4.HasFile)
                //{
                //    string[] carga4 = cargar(idpais, etapaid);
                //    ruta = carga4[1];
                //}
                bool reemplazado = reemplazar(idpais, name, etapaid, ruta);
            }

        }

    



    }
}