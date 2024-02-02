using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Vijay;
using System.Data;

public partial class Inventory_DrugsGRN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pnlError.Visible = false;
        pnlSuccess.Visible = false;
        
        if (!IsPostBack)
        {

            hdnGRNID.Value = Request.QueryString["n"].ToString();
            hdnFlag.Value = "0";
            if (Request.QueryString["n"].ToString() != "0")
            {
                loadGRN();
            }
            else
            {
                getAllDrugsOfPO();
                txtInvoiceNo.Focus();
            }
        }
        else
        {
            getbackposteddynamic();
        }

    }
    protected void loadGRN()
    {
        if (Request.QueryString.Count>0 && Request.QueryString[0].ToString()!="")
        {
            hdnGRNID.Value = Request.QueryString["n"].ToString();
            hdnFlag.Value = "0";

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT GRN_ID, INV_NO, INV_DATE, SUPPLIER_NAME, INV_AMT, REMARKS, POST_FLAG, PO_NO, PO_ID, REC_DATE FROM GRN_INFO JOIN SUPPLIER_MST ON SUPPLIER_MST. SUPPLIER_ID=GRN_INFO.SUPPLIER_ID AND GRN_ID='" + Request.QueryString["n"].ToString() + "'");
            if (objdl.flaG==true)
            {
                DataRow row = objdl.dataSet.Tables[0].Rows[0];
                txtInvoiceNo.Text = row["INV_NO"].ToString();
                txtInvDate.Text = ((DateTime)row["INV_DATE"]).ToString("dd/MM/yyyy");
                txtInvoiceAmount.Text = ((decimal)row["INV_AMT"]).ToString("0.00");
                txtPONo.Text = row["PO_NO"].ToString();
                txtRecDate.Text = ((DateTime)row["REC_DATE"]).ToString("dd/MM/yyyy");
                hdnFlag.Value = row["POST_FLAG"].ToString();
                hdnGRNID.Value = row["GRN_ID"].ToString();
            }

            objdl = dA.returnList("SELECT GRN_DTLS.MED_ID, MED_NAME, MED_REC_QTY, MED_COST, MED_AMT, MED_BATCH_NO, MED_BATCH_ID, MED_EXP_DATE, MED_PO_ID, MED_GST_AMT, MED_PACK, MED_BIG_UOM, MED_SMALL_UOM, TRANS_ID FROM GRN_DTLS JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID=GRN_DTLS.MED_ID  WHERE GRN_ID='" + hdnGRNID.Value + "'");
            if (objdl.flaG==true)
            {
                string str = "";
                decimal totalAmt = 0;
                for (int inc=0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
                {
                    DataRow row = objdl.dataSet.Tables[0].Rows[inc];

                    str += "$('#tblDrugs > tbody:last').append('";
                    str += "<tr>";
                    str += "<td><input type=\"text\" id=\"drug[]\" name=\"drug[]\" value=\"" + objdl.dataSet.Tables[0].Rows[inc]["MED_NAME"].ToString() + "\" style=\"width:90%\" ReadOnly=\"true\"/></td>";
                    str += "<td><input type=\"text\" id=\"ordq[]\" name=\"ordq[]\" value=\"" + new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT CONCAT(((MED_ORD_QTY-MED_REC_QTY)/PO_MED_PACK), ' ',MOD((MED_ORD_QTY-MED_REC_QTY),PO_MED_PACK)) FROM PURCHASE_ORDER_DTLS WHERE TRANS_ID='" + row["MED_PO_ID"].ToString() + "'").dataSet.Tables[0].Rows[0][0].ToString() + "\" style=\"width:30%\" readonly/><input type=\"text\" id=\"ordu[]\" name=\"ordu[]\" style=\"width:40%\" value=\"" + row["MED_BIG_UOM"].ToString() + "/" + row["MED_SMALL_UOM"].ToString() + "\" readonly=\"true\"/></td>";
                    str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" value=\"" + row["MED_PACK"].ToString() + "\" style=\"width:80%;text-align:right\" Readonly=\"true\"/></td>";
                    str += "<td><input type=\"text\" id=\"recb[]\" name=\"recb[]\" value=\"" + ((int)row["MED_REC_QTY"]) / ((int)row["MED_PACK"]) + "\" style=\"width:20%;text-align:right\"/>&nbsp;<input type=\"text\" id=\"bunt[]\" name=\"bunt[]\" style=\"width:20%\" value=\"" + row["MED_BIG_UOM"].ToString() + "\" readonly=\"true\"/><input type=\"text\" id=\"recs[]\" name=\"recs[]\" style=\"width:20%\" value=\"" + ((int)row["MED_REC_QTY"]) % ((int)row["MED_PACK"]) + "\"/><input type=\"text\" id=\"sunt[]\" name=\"sunt[]\" style=\"width:20%\" value=\"" + row["MED_SMALL_UOM"].ToString() + "\" readonly=\"true\"/></td>";
                    str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" value=\"" + row["MED_COST"].ToString() + "\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
                    str += "<td><input type=\"text\" id=\"gst[]\" name=\"gst[]\" value=\"" + row["MED_GST_AMT"].ToString() + "\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
                    str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" value=\"" + row["MED_AMT"].ToString() + "\" readonly style=\"width:90%;text-align:right\"/></td>";
                    str += "<td><input type=\"text\" id=\"batch[]\" name=\"batch[]\" value=\"" + row["MED_BATCH_NO"].ToString() + "\" style=\"width:90%;text-transform:uppercase\" /></td>";
                    str += "<td><input type=\"text\" id=\"exp[]\" name=\"exp[]\" value=\"" + ((DateTime)row["MED_EXP_DATE"]).ToString("dd/MM/yyyy") + "\" class=\"dp\" style=\"width:90%;text-align:right\"/>";
                    str += "<input type=\"hidden\" id=\"drid[]\" name=\"drid[]\" value=\"" + row["MED_ID"].ToString() + "\"/><input type=\"hidden\" id=\"poid[]\" name=\"poid[]\" value=\"" + row["MED_PO_ID"].ToString() + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + row["TRANS_ID"].ToString() + "\"/></td>";
                    str += "</tr>";
                    str += "');";

                    totalAmt += ((decimal)row["MED_AMT"]);
                }

                txtTotal.Text = totalAmt.ToString();
                Page page = HttpContext.Current.CurrentHandler as Page;
                page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
            }
        }
    }
    protected void getAllDrugsOfPO()
    {
        if (Request.QueryString.Count ==2)
        {
            txtPONo.Text = Request.QueryString[1].ToString();

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            vGeneral vG = new vGeneral();
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT PO_MED_ID, MED_NAME, MED_ORD_QTY, PO_MED_PACK, PO_MED_COST, MED_AMT, MED_BIG_UOM, MED_SMALL_UOM, TRANS_ID, MED_REC_QTY  FROM PURCHASE_ORDER_DTLS JOIN MEDICINE_MST ON PO_MED_ID=MED_ID WHERE PO_ID='" + Convert.ToInt32(Request.QueryString[1].ToString()) + "' ORDER BY MED_NAME");
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

                    for (int i = 0; i < 100; i++)
                    {
                        if (i < objdl.dataSet.Tables[0].Rows.Count)
                        {
                            str += "$('#tblDrugs > tbody:last').append('";
                            str += "<tr>";
                            str += "<td><input type=\"text\" id=\"drug[]\" name=\"drug[]\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["MED_NAME"].ToString() + "\" style=\"width:90%\" ReadOnly=\"true\"/></td>";
                            str += "<td><input type=\"text\" id=\"ordq[]\" name=\"ordq[]\" value=\"" + (vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["MED_ORD_QTY"].ToString()) - vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["MED_REC_QTY"].ToString())) / vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["PO_MED_PACK"].ToString()) + "/" + (vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["MED_ORD_QTY"].ToString()) - vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["MED_REC_QTY"].ToString())) % vG.getNumberV(objdl.dataSet.Tables[0].Rows[i]["PO_MED_PACK"].ToString()) + "\" style=\"width:30%\" readonly/><input type=\"text\" id=\"ordu[]\" name=\"ordu[]\" style=\"width:40%\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["MED_BIG_UOM"].ToString() + "/" + objdl.dataSet.Tables[0].Rows[i]["MED_SMALL_UOM"].ToString() + "\" readonly=\"true\"/></td>";
                            str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["PO_MED_PACK"].ToString() + "\" style=\"width:80%;text-align:right\" Readonly=\"true\"/></td>";
                            str += "<td><input type=\"text\" id=\"recb[]\" name=\"recb[]\" value=\"0\" style=\"width:20%;text-align:right\"/>&nbsp;<input type=\"text\" id=\"bunt[]\" name=\"bunt[]\" style=\"width:20%\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["MED_BIG_UOM"].ToString() + "\" readonly=\"true\"/><input type=\"text\" id=\"recs[]\" name=\"recs[]\" style=\"width:20%\" value=\"0\"/><input type=\"text\" id=\"sunt[]\" name=\"sunt[]\" style=\"width:20%\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["MED_SMALL_UOM"].ToString() + "\" readonly=\"true\"/></td>";
                            str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["PO_MED_COST"].ToString() + "\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
                            str += "<td><input type=\"text\" id=\"gst[]\" name=\"gst[]\" value=\"0\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
                            str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" value=\"0\" readonly style=\"width:90%;text-align:right\"/></td>";
                            str += "<td><input type=\"text\" id=\"batch[]\" name=\"batch[]\" value=\"\" style=\"width:90%;text-transform:uppercase\" /></td>";
                            str += "<td><input type=\"text\" id=\"exp[]\" name=\"exp[]\" value=\"\" class=\"dp\" style=\"width:90%;text-align:right\"/>";
                            str += "<input type=\"hidden\" id=\"drid[]\" name=\"drid[]\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["PO_MED_ID"].ToString() + "\"/><input type=\"hidden\" id=\"poid[]\" name=\"poid[]\" value=\"" + objdl.dataSet.Tables[0].Rows[i]["TRANS_ID"].ToString() + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"0\"/></td>";
                            str += "</tr>";
                            str += "');";
                        }
                    }
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
                }
            }

        }
    }
    protected void getbackposteddynamic()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());

        string[] drug = Request.Form.GetValues("drug[]");
        string[] drid = Request.Form.GetValues("drid[]");
        string[] ordq = Request.Form.GetValues("ordq[]");
        string[] pack = Request.Form.GetValues("pack[]");
        string[] recb = Request.Form.GetValues("recb[]");
        string[] bunt = Request.Form.GetValues("bunt[]");
        string[] recs = Request.Form.GetValues("recs[]");
        string[] sunt = Request.Form.GetValues("sunt[]");
        string[] cost = Request.Form.GetValues("cost[]");
        string[] dgst = Request.Form.GetValues("gst[]");
        string[] damt = Request.Form.GetValues("amt[]");
        string[] batc = Request.Form.GetValues("batch[]");
        string[] expr = Request.Form.GetValues("exp[]");
        string[] poid = Request.Form.GetValues("poid[]");
        string[] ordu = Request.Form.GetValues("ordu[]");


        string str="";
        
        for (int row = 0; row < drid.Count(); row++)
        {
            str += "$('#tblDrugs > tbody:last').append('";
            str += "<tr>";
            str += "<td><input type=\"text\" id=\"drug[]\" name=\"drug[]\" value=\"" + drug[row] + "\" style=\"width:90%\" ReadOnly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"ordq[]\" name=\"ordq[]\" value=\"" + ordq[row] + "\" style=\"width:30%\" readonly/><input type=\"text\" id=\"ordu[]\" name=\"ordu[]\" style=\"width:40%\" value=\"" + ordu[row] + "\" readonly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"pack[]\" name=\"pack[]\" value=\"" + pack[row] + "\" style=\"width:80%;text-align:right\" Readonly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"recb[]\" name=\"recb[]\" value=\"" + recb[row] + "\" style=\"width:20%;text-align:right\"/>&nbsp;<input type=\"text\" id=\"bunt[]\" name=\"bunt[]\" style=\"width:20%\" value=\"" + bunt[row] + "\" readonly=\"true\"/><input type=\"text\" id=\"recs[]\" name=\"recs[]\" style=\"width:20%\" value=\"" + recs[row] + "\"/><input type=\"text\" id=\"sunt[]\" name=\"sunt[]\" style=\"width:20%\" value=\"" + sunt[row] + "\" readonly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"cost[]\" name=\"cost[]\" value=\"" + cost[row] + "\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
            str += "<td><input type=\"text\" id=\"gst[]\" name=\"gst[]\" value=\"" + dgst[row] + "\" style=\"width:90%;text-align:right\" onblur=\"calculateAmount()\"/></td>";
            str += "<td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" value=\"" + damt[row] + "\" readonly style=\"width:90%;text-align:right\"/></td>";
            str += "<td><input type=\"text\" id=\"batch[]\" name=\"batch[]\" value=\"" + batc[row] + "\" style=\"width:90%;text-transform:uppercase\" /></td>";
            str += "<td><input type=\"text\" id=\"exp[]\" name=\"exp[]\" value=\"" + expr[row] +"\" class=\"dp\" style=\"width:90%;text-align:right\"/>";
            str += "<input type=\"hidden\" id=\"drid[]\" name=\"drid[]\" value=\"" + drid[row] + "\"/><input type=\"hidden\" id=\"poid[]\" name=\"poid[]\" value=\"" + poid[row] + "\"/></td>";
            str += "</tr>";
            str += "');";
        }
        Page page = HttpContext.Current.CurrentHandler as Page;
        page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
    }
    [System.Web.Services.WebMethod]
    public static string postGRN()
    {
        string msg = "";

        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MAX(GRN_ID) FROM GRN_INFO WHERE POST_FLAG=0");
        if (objdl.flaG == true)
        {
            string grnID = objdl.dataSet.Tables[0].Rows[0][0].ToString();

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            List<dbParam> objparams = new List<dbParam>();

            objparams.Add(new dbParam{col= "GrnID",image=null, dType="I", val = grnID });
            objparams.Add(new dbParam { col = "UserID", image = null, dType = "S", val = HttpContext.Current.Session["userid"].ToString() });
           
            msg = dA.runStoredProcedure("proc_postGRN", objparams, null);
        }
        else
        {
            msg = "ERROR: " + objdl.Msg;
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string saveGRN(NameValue[] frmValues)
    {
        string msg = "";
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objData objD = new objData();
        List<objData> objdata = new List<objData>();
        List<string> gID = new List<string>() { frmValues.Form("ctl00$contentForm$hdnGRNID"), "GRN_INFO", "GRN_ID", HttpContext.Current.Session["userid"].ToString() };
        bool saveFlag = false;

        if (new vGeneral().getNumberD(frmValues.Form("ctl00$contentForm$txtInvoiceAmount")) != new vGeneral().getNumberD(frmValues.Form("ctl00$contentForm$txtTotal")))
        {
            msg = "ERROR: The Total amount is not equal to the invoice amount";
            saveFlag = false;
        }
        else
        {
            objD.xTable = "GRN_INFO";
            objD.Delete = false;

            objD.KeyCol = new List<string>() { "GRN_ID" };
            objD.KeyVal = new List<string>() { frmValues.Form("ctl00$contentForm$hdnGRNID") };
            objD.Column = new List<string>() { "GRN_ID", "INV_NO", "INV_DATE", "INV_AMT", "REMARKS", "POST_FLAG", "PO_ID", "REC_DATE", "GRN_USERID" };
            objD.CValue = new ArrayList();

            List<string> col = new List<string>()
            {
                frmValues.Form("ctl00$contentForm$hdnGRNID"),
                frmValues.Form("ctl00$contentForm$txtInvoiceNo"),
                new vGeneral().convertDateForDB(frmValues.Form("ctl00$contentForm$txtInvDate")),
                frmValues.Form("ctl00$contentForm$txtInvoiceAmount"),
                "", "0", frmValues.Form("ctl00$contentForm$txtPONo"),
                new vGeneral().convertDateForDB(frmValues.Form("ctl00$contentForm$txtRecDate")),
                HttpContext.Current.Session["userid"].ToString()
            };
            objD.CValue.Add(col);
        
            objdata.Add(objD);

            objD = new objData();

            objD.xTable = "GRN_DTLS";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "GRN_ID" };
            objD.KeyVal = new List<string>() { frmValues.Form("ctl00$contentForm$hdnGRNID") };

            objD.Column = new List<string>() { "GRN_ID", "TRANS_ID", "MED_ID", "MED_REC_QTY", "MED_COST", "MED_AMT", "MED_BATCH_NO", "MED_EXP_DATE", "MED_PO_ID", "MED_GST_AMT", "MED_PACK", "DP_FLAG" };

            objD.CValue = new ArrayList();

            var drugID = frmValues.FormMultiple("drid[]");
            var drugOQ = frmValues.FormMultiple("ordq[]");
            var drugRQ = frmValues.FormMultiple("recb[]");
            var drugRS = frmValues.FormMultiple("recs[]");
            var drugCS = frmValues.FormMultiple("cost[]");
            var drugAT = frmValues.FormMultiple("amt[]");
            var drugBT = frmValues.FormMultiple("batch[]");
            var drugED = frmValues.FormMultiple("exp[]");
            var drugTR = frmValues.FormMultiple("poid[]");
            var drugGS = frmValues.FormMultiple("gst[]");
            var packMD = frmValues.FormMultiple("pack[]");
            var drugOU = frmValues.FormMultiple("ordq[]");
            var drugTN = frmValues.FormMultiple("tran[]");

            for (int i = 0; i < drugID.Count(); i++)
            {
                if (((int.Parse(drugRQ[i]) * int.Parse(packMD[i])) + int.Parse(drugRS[i]))!=0)
                {
                    if (((int.Parse(drugRQ[i]) * int.Parse(packMD[i])) + int.Parse(drugRS[i])) > ((int.Parse(drugOQ[i].Split('/')[0]) * int.Parse(packMD[i])) + int.Parse(drugOQ[i].Split('/')[1])))
                    {
                        saveFlag = false;
                        msg = "ERROR: Cannot continue.\nPossible Reasons:-\n* Received Quantity more than Ordered.\n* Received Quantity is not entered for any.";
                    }
                    else
                    {
                        if (((int.Parse(drugRQ[i]) * int.Parse(packMD[i])) + int.Parse(drugRS[i])) > 0)
                        {
                            col = new List<string>()
                            {
                                frmValues.Form("ctl00$contentForm$hdnGRNID"),
                                drugTN[i],
                                drugID[i],
                                ((int.Parse(drugRQ[i]) * int.Parse(packMD[i])) + int.Parse(drugRS[i])).ToString(),
                                drugCS[i],
                                drugAT[i],
                                drugBT[i],
                                new vGeneral().convertDateForDB(drugED[i]),
                                drugTR[i],
                                drugGS[i],
                                packMD[i],
                                drugRS[i]
                            };
                            objD.CValue.Add(col);
                            saveFlag = true;
                        }
                    }
                }
            }
            objdata.Add(objD);

            objD = new objData();
            objD.xTable = "PURCHASE_ORDER_INFO";
            objD.Delete = false;

            objD.KeyCol = new List<string>() { "PO_ID" };
            objD.KeyVal = new List<string>() { frmValues.Form("ctl00$contentForm$txtPONo") };

            objD.Column = new List<string>() { "POST_FLAG" };
            objD.CValue = new ArrayList();
            col = new List<string>() { "30" };
            objD.CValue.Add(col);
            objdata.Add(objD);
        }
        try
        {
            if (saveFlag==true)
            {
                msg = dbaction.saveCollection(objdata, gID);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message.ToString();
        }
        return msg;
    }
}