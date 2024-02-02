using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Account_MySettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getModules();
            getInfo();
        }
    }
    protected void getModules()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MODULE_ID, MODULE_NAME FROM MODULES");

        lstModule.DataSource = objdl.dataSet;
        lstModule.DataTextField = "MODULE_NAME";
        lstModule.DataValueField = "MODULE_ID";
        lstModule.DataBind();
    }
    protected void getInfo()
    {
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT USER_STARTUP, USER_IDLE_TIME FROM USER_SETTINGS WHERE USER_ID = '" + Session["uid"].ToString() + "'");
        if (objdl.flaG==true)
        {
            txtIMinutes.Text = objdl.dataSet.Tables[0].Rows[0][0].ToString();
            lstModule.SelectedValue = objdl.dataSet.Tables[0].Rows[0][1].ToString();
        }
    }
    protected void saveInfo(object sender, EventArgs e)
    {
        string msg = "";
        msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM USER_SETTINGS WHERE USER_ID = '" + Session["uid"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        if (!msg.StartsWith("ERROR"))
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO USER_SETTINGS(USER_ID, USER_STARTUP, USER_IDLE_TIME) VALUES('" + Session["uid"].ToString() + "','" + lstModule.SelectedValue + "','" + txtIMinutes.Text + "')", HttpContext.Current.Session["userid"].ToString());
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg + "INSERT INTO USER_SETTINGS(USER_ID, USER_STARTUP, USER_IDLE_TIME) VALUES('" + Session["uid"].ToString() + "','" + lstModule.SelectedValue + "','" + txtIMinutes.Text + "'";
                pnlError.Visible = true;
            }
        }

    }
}