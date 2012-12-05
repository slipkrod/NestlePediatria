<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuDoctor.aspx.cs" Inherits="NestlePediatria.MenuDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    
     <div id="datos"><p style="float: left"> Administrar:</p></div>

    
    
</div>
     <div id="articulos">
        <p>
            <asp:Button ID="btnPacientes" runat="server" Text="PACIENTES" style="height:54px;width:155px;"
                class="boton_inicio" onclick="btnPacientes_Click" />
            
            <asp:Button ID="btnCitas" runat="server" Text="CITAS" style="height:54px;width:155px;"
                class="boton_inicio" onclick="btnCitas_Click" />
        </p>
       
    </div>
</asp:Content>
