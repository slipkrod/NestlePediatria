<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditUsuario.aspx.cs" Inherits="NestlePediatria.AddEditUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function validaPassword() {
           // alert("entra");
        
           // return false;
       
            var valor1 = document.getElementById("ctl00_ContentPlaceHolder1_txtPassword").value;
            var valor2 = document.getElementById("ctl00_ContentPlaceHolder1_txtPassword2").value;

            //alert(valor1);
            //alert(valor2);
            //muestro el span
           // span.show().removeClass();
            //condiciones dentro de la función
            if (valor1 == "" && valor2 == "") {
               // $('#ctl00_ContentPlaceHolder1_lblMensajePass').text("La contraseña es requerida");
                return false;
            }


            if (valor1 != valor2) {
                //document.getElementById("txtPassword2").setAttribute("CssClass", "required");
               // alert("Las contraseñas no coinciden");
               // $('#ctl00_ContentPlaceHolder1_lblMensajePass').text("La contraseña no coincide");
                return false;

            } else {
                if (valor1 == valor2) {
                    //alert("son iguales");
                return true;
            } else {
                return false;
                }
        }
        return false;
        
        }
        

    


</script>
    <div>
        <div id="datos">
            <p style="float: left">
                Administrar:</p>
            <div id="menu">
                <a href="Eventos.aspx" runat="server" id="linkEvento" class="boton">EVENTOS</a>
                <a href="MenuArticulos.aspx" runat="server" id="linkArticulos" class="boton">ARTICULOS
                    CIENTIFICOS</a> <a href="MenuVademecum.aspx" runat="server" id="linkVademecum" class="boton">
                        VADEMECUM</a> <a href="Meal.aspx" runat="server" id="linkMeal" class="boton">MEAL PLANNER</a>
                <a href="Materiales.aspx" runat="server" id="linkMateriales" class="boton">OTROS MATERIALES</a>
                <a href="Nidito.aspx" runat="server" id="linkNidito" class="boton">NIDITO</a> <a
                    href="Annales.aspx" runat="server" id="linkAnnales" class="boton">ANNALES</a>
                <a href="Paises.aspx" runat="server" id="linkPaises" class="boton">PAISES</a> <a
                    href="Usuarios.aspx" runat="server" id="linkUsuarios" class="boton">USUARIOS</a>
            </div>
        </div>
    </div>
<p style="padding-left:15px;">><a href="Mostrar.aspx">Inicio</a>> <a href="Usuarios.aspx">Usuarios</a> > Nuevo - Edición</p>
<div id="info">
<asp:Label ID="lblMensaje" runat="server"></asp:Label><br /><br />
<table width="900" border="0" cellspacing="5" cellpadding="0">
    <tr>
        <td width="50%" valign="top">
            <table>
                <tr>
                    <td>Nombre:</td>
                    <td><asp:TextBox ID="txtNombre" runat="server" CssClass="validate[required]"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Usuario:</td>
                    <td><asp:TextBox ID="txtUsuario" runat="server" CssClass="validate[required, custom[user]]"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="validate[required] text-input"></asp:TextBox></td>
        
                </tr>
                <tr>
                    <td>Confirmar Password:</td>
                    <td><asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" CssClass="validate[required,minSize[6], equals[ctl00_ContentPlaceHolder1_txtPassword]] text-input"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Rol: </td>
                    <td>
                        <asp:DropDownList ID="DropDawnRol" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Habilitado:</td>
                    <td>
                        <asp:CheckBox ID="CheckHabilitado" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
        <td  width="50%" valign="top">
    <table>
    <tr><td>Países:</td></tr>
    <tr>
            <td>&nbsp;</td>
            <td>
            
            <asp:CheckBoxList ID="checkPaises" runat="server" >
                 
            </asp:CheckBoxList></td>
        </tr>
    </table>
    
    </td>
    </tr>
    
     
    
</table>
<div style="text-align:center; padding:20px;">
   <asp:Button ID="btnGuardar" runat="server" Text="Guardar" onclick="btnGuardar_Click" OnClientClick="return validaPassword2();"
         />
   <asp:LinkButton ID="linkCancelar" runat="server" class="rojo" onclick="linkCancelar_Click" >Cancelar</asp:LinkButton>
         </div> </div>
</asp:Content>
