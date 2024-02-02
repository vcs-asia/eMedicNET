using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Manage_Services : System.Web.UI.Page
{
    protected void newService(object sender, EventArgs e)
    {
        Response.Redirect("~/Patient/Service.aspx");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("SERVICE_ID", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("SERVICE_ID", "ASC");
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

        objdl = dA.returnList("SELECT SERVICE_ID, SERVICE_NAME, SERVICE_CHARGE, SERVICE_TYPE, SERVICE_DESC FROM SERVICE_MST " + searchKeyword);
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
        fillGrid("SERVICE_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    [System.Web.Services.WebMethod]
    public static string[] getDetails(string id)
    {
        string[] data = new string[3];

        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SERVICE_NAME, SERVICE_TYPE, SERVICE_CHARGE FROM SERVICE_MST WHERE SERVICE_ID = '" + id + "'");
        if (objdl.flaG == true)
        {
            data[0] = objdl.dataSet.Tables[0].Rows[0][0].ToString();
            data[1] = objdl.dataSet.Tables[0].Rows[0][1].ToString();
            data[2] = objdl.dataSet.Tables[0].Rows[0][2].ToString();
        }

        return data;
    }

    [System.Web.Services.WebMethod]
    public static string saveDetails(string id, string nm, string tp, string ch)
    {
        string msg = "";

        if (id == "0")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO SERVICE_MST(SERVICE_NAME, SERVICE_CHARGE, SERVICE_TYPE) VALUES('" + nm + "','" + ch + "','" + tp + "')", HttpContext.Current.Session["userid"].ToString());
        }
        else
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE SERVICE_MST SET SERVICE_NAME = '" + nm + "', SERVICE_CHARGE ='" + ch + "', SERVICE_TYPE = '" + tp + "' WHERE SERVICE_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }

        return msg;
    }
}