using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_Suppliers : System.Web.UI.Page
{
    protected void newSupplier(object sender, EventArgs e)
    {
        Response.Redirect("~/Inventory/Supplier.aspx?_id=");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("SUPPLIER_ID", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("SUPPLIER_ID", "ASC");
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
            searchKeyword = " WHERE " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT SUPPLIER_ID, SUPPLIER_NAME, SUPPLIER_CONT_PERSON, SUPPLIER_PHONE1, SUPPLIER_FAX FROM SUPPLIER_MST" + searchKeyword);
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
        fillGrid("SUPPLIER_NAME", "ASC");
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM SUPPLIER_MST WHERE SUPPLIER_ID = '" + Lst.DataKeys[e.RowIndex].Values[0].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
    }
}