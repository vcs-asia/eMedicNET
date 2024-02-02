using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Billing_Company : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["_id"].ToString() != "")
            {
                getInfo();
            }
        }
    }
    protected void getInfo()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT COMPANY_NAME, COMPANY_ADDR1, COMPANY_ADDR2, COMPANY_ADDR3, COMPANY_EMAIL, COMPANY_PHONE1, COMPANY_PHONE2, COMPANY_FAX, COMPANY_CONT_PERSON, COMPANY_CONT_PERSON_HP, COMPANY_ID FROM COMPANY_MST WHERE COMPANY_ID = '" + Request.QueryString["_id"].ToString() + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataRow Col = objdl.dataSet.Tables[0].Rows[0];
            txtSName.Text = Col[0].ToString();
            txtAddr1.Text = Col[1].ToString();
            txtAddr2.Text = Col[2].ToString();
            txtAddr3.Text = Col[3].ToString();
            txtEmail.Text = Col[4].ToString();
            txtTel1.Text = Col[5].ToString();
            txtTel2.Text = Col[6].ToString();
            txtFax.Text =Col[7].ToString();
            txtCPerson.Text = Col[8].ToString();
            txtAcctNo.Text = Col[9].ToString();
            hdnID.Value = Col[10].ToString();
        }
    }
    [System.Web.Services.WebMethod]
    public static string saveInfo(NameValue[] frmV)
    {
        string msg = "";
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        string refno = frmV.Form("ctl00$contentForm$hdnID");
        List<string> gID = new List<string>() { refno, "COMPANY_MST", "COMPANY_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "COMPANY_MST";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "COMPANY_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "COMPANY_ID", "COMPANY_NAME", "COMPANY_ADDR1", "COMPANY_ADDR2", "COMPANY_ADDR3", "COMPANY_EMAIL", "COMPANY_PHONE1", "COMPANY_PHONE2", "COMPANY_FAX", "COMPANY_CONT_PERSON", "COMPANY_CONT_PERSON_HP" };

        objD.CValue = new System.Collections.ArrayList();

        List<string> col = new List<string>()
        {
            refno,             
            frmV.Form("ctl00$contentForm$txtSName"),
            frmV.Form("ctl00$contentForm$txtAddr1"),
            frmV.Form("ctl00$contentForm$txtAddr2"),
            frmV.Form("ctl00$contentForm$txtAddr3"),
            frmV.Form("ctl00$contentForm$txtEmail"),
            frmV.Form("ctl00$contentForm$txtTel1"),
            frmV.Form("ctl00$contentForm$txtTel2"),
            frmV.Form("ctl00$contentForm$txtFax"),
            frmV.Form("ctl00$contentForm$txtCPerson"),
            frmV.Form("ctl00$contentForm$txtHPNo")
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        msg = dbaction.saveCollection(objdata, gID);

        return msg;
    }
}