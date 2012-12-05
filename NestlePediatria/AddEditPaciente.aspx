<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AddEditPaciente.aspx.cs" Inherits="NestlePediatria.AddEditPaciente" %>
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
        ><a href="MenuDoctor.aspx">Inicio</a>><a href="Pacientes.aspx">Pacientes</a>>Nuevo - Edición
    </p>
<div id="info">
<asp:Label ID="lblMensaje" runat="server"></asp:Label><br />
<table>
    <tr>
        <td>
        <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblSexo" runat="server" Text="Sexo:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="DropDownSexo" runat="server">
                <asp:ListItem Text="Femenino" Value="Femenino"></asp:ListItem>
                <asp:ListItem Text="Masculino" Value="Masculino"></asp:ListItem>
            </asp:DropDownList>
        
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblFechaNac" runat="server" Text="Fecha Nacimiento:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblLugarNac" runat="server" Text="Lugar de Nacimiento:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtLugarNac" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
   
        <td>
        <asp:Label ID="lblCiudadNac" runat="server" Text="Ciudad:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtCiudadNac" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblgrupoSan" runat="server" Text="Grupo Sanguíneo:"></asp:Label></td>
        <td>
        <asp:TextBox ID="TxtGrupoSanguineo" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
   
        <td>
        <asp:Label ID="lblRh" runat="server" Text="RH:"></asp:Label></td>
        <td>
            <asp:DropDownList ID="DropDawnRh" runat="server">
                <asp:ListItem Text="Positivo" Value="Positivo"></asp:ListItem>
                <asp:ListItem Text="Negativo" Value="Negativo"></asp:ListItem>
            </asp:DropDownList>
       
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblAlergico" runat="server" Text="Alergico a:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtAlergico" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblNombreMadre" runat="server" Text="Nombre de la Madre:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtNombreMadre" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    
        <td>
        <asp:Label ID="lblOcupacionMadre" runat="server" Text="Ocupación:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtOcupacionMadre" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblNombrePadre" runat="server" Text="Nombre del Padre:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtNombrePadre" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    
        <td>
        <asp:Label ID="lblOcupacionPadre" runat="server" Text="Ocupación:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtOcupacionPadre" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblCalle" runat="server" Text="Calle:"></asp:Label></td>
        <td>
        <asp:TextBox ID="TxtCalle" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    
        <td>
        <asp:Label ID="lblColonia" runat="server" Text="Colonia:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtColonia" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblCiudad" runat="server" Text="Ciudad:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtCiudad" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    
        <td>
        <asp:Label ID="lblEstado" runat="server" Text="Estado:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtEstado" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
         <td>
        <asp:Label ID="lblCp" runat="server" Text="CP:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtCp" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
        <td>
        <asp:Label ID="lblcorreo" runat="server" Text="Correo Electrónico:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtCorreo" runat="server" CssClass="validate[required]"></asp:TextBox>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr><td colspan="2">Si asiste a guardería:</td></tr>
    
    <tr>
    <td>
        <asp:Label ID="lblEncargado" runat="server" Text="Nombre del Encargado:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtEncargado" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="lblTelContacto" runat="server" Text="Teléfono de Contacto:"></asp:Label></td>
        <td>
        <asp:TextBox ID="txtTelContacto" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td><asp:Label ID="lblNotas" runat="server" Text="Notas:"></asp:Label></td>
    <td><asp:TextBox ID="txtNotas" runat="server" TextMode="multiline" ></asp:TextBox></td>
    </tr>
</table>
<div style="text-align:center; padding:20px;">
   <asp:Button ID="btnGuardar" runat="server" Text="Guardar" onclick="btnGuardar_Click" />
   
         <asp:LinkButton ID="linkCancelar" runat="server" class="rojo" onclick="linkCancelar_Click" >Cancelar</asp:LinkButton>
        </div>
</div>
</asp:Content>
