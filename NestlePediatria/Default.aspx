<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NestlePediatria._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="login" >
        <table width="300" id="ctl00_ContentPlaceHolder1_Table1" border="0">
	<tr>
	  <td align="right"><asp:Label ID="lblUsuario" runat="server" Text="Usuario: "></asp:Label></td>
      <td align="right"><asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox></td>
	  </tr>
	<tr>
	  <td>&nbsp;</td>
	  <td>&nbsp;</td>
	  </tr><tr>
	    <td align="right"><asp:Label ID="lblPassword" runat="server" Text="Contraseña: "></asp:Label></td>
        <td align="right"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
	    </tr>
	<tr>
	  <td></td>
	  <td align="right" height="15"></td>
	  </tr>
	<tr>
	  <td></td>
	  <td align="right"><asp:Button ID="Button1" runat="server" Text="Entrar" onclick="btnEnviar_Click" class="boton_inicio" style=" width:120px; height:54px" /></td>
	  </tr>
      <tr><td colspan="2"><asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label></td></tr>
      </table>
        </div>
        <br />
        
    
 
</asp:Content>
