using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using Vijay;

public partial class Account_changePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    [System.Web.Services.WebMethod]
    public static string saveInfo(string npwd)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE USER_MST SET USER_PWD=MD5('" + npwd + "') WHERE USER_ID = '" + HttpContext.Current.Session["uid"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string verifyPassword(string pwd, string npwd)
    {
        string msg = "";
        if (CheckingPasswordStrength(npwd) < 5)
        {
            msg = "ERROR-Password does not meet requirement";
        }
        else
        {
            objDL objdl = new objDL();
            objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COUNT(1) FROM USER_MST WHERE USER_ID = '" + HttpContext.Current.Session["uid"].ToString() + "' AND USER_PWD = MD5('" + pwd + "')");
            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows[0][0].ToString() != "0")
            {
                msg = "";
            }
            else
            {
                msg = "ERROR: Old password is not correct.";
            }
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    private static int CheckingPasswordStrength(string password)
    {
        int score = 1;

        if (password.Length < 1)
            return 1;
        if (password.Length < 4)
            return 2;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.IsMatch(password, @"/\d+/", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"^(?=.*[a-z]).+$", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"^(?=.*[A-Z]).+$", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"[0-9]", RegexOptions.ECMAScript))
            score++;
        if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,(,)]", RegexOptions.ECMAScript))
            score++;

        return score;
    }
}