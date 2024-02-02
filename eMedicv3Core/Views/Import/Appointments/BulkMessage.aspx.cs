using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;

public partial class Appointments_BulkMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getGroups();
        }
    }
    protected void getGroups()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT GROUP_ID, GROUP_NAME FROM contacts_group");
        if (objdl.flaG == true)
        {
            lstGroup.DataSource = objdl.dataSet.Tables[0];
            lstGroup.DataValueField = "GROUP_ID";
            lstGroup.DataTextField = "GROUP_NAME";
            lstGroup.DataBind();
        }
        else
        {
            lblError.Text = objdl.Msg;
            pnlError.Visible = true;
        }
    }
    protected void AssignSMS(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());

        string msg = dA.run("INSERT INTO SMS_LIST(SMS_TIME, SMS_TO, SMS_TEXT, SMS_FLAG) SELECT GETDATE(), CONTACT_HP, '" + txtSMS.Text + "', '0' FROM CONTACTS WHERE CONTACT_GROUP='" + lstGroup.SelectedValue + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg != "SUCCESS")
        {
            lblError.Text = msg;
            pnlError.Visible = true;
        }
    }
    protected void SendSMS(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT SMS_ID, SMS_TIME, SMS_TO, SMS_TEXT, SMS_FLAG FROM SMS_LIST WHERE SMS_FLAG = 0");
        if (objdl.flaG == true)
        {
            /*
            for(int i=0;i<objdl.dataSet.Tables[0].Rows.Count;i++)
            {
                    
            }
             */
        }
        else
        {
            lblError.Text = objdl.Msg;
            pnlError.Visible = true;
        }
    }
}