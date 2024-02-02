using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;
using Vijay;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        inputEmail.Focus();
    }
    protected void VerifyLogDetails(object sender, EventArgs e)
    {
        try
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            string sQuery = "SELECT USER_LOGIN_ID, USER_EMAIL, USER_NAME, USER_TYPE, DOC_ID, (SELECT DOC_SPECIALIZATION FROM DOCTOR_MST WHERE DOC_ID = USER_MST.DOC_ID), USER_ID, USER_PWD FROM USER_MST WHERE (USER_LOGIN_ID='" + inputEmail.Text + "' OR USER_EMAIL='" + inputEmail.Text + "') AND USER_PWD=MD5('" + inputPassword.Text + "')";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count >0)
            {
                HttpContext.Current.Session.Add("userid", objdl.dataSet.Tables[0].Rows[0][0].ToString());
                HttpContext.Current.Session.Add("username", objdl.dataSet.Tables[0].Rows[0][2].ToString());
                HttpContext.Current.Session.Add("useremail", objdl.dataSet.Tables[0].Rows[0][1].ToString());
                HttpContext.Current.Session.Add("logintime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                HttpContext.Current.Session.Add("usertype", objdl.dataSet.Tables[0].Rows[0][3].ToString());
                HttpContext.Current.Session.Add("docid", objdl.dataSet.Tables[0].Rows[0][4].ToString());
                HttpContext.Current.Session.Add("disc", objdl.dataSet.Tables[0].Rows[0][5].ToString());
                HttpContext.Current.Session.Add("uid", objdl.dataSet.Tables[0].Rows[0][6].ToString());

                objDL objds = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT (SELECT MODULE_PATH FROM MODULES WHERE MODULE_ID = USER_STARTUP) FROM USER_SETTINGS WHERE USER_ID= '" + Session["uid"].ToString() + "'");
                Response.Redirect(objds.dataSet.Tables[0].Rows[0][0].ToString(), false);
            }

            else if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count == 0)
            {
                lblError.Text = "Invalid credentials. Please enter the valid details and try again.";
            }
            else
            {
                lblError.Text = objdl.Msg;
                divError.Visible = true;
            }
        }
        catch(Exception ex)
        {
            lblError.Text = ex.ToString();
            divError.Visible = true;
        }
    }
    [System.Web.Services.WebMethod]
    public static string GetClientIPAddress(System.Web.HttpRequest httpRequest)
    {
        string originalIP = string.Empty;
        string remoteIP = string.Empty;

        string result = string.Empty;

        originalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //Original IP will be updated by Proxy/Load Balancer
        remoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used

        if (originalIP!=null && originalIP.Trim().Length > 0)
        {
            result =  originalIP + "  [" + remoteIP + "]"; //Lets return both the IPs
        }
        else
        {
            result = remoteIP;
        }

        return result;
    }
    [System.Web.Services.WebMethod]
    public static string getCurrentTime()
    {
        return DateTime.Now.ToString("dddd dd MMMM yyyy hh:mm:ss tt");
    }
}