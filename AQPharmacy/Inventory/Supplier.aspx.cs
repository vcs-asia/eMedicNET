using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_Supplier : System.Web.UI.Page
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

        objdl = dA.returnList("SELECT SUPPLIER_NAME, SUPPLIER_CODE, SUPPLIER_ADDR1, SUPPLIER_ADDR2, SUPPLIER_ADDR3, SUPPLIER_EMAIL, SUPPLIER_PHONE1, SUPPLIER_PHONE2, SUPPLIER_FAX, SUPPLIER_CONT_PERSON, SUPPLIER_CONT_PERSON_HP, SUPPLIER_ID FROM SUPPLIER_MST WHERE SUPPLIER_ID = '" + Request.QueryString["_id"].ToString() + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataRow Col = objdl.dataSet.Tables[0].Rows[0];
            txtSName.Text = Col[0].ToString();
            txtDistName.Text = Col[1].ToString();
            txtAddr1.Text = Col[2].ToString();
            txtAddr2.Text = Col[3].ToString();
            txtAddr3.Text = Col[4].ToString();
            txtEmail.Text = Col[5].ToString();
            txtTel1.Text = Col[6].ToString();
            txtTel2.Text = Col[7].ToString();
            txtFax.Text =Col[8].ToString();
            txtCPerson.Text = Col[9].ToString();
            txtAcctNo.Text = Col[10].ToString();
            hdnID.Value = Col[11].ToString();
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
        List<string> gID = new List<string>() { refno, "SUPPLIER_MST", "SUPPLIER_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "SUPPLIER_MST";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "SUPPLIER_ID"};
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "SUPPLIER_ID", "SUPPLIER_NAME", "SUPPLIER_CODE", "SUPPLIER_ADDR1", "SUPPLIER_ADDR2", "SUPPLIER_ADDR3", "SUPPLIER_EMAIL", "SUPPLIER_PHONE1", "SUPPLIER_PHONE2", "SUPPLIER_FAX", "SUPPLIER_CONT_PERSON", "SUPPLIER_CONT_PERSON_HP" };
        objD.CValue = new System.Collections.ArrayList();

        List<string> col = new List<string>()
        {
            refno,
            frmV.Form("ctl00$contentForm$txtSName"),
            frmV.Form("ctl00$contentForm$txtDistName"),
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
        try
        {
            msg = dbaction.saveCollection(objdata, gID);
        }
        catch(Exception ex)
        {
            msg = ex.ToString();
        }
        return msg;
    }
}