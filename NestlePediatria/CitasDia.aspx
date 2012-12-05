<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CitasDia.aspx.cs" Inherits="NestlePediatria.WebForm4" EnableEventValidation="true" %>

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
        > <a href="MenuDoctor.aspx">Inicio</a> > <a href="Citas.aspx">Calendario</a> > Citas </p>
    
        <div id="info_inicio">
        <p><asp:Label ID="lblFecha" runat="server"></asp:Label></p><br />
        <a href="AddEditCita.aspx?fc=<%Response.Write(c); %>&iframe=true&amp;width=400&amp;height=240" rel="prettyPhoto[iframe]" class="boton">+ CREAR CITA</a>
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label><br />

        <asp:GridView ID="dgCitas" runat="server" DataKeyNames="id" AutoGenerateColumns="False" Height="47px"
                        Width="644px" OnRowCommand="dgCitas_RowCommand" OnRowDataBound="dgCitas_RowDataBound"
                        BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal" >
                        <Columns>
                            
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Hora Inicio">
                                <ItemTemplate>
                                    <asp:Label ID="lblInicio" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora Final">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinal" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblEditar" runat="server" ></asp:Label>
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
