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

public partial class Inventory_DrugsIssue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["n"].ToString()!="0")
            {
                loadIssue();
            }
            else
            {
                generateDynamicControls();
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
        }
    }
    protected void loadIssue()
    {
        dbAction dO = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL tbDL = new objDL();

        tbDL = dO.returnList("SELECT ISSUE_REF_NO, ISSUE_REF_ID, ISSUE_DATE, ISSUE_OUTLET, ISSUE_POST_FLAG FROM STOCK_ISSUE_INFO WHERE ISSUE_REF_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG == true && tbDL.dataSet.Tables[0].Rows.Count > 0)
        {
            txtRefNo.Text = tbDL.dataSet.Tables[0].Rows[0]["ISSUE_REF_NO"].ToString();
            txtIssueDate.Text = ((DateTime)tbDL.dataSet.Tables[0].Rows[0]["ISSUE_DATE"]).ToString("dd/MM/yyyy");
            txtOutlet.SelectedValue = tbDL.dataSet.Tables[0].Rows[0]["ISSUE_OUTLET"].ToString();
        }
        else
        {
            lblError.Text = tbDL.Msg;
            pnlError.Visible = true;
        }

        tbDL = dO.returnList("SELECT ISSUE_MED_ID, ISSUE_QTY, ISSUE_PACK, MED_BIG_UOM, MED_SMALL_UOM, TRANS_ID FROM STOCK_ISSUE_DTLS JOIN MEDICINE_MST ON MED_ID=ISSUE_MED_ID WHERE STK_REF_ID='" + Request.QueryString[0].ToString() + "'");
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
                        str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:95%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(tbDL.dataSet.Tables[0].Rows[row]["ISSUE_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["ISSUE_PACK"].ToString()) + "</select>";
                        str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + getDrugQty(tbDL.dataSet.Tables[0].Rows[row]["ISSUE_MED_ID"].ToString() + "." + tbDL.dataSet.Tables[0].Rows[row]["ISSUE_PACK"].ToString()) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)tbDL.dataSet.Tables[0].Rows[row]["ISSUE_QTY"] / (int)tbDL.dataSet.Tables[0].Rows[row]["ISSUE_PACK"]) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["MED_BIG_UOM"].ToString() + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)tbDL.dataSet.Tables[0].Rows[row]["ISSUE_QTY"] % (int)tbDL.dataSet.Tables[0].Rows[row]["ISSUE_PACK"]) + "\"/></td>";
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;text-align:right\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["MED_SMALL_UOM"].ToString() + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tbDL.dataSet.Tables[0].Rows[row]["TRANS_ID"].ToString() + "\"/></td>";
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
                        str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
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
    public static string saveIssue(NameValue[] frmV)
    {
        string msg = "";
        bool saveFlag = true;
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        string refno = (frmV.Form("ctl00$contentForm$txtRefNo")=="" || frmV.Form("ctl00$contentForm$txtRefNo") == "0") ? "0" : frmV.Form("ctl00$contentForm$txtRefNo");
        List<string> gID = new List<string>() { refno, "STOCK_ISSUE_INFO", "ISSUE_REF_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "STOCK_ISSUE_INFO";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "ISSUE_REF_ID"};
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "ISSUE_REF_ID", "ISSUE_DATE", "ISSUE_OUTLET", "ISSUE_POST_FLAG", "SI_USERID" };
        objD.CValue = new ArrayList();

        List<string> col = new List<string>()
        {
            refno,
            new vGeneral().convertDateForDB(frmV.Form("ctl00$contentForm$txtIssueDate")),
            frmV.Form("ctl00$contentForm$txtOutlet"),
            new vGeneral().getNumberV(frmV.Form("ctl00$contentForm$hdnPFlag")).ToString(),
            HttpContext.Current.Session["userid"].ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        objD = new objData();
        objD.xTable = "STOCK_ISSUE_DTLS";
        objD.Delete = true;

        objD.KeyCol = new List<string>() { "STK_REF_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "STK_REF_ID", "TRANS_ID", "ISSUE_MED_ID", "ISSUE_QTY", "ISSUE_PACK" };
        objD.CValue = new ArrayList();

        var drug = frmV.FormMultiple("drug[]");
        var avlq = frmV.FormMultiple("avlq[]");
        var isbq = frmV.FormMultiple("isbq[]");
        var buom = frmV.FormMultiple("buom[]");
        var issq = frmV.FormMultiple("issq[]");
        var suom = frmV.FormMultiple("suom[]");
        var tran = frmV.FormMultiple("tran[]");

        for (int i = 0; i < drug.Count(); i++ )
        {
            if (drug[i]!="")
            {
                int issueQty = (int.Parse(isbq[i]) * int.Parse(drug[i].Split('.')[1])) + int.Parse(issq[i]);
                if (issueQty != 0 && issueQty <= int.Parse(avlq[i]))
                {
                    col = new List<string>()
                    {
                        refno,
                        new vGeneral().getNumberV(tran[i]).ToString(),
                        drug[i].Split('.')[0],
                        issueQty.ToString(),
                        drug[i].Split('.')[1],
                    };
                    objD.CValue.Add(col);
                }
                else
                {
                    msg = "ERROR: Some items issue quantity more than available quantity. Please check.";
                    saveFlag = false;
                }
            }
        }
        objdata.Add(objD);

        try
        {
            if (saveFlag==true)
            {
                msg = dbaction.saveCollection(objdata, gID);
            }
        }
        catch (Exception ex)
        {
            msg = ex.ToString();
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
            str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" style=\"width:80%;\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td>";
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
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:98%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(drug[row]) + "</select>";
                    str += "<input type=\"hidden\" id=\"avlq[]\" name=\"avlq[]\" style=\"width:98%\" value=\"" + avlq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"isbq[]\" name=\"isbq[]\"  style=\"width:80%;text-align:right\" value=\"" + isbq[row] +  "\"/></td>";
                    str += "<td><input type=\"text\" id=\"buom[]\" name=\"buom[]\"  style=\"width:80%;\" value=\"" + buom[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"issq[]\" name=\"issq[]\"  style=\"width:80%;text-align:right\" value=\"" + issq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\"  style=\"width:80%;\" value=\"" + suom[row] + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tran[row] + "\"/></td>";
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

        objdl = dA.returnList("SELECT MED_ID, MED_PACK, curStock(SUM(CUR_BAL_STK), MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS SUOM FROM STOCK_DTLD_INFO GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                str += "<option value=\"" + Row["MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
            }
        }
        return str;
    }
    protected string getAllDrugs(string medID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT MED_ID, MED_PACK, curStock(SUM(CUR_BAL_STK), MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID)) AS QTY, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS MNAME, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS SUOM FROM STOCK_DTLD_INFO GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                                
                if (Row["MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() == medID)
                    str += "<option value=\"" + Row["MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "\" selected>" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
                else
                    str += "<option value=\"" + Row["MED_ID"].ToString() + "." + Row["MED_PACK"].ToString() + "\">" + Regex.Replace(Row["MNAME"].ToString(), @"[^0-9a-zA-Z]+", " ") + " " + Row["QTY"].ToString() + "[" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "]" + "</option>";
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
            sQuery = "SELECT  SUM(CUR_BAL_STK) AS QTY FROM STOCK_DTLD_INFO WHERE MED_ID = '" + medID.Split('.')[0] + "' AND MED_PACK = '" + medID.Split('.')[1] + "' GROUP BY MED_ID, MED_PACK";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0]["QTY"].ToString();
            }
        }

        return rVal;
    }
    [System.Web.Services.WebMethod]
    public static string getDrugDetails(string medID)
    {
        string rVal = "";
        string sQuery = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID != "")
        {
            sQuery = "SELECT  SUM(CUR_BAL_STK) AS QTY, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS SUOM FROM STOCK_DTLD_INFO WHERE MED_ID = '" + medID.Split('.')[0] + "' AND MED_PACK = '" + medID.Split('.')[1] + "' GROUP BY MED_ID, MED_PACK";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0]["QTY"].ToString() + "-" + objdl.dataSet.Tables[0].Rows[0]["BUOM"].ToString() + "-" + objdl.dataSet.Tables[0].Rows[0]["SUOM"].ToString() ;
            }
        }

        return rVal;
    }
}