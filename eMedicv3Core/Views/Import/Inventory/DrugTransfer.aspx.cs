using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data;

public partial class Inventory_DrugTransfer : System.Web.UI.Page
{
    protected void btnOK(object sender, EventArgs e)
    {
        generateDynamicControls();
    }
    protected void btnReset(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["n"].ToString()!="0")
            {
                loadTransfer();
            }
            else
            {
                //generateDynamicControls();
            }
            getOutlets();
            txtRefNo.Focus();
        }
        else
        {
            getbackDynamicControls();
        }
    }

    protected void getOutlets()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_ID, OUTLET_NAME FROM OUTLET_MST ORDER BY OUTLET_NAME");
        if (objdl.flaG == true)
        {
            txtOutlet.DataSource = objdl.dataSet;
            txtOutlet.DataTextField = "OUTLET_NAME";
            txtOutlet.DataValueField = "OUTLET_ID";
            txtOutlet.DataBind();

            lstOutlet.DataSource = objdl.dataSet;
            lstOutlet.DataTextField = "OUTLET_NAME";
            lstOutlet.DataValueField = "OUTLET_ID";
            lstOutlet.DataBind();
        }
    }
    protected void loadTransfer()
    {
        string outletID = "";

        dbAction dO = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL tbDL = new objDL();

        tbDL = dO.returnList("SELECT TRAN_REF_NO, TRAN_REF_ID, TRAN_DATE, TRAN_OUT_OUTLET, TRAN_IN_OUTLET, TRAN_POST_FLAG FROM STOCK_TRANSFER_INFO WHERE TRAN_REF_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG == true && tbDL.dataSet.Tables[0].Rows.Count > 0)
        {
            txtRefNo.Text = tbDL.dataSet.Tables[0].Rows[0]["TRAN_REF_NO"].ToString();
            txtIssueDate.Text = ((DateTime)tbDL.dataSet.Tables[0].Rows[0]["TRAN_DATE"]).ToString("dd/MM/yyyy");
            txtOutlet.SelectedValue = tbDL.dataSet.Tables[0].Rows[0]["TRAN_OUT_OUTLET"].ToString();
            lstOutlet.SelectedValue = tbDL.dataSet.Tables[0].Rows[0]["TRAN_IN_OUTLET"].ToString();
            outletID = tbDL.dataSet.Tables[0].Rows[0]["TRAN_OUT_OUTLET"].ToString();
            hdnFlag.Value = tbDL.dataSet.Tables[0].Rows[0]["TRAN_POST_FLAG"].ToString();
        }
        tbDL = dO.returnList("SELECT TRAN_MED_ID, TRAN_QTY, TRAN_PACK, MED_BIG_UOM, MED_SMALL_UOM, TRANS_ID FROM STOCK_TRANSFER_DTLS JOIN MEDICINE_MST ON MED_ID=TRAN_MED_ID WHERE STK_REF_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG==true)
        {
            string str = "";

            if (tbDL.flaG == true && tbDL.dataSet.Tables[0].Rows.Count > 0)
            {

                for (int row = 0; row < 50; row++)
                {
                    if (row < tbDL.dataSet.Tables[0].Rows.Count)
                    {
                        str += "$('#tblDrugs > tbody:last').append('<tr>";
                        str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(tbDL.dataSet.Tables[0].Rows[row]["TRAN_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["TRAN_PACK"].ToString(), outletID) + "</select>";
                        str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + getDrugQty(tbDL.dataSet.Tables[0].Rows[row]["TRAN_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["TRAN_PACK"].ToString(), outletID) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)tbDL.dataSet.Tables[0].Rows[row]["TRAN_QTY"] % (int)tbDL.dataSet.Tables[0].Rows[row]["TRAN_PACK"]) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["MED_SMALL_UOM"].ToString() + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["TRANS_ID"].ToString() + "\"/></td>";
                        str += "</tr>');";
                    }
                    else
                    {
                        str += "$('#tblDrugs > tbody:last').append('<tr>";
                        str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select>";
                        str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:95%\" value=\"\"/></td>";
                        str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"0\"/></td>";
                        str += "</tr>');";
                    }
                }
                Page page = HttpContext.Current.CurrentHandler as Page;
                page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
            }
        }
        else
        {
            lblError.Text = tbDL.Msg;
            pnlError.Visible = true;
        }
    }
    [System.Web.Services.WebMethod]
    public static string saveInfo(NameValue[] frmValues)
    {
        string msg = "";
        bool saveFlag = false;
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string refno = (frmValues.Form("ctl00$contentForm$txtRefNo") == "" || frmValues.Form("ctl00$contentForm$txtRefNo") == "0") ? "0" : frmValues.Form("ctl00$contentForm$txtRefNo");
        List<string> gID = new List<string>() { refno, "STOCK_TRANSFER_INFO", "TRAN_REF_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "STOCK_TRANSFER_INFO";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "TRAN_REF_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "TRAN_REF_ID", "TRAN_REF_NO", "TRAN_DATE", "TRAN_OUT_OUTLET", "TRAN_IN_OUTLET", "TRAN_POST_FLAG", "ST_USERID" };
        objD.CValue = new ArrayList();
        List<string> col = new List<string>() 
        { 
            refno,
            frmValues.Form("ctl00$contentForm$txtRefNo"),
            new vGeneral().convertDateForDB(frmValues.Form("ctl00$contentForm$txtIssueDate")),
            frmValues.Form("ctl00$contentForm$txtOutlet"),
            frmValues.Form("ctl00$contentForm$lstOutlet"),
            frmValues.Form("ctl00$contentForm$hdnFlag"),
            HttpContext.Current.Session["userid"].ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        objD = new objData();

        objD.xTable = "STOCK_TRANSFER_DTLS";
        objD.Delete = true;

        objD.KeyCol = new List<string>() { "STK_REF_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "STK_REF_ID", "TRANS_ID", "TRAN_MED_ID", "TRAN_QTY", "TRAN_PACK", "DP_FLAG" };

        var drug = frmValues.FormMultiple("drug[]");
        var avlq = frmValues.FormMultiple("avlq[]");
        var issq = frmValues.FormMultiple("issq[]");
        var suom = frmValues.FormMultiple("suom[]");
        var tran = frmValues.FormMultiple("tran[]");

        objD.CValue = new ArrayList();
        
        for (int i = 0; i < drug.Count(); i++)
        {
            if (drug[i] != "")
            {
                int tranQty = int.Parse(issq[i]);
                if (tranQty != 0 && tranQty <= int.Parse(avlq[i]))
                {
                    col = new List<string>()
                    {
                        refno,
                        tran[i],
                        drug[i].Split('.')[0],
                        tranQty.ToString(),
                        "0",
                        "0"
                    };
                    objD.CValue.Add(col);
                    saveFlag = true;
                }
                else
                {
                    msg = "ERROR: The transfer quantity cannot be more than available quantity or blank or 0.";
                    saveFlag = false;
                }
            }
        }
        objdata.Add(objD);
        try
        {
            if (saveFlag == true)
            {
                msg = dbaction.saveCollection(objdata, gID);
            }
        }
        catch (Exception ex)
        {
            msg  = ex.Message;
        }
        return msg;
    }
    //-----------
    protected void generateDynamicControls()
    {
        string str = "";
        for (int row = 0; row < 50; row++)
        {
            str += "$('#tblDrugs > tbody:last').append('<tr>";
            str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select>";
            str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:95%\" value=\"\"/></td>";
            str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
            str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"0\"/></td>";
            str += "</tr>');";
        }

        Page page = HttpContext.Current.CurrentHandler as Page;
        page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
    }
    protected void getbackDynamicControls()
    {
        if (Request.Form.Count > 0)
        {
            if (Request.Form.GetValues("drug[]") == null) return;

            string str = "";

            var drug = Request.Form.GetValues("drug[]");
            var avlq = Request.Form.GetValues("avlq[]");
            var isbq = Request.Form.GetValues("isbq[]");
            var buom = Request.Form.GetValues("buom[]");
            var issq = Request.Form.GetValues("issq[]");
            var suom = Request.Form.GetValues("suom[]");
            var tran = Request.Form.GetValues("tran[]");

            for (int row = 0; row < 50; row++)
            {
                if (row < drug.Count() && drug[row]!="")
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:98%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(drug[row], txtOutlet.SelectedValue) + "</select>";
                    str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + avlq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + issq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;\" value=\"" + suom[row] + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tran[row] + "\"/></td>";
                    str += "</tr>');";
                }
                else
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:98%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select>";
                    str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"\"/></td>";
                    str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                    str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
                    str += "</tr>');";
                }
            }
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
        }
    }
    protected string getAllDrugs()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        //objdl = dA.returnList("SELECT OUTLET_MED_ID, MED_PACK, curStock(OUTLET_QTY, OUTLET_STOCK_INFO.MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = " + txtOutlet.SelectedValue + " AND MED_PACK IS NOT NULL GROUP BY OUTLET_MED_ID, MED_PACK HAVING SUM(OUTLET_QTY) > 0 ORDER BY MNAME");
        objdl = dA.returnList("SELECT OUTLET_MED_ID, OUTLET_QTY AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS MNAME, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = " + txtOutlet.SelectedValue + " AND OUTLET_QTY > 0");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                str += "<option value=\"" + Row["OUTLET_MED_ID"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["SUOM"].ToString() + "]" + "</option>";
            }
        }
        return str;
    }
    protected string getAllDrugs(string medID, string outletID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        //objdl = dA.returnList("SELECT MED_ID, MED_PACK, curStock(SUM(CUR_BAL_STK), OUTLET_STOCK_INFO.MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = '" + outletID + "' GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) > 0 ORDER BY MNAME");
        objdl = dA.returnList("SELECT MED_ID, OUTLET_QTY AS QTY, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = '" + outletID + "' AND OUTLET_QTY > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                                
                if (Row["MED_ID"].ToString() == medID)
                    str += "<option value=\"" + Row["MED_ID"].ToString() + "\" selected>" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["SUOM"].ToString() + "]" + "</option>";
                else
                    str += "<option value=\"" + Row["MED_ID"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["SUOM"].ToString() + "]" + "</option>";
            }
        }
        return str;
    }
    private string getDrugQty(string medID, string outletID)
    {
        string rVal = "";
        string sQuery = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID != "")
        {
            sQuery = "SELECT  SUM(CUR_BAL_STK) AS QTY FROM OUTLET_STOCK_INFO WHERE MED_ID = '" + medID.Split('.')[0] + "' AND MED_PACK = '" + medID.Split('.')[1] + "' AND OUTLET_ID = '" + outletID + "' GROUP BY MED_ID, MED_PACK";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0]["QTY"].ToString();
            }
        }

        return rVal;
    }
    [System.Web.Services.WebMethod]
    public static string getDrugDetails(string medID, string outletID)
    {
        string rVal = "";
        string sQuery = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID != "")
        {
            sQuery = "SELECT  OUTLET_QTY AS QTY, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_STOCK_INFO.OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_MED_ID = '" + medID.Split('.')[0] + "' AND OUTLET_ID = '" + outletID + "'";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0]["QTY"].ToString() + "-" + objdl.dataSet.Tables[0].Rows[0]["SUOM"].ToString() ;
            }
        }

        return rVal;
    }
}