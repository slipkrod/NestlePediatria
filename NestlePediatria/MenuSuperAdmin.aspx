<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MenuSuperAdmin.aspx.cs" Inherits="NestlePediatria.MenuSuperAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <hr />
    <p>Administrar: </p>
    <hr />
</div>

<div id="Menu" style="position: relative; top: 147px; margin: 0 auto; width: 362px;">
        <p>
            <asp:Button ID="btnPaises" runat="server" Text="PAISES" Height="68px" 
                Width="155px" onclick="btnPaises_Click"  />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUsuarios" runat="server" Text="USUARIOS" 
                Height="68px" Width="155px" onclick="btnUsuarios_Click"  />
        </p>
        <p align="center">
            <!--<asp:Button ID="btnReportes" runat="server" Text="REPORTES" Height="68px" 
                Width="155px"  />&nbsp;&nbsp;&nbsp;-->
            
        </p>
        
    </div>
</asp:Content>
