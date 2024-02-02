using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Manage_Parameters : System.Web.UI.Page
{
    protected void newOLet(object sender, EventArgs e)
    {
        Response.Redirect("~/Manage/Parameter.aspx");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("PARAM_NAME", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("PARAM_NAME", "ASC");
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

        objdl = dA.returnList("SELECT PARAM_NAME, PARAM_ID, PARAM_TYPE, getParamType(PARAM_TYPE) AS PTYPE FROM PARAMETERS_INFO " + searchKeyword);
        if (objdl.flaG == true)
        {
            DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
            Lst.DataSource = dv;
            Lst.DataBind();
        }
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
        fillGrid("PARAM_NAME", "ASC");
    }
    [System.Web.Services.WebMethod]
    public static string[] getDetails(string id)
    {
        string[] data = new string[2];
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PARAM_NAME, PARAM_TYPE FROM PARAMETERS_INFO WHERE PARAM_ID = '" + id + "'");
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
        if (id == "0")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO PARAMETERS_INFO(PARAM_NAME, PARAM_TYPE) VALUES('" + nm + "','" + tp + "')", HttpContext.Current.Session["userid"].ToString());
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO PARAMETER(PrmPdesc, PrmPtype, PrmState, PrmUsrid, PrmCdate, PrmUdate) VALUES('" + nm + "','" + tp + "',1, 'SYS',NOW(),NOW(),)", HttpContext.Current.Session["userid"].ToString());
        }
        else
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PARAMETERS_INFO SET PARAM_NAME ='" + nm + "', PARAM_TYPE = '" + tp + "' WHERE PARAM_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PARAMETERS SET PrmPdesc ='" + nm + "', PrmPtype = '" + tp + "', PrmUdate = NOW() WHERE PrmAutid = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }

        return msg;
    }
}