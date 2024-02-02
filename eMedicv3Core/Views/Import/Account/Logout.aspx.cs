using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;

public partial class Account_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO USER_LOG(USER_ID, USER_LOGIN_ID, LOG_TIME, LOG_TYPE) VALUES('" + Session["uid"].ToString() + "','" + Session["userid"].ToString() + "',NOW(),1)", HttpContext.Current.Session["userid"].ToString());
        Session.Abandon();
        Response.Redirect("~/Account/Login.aspx");
    }
}