using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugsPOList : System.Web.UI.Page
{
    protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "recPO")
        {
            Response.Redirect("~/Inventory/DrugsGRN.aspx?n=0&p=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "bEdit")
        {
            Response.Redirect("~/Inventory/DrugsPO.aspx?n=" + e.CommandArgument.ToString().Split('.')[0] + "&p=" + e.CommandArgument.ToString().Split('.')[1]);
        }
        if (e.CommandName == "bDelete")
        {
            hdnID.Value = e.CommandArgument.ToString();
            pnlDeleteAlert.Visible = true;
        }
        if (e.CommandName == "bPrint")
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script>window.open(\"" + ResolveUrl("~/report-preview.aspx") + "?id=" + e.CommandArgument.ToString() + "&mod=PO\");</script>");
        }
        if (e.CommandName == "bClose")
        {
            
            string message = "";
            message = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PURCHASE_ORDER_DTLS SET MED_CANCEL_QTY = MED_ORD_QTY - MED_REC_QTY WHERE PO_ID='" + e.CommandArgument.ToString() + "'", HttpContext.Current.Session["userid"].ToString());
            if (message.StartsWith("ERROR"))
            {
                lblError.Text = message;
                pnlError.Visible = true;
            }
            else
            {
                message = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PURCHASE_ORDER_INFO SET POST_FLAG=3, PO_USERID='" + HttpContext.Current.Session["userid"].ToString() + "' WHERE PO_ID='" + e.CommandArgument.ToString() + "'", HttpContext.Current.Session["userid"].ToString());
                if (message.StartsWith("ERROR"))
                {
                    lblError.Text = message;
                    pnlError.Visible = true;
                }
                else
                {
                    int pageIndex = Lst.PageIndex;
                    Response.Redirect(Request.RawUrl);
                    Lst.PageIndex = pageIndex;
                }
            }
        }
    }
    protected void newPO(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/Inventory/DrugsPO.aspx?n=0&p=");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sortExpression"] = "PO_ID";
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
            searchKeyword = " WHERE " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT SUPPLIER_NAME, PO_ID, PO_NO, PO_AMT, PO_DATE, POST_FLAG, getStatus('PURCHASE_ORDER_INFO', PO_ID) AS FLAG FROM PURCHASE_ORDER_INFO JOIN SUPPLIER_MST ON PO_SUPPLIER_ID=SUPPLIER_ID" + searchKeyword + " ORDER BY PO_ID DESC");
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
            str = "<span class='label label-inverse'>N</span>";
        }
        else if (Convert.ToInt32(flag) == 10)
        {
            str = "<span class='label label-inverse'>L</span>";
        }

        return str;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "1" || Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "3" || Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "10")
            {
                e.Row.Cells[6].Controls[1].Visible = false;
                e.Row.Cells[7].Controls[1].Visible = false;
                e.Row.Cells[8].Controls[1].Visible = false;
                e.Row.Cells[10].Controls[1].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT TRANS_ID, MED_NAME, getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_ORD_QTY) AS OQTY,getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_REC_QTY) AS RQTY,getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_OUT_QTY) AS SQTY, MED_AMT, PO_MED_COST FROM PURCHASE_ORDER_DTLS JOIN MEDICINE_MST ON MED_ID=PO_MED_ID WHERE PO_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
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
        string msg = dA.run("DELETE FROM PURCHASE_ORDER_DTLS  WHERE PO_ID='" + int.Parse(hdnID.Value) + "'", HttpContext.Current.Session["userid"].ToString());
        if (!msg.StartsWith("SUCCESS"))
        {
            pnlError.Visible = true;
            lblError.Text = msg;
        }
        else
        {
            msg = dA.run("DELETE FROM PURCHASE_ORDER_INFO WHERE PO_ID='" + int.Parse(hdnID.Value) + "'", HttpContext.Current.Session["userid"].ToString());
            if (!msg.StartsWith("SUCCESS"))
            {
                pnlError.Visible = true;
                lblError.Text = msg;
            }
        }

        hdnID.Value = "";
    }
    protected void cancelDelete(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = false;
    }
}