﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="AddEditProducto.aspx.cs" Inherits="NestlePediatria.AddEditProducto" %>

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
    <p style="padding-left: 15px;">
        <<a href="Mostrar.aspx">Inicio</a>> <a href="MenuVademecum.aspx">Menú Vademecum</a>>
        <a href="Productos.aspx">Productos</a>> Edición - Nuevo</p>
    <div id="info">
        <asp:Label ID="lblMensaje" runat="server"></asp:Label><br />
        <table width="900" border="0" cellspacing="5" cellpadding="0">
            <tr>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="validate[required]"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="multiline" CssClass="validate[required]"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Padecimiento:
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDawnPadecimiento" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <a id="imageProducto" runat="server" target="_blank">Ver Imagen</a>
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Foto:"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fileFoto" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Publicado:
                            </td>
                            <td>
                                <asp:CheckBox ID="CheckPublicado" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%" valign="top">
                    <table>
                        <tr>
                            <td>
                                Países:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:CheckBoxList ID="checkPaises" runat="server">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr><td>&nbsp;</td></tr>
    <tr>
    <td align="right"><input type="checkbox" id="CheckCcepta" class="validate[required] checkbox" runat="server" /></td>
    <td class="aviso">Estoy consciente y seguro de contar con todos los derechos de autor y legales necesarios para la publicación de esta información en el país involucrado.</td>
    </tr>
        </table>
        <div style="text-align: center; padding: 20px;">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            <asp:LinkButton ID="linkCancelar" runat="server" class="rojo" onclick="linkCancelar_Click" >Cancelar</asp:LinkButton>
            <asp:HiddenField ID="hiddenFoto" runat="server" />
        </div>
    </div>
</asp:Content>
