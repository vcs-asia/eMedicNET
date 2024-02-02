using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;
public partial class Manage_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadProfile();
        }
    }
    private void LoadProfile()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT CLINIC_NAME, CLINIC_REG_NO, CLINIC_ADDR1, CLINIC_ADDR2, CLINIC_ADDR3, CLINIC_PHONE_O, CLINIC_FAX, CLINIC_WEBSITE, CLINIC_EMAIL FROM CLINIC_MST WHERE CLINIC_ID=1");
        if (objdl.flaG==true)
        {
            txtName.Text = objdl.dataSet.Tables[0].Rows[0][0].ToString();
            txtRegNo.Text = objdl.dataSet.Tables[0].Rows[0][1].ToString();
            txtAdd1.Text = objdl.dataSet.Tables[0].Rows[0][2].ToString();
            txtAdd2.Text = objdl.dataSet.Tables[0].Rows[0][3].ToString();
            txtAdd3.Text = objdl.dataSet.Tables[0].Rows[0][4].ToString();
            txtPhone.Text = objdl.dataSet.Tables[0].Rows[0][5].ToString();
            txtFax.Text = objdl.dataSet.Tables[0].Rows[0][6].ToString();
            txtWeb.Text = objdl.dataSet.Tables[0].Rows[0][7].ToString();
            txtEmail.Text = objdl.dataSet.Tables[0].Rows[0][8].ToString();
        }
        else
        {
            pnlError.Visible = true;
        }
    }
    protected void SaveInfo(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        msg = dA.run("DELETE FROM CLINIC_MST", HttpContext.Current.Session["userid"].ToString());
        if (msg.StartsWith("ERROR"))
        {
            pnlError.Visible = true;
            lblError.Text = msg;
        }
        else
        {
            msg = dA.run("INSERT INTO CLINIC_MST(CLINIC_ID, CLINIC_NAME, CLINIC_REG_NO, CLINIC_ADDR1, CLINIC_ADDR2, CLINIC_ADDR3, CLINIC_PHONE_O, CLINIC_FAX, CLINIC_WEBSITE, CLINIC_EMAIL) VALUES('1','" + txtName.Text + "','" + txtRegNo.Text + "', '" + txtAdd1.Text + "', '" + txtAdd2.Text + "', '" + txtAdd3.Text + "', '" + txtPhone.Text + "', '" + txtFax.Text + "', '" + txtWeb.Text + "', '" + txtEmail.Text + "')", HttpContext.Current.Session["userid"].ToString());
            if (msg.StartsWith("ERROR"))
            {
                pnlError.Visible = true;
                lblError.Text = msg;
            }
            else
            {
                pnlSuccess.Visible = true;
            }
        }
    }
}