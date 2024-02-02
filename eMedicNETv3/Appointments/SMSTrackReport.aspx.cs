using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Appointments_SMSTrackReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("SMS_TIME", "ASC");
        }
    }
    protected void fillGrid(string sortField, string sortDir)
    {
        objDL objdl = new objDL();
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objdl = dA.returnList("SELECT SMS_ID, SMS_TO, SMS_SENT, SMS_TIME, SMS_APPT FROM SMS_LIST");
        if (objdl.flaG == true)
        {
            Lst.DataSource = new DataView(objdl.dataSet.Tables[0]) { Sort = sortField + " " + sortDir };
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
        fillGrid("SMS_TIME", "ASC");
    }
}