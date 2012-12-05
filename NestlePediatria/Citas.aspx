<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Citas.aspx.cs" Inherits="NestlePediatria.Citas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="datos">
        <p style="float: left">
            Administrar:
        </p>
        <div id="Div1">
            <a href="Pacientes.aspx" runat="server" id="linkPacientes" class="boton">PACIENTES</a>
            <a href="Citas.aspx" runat="server" id="linkCitas" class="boton">CITAS</a>
        </div>
    </div>
    <p style="padding-left: 15px;">
        > <a href="MenuDoctor.aspx">Inicio</a> > Citas </p>
<div id="calendario1" style=" padding-top:40px; margin:0 auto; width:300px; text-align:center">
</div>

<div style="display:none">
<asp:TextBox ID="fechabuscar" ClientIDMode="Static" runat="server" ></asp:TextBox>
<asp:Button ID="submitfecha" runat="server" Text="submit"  ClientIDMode="Static" onclick="submitfecha_Click" />
    
</div>


</asp:Content>
