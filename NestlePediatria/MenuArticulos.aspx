<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuArticulos.aspx.cs" Inherits="NestlePediatria.MenuArticulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div id="datos">
            <p style="float: left">
                Administrar:</p>
            <div id="Div1">
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
<p style="padding-left:15px;">><a href="Mostrar.aspx">Inicio</a>> Menu Artículos</p>
    <div id="articulos">
        <p>
            <asp:Button ID="btnCategorias" runat="server" Text="CATEGORÍAS" class="boton_inicio" onclick="btnCategorias_Click" style="height:54px;width:155px;" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnArticulos" runat="server" Text="ART&Iacute;CULOS" style="height:54px;width:155px;"
                class="boton_inicio" onclick="btnArticulos_Click" />
        </p>
        
        
    </div>
</asp:Content>
