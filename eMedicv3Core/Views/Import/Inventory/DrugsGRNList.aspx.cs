using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugsGRNList : System.Web.UI.Page
{
    protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string msg = "";
        if (e.CommandName=="bEdit")
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script>window.open(\"DrugsGRN.aspx?id=" + e.CommandArgument.ToString() + "\");</script>");
        }
        else if (e.CommandName=="bDelete")
        {
            hdnGrnID.Value = e.CommandArgument.ToString();
            pnlDeleteAlert.Visible = true;
        }
        else if (e.CommandName=="bPrint")
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script>window.open(\"" + ResolveUrl("~/report-preview.aspx") + "?param=" + e.CommandArgument.ToString() + "&mod=GRN\");</script>");
        }
        else if (e.CommandName=="bPOST")
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            List<dbParam> objparams = new List<dbParam>();

            objparams.Add(new dbParam{col= "GrnID",image=null, dType="I", val=e.CommandArgument.ToString()});
            objparams.Add(new dbParam { col = "UserID", image = null, dType = "S", val = Session["userid"].ToString() });
           
            msg = dA.runStoredProcedure("proc_postGRN", objparams,null);
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg;
                pnlError.Visible = true;
            }
            else
            {
                Response.Redirect("~/Inventory/DrugsGRNList.aspx");
            }
        }
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sortExpression"] = "GRN_ID";
            Session["sortDirection"] = "DESC";
            fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
        }
        else
        {
            fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
        }
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string searchKeyword = "";

        if (txtKeyword.Text != "")
        {
            if (lstFields.SelectedValue == "PO_ID")
            {
                searchKeyword = " WHERE GRN_INFO." + lstFields.SelectedValue + " = '" + int.Parse(txtKeyword.Text) + "'";
            }
            else
            {
                searchKeyword = " WHERE " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
            }
        }

        //objdl = dA.returnList("SELECT SUPPLIER_NAME, GRN_ID, INV_NO, INV_AMT, INV_DATE, GRN_INFO.POST_FLAG, getStatus('GRN_INFO', GRN_ID) AS FLAG, GRN_INFO.PO_ID FROM GRN_INFO JOIN PURCHASE_ORDER_INFO ON PURCHASE_ORDER_INFO.PO_ID=GRN_INFO.PO_ID JOIN SUPPLIER_MST ON PURCHASE_ORDER_INFO.PO_SUPPLIER_ID=SUPPLIER_MST.SUPPLIER_ID" + searchKeyword + " ORDER BY GRN_ID DESC");
        objdl = dA.returnList("SELECT SUPPLIER_NAME, GRN_ID, INV_NO, INV_AMT, INV_DATE, GRN_INFO.POST_FLAG, '' AS FLAG, GRN_INFO.PO_ID FROM GRN_INFO JOIN PURCHASE_ORDER_INFO ON PURCHASE_ORDER_INFO.PO_ID=GRN_INFO.PO_ID JOIN SUPPLIER_MST ON PURCHASE_ORDER_INFO.PO_SUPPLIER_ID=SUPPLIER_MST.SUPPLIER_ID" + searchKeyword + " ORDER BY GRN_ID DESC");
        DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
        Lst.DataSource = dv;
        Lst.DataBind();
    }
    protected void Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortDirection = "ASC";

        string lastDirection = ViewState["SortDirection"] as string;

        if ((lastDirection != null) && (lastDirection == "ASC"))
        {
            sortDirection = "DESC";
        }
        ViewState["SortDirection"] = sortDirection;
        fillGrid(e.SortExpression.ToString(), sortDirection);

        Session["sortExpression"] = e.SortExpression;
        Session["sortDirection"] = sortDirection;
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lst.PageIndex = e.NewPageIndex;
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected string getStatus(string flag)
    {
        string str = "";
        if (Convert.ToInt32(flag) == 0)
        {
            str = "<span class='label label-success'>O</span>";
        }
        else if (Convert.ToInt32(flag) == 1)
        {
            str = "<span class='label label-important'>C</span>";
        }
        else if (Convert.ToInt32(flag) == 2)
        {
            str = "<span class='label label-warning'>P</span>";
        }
        else if (Convert.ToInt32(flag) == 3)
        {
            str = "<span class='label label-info'>C</span>";
        }

        return str;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Lst.DataKeys[e.Row.RowIndex].Values[1].ToString()=="1")
            {
                e.Row.Cells[8].Controls[1].Visible = false;
                e.Row.Cells[9].Controls[1].Visible = false;
                e.Row.Cells[10].Controls[1].Visible = false;
            }
            else if (Lst.DataKeys[e.Row.RowIndex].Values[1].ToString()=="0")
            {
                e.Row.Cells[11].Controls[0].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            string qry = "SELECT GRN_DTLS.TRANS_ID, MED_NAME, getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_ORD_QTY) AS OQTY,getMedQuantity(GRN_DTLS.MED_ID, GRN_DTLS.MED_PACK, GRN_DTLS.MED_REC_QTY) AS RQTY,getMedQuantity(GRN_DTLS.MED_ID, GRN_DTLS.MED_PACK, 0) AS SQTY, GRN_DTLS.MED_AMT, GRN_DTLS.MED_COST, MED_BATCH_NO, MED_EXP_DATE, MED_GST_AMT FROM GRN_DTLS JOIN PURCHASE_ORDER_DTLS ON PURCHASE_ORDER_DTLS.TRANS_ID=MED_PO_ID JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID=GRN_DTLS.MED_ID WHERE GRN_DTLS.GRN_ID ='" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'";
            objdl = dA.returnList(qry);
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstPODrugs") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]) { Sort = "MED_NAME  ASC" };
                gv.DataBind();
            }
        }
    }
    protected void deleteMe(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = false;

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = dA.run("DELETE FROM GRN_DTLS  WHERE GRN_ID='" + int.Parse(hdnGrnID.Value) + "'", HttpContext.Current.Session["userid"].ToString());
        if (!msg.StartsWith("SUCCESS"))
        {
            pnlError.Visible = true;
            lblError.Text = msg;
        }
        else
        {
            msg = dA.run("DELETE FROM GRN_INFO WHERE GRN_ID='" + int.Parse(hdnGrnID.Value) + "'", HttpContext.Current.Session["userid"].ToString());
            if (!msg.StartsWith("SUCCESS"))
            {
                pnlError.Visible = true;
                lblError.Text = msg;
            }
        }

        hdnGrnID.Value = "";
    }
    protected void cancelDelete(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = false;
    }
}