<%@ Page Language="C#" MasterPageFile="~/invoice/manager.master" AutoEventWireup="true" CodeFile="searchNC_cliente.aspx.cs" Inherits="invoice_searchdoc" Title="AIMAR - BAW" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Contenido" Runat="Server">
<div id="box">
<h3 id="adduser">BUSCAR NOTA CREDITO<asp:ScriptManager ID="ScriptManager1" runat="server" />
                            </h3>
<table width="700">
    <tr><td>
        <table width="650" align="center">
        <tbody>
            <tr>
                <td>Serie</td>
                <td>
                    <asp:DropDownList ID="lb_serie" runat="server" Width="150px">
                    </asp:DropDownList>
                                                    </td>
            </tr>
            <tr>
                <td>Correlativo</td>
                <td>
                    <asp:TextBox ID="tb_correlativo" runat="server" Height="16px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>HBL</td>
                <td>
                    <asp:TextBox ID="tb_hbl" runat="server" Height="16px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>MBL</td>
                <td>
                    <asp:TextBox ID="tb_mbl" runat="server" Height="16px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>CONTENEDOR</td>
                <td>
                    <asp:TextBox ID="tb_contenedor" runat="server" Height="16px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>ROUTING</td>
                <td>
                    <asp:TextBox ID="tb_routing" runat="server" Height="16px" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Estado Documento</td>
                <td>
                    
                    <asp:DropDownList ID="lb_ted_id" runat="server" Width="150px">
                        <asp:ListItem Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="1">Activa</asp:ListItem>
                        <asp:ListItem Value="2">Abonada</asp:ListItem>
                        <asp:ListItem Value="3">Anulada</asp:ListItem>
                        <asp:ListItem Value="4">Pagada</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>Codigo de Cliente</td>
                <td>
                    
                    <asp:TextBox ID="tbCliCod" runat="server" Height="16px" 
                        Width="300px"></asp:TextBox>
                    
                    <cc1:FilteredTextBoxExtender ID="tbCliCod_FilteredTextBoxExtender" 
                        runat="server" Enabled="True" FilterType="Numbers" TargetControlID="tbCliCod">
                    </cc1:FilteredTextBoxExtender>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btn_buscar" runat="server" onclick="btn_buscar_Click" 
                        Text="Buscar" />
                </td>
            </tr>
        </tbody>
        </table>
    </td>
    </tr>
    <tr>
        <td>
            <table align="left">
            <tr>
                <td>
                    <asp:GridView ID="gv_facturas" runat="server" 
                        EmptyDataText="No existen datos que cumplan con estos criterios." 
                        onrowcommand="gv_facturas_RowCommand" PageSize="30" 
                        onrowcreated="gv_facturas_RowCreated">
                        <EmptyDataRowStyle BackColor="Yellow" ForeColor="#FF3300" />
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Ver Detalle" 
                                ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            </table>
        </td>
    </tr>
</table>
</div>
            
</asp:Content>

