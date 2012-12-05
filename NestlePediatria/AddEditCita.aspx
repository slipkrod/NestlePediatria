<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCita.aspx.cs" Inherits="NestlePediatria.AddEditCita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript" src="scripts/jquery-1.8.2.min.js"></script>
    <script src="scripts/js/jquery.validationEngine-es.js" type="text/javascript" charset="utf-8"></script>
    <script src="scripts/js/jquery.validationEngine.js" type="text/javascript" charset="utf-8"></script>
    <link href="scripts/css/template.css" rel="stylesheet" type="text/css" />
    <!-- Css Time-->
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.9.1/themes/smoothness/jquery-ui.css" />
    <link href="scripts/css/jquery-ui-timepicker-addon.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://code.jquery.com/ui/1.9.1/jquery-ui.min.js"></script>
	<script type="text/javascript" src="scripts/js/jquery-ui-timepicker-addon.js"></script>
	<script type="text/javascript" src="scripts/js/jquery-ui-sliderAccess.js"></script>
    <!-- fin time-->
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        jQuery(document).ready(function () {
           
            jQuery("#form2").validationEngine();
        });

        $(document).ready(function () {

            $('#horatxt').timepicker();
            $('#horatxt2').timepicker();
        });

        
     </script>
</head>
<body>
    <form id="form2" runat="server">
    <div>
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    </div>
    <div id="contenido">
    <div id="articulos">
    
        <table>
            <tr>
                <td>
                    Paciente:
                </td>
                <td>
                    <asp:DropDownList ID="DropDownPacientes" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Inicio:
                </td>
                <td>
                    <asp:TextBox ID="horatxt" runat="server" CssClass="validate[required]"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Final:
                </td>
                <td>
                    <asp:TextBox Width="45px" ID="horatxt2" runat="server" CssClass="validate[required]">
                    </asp:TextBox>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="Button1" runat="server" Text="agregar" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        </div>
    </form>
</body>
</html>
