<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="Pacientes.aspx.cs" Inherits="NestlePediatria.Pacientes" %>

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
        ><a href="MenuDoctor.aspx">Inicio</a>> Pacientes
    </p>
    <div id="info_inicio">
        <p>
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" class="boton">+ CREAR NUEVO</asp:LinkButton>
        </p>
        <br />
        <br />
        <div>
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">A</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">B</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">C</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">D</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">E</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">F</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">G</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click">H</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click">I</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click">J</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click">K</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click">L</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click">M</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click">N</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton16_Click">O</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton17" runat="server" OnClick="LinkButton17_Click">P</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton18" runat="server" OnClick="LinkButton18_Click">Q</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton19" runat="server" OnClick="LinkButton19_Click">R</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click">S</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton21" runat="server" OnClick="LinkButton21_Click">T</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton22" runat="server" OnClick="LinkButton22_Click">U</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton23" runat="server" OnClick="LinkButton23_Click">V</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton24" runat="server" OnClick="LinkButton24_Click">W</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton25" runat="server" OnClick="LinkButton25_Click">X</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton26" runat="server" OnClick="LinkButton26_Click">Y</asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton27" runat="server" OnClick="LinkButton27_Click">Z</asp:LinkButton>&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LinkButton28" runat="server" OnClick="LinkButton28_Click">TODOS</asp:LinkButton>
        </div>
        <br />
        <asp:Label ID="lblConfirmacion" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:GridView ID="dgPacientes" runat="server" DataKeyNames="id" AutoGenerateColumns="False"
            Height="47px" Width="644px" OnRowCommand="dgPacientes_RowCommand" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <center>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="btnEditar" CausesValidation="true"
                                CommandArgument='<%# Eval("id") %>' />
                        </center>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <center>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="btnEliminar"
                                CausesValidation="true" CommandArgument='<%# Eval("id") %>' />
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
