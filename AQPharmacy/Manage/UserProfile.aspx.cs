using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Account_UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getDoctors();
            getInfo();
        }
    }
    protected void getDoctors()
    {
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT DOC_ID, DOC_NAME FROM DOCTOR_MST ORDER BY DOC_NAME");

        lstDoctor.DataSource = objdl.dataSet;
        lstDoctor.DataTextField = "DOC_NAME";
        lstDoctor.DataValueField = "DOC_ID";
        lstDoctor.DataBind();
    }
    protected void getInfo()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT USER_NAME, USER_LOGIN_ID, USER_SEX, USER_TYPE, USER_EMAIL, DOC_ID, USER_STATUS FROM USER_MST WHERE USER_ID = '" + Session["uid"].ToString() + "'");
        if (objdl.flaG == true)
        {
            DataRow Col = objdl.dataSet.Tables[0].Rows[0];
            txtUserName.Text = Col[0].ToString();
            txtLoginID.Text = Col[1].ToString();
            lstSex.SelectedValue = Col[2].ToString();
            lstUType.SelectedValue = Col[3].ToString();
            txtUEmail.Text = Col[4].ToString();
            lstDoctor.SelectedValue = Col[5].ToString();
            lstStatus.SelectedValue = Col[6].ToString();
        }
    }
    protected void saveInfo(object sender, EventArgs e)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE USER_MST SET USER_NAME='" + txtUserName.Text + "', USER_SEX ='" + lstSex.SelectedValue + "', USER_EMAIL = '" + txtUEmail.Text + "', DOC_ID ='" + lstDoctor.SelectedValue + "' WHERE USER_ID = '" + Session["uid"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg.StartsWith("ERROR"))
        {
            lblError.Text = msg;
            pnlError.Visible = true;
        }
    }
}