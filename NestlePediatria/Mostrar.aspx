<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mostrar.aspx.cs" Inherits="NestlePediatria.Mostrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
   <div id="datos"><p style="float:left">Administrar:</p>
</div>

<div id="info_inicio">
        <p>
            <asp:Button ID="btnEventos" runat="server" Text="EVENTOS" class="boton_inicio" style="height:54px;width:170px;" onclick="btnEventos_Click" />
            <asp:Button ID="btnArticulos" runat="server" Text="ART&Iacute;CULOS" class="boton_inicio" 
                style="height:54px;width:170px;" onclick="btnArticulos_Click" />
        
            <asp:Button ID="btnVademecum" runat="server" Text="VADEMECUM" class="boton_inicio" style="height:54px;width:170px;" onclick="btnVademecum_Click" />
            <asp:Button ID="btnMealplanner" runat="server" Text="MEAL PLANNER" class="boton_inicio" 
                style="height:54px;width:170px;" onclick="btnMealplanner_Click" />
        </p>
        <p>
            <asp:Button ID="btnMateriales" runat="server" Text="OTROS MATERIALES" class="boton_inicio" style="height:54px;width:170px;" onclick="btnMateriales_Click" />
            <asp:Button ID="btnNidito" runat="server" Text="NIDITO"  class="boton_inicio"
                style="height:54px;width:170px;" onclick="btnNidito_Click" />
        
            <asp:Button ID="btnAnnales" runat="server" Text="ANNALES" class="boton_inicio" style="height:54px;width:170px;" onclick="btnAnnales_Click" />
            <asp:Button ID="btnPaises" runat="server" Text="PAISES" class="boton_inicio"
                style="height:54px;width:170px;" onclick="btnPaises_Click" />
        </p>
         <p>
            <asp:Button ID="btnUsuarios" runat="server" Text="USUARIOS" class="boton_inicio" style="height:54px;width:170px;" onclick="btnUsuarios_Click" />
            <!--<asp:Button ID="btnReportes" runat="server" Text="REPORTES" 
                Height="68px" Width="155px" />-->
        </p>
        
    </div>
</asp:Content>
