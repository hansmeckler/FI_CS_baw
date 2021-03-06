using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class invoice_searchdoc : System.Web.UI.Page
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("../default.aspx");
        }

        user = (UsuarioBean)Session["usuario"];
        if (!Page.IsPostBack) {
            ArrayList arr = null;
            ListItem item = null;
            arr = (ArrayList)DB.getSerieFactbySuc(user.SucursalID, 4, user,0);//1 porque es el tipo de documento para facturacion
            foreach (string valor in arr)
            {
                item = new ListItem(valor, valor);
                lb_serie.Items.Add(item);
            }
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        string serie = lb_serie.SelectedValue;
        string correlativo = tb_correlativo.Text.Trim();
        string hbl = tb_hbl.Text.Trim();
        string mbl = tb_mbl.Text.Trim();
        string contenedor = tb_contenedor.Text.Trim();
        string routing = tb_routing.Text.Trim();
        string sql = "Select nd.tnd_id, nd.tnd_serie, nd.tnd_correlativo, nd.tnd_cli_id, nd.tnd_nit,"
                     + " nd.tnd_nombre, nd.tnd_fecha_emision, nd.tnd_total, nd.tnd_moneda, nd.tnd_hbl, nd.tnd_mbl, nd.tnd_contenedor, nd.tnd_routing, nd.tnd_usu_id, t.ted_nombre"
                     + " from tbl_nota_debito nd"
                     + " left join tbl_estado_documento t on t.ted_id = nd.tnd_ted_id"
                     + " where nd.tnd_pai_id=" + user.PaisID;

        if (!serie.Equals("")) sql += " and nd.tnd_serie='" + serie + "'";
        if (!correlativo.Equals("")) sql += " and nd.tnd_correlativo=" + correlativo;
        if (!hbl.Equals("")) sql += " and nd.tnd_hbl='" + hbl + "'";
        if (!mbl.Equals("")) sql += " and nd.tnd_mbl='" + mbl + "'";
        if (!contenedor.Equals("")) sql += " and nd.tnd_contenedor='" + contenedor + "'";
        if (!routing.Equals("")) sql += " and nd.tnd_routing='" + routing + "'";
        if (!lb_ted_id.SelectedValue.Equals("0")) sql += " and nd.tnd_ted_id="+lb_ted_id.SelectedValue;
        if (!tbCliCod.Text.Trim().Equals(""))
        {
            sql += " and nd.tnd_cli_id=" + tbCliCod.Text.Trim();
        }
        
        sql += " and nd.tnd_tcon_id="+ user.contaID +" order by nd.tnd_fecha_emision desc, nd.tnd_id asc ";
        DataTable dt = (DataTable)DB.getND(sql);
        gv_facturas.DataSource = dt;
        gv_facturas.DataBind();
    }
    protected void gv_facturas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        Response.Redirect("viewND.aspx?ndID=" + gv_facturas.Rows[index].Cells[1].Text);
    }
    protected void gv_facturas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
            e.Row.Cells[1].Visible = false;

    }
}
