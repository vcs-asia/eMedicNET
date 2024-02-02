using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Manage_Components : System.Web.UI.Page
{
    protected void newDoctor(object sender, EventArgs e)
    {
        Response.Redirect("~/Manage/Component.aspx");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("COMP_NAME", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("COMP_NAME", "ASC");
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

        objdl = dA.returnList("SELECT COMP_NAME, COMP_ID, getCompType(COMP_TYPE) AS COMPTYPE, COMP_TYPE FROM COMP_MST " + searchKeyword + " ORDER BY COMP_TYPE");
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
        fillGrid("COMP_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    [System.Web.Services.WebMethod]
    public static string[] getDetails(string id)
    {
        string[] data = new string[2];
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMP_NAME, COMP_TYPE FROM COMP_MST WHERE COMP_ID = '" + id + "'");
        if (objdl.flaG == true)
        {
            data[0] = objdl.dataSet.Tables[0].Rows[0][0].ToString();
            data[1] = objdl.dataSet.Tables[0].Rows[0][1].ToString();
        }

        return data;
    }
    [System.Web.Services.WebMethod]
    public static string saveDetails(string id, string nm, string tp)
    {
        string msg = "";
        if (id=="0")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO COMP_MST(COMP_NAME, COMP_TYPE) VALUES('" + nm + "','" + tp + "')", HttpContext.Current.Session["userid"].ToString());
        }
        else
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE COMP_MST SET COMP_NAME ='" + nm + "', COMP_TYPE = '" + tp + "' WHERE COMP_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }

        return msg;
    }

    protected void Lst_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string msg  = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM COMP_MST WHERE COMP_ID = '" + Lst.DataKeys[e.RowIndex].Value.ToString() + "'", HttpContext.Current.Session["userid"].ToString());
    }
}