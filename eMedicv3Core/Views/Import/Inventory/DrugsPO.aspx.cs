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

public partial class Inventory_DrugsPO: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getAllDrugsOfPO();
            generateDynamicControls();
            getSuppliers();
            txtPONo.Focus();
        }
        else
        {
            getbackDynamicControls();
        }
    }

    protected void getSuppliers()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER_MST ORDER BY SUPPLIER_NAME");
        if (objdl.flaG==true)
        {
            txtSupplier.DataSource = objdl.dataSet;
            txtSupplier.DataTextField = "SUPPLIER_NAME";
            txtSupplier.DataValueField = "SUPPLIER_ID";
            txtSupplier.DataBind();
        }
    }
    protected void getAllDrugsOfPO()
    {
        dbAction dO = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL tbDL = new objDL();

        tbDL = dO.returnList("SELECT PO_SUPPLIER_ID, PO_DATE, PO_TERMS, PO_AMT, PO_REMARKS, PO_AMT, POST_FLAG FROM PURCHASE_ORDER_INFO WHERE PO_ID='" + Request.QueryString[0].ToString() + "'");
        if (tbDL.flaG==true && tbDL.dataSet.Tables[0].Rows.Count > 0)
        {
            txtSupplier.SelectedValue = tbDL.dataSet.Tables[0].Rows[0]["PO_SUPPLIER_ID"].ToString();
            txtOrderDate.Text = ((DateTime)tbDL.dataSet.Tables[0].Rows[0]["PO_DATE"]).ToString("dd/MM/yyyy");
            txtTerms.Text = tbDL.dataSet.Tables[0].Rows[0]["PO_TERMS"].ToString();
            txtRemarks.Text = tbDL.dataSet.Tables[0].Rows[0]["PO_REMARKS"].ToString();
            txtAmount.Text = ((decimal)tbDL.dataSet.Tables[0].Rows[0]["PO_AMT"]).ToString("0.00");
            hdnPF.Value = tbDL.dataSet.Tables[0].Rows[0]["POST_FLAG"].ToString();
        }
        
        if (Request.QueryString.Count > 0)
        {
            txtPONo.Text = Request.QueryString[0].ToString();

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT TRANS_ID, PO_ID, PO_MED_ID, MED_ORD_QTY, PO_MED_COST, MED_AMT, MED_DESC, PO_MED_PACK, MED_DESC_PRINT_FLAG, MED_NAME, MED_BIG_UOM, MED_SMALL_UOM, MED_REC_QTY FROM PURCHASE_ORDER_DTLS JOIN MEDICINE_MST ON PO_MED_ID=MED_ID WHERE PO_ID='" + Request.QueryString[0].ToString() + "' ORDER BY TRANS_ID");
            if (objdl.flaG == false)
            {
                lblError.Text = objdl.Msg;
                pnlError.Visible = true;
            }
            else
            {
                string str = "";
                if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int row = 0; row < 100; row++)
                    {
                        if (row < objdl.dataSet.Tables[0].Rows.Count)
                        {
                            str += "$('#tblDrugs > tbody:last').append('<tr>";
                            str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(objdl.dataSet.Tables[0].Rows[row]["PO_MED_ID"].ToString()) + "</select></td>";
                            str += "<td><textarea id=\"ddsc[]\" name=\"ddsc[]\" style=\"width:90%\" Rows=\"2\">" + objdl.dataSet.Tables[0].Rows[row]["MED_DESC"].ToString() + "</textarea></td>";
                            str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" style=\"width:30%\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["PO_MED_PACK"].ToString() + "\"/><input type=\"text\" id=\"uom[]\" name=\"uom[]\" style=\"width:50%\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["MED_SMALL_UOM"].ToString() + "/" + objdl.dataSet.Tables[0].Rows[row]["MED_BIG_UOM"].ToString() + "\"/></td>";
                            str += "<td><input type=\"text\" id=\"ordQ[]\" name=\"ordQ[]\"  style=\"width:80%;text-align:right\" value=\"" + ((int)objdl.dataSet.Tables[0].Rows[row]["MED_ORD_QTY"] /(int)objdl.dataSet.Tables[0].Rows[row]["PO_MED_PACK"]) + "\" /></td>";
                            str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" style=\"width:80%;text-align:right\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["PO_MED_COST"].ToString() + "\" onblur=\"calculateAmount()\"/></td>";
                            str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:80%;text-align:right\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["MED_AMT"].ToString() + "\"/><input type=\"hidden\" id=\"recq[]\" name=\"recq[]\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["MED_REC_QTY"].ToString() + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + objdl.dataSet.Tables[0].Rows[row]["TRANS_ID"].ToString() + "\"/><input type=\"hidden\" id=\"pord[]\" name=\"pord[]\" value=\"" + ((int)objdl.dataSet.Tables[0].Rows[row]["MED_ORD_QTY"] / (int)objdl.dataSet.Tables[0].Rows[row]["PO_MED_PACK"]) + "\"/></td>";
                            if (objdl.dataSet.Tables[0].Rows[row]["MED_DESC_PRINT_FLAG"].ToString() == "1")
                                str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\">N</option><option value=\"1\" selected>Y</option></select></tr>');";
                            else
                                str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\" selected>N</option><option value=\"1\">Y</option></select></tr>');";
                        }
                        else
                        {
                            str += "$('#tblDrugs > tbody:last').append('<tr>";
                            str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select></td>";
                            str += "<td><textarea id=\"ddsc[]\" name=\"ddsc[]\" style=\"width:90%\" Rows=\"2\"></textarea></td>";
                            str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" style=\"width:30%\" value=\"\"/><input type=\"text\" id=\"uom[]\" name=\"uom[]\" style=\"width:50%\" value=\"\"/></td>";
                            str += "<td><input type=\"text\" id=\"ordQ[]\" name=\"ordQ[]\"  style=\"width:80%;text-align:right\" value=\"\"/></td>";
                            str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" style=\"width:80%;text-align:right\" value=\"\" onblur=\"calculateAmount()\"/></td>";
                            str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:80%;text-align:right\" value=\"\"/><input type=\"hidden\" id=\"recq[]\" name=\"recq[]\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/><input type=\"hidden\" id=\"pord[]\" name=\"pord[]\" value=\"\"/></td>";
                            str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\" selected>N</option><option value=\"1\">Y</option></select></tr>');";
                        }
                    }
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
                }
            }

        }
    }
    [System.Web.Services.WebMethod]
    public static string saveInfo(NameValue[] formVars)
    {
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        string refno = formVars.Form("ctl00$contentForm$txtPONo");
        List<string> gID = new List<string>() { refno, "PURCHASE_ORDER_INFO", "PO_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "PURCHASE_ORDER_INFO";
        
        objD.Delete = false;
        objD.KeyCol = new List<string>() { "PO_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "PO_ID", "PO_SUPPLIER_ID", "PO_DATE", "PO_TERMS", "PO_AMT", "PO_REMARKS", "PO_USERID" };
        objD.CValue = new ArrayList();

        List<string> col = new List<string>()
        {
            refno,
            formVars.Form("ctl00$contentForm$txtSupplier"),
            new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtOrderDate")),
            formVars.Form("ctl00$contentForm$txtTerms"),
            formVars.Form("ctl00$contentForm$txtAmount"),
            formVars.Form("ctl00$contentForm$txtRemarks"),
            HttpContext.Current.Session["userid"].ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);
        
        objD = new objData();
        objD.xTable = "PURCHASE_ORDER_DTLS";
        objD.Delete = true;

        objD.KeyCol = new List<string>() { "PO_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "PO_ID", "PO_MED_ID", "MED_ORD_QTY", "PO_MED_COST", "MED_AMT", "PO_MED_PACK", "MED_DESC", "MED_REC_QTY", "MED_DESC_PRINT_FLAG", "TRANS_ID" };
        objD.CValue = new ArrayList();

        var drug = formVars.FormMultiple("drug[]");
        var ordq = formVars.FormMultiple("ordQ[]");
        var cost = formVars.FormMultiple("cost[]");
        var damt = formVars.FormMultiple("amt[]");
        var pack = formVars.FormMultiple("pack[]");
        var ddsc = formVars.FormMultiple("ddsc[]");
        var recq = formVars.FormMultiple("recq[]");
        var pflg = formVars.FormMultiple("pflg[]");
        var tran = formVars.FormMultiple("tran[]");

        for (int inc = 0; inc < drug.Count(); inc++ )
        {
            if (drug[inc]!="")
            {
                col = new List<string>()
                {
                    refno,
                    drug[inc],
                    (new vGeneral().getNumberV(ordq[inc]) * new vGeneral().getNumberV(pack[inc])).ToString(),
                    cost[inc],
                    damt[inc],
                    pack[inc],
                    ddsc[inc],
                    new vGeneral().getNumberV(recq[inc]).ToString(),
                    pflg[inc],
                    new vGeneral().getNumberV(tran[inc]).ToString()
                };
                objD.CValue.Add(col);
            }
        }
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
    //-----------
    protected void generateDynamicControls()
    {
        string str = "";
        for (int row = 0; row < 100; row++)
        {
            str += "$('#tblDrugs > tbody:last').append('<tr>";
            str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select></td>";
            str += "<td><textarea id=\"ddsc[]\" name=\"ddsc[]\" style=\"width:90%\" Rows=\"2\"></textarea></td>";
            str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" style=\"width:30%\" value=\"0\"/><input type=\"text\" id=\"uom[]\" name=\"uom[]\" style=\"width:50%\"/></td>";
            str += "<td><input type=\"text\" id=\"ordQ[]\" name=\"ordQ[]\"  style=\"width:80%;text-align:right\" value=\"0\"/></td>";
            str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" style=\"width:80%;text-align:right\" value=\"0\" onblur=\"calculateAmount()\"/></td>";
            str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:80%;text-align:right\" value=\"0\"/><input type=\"hidden\" id=\"recq[]\" name=\"recq[]\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/><input type=\"hidden\" id=\"pord[]\" name=\"pord[]\" value=\"\"/></td>";
            str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\">N</option><option value=\"1\">Y</option></select></tr>');";
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
            var ddsc = Request.Form.GetValues("ddsc[]");
            var pack = Request.Form.GetValues("pack[]");
            var ordq = Request.Form.GetValues("ordq[]");
            var cost = Request.Form.GetValues("cost[]");
            var damt = Request.Form.GetValues("amt[]");
            var pflg = Request.Form.GetValues("pflg[]");
            var recq = Request.Form.GetValues("recq[]");
            var tran = Request.Form.GetValues("tran[]");
            var pord = Request.Form.GetValues("pord[]");

            for (int row = 0; row < 100; row++)
            {
                if (row < drug.Count())
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(drug[row]) + "</select></td>";
                    str += "<td><textarea id=\"ddsc[]\" name=\"ddsc[]\" style=\"width:90%\" Rows=\"2\">" + ddsc[row] + "</textarea></td>";
                    str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" style=\"width:30%\" value=\"" + pack[row] + "\"/><input type=\"text\" id=\"uom[]\" name=\"uom[]\" style=\"width:50%\"/></td>";
                    str += "<td><input type=\"text\" id=\"ordQ[]\" name=\"ordQ[]\"  style=\"width:80%;text-align:right\" value=\"" + ordq[row] + "\"/></td>";
                    str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" style=\"width:80%;text-align:right\" value=\"" + cost[row] + "\" onblur=\"calculateAmount()\"/></td>";
                    str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:80%;text-align:right\" value=\"" + damt[row] + "\"/><input type=\"hidden\" id=\"recq[]\" name=\"recq[]\" value=\"" + recq[row] + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tran[row] + "\"/><input type=\"hidden\" id=\"pord[]\" name=\"pord[]\" value=\"" + pord[row] + "\"/></td>";
                    if (pflg[row]=="0")
                        str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\" selected>N</option><option value=\"1\">Y</option></select></tr>');";
                    else
                        str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\">N</option><option value=\"1\" selected>Y</option></select></tr>');";
                }
                else
                {
                    str += "$('#tblDrugs > tbody:last').append('<tr>";
                    str += "<td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select></td>";
                    str += "<td><textarea id=\"ddsc[]\" name=\"ddsc[]\" style=\"width:90%\" Rows=\"2\"></textarea></td>";
                    str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" style=\"width:30%\" value=\"0\"/><input type=\"text\" id=\"uom[]\" name=\"uom[]\" style=\"width:50%\"/></td>";
                    str += "<td><input type=\"text\" id=\"ordQ[]\" name=\"ordQ[]\"  style=\"width:80%;text-align:right\" value=\"0\"/></td>";
                    str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" style=\"width:80%;text-align:right\" value=\"0\" onblur=\"calculateAmount()\"/></td>";
                    str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:80%;text-align:right\" value=\"0\"/><input type=\"hidden\" id=\"recq[]\" name=\"recq[]\" value=\"\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/><input type=\"hidden\" id=\"pord[]\" name=\"pord[]\" value=\"\"/></td>";
                    str += "<td><select id=\"pflg[]\" name=\"pflg[]\" style=\"width:90%\"><option value=\"0\">N</option><option value=\"1\">Y</option></select></tr>');";
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

        objdl = dA.returnList("SELECT MED_NAME, MED_ID FROM MEDICINE_MST WHERE MED_FLAG = 1 ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
        }
        return str;
    }
    protected string getAllDrugs(string medID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT MED_NAME, MED_ID FROM MEDICINE_MST WHERE MED_FLAG = 1 ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                if (objdl.dataSet.Tables[0].Rows[row][1].ToString() == medID)
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\" selected>" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
                else
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";

            }
        }
        return str;
    }
    [System.Web.Services.WebMethod]
    public static string getDrugDetails(string medID)
    {
        string rVal = "";
        string sQuery = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID!="")
        {
            sQuery = "SELECT MED_PACKING, MED_COST_PRICE, MED_BIG_UOM, MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID = '" + medID + "'";
            objdl = dA.returnList(sQuery);

            if (objdl.flaG == true)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0][0].ToString().Split('.')[0] + "|" + objdl.dataSet.Tables[0].Rows[0][1].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][3].ToString();
            }
        }

        return rVal;
    }
}