using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_Drug : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCode.Focus();
        if (!IsPostBack)
        {
            loadPreData();
            if (Request.QueryString["id"].ToString()!="0")
                getInfo();
        }
    }
    protected void loadPreData()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT PARAM_ID, PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_TYPE=11 ORDER BY PARAM_NAME");
        if (objdl.flaG==true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            lstCategory.DataSource = objdl.dataSet;
            lstCategory.DataTextField = "PARAM_NAME";
            lstCategory.DataValueField = "PARAM_ID";
            lstCategory.DataBind();

            lstCategory.SelectedValue = "115";
        }
        objdl = dA.returnList("SELECT SERVICE_ID, SERVICE_NAME FROM SERVICE_MST ORDER BY SERVICE_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            lstCostCat.DataSource = objdl.dataSet;
            lstCostCat.DataTextField = "SERVICE_NAME";
            lstCostCat.DataValueField = "SERVICE_ID";
            lstCostCat.DataBind();
        }
        objdl = dA.returnList("SELECT PARAM_ID, PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_TYPE=12 ORDER BY PARAM_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            lstForm.DataSource = objdl.dataSet;
            lstForm.DataTextField = "PARAM_NAME";
            lstForm.DataValueField = "PARAM_ID";
            lstForm.DataBind();
        }
    }
    protected void getInfo()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT * FROM MEDICINE_MST WHERE MED_ID='" + Request.QueryString["id"].ToString() + "'");
        if (objdl.flaG==true && objdl.dataSet.Tables[0].Rows.Count>0)
        {
            DataRow col = objdl.dataSet.Tables[0].Rows[0];

            txtCode.Text = col[1].ToString();
            if (col[20].ToString()!="0")
                lstCostCat.SelectedValue = col[20].ToString();
            txtDesc.Text = col[2].ToString();
            txtDescE.Text = col[17].ToString();
            lstCategory.SelectedValue = col[3].ToString();
            lstForm.SelectedValue = col[4].ToString();
            txtCostPrice.Text = col[5].ToString();
            //((decimal)col[2]).ToString("0.00");
            txtBUOM.Text = col[6].ToString();
            txtSellingIn.Text = col[7].ToString();
            txtSUOMI.Text = col[8].ToString();
            txtSellingOut.Text = col[15].ToString();
            txtSUOMO.Text = col[8].ToString();
            txtPacking.Text = col[9].ToString();
            txtPSUOM.Text = col[8].ToString();
            txtPBUOM.Text = col[6].ToString();
            txtUnitCost.Text = col[12].ToString();
            txtUSUOM.Text = col[8].ToString();
            txtCaution.Text = col[19].ToString();
            txtIMarkup.Text = col[13].ToString();
            txtOMarkup.Text = col[16].ToString();
            actFlag.SelectedValue = col[22].ToString();
            txtMinLevel.Text = col[10].ToString();
            txtSUOMM.Text = col[8].ToString();
            hdnID.Value = col[0].ToString();
        }
        else
        {
            lblError.Text = objdl.Msg;
            pnlError.Visible = true;
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
        List<string> gID = new List<string>() { refno, "MEDICINE_MST", "MED_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "MEDICINE_MST";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "MED_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "MED_ID", "MED_STOCK_CODE", "COST_CAT_ID", "MED_NAME", "MED_GENERIC_NAME", "MED_TYPE", "MED_FORM_TYPE", 
            "MED_COST_PRICE", "MED_BIG_UOM", "MED_PACKING", "MED_SMALL_UOM", "MED_UNIT_COST", "MED_SELLING_PRICE", "MED_MARK_UP", 
            "MED_OUT_SELLING_COST", "MED_OUT_MARK_UP", "MED_CAUTION",  "MED_USER_ID", "MED_FLAG", "MED_REORDER" };

        objD.CValue = new System.Collections.ArrayList();

        List<string> col = new List<string>()
        {
            refno,
            frmV.Form("ctl00$contentForm$txtCode"),
            frmV.Form("ctl00$contentForm$lstCostCat"),
            frmV.Form("ctl00$contentForm$txtDesc"),
            frmV.Form("ctl00$contentForm$txtDescE"),
            frmV.Form("ctl00$contentForm$lstCategory"),
            frmV.Form("ctl00$contentForm$lstForm"),
            frmV.Form("ctl00$contentForm$txtCostPrice"),
            frmV.Form("ctl00$contentForm$txtBUOM"),
            frmV.Form("ctl00$contentForm$txtPacking"),
            frmV.Form("ctl00$contentForm$txtPSUOM"),
            frmV.Form("ctl00$contentForm$txtUnitCost"),
            frmV.Form("ctl00$contentForm$txtSellingIn"),
            frmV.Form("ctl00$contentForm$txtIMarkup"),
            frmV.Form("ctl00$contentForm$txtSellingOut"),
            frmV.Form("ctl00$contentForm$txtOMarkup"),
            frmV.Form("ctl00$contentForm$txtCaution"),
            HttpContext.Current.Session["userid"].ToString(),
            frmV.Form("ctl00$contentForm$actFlag"),
            frmV.Form("ctl00$contentForm$txtMinLevel")
        };

        objD.CValue.Add(col);
        objdata.Add(objD);

        msg = dbaction.saveCollection(objdata, gID);
        return msg;
    }
}