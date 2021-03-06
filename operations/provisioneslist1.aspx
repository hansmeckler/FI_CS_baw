<%@ Page Language="C#" MasterPageFile="~/operations/manager.master" AutoEventWireup="true" CodeFile="provisioneslist1.aspx.cs" Inherits="operations_provisioneslist" Title="AIMAR - BAW" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" Runat="Server">
    <div id="box">
<fieldset id="Fieldset1">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script language="javascript" type="text/javascript">   
    function mpeCuentaOnCancel()
    {
        
    }  

    function mpeSeleccionOnCancel()
    {
        
    }
    </script>
<h3 id="adduser">PROVISIONES</h3>
<table width="650">
<tr><td>
    <table width="650">
    <tr><td>
        Tipo de proveedor:
       <asp:DropDownList ID="lb_tipopersona" runat="server" AutoPostBack="True" 
            onselectedindexchanged="lb_tipopersona_SelectedIndexChanged">
                    <asp:ListItem>Seleccione...</asp:ListItem>
                    <asp:ListItem Value="4">Proveedor</asp:ListItem>
                    <asp:ListItem Value="2">Agente</asp:ListItem>
                    <asp:ListItem Value="5">Naviera</asp:ListItem>
                    <asp:ListItem Value="6">Lineas Aereas</asp:ListItem>
                    <asp:ListItem Value="8">Caja Chica</asp:ListItem>
                </asp:DropDownList>
                &nbsp; Moneda:
       <asp:DropDownList ID="lb_moneda" runat="server" Enabled="False">
                </asp:DropDownList>
                <br />
        Codigo de proveedor: <asp:TextBox ID="tb_agenteID" runat="server" Height="16px" 
                        Width="115px"></asp:TextBox>
                    <br />
                    Nombre de proveedor: 
                    <asp:TextBox ID="tb_agentenombre" runat="server" Height="16px" Width="377px"></asp:TextBox>
                    <!--
                    <br />
                     Contacto:<asp:TextBox ID="tb_contacto" runat="server" Height="16px" 
                        Width="308px"></asp:TextBox>
                    <br />
                    Direccion:
                    <asp:TextBox ID="tb_direccion" runat="server" Height="16px" Width="326px"></asp:TextBox>
                    &nbsp;Teléfono:&nbsp;
                    <asp:TextBox ID="tb_telefono" runat="server" Height="16px" Width="96px"></asp:TextBox>
                    <br />
                    Correo electrónico:<asp:TextBox ID="tb_correoelectronico" runat="server" 
                        Height="16px" Width="231px"></asp:TextBox>
        <br />
        -->
        <br />
        Orden de compra:         <asp:DropDownList ID="lb_seriefactura" runat="server">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <asp:TextBox ID="tb_corr_oc" runat="server" Height="16px" Width="50px"></asp:TextBox>
        <br />Correlativo de la provision:&nbsp;&nbsp;
        <asp:TextBox ID="tb_corr_provision" runat="server" Height="16px" Width="50px"></asp:TextBox>
        &nbsp;
       <asp:DropDownList ID="lb_ted" runat="server">
                    <asp:ListItem Value="1">Activo</asp:ListItem>
                    <asp:ListItem Value="5">Autorizada</asp:ListItem>
                    <asp:ListItem Value="3">Anulada</asp:ListItem>
                    <asp:ListItem Value="9">Cortado</asp:ListItem>
                    <asp:ListItem Value="4">Pagado</asp:ListItem>
                </asp:DropDownList>
        <br />
        MBL:                    <asp:TextBox ID="tb_mbl" runat="server" Height="16px" 
            Width="96px"></asp:TextBox>
        <br />
        <br />
    </td></tr>
    <tr><td align="center">
        <asp:Button ID="btn_buscar" runat="server" onclick="btn_buscar_Click1" 
            Text="Buscar" />
        </td></tr>
    </table>
</td></tr>
<tr><td><h3 id="H1">Listado de provisiones</h3></td></tr>
<tr><td>&nbsp;<asp:GridView ID="gv_detalle" runat="server" Width="623px" 
        onrowcommand="gv_detalle_RowCommand" onrowcreated="gv_detalle_RowCreated">
        <Columns>
            <asp:ButtonField ButtonType="Button" CommandName="Detalle" Text="Ver Detalle" />
        </Columns>
        </asp:GridView>
        <br />
</td></tr>
<tr><td>

    &nbsp;</td></tr>
<tr><td>
    <!-- ***************************************************************************** -->
    <asp:Panel ID="pnlProveedor" runat="server" CssClass="CajaDialogo" 
        Width="800px">
            <div><table>
                <tr><td colspan="2" align="center">Filtrar por</td></tr>
                <tr>
                    <td>Nit:<asp:TextBox ID="tb_nitb" runat="server" Height="16px" Width="131px" /></td>
                </tr>
                <tr>
                    <td>Nombre:<asp:TextBox ID="tb_nombreb" runat="server" Height="16px" Width="293px" /></td>
                </tr>
                <tr>
                    <td>Codigo:<asp:TextBox ID="tb_codigo" runat="server" Height="16px" Width="293px" /></td>
                </tr>
                <tr>
                    <td><asp:Button ID="bt_buscar" runat="server" Text="Buscar" onclick="bt_buscar_Click" /></td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar Agente" />
            </div>
            <div>
                <asp:GridView ID="gv_proveedor" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" onload="gv_proveedor_Load" 
                    onpageindexchanging="gv_proveedor_PageIndexChanging" 
                    onselectedindexchanged="gv_proveedor_SelectedIndexChanged" PageSize="5">
                </asp:GridView>
            </div>
            <div>
                &nbsp;&nbsp;
                <asp:Button ID="btnProveedorCancelar" runat="server" Text="Cancelar" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="tb_agenteID"
            PopupControlID="pnlProveedor" CancelControlID="btnProveedorCancelar"
            OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True"
            BackgroundCssClass="FondoAplicacion" />
    <!-- ***************************************************************************** -->
    </td>
</tr>
</table>
</fieldset>
</div>
</asp:Content>

