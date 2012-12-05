<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Annales.aspx.cs" Inherits="NestlePediatria.Annales" %>
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
<p style="padding-left:15px;">><a href="Mostrar.aspx">Inicio</a>> Annales</p>
<div id="info_inicio">
    <asp:LinkButton ID="LinkNuevo" runat="server" onclick="LinkNuevo_Click" class="boton">+ CREAR NUEVO</asp:LinkButton>
    <br /><br />
    <asp:Label ID="lblConfirmacion" runat="server" Text=""></asp:Label>
    <br /><br />
    <asp:GridView ID="dgAnnales" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Height="47px"
                        Width="644px" OnRowCommand="dgAnnales_RowCommand" OnRowDataBound="dgAnnales_RowDataBound"
                        BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal" >
                        <Columns>
                            
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Publicado">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <center>
                                        <asp:Button ID="btnEditar" runat = "server" Text = "Editar" CommandName="btnEditar" CausesValidation="true" CommandArgument='<%# Eval("id") %>' />
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <center>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="btnEliminar" CausesValidation="true" CommandArgument='<%# Eval("id") %>'/>
                                        
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No existen Registros
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="#F7F7F7" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    </asp:GridView>
                    </div>
</asp:Content>
