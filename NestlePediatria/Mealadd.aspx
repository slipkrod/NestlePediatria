<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master"   AutoEventWireup="true" CodeBehind="Mealadd.aspx.cs" Inherits="NestlePediatria.Mealadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
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

<p style="padding-left:15px;">><a href="Mostrar.aspx">Inicio</a>> <a href="Meal.aspx"> Meal Planner</a> > <asp:Label runat="server" ID="pais"></asp:Label></p>
<div id="info_inicio">
 <asp:Label ID="labalol" runat="server" Text=""></asp:Label>
 <br />
<p style=" text-decoration:underline; color:Blue" onclick="javascript: mostrar('etapa1');">Etapa 1</p>


<div id="etapa1" style="display:block">
   <table cellspacing="10">
   <tr>
    <td>Descripcion:</td>
    <td>
        <asp:TextBox ID="TextBox1"  runat="server" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
   <asp:Label ID="lb1" runat="server" Text="1" Visible="False"></asp:Label></td>
   </tr>
   <tr>
   <td>Pdf:</td>
   <td>
        <asp:FileUpload ID="FileUpload1"  runat="server" /><asp:HyperLink ID="file01" runat="server" Target="_blank"></asp:HyperLink>
   </td>
   </tr>
   <tr><td colspan="2" align="center"><asp:Button ID="ctl00_ContentPlaceHolder1_Button1" runat="server" OnClientClick="javascript:return validartxt('TextBox1');"  onclick="Button1_Click" 
            Text="Guardar" /></td></tr>
   </table>

    


</div>

<p style=" text-decoration:underline; color:Blue" onclick="javascript: mostrar('etapa2');">Etapa 2</p>
<div id="etapa2" style="display:none">
   <table cellspacing="10">
   <tr>
   <td>Descripcion:</td>
   <td><asp:TextBox ID="TextBox2"  runat="server" ClientIDMode="Static" TextMode="MultiLine"
             ></asp:TextBox><asp:Label ID="lb2" runat="server" Text="2" Visible="False"></asp:Label></td>
   </tr>
   <tr><td>Pdf:</td>
   <td><asp:FileUpload ID="FileUpload2"  runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="file02" runat="server" Target="_blank"></asp:HyperLink></td>
   </tr>
   <tr>
   <td colspan="2" align="center"><asp:Button ID="Button2" runat="server" onclick="Button2_Click" OnClientClick="javascript:return validartxt('TextBox2');" Text="Guardar" /></td>
   </tr>
   </table>

</div>

<p style=" text-decoration:underline; color:Blue" onclick="javascript: mostrar('etapa3');">Etapa 3</p>
<div id="etapa3" style="display:none">
    <table cellspacing="10">
    <tr>
    <td>Descripcion:</td>
    <td><asp:TextBox ID="TextBox3"  runat="server" Width="128px" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="lb3" runat="server" Text="3" Visible="False"></asp:Label></td>
    </tr>
    <tr><td>Pdf:</td>
    <td><asp:FileUpload ID="FileUpload3"  runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="file03" runat="server" Target="_blank"></asp:HyperLink></td></tr>
    <tr><td  colspan="2" align="center"><asp:Button ID="Button3" runat="server" onclick="Button3_Click" OnClientClick="javascript:return validartxt('TextBox3');" Text="Guardar" /></td></tr>
    </table>

</div>

<p style=" text-decoration:underline; color:Blue" onclick="javascript: mostrar('etapa4');">Etapa 4</p>
<div id="etapa4" style="display:none">
    <table cellspacing="10">
    <tr>
        <td>Descripcion:</td>
        <td><asp:TextBox ID="TextBox4"  runat="server" Width="130px" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="lb4" runat="server" Text="4" Visible="False"></asp:Label></td>
    </tr>
    <tr><td>Pdf:</td>
    <td><asp:FileUpload ID="FileUpload4"  runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="file04" runat="server" Target="_blank"></asp:HyperLink></td>
    </tr>
    <tr>
        <td colspan="2" align="center"><asp:Button ID="Button4" runat="server"  onclick="Button4_Click" OnClientClick="javascript:return validartxt('TextBox4');" Text="Guardar" /></td>
    </tr>
    </table>
</div>

<div>

<script type="text/javascript">

    function mostrar(a) {

        if ($('#' + a).css('display') == 'none') {
            $('#' + a).show();
        } else {
            $('#' + a).hide();
        }


    }


</script>

    
   

</div>
    </div>
    </div>
</asp:Content>
