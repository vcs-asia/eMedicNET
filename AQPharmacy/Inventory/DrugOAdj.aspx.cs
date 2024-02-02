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

public partial class Inventory_DrugOAdj : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getOutlets();
            if (Request.QueryString["n"].ToString() != "0")
            {
                loadAdjustment();
            }
            txtRefNo.Focus();
        }
        else
        {
            getbackDynamicControls();
        }
    }
    private void getOutlets()
    {
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_NAME, OUTLET_ID FROM OUTLET_MST ORDER BY OUTLET_NAME");

        lstOutlet.DataSource = objdl.dataSet.Tables[0];
        lstOutlet.DataTextField = "OUTLET_NAME";
        lstOutlet.DataValueField = "OUTLET_ID";
        lstOutlet.DataBind();
    }
    protected void fillDrugs(object sender, EventArgs e)
    {
        generateDynamicControls();
    }
    protected void loadAdjustment()
    {
        dbAction dO = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL tbDL = new objDL();

        tbDL = dO.returnList("SELECT ADJ_REF_NO, ADJ_REF_ID, ADJ_DATE, ADJ_POST_FLAG FROM OUTLET_STOCK_ADJUSTMENT_INFO WHERE ADJ_REF_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG == true && tbDL.dataSet.Tables[0].Rows.Count > 0)
        {
            txtRefNo.Text = tbDL.dataSet.Tables[0].Rows[0]["ADJ_REF_NO"].ToString();
            txtIssueDate.Text = ((DateTime)tbDL.dataSet.Tables[0].Rows[0]["ADJ_DATE"]).ToString("dd/MM/yyyy");
        }
        else
        {
            lblError.Text = tbDL.Msg;
            pnlError.Visible = true;
        }

        tbDL = dO.returnList("SELECT ADJ_MED_ID, ADJ_QTY, ADJ_PHY_QTY, ADJ_PACK, ADJ_EXP_DATE, MED_BIG_UOM, MED_SMALL_UOM, TRANS_ID, (SELECT SUM(CUR_BAL_STK) FROM STOCK_DTLD_INFO WHERE MED_ID=ADJ_MED_ID AND MED_PACK=ADJ_PACK) AS CURSTK, CurStock(ADJ_PHY_QTY, ADJ_PACK, MED_BIG_UOM, MED_SMALL_UOM) AS ADJQTY FROM OUTLET_STOCK_ADJUSTMENT_DTLS JOIN MEDICINE_MST ON MED_ID=ADJ_MED_ID WHERE STK_REF_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG == true)
        {
            string str = "";

            if (tbDL.flaG == true && tbDL.dataSet.Tables[0].Rows.Count > 0)
            {

                for (int row = 0; row < 50; row++)
                {
                    if (row < tbDL.dataSet.Tables[0].Rows.Count)
                    {
                        str += "$('#tblDrugs > tbody:last').append('<tr>";
                        str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(tbDL.dataSet.Tables[0].Rows[row]["ADJ_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["ADJ_PACK"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["CURSTK"].ToString()) + "</select>";
                        str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + getDrugQty(tbDL.dataSet.Tables[0].Rows[row]["ADJ_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["ADJ_PACK"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["ADJ_PACK"].ToString()) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)tbDL.dataSet.Tables[0].Rows[row]["ADJ_QTY"] / (int)tbDL.dataSet.Tables[0].Rows[row]["ADJ_PACK"]) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["MED_BIG_UOM"].ToString() + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)tbDL.dataSet.Tables[0].Rows[row]["ADJ_QTY"] % (int)tbDL.dataSet.Tables[0].Rows[row]["ADJ_PACK"]) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["MED_SMALL_UOM"].ToString() + "\" onblur=\"getVariance(" + row + ")\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["TRANS_ID"].ToString() + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"varq[]\" name=\"varq[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["ADJQTY"].ToString() + "\"/><input type=\"hidden\" id=\"vqty[]\" name=\"vqty[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["ADJ_PHY_QTY"].ToString() + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"expd[]\" name=\"expd[]\"  style=\"width:80%;text-align:right\" class=\"dt\" value=\"" + ((DateTime)tbDL.dataSet.Tables[0].Rows[row]["ADJ_EXP_DATE"]).ToString("dd/MM/yyyy") + "\"/></td>";
                        str += "</tr>');";
                    }
                    else
                    {
                        str += "$('#tblDrugs > tbody:last').append('<tr>";
                        str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select>";
                        str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:95%\" value=\"\"/></td>";
                        str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                        str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\" style=\"width:80%;\" value=\"\"/></td>";
                        str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\" onblur=\"getVariance(" + row + ")\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
                        str += "<td><input type=\"text\" id=\"varq[]\" name=\"varq[]\"  style=\"width:80%;text-align:right\" value=\"\"/><input type=\"hidden\" id=\"vqty[]\" name=\"vqty[]\"  style=\"width:80%;text-align:right\" value=\"\"/></td>";
                        str += "<td><input type=\"text\" id=\"expd[]\" name=\"expd[]\"  style=\"width:80%;text-align:right\" class=\"dt\" value=\"\"/></td>";
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
    public static string saveInfo(NameValue[] frmV)
    {
        string msg = "";
        bool saveFlag = false;
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        List<objData> objdata = new List<objData>();
        objData objD = new objData();

        string refno = (frmV.Form("ctl00$contentForm$txtRefNo") == "" || frmV.Form("ctl00$contentForm$txtRefNo") == "0") ? "0": frmV.Form("ctl00$contentForm$txtRefNo");
        List<string> gID = new List<string>() { refno, "OUTLET_STOCK_ADJUSTMENT_INFO", "ADJ_REF_ID", HttpContext.Current.Session["userid"].ToString()};

        objD.xTable = "OUTLET_STOCK_ADJUSTMENT_INFO";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "ADJ_REF_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "ADJ_REF_ID", "ADJ_DATE", "ADJ_OUTLET", "ADJ_POST_FLAG", "SA_USERID" };
        objD.CValue = new ArrayList();
        List<string> col = new List<string>()
        {
            refno,
            new vGeneral().convertDateForDB(frmV.Form("ctl00$contentForm$txtIssueDate")),
            frmV.Form("ctl00$contentForm$lstOutlet"),
            "0",
            HttpContext.Current.Session["userid"].ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        objD = new objData();

        objD.xTable = "OUTLET_STOCK_ADJUSTMENT_DTLS";
        objD.KeyCol = new List<string>() { "STK_REF_ID" };
        objD.KeyVal = new List<string>() { refno };

        var drug = frmV.FormMultiple("drug[]");
        var avlq = frmV.FormMultiple("avlq[]");
        var isbq = frmV.FormMultiple("isbq[]");
        var buom = frmV.FormMultiple("buom[]");
        var issq = frmV.FormMultiple("issq[]");
        var suom = frmV.FormMultiple("suom[]");
        var tran = frmV.FormMultiple("tran[]");
        var varq = frmV.FormMultiple("varq[]");
        var vqty = frmV.FormMultiple("vqty[]");
        var expd = frmV.FormMultiple("expd[]");

        objD.Column = new List<string>() { "STK_REF_ID", "TRANS_ID", "ADJ_MED_ID", "ADJ_QTY", "ADJ_PACK", "ADJ_PHY_QTY", "ADJ_EXP_DATE" };
        objD.CValue = new ArrayList();

        for (int i=0; i < drug.Count(); i++)
        {
            if (drug[i] != "")
            {
                int adjustQty = (int.Parse(isbq[i]) * int.Parse(drug[i].Split('.')[1]) + int.Parse(issq[i]));
                if (int.Parse(vqty[i]) != 0)
                {
                    col = new List<string>()
                    {
                        refno,
                        new vGeneral().getNumberV(tran[i]).ToString(),
                        drug[i].Split('.')[0],
                        adjustQty.ToString(),
                        drug[i].Split('.')[1],
                        vqty[i],
                        new vGeneral().convertDateForDB(expd[i])
                    };
                    objD.CValue.Add(col);
                    saveFlag = true;
                }
                else
                {
                    msg = "ERROR: The physical quantity cannot be blank or 0";
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
            msg = "ERROR: " + ex.Message;
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
            str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
            str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\" style=\"width:80%;\" value=\"\"/></td>";
            str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\" style=\"width:80%;text-align:right\" value=\"0\"/></td>";
            str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\" onblur=\"getVariance(" + row + ")\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
            str += "<td><input type=\"text\" id=\"varq[]\" name=\"varq[]\"  style=\"width:80%;text-align:right\" value=\"\"/><input type=\"hidden\" id=\"vqty[]\" name=\"vqty[]\"  style=\"width:80%;text-align:right\" value=\"\"/></td>";
            str += "<td><input type=\"text\" id=\"expd[]\" name=\"expd[]\"  style=\"width:80%;text-align:right\" class=\"dt\" value=\"\"/></td>";
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
            var varq = Request.Form.GetValues("varq[]");
            var vqty = Request.Form.GetValues("vqty[]");
            var expd = Request.Form.GetValues("expd[]");

            for (int row = 0; row < 50; row++)
            {
                if (row < drug.Count() && drug[row] != "")
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:98%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(drug[row]) + "</select>";
                    str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + avlq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\"  style=\"width:80%;text-align:right\" value=\"" + isbq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\"  style=\"width:80%;\" value=\"" + buom[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + issq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;\" value=\"" + suom[row] + "\" onblur=\"getVariance(" + row + ")\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tran[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"varq[]\" name=\"varq[]\"  style=\"width:80%;text-align:right\" value=\"" + varq[row] + "\"/><input type=\"hidden\" id=\"vqty[]\" name=\"vqty[]\"  style=\"width:80%;text-align:right\" value=\"" + vqty[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"expd[]\" name=\"expd[]\"  style=\"width:80%;text-align:right\" class=\"dt\" value=\"" + expd[row] + "\"/></td>";
                    str += "</tr>');";
                }
                else
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:98%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select>";
                    str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"\"/></td>";
                    str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\"  style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                    str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\"  style=\"width:80%;\" value=\"\"/></td>";
                    str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                    str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;\" value=\"\" onblur=\"getVariance(" + row + ")\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
                    str += "<td><input type=\"text\" id=\"varq[]\" name=\"varq[]\"  style=\"width:80%;text-align:right\" value=\"\"/><input type=\"hidden\" id=\"vqty[]\" name=\"vqty[]\"  style=\"width:80%;text-align:right\" value=\"\"/></td>";
                    str += "<td><input type=\"text\" id=\"expd[]\" name=\"expd[]\"  style=\"width:80%;text-align:right\" class=\"dt\" value=\"\"/></td>";
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

        objdl = dA.returnList("SELECT OUTLET_MED_ID, 1 AS MED_PACK, SUM(OUTLET_QTY) AS CSTK, curStock(SUM(OUTLET_QTY), 1, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = '" + lstOutlet.SelectedValue + "' GROUP BY OUTLET_MED_ID ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                str += "<option value=\"" + Row["OUTLET_MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "." + Row["CSTK"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
            }
        }
        return str;
    }
    protected string getAllDrugs(string medID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT OUTLET_MED_ID, MED_PACK, SUM(OUTLET_QTY) AS CSTK, curStock(SUM(OUTLET_QTY), OUTLET_STOCK_INFO.MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_ID = '" + lstOutlet.SelectedValue + "' GROUP BY OUTLET_MED_ID, MED_PACK ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];

                if (Row["OUTLET_MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "." + Row["CSTK"].ToString() == medID)
                    str += "<option value=\"" + Row["OUTLET_MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "." + Row["CSTK"].ToString() + "\" selected>" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
                else
                    str += "<option value=\"" + Row["OUTLET_MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "." + Row["CSTK"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
            }
        }
        return str;
    }
    private string getDrugQty(string medID)
    {
        string rVal = "";
        string sQuery = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID != "")
        {
            sQuery = "SELECT  SUM(OUTLET_QTY) AS QTY FROM OUTLET_STOCK_INFO WHERE OUTLET_MED_ID = '" + medID.Split('.')[0] + "' AND MED_PACK = '" + medID.Split('.')[1] + "' AND OUTLET_ID = '" + lstOutlet.SelectedValue + "' GROUP BY OUTLET_MED_ID, MED_PACK";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true)
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
            sQuery = "SELECT  SUM(OUTLET_QTY) AS QTY, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=OUTLET_MED_ID) AS SUOM FROM OUTLET_STOCK_INFO WHERE OUTLET_MED_ID = '" + medID.Split('.')[0] + "' AND OUTLET_ID = '" + outletID + "' GROUP BY OUTLET_MED_ID";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0]["QTY"].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0]["BUOM"].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0]["SUOM"].ToString();
            }
        }

        return rVal;
    }
}