using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Account_Memos : System.Web.UI.Page
{
    protected void newOLet(object sender, EventArgs e)
    {
        Response.Redirect("~/Manage/Memo.aspx");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("MEMO_DATE", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("MEMO_DATE", "ASC");
        }
        else
        {
        }
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string searchKeyword = "";

        if (txtKeyword.Text != "")
        {
            searchKeyword = " AND " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT MEMO_ID, MEMO_TUSER, MEMO_CUSER, MEMO_BUSER, MEMO_DATE, MEMO_SUBJECT FROM USER_MEMO WHERE MEMO_USER = '" + Session["uid"].ToString() + "'" + searchKeyword);
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
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lst.PageIndex = e.NewPageIndex;
        fillGrid("USER_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}