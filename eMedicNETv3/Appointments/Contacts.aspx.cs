using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Appointments_Contacts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getGroups();
            fillGrid("CONTACT_NAME", "ASC");
        }
    }
    protected void getGroups()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT GROUP_NAME, GROUP_ID FROM CONTACTS_GROUP");
        if (objdl.flaG == true)
        {
            lstGroup.DataSource = objdl.dataSet.Tables[0];
            lstGroup.DataValueField = "GROUP_ID";
            lstGroup.DataTextField = "GROUP_NAME";
            lstGroup.DataBind();
        }
    }
    protected void fillGrid(string sortField, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT CONTACT_ID, CONTACT_NAME, CONTACT_HP, GROUP_NAME, CONTACT_EMAIL FROM CONTACTS JOIN CONTACTS_GROUP ON GROUP_ID=CONTACT_GROUP");
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
        fillGrid("CONTACT_NAME", "ASC");
    }
    protected void saveInfo(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = dA.run("INSERT INTO CONTACTS(CONTACT_NAME, CONTACT_HP, CONTACT_EMAIL, CONTACT_GROUP) VALUES('" + txtName.Text + "','" + txtHP.Text + "','" + txtEmail.Text + "','" + lstGroup.SelectedValue + "')", HttpContext.Current.Session["userid"].ToString());
        if (msg != "SUCCESS")
        {
            lblError.Text = msg;
            pnlError.Visible = true;
        }
        else
        {
            fillGrid("CONTACT_NAME", "ASC");
        }
    }
}