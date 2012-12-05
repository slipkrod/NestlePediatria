﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditPadecimiento.aspx.cs" Inherits="NestlePediatria.AddEditPadecimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <p style="padding-left:15px;">><a href="Mostrar.aspx">Inicio</a>> <a href="MenuVademecum.aspx">Menú Vademecum</a>> <a href="Padecimientos.aspx">Padecimientos</a>> Edición - Nuevo</p>
    <div id="articulos">
<asp:Label ID="lblMensaje" runat="server"></asp:Label><br />
<table>
    <tr>
        <td><asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label></td>
        <td><asp:TextBox ID="txtNombre" runat="server" CssClass="validate[required]"></asp:TextBox></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label></td>
        <td><asp:TextBox ID="txtDescripcion" runat="server" TextMode="multiline" CssClass="validate[required]"></asp:TextBox></td>
    </tr>
    <tr>
        <td><asp:Label ID="lblPublicado" runat="server" Text="Publicado:"></asp:Label></td>
        <td><asp:CheckBox ID="CheckPublicado" runat="server" /></td>
    </tr>
</table>
<div style="text-align:center; padding:20px">
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
        onclick="btnGuardar_Click" />
    <asp:LinkButton ID="linkCancelar" runat="server" class="rojo" onclick="linkCancelar_Click" >Cancelar</asp:LinkButton>
        </div>
        </div>
</asp:Content>
