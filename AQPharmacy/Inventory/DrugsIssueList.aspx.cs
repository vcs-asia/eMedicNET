using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugsIssueList : System.Web.UI.Page
{
    protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "edit")
        {
            Response.Redirect("~/Inventory/DrugsIssue.aspx?n=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "print")
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script>window.open(\"" + ResolveUrl("~/report-preview.aspx") + "?param=" + e.CommandArgument.ToString() + "&mod=ISS\");</script>");
        }
        if (e.CommandName == "post")
        {
            string msg = "";
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            List<dbParam> objparams = new List<dbParam>();

            objparams.Add(new dbParam { col = "IssueID", image = null, dType = "I", val = e.CommandArgument.ToString() });

            msg = dA.runStoredProcedure("proc_postIssue", objparams, null);
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg;
                pnlError.Visible = true;
            }
            else
            {
                int pageIndex = Lst.PageIndex;
                fillGrid(Session["sortexpression"].ToString(), Session["sortdirection"].ToString());
            }
        }
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM STOCK_ISSUE_DTLS WHERE STK_REF_ID = '" + Lst.DataKeys[e.RowIndex].Values[0].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg.StartsWith("ERROR"))
        {
            lblError.Text = msg;
            pnlError.Visible = true;
        }
        else
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM STOCK_ISSUE_INFO WHERE ISSUE_REF_ID = '" + Lst.DataKeys[e.RowIndex].Values[0].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg;
                pnlError.Visible = true;
            }
        }
    }
    protected void newIssue(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/Inventory/DrugsIssue.aspx?n=0&p=");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sortExpression"] = "ISSUE_REF_ID";
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

        objdl = dA.returnList("SELECT OUTLET_NAME, ISSUE_REF_ID, ISSUE_REF_NO, ISSUE_DATE, ISSUE_POST_FLAG, getStatus('STOCK_ISSUE_INFO', ISSUE_REF_ID) AS FLAG FROM STOCK_ISSUE_INFO JOIN OUTLET_MST ON ISSUE_OUTLET=OUTLET_ID" + searchKeyword + " ORDER BY ISSUE_REF_ID DESC");
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
            if (Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "1")
            {
                e.Row.Cells[5].Controls[1].Visible = false;
                e.Row.Cells[6].Controls[1].Visible = false;
                e.Row.Cells[7].Controls[1].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT TRANS_ID, MED_NAME, getMedQuantity(MED_ID, ISSUE_PACK, ISSUE_QTY) AS RQTY FROM STOCK_ISSUE_DTLS JOIN MEDICINE_MST ON MED_ID=ISSUE_MED_ID WHERE STK_REF_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstPODrugs") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]) { Sort = "MED_NAME  ASC" };
                gv.DataBind();
            }
        }
    }
}