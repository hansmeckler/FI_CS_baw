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

public partial class invoice_searchBL : System.Web.UI.Page
{
    private DataTable dt1;
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["factura"] = null;
        if (Session["usuario"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Nombre", "<script> refresh(); </script>");
        }
        user = (UsuarioBean)Session["usuario"];
        if (!IsPostBack)
        {
        }
    }
    protected void bt_search_Click(object sender, EventArgs e)
    {
        UsuarioBean user = (UsuarioBean)Session["usuario"];
        int lista = 0;
        if (lb_tipo.SelectedValue.Equals("LCL"))
        {
            DataSet ds = DB.getBL_LCL_invoice(lista, tb_criterio.Text.Trim(), user);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        } else if (lb_tipo.SelectedValue.Equals("FCL")) {
            DataSet ds = DB.getBL_FCL_invoice(lista, tb_criterio.Text.Trim(), user);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        } else if (lb_tipo.SelectedValue.Equals("ALMACENADORA")) {

            try { 
                string codDistribucion = Utility.CodigoDistribucionPicking(user.PaisID);
                DataSet ds = DB.getBL_ALMACENADORA(lista, tb_criterio.Text.Trim(), user.pais.schema, codDistribucion, user);
                this.dgw1.DataSource = ds.Tables["bl_list"];
                this.dgw1.DataBind();
                ViewState["dt"] = ds.Tables["bl_list"];
            }
            catch (Exception ex)
            {
            }
        }
        else if (lb_tipo.SelectedValue.Equals("AEREO"))
        {
            DataSet ds = DB.getBL_AEREO(lista, tb_criterio.Text.Trim(), user.pais.schema);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("AEREO BATCH"))
        {
            DataSet ds = DB.getBatch_AEREO(tb_criterio.Text.Trim(), user);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("TERRESTRE T"))
        {
            DataSet ds = DB.getBL_TERRESTRE(lista, tb_criterio.Text.Trim(), user.pais.ISO, user);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("RO ADUANAS"))
        {
            DataSet ds = DB.getRO_Aduanas(user, tb_criterio.Text.Trim());
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("RO SEGUROS"))
        {
            DataSet ds = DB.getRO_Seguros(user, tb_criterio.Text.Trim());
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("DEMORAS"))
        {
            DataSet ds = DB.getContenedor_Demoras(user, tb_criterio.Text.Trim());
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (lb_tipo.SelectedValue.Equals("FCL APL"))
        {
            DataSet ds = DB.getBL_FCL_APL_invoice(lista, tb_criterio.Text.Trim(), user);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
    }

    protected void dgw1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Seleccionar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (lb_tipo.SelectedValue == "AEREO BATCH")
            {
                Response.Write("<script languaje='JavaScript'>window.opener.location.href='invoice.aspx?bl_no=" + dgw1.Rows[index].Cells[2].Text + "&tipo=AEREO BATCH&opid=22&blid=" + dgw1.Rows[index].Cells[1].Text + "&id_cliente=" + dgw1.Rows[index].Cells[3].Text + "'; window.close();</script>");
            }
            else
            {
		        #region Validar Demoras
		        if (dgw1.Rows[index].Cells[6].Text == "19")
		        {
		            string Fecha_Arribo = Server.HtmlDecode(dgw1.Rows[index].Cells[8].Text).Trim();
		            string Fecha_Salida = Server.HtmlDecode(dgw1.Rows[index].Cells[9].Text).Trim();
		            string Fecha_Retorno = Server.HtmlDecode(dgw1.Rows[index].Cells[10].Text).Trim();
		            string Dias_Gracia = Server.HtmlDecode(dgw1.Rows[index].Cells[11].Text).Trim();
		            string Dias_Puerto = Server.HtmlDecode(dgw1.Rows[index].Cells[12].Text).Trim();
		            if (Fecha_Arribo == "")
		            {
		                WebMsgBox.Show("No se puede emitir Cobro por Demoras porque el Contenedor no ha Arribado");
		                return;
		            }
		            if (Fecha_Salida == "")
		            {
		                WebMsgBox.Show("No se puede emitir Cobro por Demoras porque el Contenedor aun esta en Predio");
		                return;
		            }
		            if (Fecha_Retorno == "")
		            {
		                WebMsgBox.Show("No se puede emitir Cobro por Demoras porque el Contenedor no ha retornado a Predio");
		                return;
		            }
		            DateTime F_Retorno = DateTime.Parse(Fecha_Retorno);
		            DateTime F_Salida = DateTime.Parse(Fecha_Salida);
		            DateTime F_Arribo = DateTime.Parse(Fecha_Arribo);
		            DateTime F_Permitida = F_Arribo.AddDays(double.Parse(Dias_Gracia) + double.Parse(Dias_Puerto));
		            int result = DateTime.Compare(F_Retorno, F_Permitida);
		            if (result <= 0)
		            {
		                WebMsgBox.Show("No se puede emitir Cobro por Demoras porque el Contenedor fue retornado en Tiempo");
		                return;
		            }

		        }
		        #endregion 
		        Response.Write("<script languaje='JavaScript'>window.opener.location.href='invoice.aspx?bl_no=" + dgw1.Rows[index].Cells[2].Text + "&tipo=" + lb_tipo.SelectedValue + "&opid=" + dgw1.Rows[index].Cells[6].Text + "&blid=" + dgw1.Rows[index].Cells[7].Text + "'; window.close();</script>");
        	}
		}
    }
    protected void dgw1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dt1 = (DataTable)ViewState["dt"];
        dgw1.DataSource = dt1;
        dgw1.PageIndex = e.NewPageIndex;
        dgw1.DataBind();
    }
    protected void dgw1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            if (e.Row.Cells.Count == 7)
            {
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
            else
            {
                e.Row.Cells[6].Visible = false;
            }
        }
    }
}
