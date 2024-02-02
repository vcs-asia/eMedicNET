using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Vijay;
using System.Globalization;

public partial class report_preview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.convertToPDF();
    }
    protected string dataReport(string strDate, string strData)
    {
        string output = "";
        switch(strData)
        {
            case "GRNDetailsList":
                output = getTitle("GRN LIST - DETAILED", strDate) + getGRNDetailsList(strDate);
                break;
            case "OUTPO":
                output = getTitle("OUTSTANDING PO LIST", strDate) + getOutPODetailsList(strDate);
                break;
            case "GRNList":
                output = getTitle("GRN LIST", strDate) + getGRNList(strDate);
                break;
            case "POList":
                output = getTitle("PURCHASE ORDER LIST", strDate) + getPOList(strDate);
                break;
            case "PO":
                output = getTitle("PURCHASE ORDER", "") + getPO(strDate);
                break;
            case "GRN":
                output = getTitle("GOODS RECEIVED NOTE", "") + getGRN(strDate);
                break;
            case "MOVE":
                output = getTitle("STOCK MOVEMENT", strDate) + getMovement(strDate);
                break;
            case "OMOVE":
                output = getTitle("OUTLET STOCK MOVEMENT", strDate) + getOMovement(strDate);
                break;
            case "PSY":
                output = getTitle("", strDate) + getPsyReport(strDate);
                break;
            case "ISS":
                output = getTitle("STOCK ISSUE", "") + getIss(strDate);
                break;
            case "ADJ":
                output = getTitle("STOCK ADJUSTMENT", "") + getAdj(strDate);
                break;
            case "TRN":
                output = getTitle("STOCK TRANSFER", "") + getTransfer(strDate);
                break;
            case "RORDER":
                output = getTitle("STOCK TO BE REORDERED", "") + getRecorderStock();
                break;
            case "STK":
                output = getTitle("CURRENT STOCK", "") + getCurStock();
                break;
            case "STKB":
                output = getTitle("CURRENT STOCK - BATCH", "") + getCurStockB();
                break;
            case "EXPIRE":
                output = getTitle("EXPIRING STOCK", "") + getExpStock();
                break;
            case "DRUGLIST":
                output = getTitle("DRUGS LIST", "") + getDrugList();
                break;
            case "STKVAL":
                output = getTitle("CURRENT STOCK WITH VALUE", "") + getCurStockVal();
                break;
            case "OSTKVAL":
                output = getTitle(strDate.Split('.')[1] + " CURRENT STOCK WITH VALUE", "") + getOutletCurStockVal(strDate.Split('.')[0]);
                break;
            case "ISSVAL":
                output = getTitle("ISSUE", "") + getIssStockVal(strDate);
                break;
            case "CUSTOM":
                output = getTitle("CUSTOM REPORT", strDate) + getReportOne(strDate);
                break;
            case "TRAFFIC":
                output = getTitle("TRAFFIC REPORT", strDate) + getTrafficReport(strDate);
                break;
            case "RECEIPT":
                output = getTitle("RECEIPT", strDate) + printReceipt(strDate);
                break;
            case "PINVOICE":
                output = getTitle("PATIENT INVOICE", strDate) + printReceiptBreakdown(strDate);
                break;
            case "DRUGDISPENSE":
                output = getTitle("DRUG DISPENSE", strDate) + printDrugDispense(strDate);
                break;
            case "DRUGSDISPENSE":
                output = getTitle("DRUGS DISPENSE", strDate) + printDrugsDispense(strDate);
                break;
            case "PANELR":
                output = getTitle("PATIENT PAYMENT DETAILS (PAYMENT)", strDate) + getPaymentPanel(strDate);
                break;
            case "PANELS":
                output = getTitle("PATIENT PAYMENT DETAILS", strDate) + getPaymentPanel(strDate);
                break;
            case "CASHR":
                output = getTitle("PATIENT PAYMENT DETAILS (" + strDate.Split('.')[2] + ")", strDate) + getPaymentCash(strDate);
                break;
            case "sd":
                output = getTitle("DRUGS PRICE LIST", strDate) + getDrugPriceList();
                break;
            case "ls":
                output = getTitle("SERVICE CHARGES LIST", strDate) + getServicesPriceList();
                break;
            case "pl":
                output = getTitle("PATIENT LIST", strDate) + getPatientList();
                break;
            case "AUDIT":
                output = getTitle("AUDIT REPORT", strDate) + getAuditReport(strDate);
                break;
            case "LOGIN":
                output = getTitle("LOG DETAILS", strDate) + getLoginDetails(strDate);
                break;
            case "US":
                output = getTitle("USERS LIST", strDate) + getUsersList(strDate);
                break;
            case "LD":
                output = getTitle("DOCTORS LIST", strDate) + getDoctorsList(strDate);
                break;
            case "CM":
                output = getTitle("COMPONENTS LIST", strDate) + getComponentsList(strDate);
                break;
            case "SR":
                output = getTitle("SERVICES LIST", strDate) + getServicesList(strDate);
                break;
            case "PR":
                output = getTitle("PARAMETERS LIST", strDate) + getParametersList(strDate);
                break;
            case "AL":
                output = getTitle("ALLERGIES LIST", strDate) + getAllergiesList(strDate);
                break;
            case "ME":
                output = getTitle("Medical Examination", strDate) + getMedicalExam(strDate);
                break;
            case "DMARKUP":
                output = getTitle("Drugs Markup Report", strDate) + getDrugsMarkup(strDate);
                break;
            case "IMARKUP":
                output = getTitle("Items Markup Report", strDate) + getItemsMarkup(strDate);
                break;
            case "OADJ":
                output = getTitle("OUTLET - STOCK ADJUSTMENT", "") + getOutletAdj(strDate);
                break;

        }
        return getFooter() + output;
    }
    private void SaveToPDF()
    {
        /*
        StringWriter writer = new StringWriter();
        HtmlTextWriter writer2 = new HtmlTextWriter(writer);
        this.Page.RenderControl(writer2);
        StringReader reader = new StringReader(writer.ToString());
        Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
        HTMLWorker worker = new HTMLWorker(document);
        PdfWriter.GetInstance(document, new FileStream(base.Server.MapPath("_PublicData") + "/xray.pdf", FileMode.Create));
        document.Open();
        worker.Parse(reader);
        document.Close();*/
    }
    private void convertToPDF()
    {
        /*
        base.Response.ContentType = "application/PDF";
        base.Response.AddHeader("Content-Disposition:", "inline; filename=Reporting.pdf");
        base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter writer = new StringWriter();
        HtmlTextWriter writer2 = new HtmlTextWriter(writer);
        this.Page.RenderControl(writer2);
        StringReader reader = new StringReader(writer.ToString());
        Document document = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
        HTMLWorker worker = new HTMLWorker(document);
        PdfWriter.GetInstance(document, base.Response.OutputStream);
        document.Open();
        worker.Parse(reader);
        document.Close();
        base.Response.Write(document);
        base.Response.End();*/
    }
    private string getGRNDetailsList(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<thead>";
        rptStr += "<tr><td width='5%'>#</td><td width='5%'>GRN</td><td width='8%'>Rec Date</td><td width='25%'>Supplier</td><td width='30%'>Drug Name</td><td width='10%'>Batch No.</td><td width='10%'>Quantity</td><td width='8%' align='right'>Cost[RM]</td><td width='7%' align='right'>GST[RM]</td><td width='8%' align='right'>Amount[RM]</td></tr>";
        rptStr += "</thead>";

        decimal totalAmt = 0; decimal totalGST = 0;

        //GRN Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT GRN_INFO.GRN_ID, REC_DATE, SUPPLIER_NAME, MED_NAME, MED_BATCH_NO, getMedQuantity(GRN_DTLS.MED_ID, MED_PACK, MED_REC_QTY) AS QTY, MED_COST, MED_GST_AMT, MED_AMT, INV_DATE  FROM GRN_DTLS JOIN MEDICINE_MST ON GRN_DTLS.MED_ID=MEDICINE_MST.MED_ID  JOIN GRN_INFO ON GRN_INFO.GRN_ID=GRN_DTLS.GRN_ID JOIN SUPPLIER_MST ON SUPPLIER_MST.SUPPLIER_ID = GRN_INFO.SUPPLIER_ID WHERE INV_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' ORDER BY REC_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<tbody>";
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy") + "<br/>" + ((DateTime)Row[9]).ToString("dd/MM/yyyy") + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + "</td><td>" + Row[5] + "</td><td align='right'>" + Row[6] + "</td><td align='right'>" + Row[7] + "</td><td align='right'>" + Row[8] + "</td></tr>";
                totalAmt += decimal.Parse(Row[8].ToString());
                totalGST += decimal.Parse(Row[7].ToString());
            }
            rptStr += "</tbody>";
            rptStr += "<tfoot>";
            rptStr += "<tr><td colspan='8'>Total Amount</td><td align='right'>" + totalGST.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
            rptStr += "</tfoot>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getOutPODetailsList(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<thead>";
        rptStr += "<tr><td width='5%'>#</td><td width='5%'>PO</td><td width='8%'>Ord Date</td><td width='25%'>Supplier</td><td width='30%'>Drug Name</td><td width='10%'>Ord. Qty</td><td width='10%'>Rec. Qty</td></tr>";
        rptStr += "</thead>";

        //GRN Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PURCHASE_ORDER_INFO.PO_NO, PO_DATE, SUPPLIER_NAME, MED_NAME, getMedQuantity(PURCHASE_ORDER_DTLS.PO_MED_ID, PURCHASE_ORDER_DTLS.PO_MED_PACK, MED_ORD_QTY) AS OQTY,  getMedQuantity(PURCHASE_ORDER_DTLS.PO_MED_ID, PURCHASE_ORDER_DTLS.PO_MED_PACK, MED_REC_QTY) AS RQTY, MED_ORD_QTY, MED_REC_QTY  FROM PURCHASE_ORDER_DTLS JOIN MEDICINE_MST ON PURCHASE_ORDER_DTLS.PO_MED_ID=MEDICINE_MST.MED_ID  JOIN PURCHASE_ORDER_INFO ON PURCHASE_ORDER_INFO.PO_ID=PURCHASE_ORDER_DTLS.PO_ID JOIN SUPPLIER_MST ON SUPPLIER_MST.SUPPLIER_ID = PURCHASE_ORDER_INFO.PO_SUPPLIER_ID WHERE POST_FLAG IN (0,2) AND PO_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' ORDER BY PURCHASE_ORDER_INFO.PO_ID DESC");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<tbody>";
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                if (Row["MED_REC_QTY"].ToString() == "" || (int)Row["MED_REC_QTY"] < (int)Row["MED_ORD_QTY"])
                    rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy")  + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + "</td><td>" + Row[5] + "</td></tr>";
            }
            rptStr += "</tbody>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getMovement(string strParam)
    {

        string rptStr = "";
        objDL objdl = new objDL();

        //Drug or Item Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID='" + strParam.Split('.')[0] + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td></tr>";
            rptStr += "</table>";
        }
        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='10%'>Date</td><td width='20%'>Ref No.</td><td width='10%'>Type</td><td width='10%'>ID</td><td width='10%'>Qty In</td><td width='10%'>Qty Out</td><td width='10%'>Balance</td></tr>";

        //Stock Movement List
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT TRAN_DATE, REF_NO, TRAN_TYPE, STOCK_ID, getMedQuantity(MED_ID, SM_MED_PACK,QTY_IN), getMedQuantity(MED_ID, SM_MED_PACK,QTY_OUT), getMedQuantity(MED_ID, SM_MED_PACK, CURRENT_BAL) FROM STOCK_MOVEMENT_INFO WHERE MED_ID='" + strParam.Split('.')[0] + "' AND SM_MED_PACK='" + strParam.Split('.')[1] + "' ORDER BY TRAN_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                //rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + ((int)Row[4]).ToString() + "</td><td>" + ((int)Row[5]).ToString() + "</td><td>" + ((int)Row[6]).ToString() + "</td></tr>";
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4].ToString() + "</td><td>" + Row[5].ToString() + "</td><td>" + Row[6].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getOMovement(string strParam)
    {

        string rptStr = "";
        objDL objdl = new objDL();
        string outlet = "";

        //Outlet Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID='" + strParam.Split('.')[1] + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            outlet = objdl.dataSet.Tables[0].Rows[0][0].ToString();
        }

        //Drug or Item Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID='" + strParam.Split('.')[2] + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td>" + outlet + " [" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "]</td></tr>";
            rptStr += "</table>";
        }
        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='10%'>Date</td><td width='30%'>Ref No.</td><td width='10%'>Type</td><td width='10%'>Qty In</td><td width='10%'>Qty Out</td><td width='10%'>Balance</td></tr>";

        //Stock Movement List
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT TRAN_DATE, REF_NO, TRAN_TYPE, ID, QTY_IN, QTY_OUT, CURRENT_BAL, MED_SMALL_UOM FROM OUTLET_STOCK_MOVEMENT_INFO JOIN MEDICINE_MST ON OUTLET_STOCK_MOVEMENT_INFO.MED_ID = MEDICINE_MST.MED_ID WHERE OUTLET_STOCK_MOVEMENT_INFO.MED_ID='" + strParam.Split('.')[2] + "' AND OUTLET_ID='" + strParam.Split('.')[1] + "' AND TRAN_DATE BETWEEN DATE_ADD(NOW(),INTERVAL " + -1 * int.Parse(strParam.Split('.')[0]) + " DAY) AND NOW() ORDER BY TRAN_ID");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                //rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + ((int)Row[4]).ToString() + "</td><td>" + ((int)Row[5]).ToString() + "</td><td>" + ((int)Row[6]).ToString() + "</td></tr>";
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy HH:mm") + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td>" + (Row[4].ToString() != "0" ? (Row[4].ToString() + " " + Row[7].ToString()) : "") + "</td><td>" + (Row[5].ToString() != "0" ? (Row[5].ToString() + " " + Row[7].ToString()) : "") + "</td><td>" + (Row[6].ToString() != "0" ? (Row[6].ToString() + " " + Row[7].ToString()) : "") + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPsyReport(string strDate)
    {

        string rptStr = "";
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string strParam = strDate;

        objDL objdl = new objDL();
        string outlet = "";

        string drgName = "";

        //Outlet Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID='" + strParam.Split('.')[1] + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            outlet = objdl.dataSet.Tables[0].Rows[0][0].ToString();
        }

        //Drug or Item Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID='" + strParam.Split('.')[2] + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            drgName = objdl.dataSet.Tables[0].Rows[0][0].ToString();
        }
        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table class='tblx' style='font-size:60%'>";
        rptStr += "<tr><td align='center'>BUKU REKOD MENGIKUT SYARAT-SYARAT PERMIT DAN PERATURAN 15(3) PERATURAN RACUN (BAHAN-BAHAN</td></tr>";
        rptStr += "<tr><td align='center'>PSIKOTROPIK) 1989 UNTUK MEMBELI DAN MENGGUNA BAHAN-BAHAN PSIKOTROPIK (BUPRENORPHINE DAN METHADON)</td></tr>";
        rptStr += "</table>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tblx'>";
        rptStr += "<tr><th colspan='4'>Nama, Bentuk dan Kepekatan Bahan Psikotropik</th><th>Saiz Pek</th><th colspan='3' rowspan='2'>Butir-butir Pesakit yang dibenakalkan Bahan Psikotropik</th></tr>";
        rptStr += "<tr><td colspan='4'>" + drgName + "</td><td align='center'>60 mls</td></tr>";
        rptStr += "<tr><th width='10%' rowspan='2'>Tarikh Terima <br/> Tarikh Guna</th><th width='15%' align='center' rowspan='2'>Nama dan Alamat Pembekal</th><th colspan='3' width='21%'>AMAUN</th><th width='20%' rowspan='2'>Nama</th><th width='5%' rowspan='2'>No. Kad<br/>Pengenalan</th><th width='24%' rowspan='2'>Alamat</th></tr>";
        rptStr += "<tr><th width='7%'>Terima<br/>(ml)</th><th width='7%'>Guna<br/>(ml)</th><th width='7%'>Baki Stok<br/>(ml)</th></tr>";

        //Visit List with Medication
        //objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_DATE, (SELECT CONCAT(CONCAT(CONCAT(SUPPLIER_NAME, SUPPLIER_ADDR1), SUPPLIER_ADDR2), SUPPLIER_ADDR3) FROM SUPPLIER_MST WHERE SUPPLIER_ID = (SELECT PO_SUPPLIER_ID FROM PURCHASE_ORDER_INFO WHERE PO_ID = (SELECT PO_ID FROM PURCHASE_ORDER_DTLS WHERE PO_MED_ID = 1))) , '', MED_QTY, '', PAT_NAME, PAT_IC_NO, CONCAT(CONCAT(PAT_ADDR1, PAT_ADDR2) , PAT_ADDR3) AS ADDR FROM PATIENT_VISIT_MST JOIN VISIT_MEDICINE_DTLS ON VISIT_MEDICINE_DTLS.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID WHERE MED_ID='" + strParam.Split('.')[1] + "' AND VISIT_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' ORDER BY VISIT_DATE");
        decimal previousBalance = 0;

        objdl = null;
        decimal balStock = 0;
        decimal curBal1 = 0; decimal curBal2 = 0;
        bool check = false;

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_DATE, (SELECT SUPPLIER_NAME FROM SUPPLIER_MST WHERE SUPPLIER_ID = (SELECT PO_SUPPLIER_ID FROM PURCHASE_ORDER_INFO WHERE PO_ID = (SELECT PO_ID FROM PURCHASE_ORDER_DTLS WHERE PO_MED_ID = '" + strParam.Split('.')[2] + "'))) , '', MED_QTY, '', PAT_NAME, PAT_IC_NO, CONCAT(CONCAT(PAT_ADDR1, PAT_ADDR2), PAT_ADDR3) AS PAT_ADDR, PATIENT_VISIT_MST.VISIT_ID AS VISIT_ID FROM PATIENT_VISIT_MST JOIN VISIT_MEDICINE_DTLS ON VISIT_MEDICINE_DTLS.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID WHERE MED_ID='" + strParam.Split('.')[2] + "' AND VISIT_DATE < '" + fdate + " 00:00:00' UNION SELECT ADJ_DATE, (SELECT SUPPLIER_NAME FROM SUPPLIER_MST WHERE SUPPLIER_ID = (SELECT PO_SUPPLIER_ID FROM PURCHASE_ORDER_INFO WHERE PO_ID = (SELECT PO_ID FROM PURCHASE_ORDER_DTLS WHERE PO_MED_ID = '" + strParam.Split('.')[2] + "'))), ADJ_QTY, '', '', '', '', '', 0 FROM STOCK_ADJUSTMENT_DTLS JOIN STOCK_ADJUSTMENT_INFO ON ADJ_REF_ID = STK_REF_ID WHERE ADJ_MED_ID = '" + strParam.Split('.')[2] + "' AND ADJ_DATE < '" + fdate + " 00:00:00' ORDER BY VISIT_DATE, VISIT_ID");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                check = decimal.TryParse(Row[3].ToString(), out curBal1);
                check = decimal.TryParse(Row[2].ToString(), out curBal2);
                previousBalance -= (curBal1 - curBal2);
            }
        }

        balStock = previousBalance;

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_DATE, (SELECT SUPPLIER_NAME FROM SUPPLIER_MST WHERE SUPPLIER_ID = (SELECT PO_SUPPLIER_ID FROM PURCHASE_ORDER_INFO WHERE PO_ID = (SELECT PO_ID FROM PURCHASE_ORDER_DTLS WHERE PO_MED_ID = '" + strParam.Split('.')[2] + "'))) , '', MED_QTY, '', PAT_NAME, PAT_IC_NO, CONCAT(CONCAT(PAT_ADDR1, PAT_ADDR2), PAT_ADDR3) AS PAT_ADDR, PATIENT_VISIT_MST.VISIT_ID AS VISIT_ID FROM PATIENT_VISIT_MST JOIN VISIT_MEDICINE_DTLS ON VISIT_MEDICINE_DTLS.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID WHERE MED_ID='" + strParam.Split('.')[2] + "' AND VISIT_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' UNION SELECT ADJ_DATE, (SELECT SUPPLIER_NAME FROM SUPPLIER_MST WHERE SUPPLIER_ID = (SELECT PO_SUPPLIER_ID FROM PURCHASE_ORDER_INFO WHERE PO_ID = (SELECT PO_ID FROM PURCHASE_ORDER_DTLS WHERE PO_MED_ID = '" + strParam.Split('.')[2] + "'))), ADJ_QTY, '', '', '', '', '', 0 FROM STOCK_ADJUSTMENT_DTLS JOIN STOCK_ADJUSTMENT_INFO ON ADJ_REF_ID = STK_REF_ID WHERE ADJ_MED_ID = '" + strParam.Split('.')[2] + "' AND ADJ_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' ORDER BY VISIT_DATE, VISIT_ID");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                check = decimal.TryParse(Row[3].ToString(), out curBal1);
                check = decimal.TryParse(Row[2].ToString(), out curBal2);
                balStock -= (curBal1 - curBal2);
                rptStr += "<tr><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td align='center'>" + Row[2] + "</td><td align='center'>" + Row[3].ToString() + "</td><td align='center'>" + balStock.ToString() + "</td><td>" + Row[5].ToString() + "</td><td>" + Row[6].ToString() + "</td><td>" + Row[7].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getGRNList(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:6px'>";
        rptStr += "<tr><td width='5%'>#</td><td width='5%'>GRN</td><td width='8%'>PO</td><td width='41%'>Supplier</td><td width='20%'>Invoive No.</td><td width='10%'>Invoice Date</td><td width='10%'>Receive Date</td><td width='5%' align='right'>Amount[RM]</td><td>Status</td></tr>";

        decimal totalAmt = 0;

        //GRN List
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT GRN_ID, PO_NO, SUPPLIER_NAME, INV_NO, INV_DATE, REC_DATE, INV_AMT, POST_FLAG FROM GRN_INFO JOIN SUPPLIER_MST ON SUPPLIER_MST.SUPPLIER_ID = GRN_INFO.SUPPLIER_ID WHERE INV_DATE BETWEEN '" + fdate + " 00:00' AND '" + tdate + " 23:59' ORDER BY INV_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + ((DateTime)Row[4]).ToString("dd/MM/yyyy") + "</td><td>" + ((DateTime)Row[5]).ToString("dd/MM/yyyy") + "</td><td align='right'>" + Row[6] + "</td><td>" + Row[7] + "</td></tr>";
                totalAmt += decimal.Parse(Row[6].ToString());
            }
            rptStr += "<tr><td colspan='8'>Total Amount</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPOList(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:6px'>";
        rptStr += "<tr><td width='5%'>#</td><td width='5%'>GRN</td><td width='8%'>Rec Date</td><td width='41%'>Supplier</td><td width='20%'>Drug Name</td><td width='10%'>Batch No.</td><td width='10%'>Quantity</td><td width='5%' align='right'>Cost[RM]</td><td width='5%' align='right'>GST[RM]</td><td width='7%' align='right'>Amount[RM]</td></tr>";

        decimal totalAmt = 0; decimal totalGST = 0;

        //GRN Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT GRN_INFO.GRN_ID, REC_DATE, SUPPLIER_NAME, MED_NAME, MED_BATCH_NO, getMedQuantity(MED_ID, MED_PACKING, MED_REC_QTY) AS QTY, MED_COST, MED_GST_AMT, MED_AMT  FROM GRN_DTLS JOIN MEDICINE_MST ON GRN_DTLS.MED_ID=MEDICINE_MST.MED_ID  JOIN GRN_INFO ON GRN_INFO.GRN_ID=GRN_DTLS.GRN_ID JOIN SUPPLIER_MST ON SUPPLIER_MST.SUPPLIER_ID = GRN_INFO.SUPPLIER_ID WHERE REC_DATE BETWEEN '" + fdate + " 00:00' AND '" + tdate + " 23:59' ORDER BY REC_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy") + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + "</td><td>" + Row[5] + "</td><td align='right'>" + Row[6] + "</td><td align='right'>" + Row[7] + "</td><td align='right'>" + Row[8] + "</td></tr>";
                totalAmt += decimal.Parse(Row[8].ToString());
                totalGST += decimal.Parse(Row[7].ToString());
            }
            rptStr += "<tr><td colspan='8'>Total Amount</td><td align='right'>" + totalGST.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getGRN(string grnID)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        //GRN Info including Supplier details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SUPPLIER_NAME, SUPPLIER_ADDR1, SUPPLIER_ADDR2, SUPPLIER_ADDR3, GRN_ID, INV_NO, INV_DATE, REC_DATE FROM GRN_INFO JOIN SUPPLIER_MST ON GRN_INFO.SUPPLIER_ID=SUPPLIER_MST.SUPPLIER_ID WHERE GRN_ID='" + grnID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td width='70%'>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td><td width='15%'> Ref No :</td><td style='text-align:right' width='15%'>" + objdl.dataSet.Tables[0].Rows[0][4].ToString() + "</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][1].ToString() + "</td><td> Received Date :</td><td style='text-align:right'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][7]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "</td><td> Invoice Date :</td><td style='text-align:right'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][6]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][3].ToString() + "</td><td> Invoice No :</td><td style='text-align:right'>" + objdl.dataSet.Tables[0].Rows[0][5].ToString() + "</td></tr>";
            rptStr += "</table>";
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Batch No.</td><td width='15%'>Quantity</td><td width='10%' align='right'>Cost[RM]</td><td width='15%' align='right'>Amount[RM]</td></tr>";

        decimal totalAmt = 0; ;

        //GRN Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, MED_BATCH_NO, getMedQuantity(GRN_DTLS.MED_ID, MED_PACKING, MED_REC_QTY) AS QTY, MED_COST, MED_AMT FROM GRN_DTLS JOIN MEDICINE_MST ON GRN_DTLS.MED_ID=MEDICINE_MST.MED_ID WHERE GRN_ID='" + grnID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td><td align='right'>" + Row[3] + "</td><td align='right'>" + Row[4] + "</td></tr>";
                totalAmt += decimal.Parse(Row[4].ToString());
            }
            rptStr += "<tr><td colspan='5'>Total Amount</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getPO(string poID)
    {
        string rptStr = "";
        string remarks = "";

        objDL objdl = new objDL();

        //PO Info including Supplier details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SUPPLIER_NAME, SUPPLIER_ADDR1, SUPPLIER_ADDR2, SUPPLIER_ADDR3, PO_ID, PO_NO, PO_DATE, PO_TERMS, SUPPLIER_PHONE1, SUPPLIER_PHONE2, SUPPLIER_FAX, SUPPLIER_CONT_PERSON, SUPPLIER_CONT_PERSON_HP, SUPPLIER_CODE, PO_REMARKS FROM PURCHASE_ORDER_INFO JOIN SUPPLIER_MST ON PURCHASE_ORDER_INFO.PO_SUPPLIER_ID=SUPPLIER_MST.SUPPLIER_ID WHERE PO_ID='" + poID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table cellpadding='1' cellspacing='0' class='tbl'>";
            rptStr += "<tr><td width='70%'>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td><td width='15%'> Ref No :</td><td style='text-align:right' width='15%'>" + objdl.dataSet.Tables[0].Rows[0][5].ToString() + "</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][1].ToString() + "</td><td> Order Date :</td><td style='text-align:right'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][6]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "</td><td> Terms :</td><td style='text-align:right'>" + objdl.dataSet.Tables[0].Rows[0][7].ToString() + "</td></tr>";
            rptStr += "<tr><td>Tel: " + objdl.dataSet.Tables[0].Rows[0][8].ToString() + "," + objdl.dataSet.Tables[0].Rows[0][9].ToString() + ", Fax: " + objdl.dataSet.Tables[0].Rows[0][10].ToString() + "</td><td>&nbsp;</td><td style='text-align:right'>&nbsp;</td></tr>";
            rptStr += "<tr><td>" + objdl.dataSet.Tables[0].Rows[0][11].ToString() + ", H/P" + objdl.dataSet.Tables[0].Rows[0][12].ToString() + "</td><td>Account :</td><td style='text-align:right'>" + objdl.dataSet.Tables[0].Rows[0][13].ToString() + "</td></tr>";
            rptStr += "</table>";

            remarks = objdl.dataSet.Tables[0].Rows[0]["PO_REMARKS"].ToString();
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='10%'>#</th><td width='40%'>Description</td><td width='20%'>Order Quantity</td><td width='15%'>U.Price</td><td width='15%'>Amount</td></tr>";

        decimal total = 0;

        //PO Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_DESC, getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_ORD_QTY) AS QTY, PO_MED_COST, MED_AMT FROM PURCHASE_ORDER_DTLS JOIN MEDICINE_MST ON PO_MED_ID=MED_ID WHERE PO_ID='" + poID + "' AND MED_DESC_PRINT_FLAG=1");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td><td>" + ((decimal)Row[2]).ToString("0.00") + "</td><td>" + ((decimal)Row[3]).ToString("0.00") + "</td></tr>";
                total += Convert.ToDecimal(Row[3]);
            }
        }
        rptStr += "<tr><td colspan='4'>Total</td><td>" + total.ToString("0.00") + "</td></tr>";
        rptStr += "</table>";
        rptStr += "<br/><br/>";

        rptStr += "<table class='tbl' cellspacing='0'>";
        rptStr += "<tr><td>" + remarks + "</td></tr>";
        rptStr += "</table>";

        rptStr += "<table class='tbl' cellspacing='0'>";
        rptStr += "<tr><td colspan='2'>Terms & Conditions</td></tr>";
        rptStr += "<tr><td width='5%'>&nbsp;</td><td width='95%'>*This purchase order is only valid for 14 days from the date of order, unless stated otherwise.</td></tr>";
        rptStr += "<tr><td>&nbsp;</td><td>*Please quote this purchase order number in all your Invoices/Delivery Orders.</td></tr>";
        rptStr += "<tr><td>&nbsp;</td><td>*Please forward all the Invoices/ Delivery Orders related to this PO to the address stated above.</td></tr>";
        rptStr += "</table>";
        rptStr += "<br/><br/><br/>";
        rptStr += "<table class='tbl'><tr>";
        rptStr += "<td style='width:35%'>Order by __________________</td><td style='width:30%'>&nbsp;</td><td style='width:35%'>Approved by ___________________</td>";
        rptStr += "</table>";
        return rptStr;
    }
    public string getIss(string ID)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        //Issue Info including Supplier details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_NAME, ISSUE_REF_ID, ISSUE_REF_NO, ISSUE_DATE FROM STOCK_ISSUE_INFO JOIN OUTLET_MST ON ISSUE_OUTLET= OUTLET_ID WHERE ISSUE_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td colspan='2'>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td></tr>";
            rptStr += "<tr><td width='10%'>Ref No</td><td>" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "</td></tr>";
            rptStr += "<tr><td>Issue Date</td><td>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][3]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "</table>";
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Quantity</td></tr>";


        //Issue Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, getMedQuantity(ISSUE_MED_ID, ISSUE_PACK, ISSUE_QTY) AS QTY FROM STOCK_ISSUE_DTLS JOIN MEDICINE_MST ON ISSUE_MED_ID=MED_ID WHERE STK_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getAdj(string ID)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        //Adjustment Info including Supplier details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT ADJ_REF_NO, ADJ_DATE FROM STOCK_ADJUSTMENT_INFO WHERE ADJ_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td width='10%'>Ref No</td><td>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td></tr>";
            rptStr += "<tr><td>Date</td><td>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][1]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "</table>";
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Batch No.</td><td width='15%'>Quantity</td></tr>";

        //Adjust Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_QTY) AS QTY, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_PHY_QTY) AS PQTY FROM STOCK_ADJUSTMENT_DTLS JOIN MEDICINE_MST ON ADJ_MED_ID=MED_ID WHERE STK_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getTransfer(string ID)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        //Transfer Info
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT TRAN_REF_ID, TRAN_DATE, (SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID = TRAN_OUT_OUTLET) AS OOUT, (SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID = TRAN_IN_OUTLET) AS OIN FROM STOCK_TRANSFER_INFO WHERE TRAN_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td width='10%'>Ref No</td><td>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td></tr>";
            rptStr += "<tr><td>Date</td><td>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][1]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "</table>";
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Quantity</td></tr>";

        //Transfer Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, CONCAT(TRAN_QTY, ' ', MED_SMALL_UOM) AS QTY FROM STOCK_TRANSFER_DTLS JOIN MEDICINE_MST ON TRAN_MED_ID=MED_ID WHERE STK_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getCurStock()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Packing</td><td width='15%'>Quantity</td></tr>";

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS MNAME, MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS SUOM, curStock(SUM(CUR_BAL_STK), STOCK_DTLD_INFO.MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID)) AS QTY, MED_ID FROM STOCK_DTLD_INFO GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getRecorderStock()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='30%'>Description</td><td width='15%'>Packing</td><td width='15%'>Quantity</td><td width='10%'>Reorder</td></tr>";

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME AS MNAME, MED_PACK, MED_BIG_UOM AS BUOM, MED_SMALL_UOM AS SUOM, curStock(SUM(CUR_BAL_STK), STOCK_DTLD_INFO.MED_PACK, MED_BIG_UOM, MED_SMALL_UOM) AS QTY, MED_REORDER FROM STOCK_DTLD_INFO JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID = STOCK_DTLD_INFO.MED_ID WHERE MED_TYPE != 223 AND MED_FLAG = 1 AND MED_PACK = (SELECT MED_PACK FROM STOCK_DTLD_INFO WHERE MED_ID = MEDICINE_MST.MED_ID AND BATCH_ID = (SELECT MAX(BATCH_ID) FROM STOCK_DTLD_INFO WHERE STOCK_DTLD_INFO.MED_ID = MEDICINE_MST.MED_ID)) GROUP BY MEDICINE_MST.MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) < MED_REORDER ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td>" + Row["MED_REORDER"].ToString() + " " + Row["SUOM"].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getOutletCurStockVal(string outlet)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='20%'>Description</td><td width='15%'>Packing</td><td width='15%'>Quantity</td><td width='5%'>Cost</td><td width='10%'>Value[RM]</td></tr>";

        Decimal total = 0;
        int stk = 0;

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_QTY AS STK, MED_UNIT_COST AS COST, MED_NAME AS MNAME, MED_SMALL_UOM AS SUOM, CONCAT(OUTLET_QTY, ' ', MED_SMALL_UOM) AS QTY FROM OUTLET_STOCK_INFO JOIN MEDICINE_MST ON MED_ID = OUTLET_MED_ID WHERE OUTLET_ID = '" + outlet + "' ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                stk = int.Parse(Row["STK"].ToString());
                //rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row["MNAME"].ToString() + "</td><td>" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td align='right'>" + ((Decimal)Row["COST"]).ToString("0.00") + "</td><td align='right'>" + (((int)Row["STK"]/(int)Row["MED_PACK"]) * ((Decimal)Row["COST"])).ToString("0.00") + "</td></tr>";
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row["MNAME"].ToString() + "</td><td>" + Row["SUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td align='right'>" + ((Decimal)Row["COST"]).ToString("0.00") + "</td><td align='right'>" + (stk * ((Decimal)Row["COST"])).ToString("0.00") + "</td></tr>";
                total += (stk * ((Decimal)Row["COST"]));
            }
            rptStr += "<tr><td colspan='5'>Total Amount</td><td align='right'>" + ((Decimal)total).ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getCurStockVal()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='20%'>Description</td><td width='15%'>Packing</td><td width='15%'>Quantity</td><td width='5%'>Cost</td><td width='10%'>Value[RM]</td></tr>";

        Decimal total = 0;
        int stk = 0;

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT (SELECT MED_COST_PRICE FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS COST, SUM(CUR_BAL_STK) AS STK, (SELECT MED_NAME FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS MNAME, MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS BUOM, (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID) AS SUOM, curStock(SUM(CUR_BAL_STK), STOCK_DTLD_INFO.MED_PACK, (SELECT MED_BIG_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID), (SELECT MED_SMALL_UOM FROM MEDICINE_MST WHERE MED_ID=STOCK_DTLD_INFO.MED_ID)) AS QTY FROM STOCK_DTLD_INFO GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                stk = int.Parse(Row["STK"].ToString());
                //rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row["MNAME"].ToString() + "</td><td>" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td align='right'>" + ((Decimal)Row["COST"]).ToString("0.00") + "</td><td align='right'>" + (((int)Row["STK"]/(int)Row["MED_PACK"]) * ((Decimal)Row["COST"])).ToString("0.00") + "</td></tr>";
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row["MNAME"].ToString() + "</td><td>" + Row["MED_PACK"].ToString() + " " + Row["SUOM"].ToString() + "/" + Row["BUOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td align='right'>" + ((Decimal)Row["COST"]).ToString("0.00") + "</td><td align='right'>" + (stk * (((Decimal)Row["COST"]) / (int)Row["MED_PACK"])).ToString("0.00") + "</td></tr>";
                total += (stk * (((Decimal)Row["COST"]) / (int)Row["MED_PACK"]));
            }
            rptStr += "<tr><td colspan='5'>Total Amount</td><td align='right'>" + ((Decimal)total).ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getCurStockB()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Quantity</td><td width='15%'>Batch No.</td><td width='15%'>Exp. Date</td></tr>";

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT A.MED_NAME AS MNAME, curStock(CUR_BAL_STK, B.MED_PACK, A.MED_BIG_UOM, A.MED_SMALL_UOM) AS QTY, B.BATCH_NO AS BATCH_NO, B.EXP_DATE AS EXP_DATE, B.MED_ID, B.BATCH_ID FROM STOCK_DTLD_INFO B JOIN MEDICINE_MST A ON A.MED_ID=B.MED_ID WHERE CUR_BAL_STK > 0 ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + " " + Row["MED_ID"] + " " + Row["BATCH_ID"] + "</td><td>" + Row["QTY"].ToString() + "</td><td>" + Row["BATCH_NO"].ToString() + "</td><td>" + ((Row["EXP_DATE"].ToString() == "") ? "" : ((DateTime)Row["EXP_DATE"]).ToString("dd/MM/yyyy")) + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getExpStock()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Quantity</td><td width='15%'>Batch No.</td><td width='15%'>Exp. Date</td></tr>";

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT A.MED_NAME AS MNAME, curStock(CUR_BAL_STK, B.MED_PACK, A.MED_BIG_UOM, A.MED_SMALL_UOM) AS QTY, B.BATCH_NO AS BATCH_NO, B.EXP_DATE AS EXP_DATE FROM STOCK_DTLD_INFO B JOIN MEDICINE_MST A ON A.MED_ID=B.MED_ID WHERE CUR_BAL_STK > 0 AND DATEDIFF(B.EXP_DATE, CURDATE()) < " + Request.QueryString[0].ToString() + "  ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row["QTY"].ToString() + "</td><td>" + Row["BATCH_NO"].ToString() + "</td><td>" + ((Row["EXP_DATE"].ToString() == "") ? "" : ((DateTime)Row["EXP_DATE"]).ToString("dd/MM/yyyy")) + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getDrugList()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Selling Price - Out</td><td width='15%'>Selling Price - In</td></tr>";

        //List of Drugs with Packing
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME AS MNAME, MED_SELLING_PRICE, MED_OUT_SELLING_COST FROM MEDICINE_MST ORDER BY MNAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row["MED_SELLING_PRICE"].ToString() + "</td><td>" + Row["MED_OUT_SELLING_COST"].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getIssStockVal(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='10%'>Ref. No</td><td width='10%'>Date</td><td width='20%'>Description</td><td width='15%'>Packing</td><td width='15%'>Quantity</td><td width='5%'>Selling</td><td width='10%'>Value[RM]</td></tr>";

        Decimal total = 0;

        //List of Drugs with Packing
        string sql = "SELECT ISSUE_REF_NO, ISSUE_DATE, ISSUE_MED_ID, MED_NAME, MED_SMALL_UOM, MED_BIG_UOM, ISSUE_PACK, curStock(ISSUE_QTY, ISSUE_PACK, MED_BIG_UOM, MED_SMALL_UOM) AS QTY, MED_SELLING_PRICE, (MED_SELLING_PRICE * ISSUE_QTY) AS AMT FROM STOCK_ISSUE_DTLS JOIN MEDICINE_MST ON MED_ID=ISSUE_MED_ID JOIN STOCK_ISSUE_INFO ON ISSUE_REF_ID=STK_REF_ID WHERE ISSUE_DATE BETWEEN  '" + fdate + " 00:00' AND '" + tdate + " 23:59' ORDER BY MED_NAME";
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList(sql);
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row["ISSUE_REF_NO"].ToString() + "</td><td>" + ((DateTime)Row["ISSUE_DATE"]).ToString("dd/MM/yyyy") + "</td><td>" + Row["MED_NAME"].ToString() + "</td><td>" + Row["ISSUE_PACK"].ToString() + " " + Row["MED_SMALL_UOM"].ToString() + "/" + Row["MED_BIG_UOM"].ToString() + "</td><td>" + Row["QTY"].ToString() + "</td><td align='right'>" + ((Decimal)Row["MED_SELLING_PRICE"]).ToString("0.00") + "</td><td align='right'>" + ((Decimal)Row["AMT"]).ToString("0.00") + "</td></tr>";
                total += (Decimal)Row["AMT"];
            }
            rptStr += "<tr><td colspan='7'>Total Amount</td><td align='right'>" + ((Decimal)total).ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    public string getOutletAdj(string ID)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        //Adjustment Info including Supplier details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT ADJ_REF_NO, ADJ_DATE, (SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID = ADJ_OUTLET) AS OUTLET FROM OUTLET_STOCK_ADJUSTMENT_INFO WHERE ADJ_REF_ID='" + ID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<table class='tbl'>";
            rptStr += "<tr><td width='10%'>Ref No</td><td>" + objdl.dataSet.Tables[0].Rows[0][0].ToString() + "</td></tr>";
            rptStr += "<tr><td width='10%'>Outlet</td><td>" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "</td></tr>";
            rptStr += "<tr><td>Date</td><td>" + ((DateTime)objdl.dataSet.Tables[0].Rows[0][1]).ToString("dd/MM/yyyy") + "</td></tr>";
            rptStr += "</table>";
        }

        rptStr += "<br/>";
        rptStr += "<br/>";

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr bg-color='gray'><th width='5%'>#</th><td width='40%'>Description</td><td width='15%'>Physical Stock</td><td width='15%'>Variance</td></tr>";

        //Adjust Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_QTY) AS QTY, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_PHY_QTY) AS PQTY FROM OUTLET_STOCK_ADJUSTMENT_DTLS JOIN MEDICINE_MST ON ADJ_MED_ID=MED_ID WHERE STK_REF_ID='" + ID + "' ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + Row[1] + "</td><td>" + Row[2] + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getReportOne(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='5%'>Drug</td><td width='8%'>Pack</td><td width='25%'>Reorder</td><td width='30%'>Drug Name</td><td width='10%'>Batch No.</td><td width='10%'>Quantity</td><td width='8%' align='right'>Cost[RM]</td><td width='7%' align='right'>GST[RM]</td><td width='8%' align='right'>Amount[RM]</td></tr>";

        decimal totalAmt = 0; decimal totalGST = 0;

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT GRN_INFO.GRN_ID, REC_DATE, SUPPLIER_NAME, MED_NAME, MED_BATCH_NO, getMedQuantity(GRN_DTLS.MED_ID, MED_PACKING, MED_REC_QTY) AS QTY, MED_COST, MED_GST_AMT, MED_AMT, INV_DATE  FROM GRN_DTLS JOIN MEDICINE_MST ON GRN_DTLS.MED_ID=MEDICINE_MST.MED_ID  JOIN GRN_INFO ON GRN_INFO.GRN_ID=GRN_DTLS.GRN_ID JOIN SUPPLIER_MST ON SUPPLIER_MST.SUPPLIER_ID = GRN_INFO.SUPPLIER_ID WHERE INV_DATE BETWEEN '" + fdate + " 00:00' AND '" + tdate + " 23:59' ORDER BY REC_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy") + "<br/>" + ((DateTime)Row[9]).ToString("dd/MM/yyyy") + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + "</td><td>" + Row[5] + "</td><td align='right'>" + Row[6] + "</td><td align='right'>" + Row[7] + "</td><td align='right'>" + Row[8] + "</td></tr>";
                totalAmt += decimal.Parse(Row[8].ToString());
                totalGST += decimal.Parse(Row[7].ToString());
            }
            rptStr += "<tr><td colspan='8'>Total Amount</td><td align='right'>" + totalGST.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getAuditReport(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='10%'>Time In</td><td width='10%'>Time Out</td><td width='65%'>Patient Name (IC No)</td><td>New/Old</td></tr>";

        string fDisc = ""; string nDisc = "";

        ArrayList summaryRow = new ArrayList();

        int temp = 0; int tempTot = 0; int seq = 0;

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_QUEUE_DATE, PAT_TIME_OUT, PAT_NAME, PAT_IC_NO, PAT_DISC, COMP_NAME, PAT_QUEUE_NEW_OLD FROM PAT_QUEUE JOIN COMP_MST ON PAT_DISC = COMP_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = PAT_QUEUE.PAT_ID WHERE PAT_QUEUE_DATE BETWEEN '" + fdate + " 00:00' AND '" + fdate + " 23:59' ORDER BY PAT_DISC, PAT_QUEUE_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                fDisc = Row[5].ToString();
                if (fDisc != nDisc)
                {
                    rptStr += "<tr><td colspan='5'>" + Row[5].ToString() + "</td></tr>";
                    tempTot = temp;
                    if (tempTot > 0)
                    {
                        List<string> summaryCol = new List<string>();
                        summaryCol.Add((seq + 1).ToString());
                        summaryCol.Add(nDisc);
                        summaryCol.Add(tempTot.ToString());

                        summaryRow.Add(summaryCol);
                        seq++;

                        tempTot = 0;
                    }
                    nDisc = fDisc;
                    temp = 0;
                }
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("HH:mm") + "</td><td>" + (DBNull.Value.Equals(Row[1]) ? "" : ((DateTime)Row[1]).ToString("HH:mm")) + "</td><td>" + Row[2].ToString() + "(" + Row[3].ToString() + ")</td><td>" + Row[6].ToString() + "</td></tr>";
                temp++;
            }
            List<string> summaryLCol = new List<string>();
            summaryLCol.Add((seq + 1).ToString());
            summaryLCol.Add(nDisc);
            summaryLCol.Add(temp.ToString());

            summaryRow.Add(summaryLCol);

            rptStr += "<tr><td colspan='5'>Summary</td></tr>";
            foreach (List<string> col in summaryRow)
            {
                rptStr += "<tr><td>" + col[0] + "</td><td colspan='3'>" + col[1] + "</td><td>" + col[2] + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getTrafficReport(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><th width='5%'>#</th><th width='5%'>Time In</th><th width='5%'>Time Out</th><th width='5%'>Folder No.</th><th width='5%'>Cash Bill.</th><th width='30%'>Patient Name/IC</th><th width='10%'>Discipline</th><th width='5%'>Discount</th><th width='5%' align='right'>Total</th><th width='5%' align='right'>Cash[RM]</th><th width='5%' align='right'>Card[RM]</th><th width='5%' align='right'>Panel[RM]</th><th width='5%' align='right'>Owing[RM]</th></tr>";

        decimal totalAmt = 0; decimal totalDisc = 0; decimal totalPanel = 0; decimal totalCash = 0; decimal totalCard = 0; decimal totalOwing = 0;
        decimal cardAmt = 0; decimal cashAmt = 0; decimal panelAmt = 0; decimal owingAmt = 0; decimal charges = 0; decimal discount = 0; decimal paidAmt = 0;

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PQ.PAT_QUEUE_DATE, VISIT_TIME_OUT, PR.PAT_REG_NO, PVM.CASH_BILL_NO, PR.PAT_NAME, PR.PAT_IC_NO, (SELECT COMP_NAME FROM COMP_MST WHERE COMP_ID = PQ.PAT_DISC) AS DISC, PVM.VISIT_DISCOUNT, PVM.VISIT_TOT_AMT, RM.PAID_AMT, RM.PAYMENT_MODE, PVM.COMPANY_ID, RM.RECEIPT_NO FROM PATIENT_VISIT_MST PVM JOIN PATIENT_REGISTRATION PR ON PR.PAT_ID = PVM.PAT_ID LEFT OUTER JOIN RECEIPT_MST RM ON RM.VISIT_ID = PVM.VISIT_ID JOIN PAT_QUEUE PQ ON PQ.VISIT_ID = PVM.VISIT_ID WHERE PVM.VISIT_DATE BETWEEN '" + fdate + " 00:00:00' AND '" + tdate + " 23:59:59' ORDER BY PQ.PAT_QUEUE_DATE");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            //11 Company 12 Receipt No

            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                totalDisc += decimal.Parse(Row[7].ToString());

                totalAmt += decimal.Parse(Row[8].ToString());
                charges = decimal.Parse(Row[8].ToString());
                discount = decimal.Parse(Row[7].ToString());

                if (!DBNull.Value.Equals(Row[12]))
                {
                    objDL recDtls = new objDL();
                    recDtls = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PMODE, PAMT FROM RECEIPT_DTLS WHERE RECEIPT_NO = '" + Row[12].ToString() + "'");
                    if (recDtls.flaG == true)
                    {
                        for (int increment = 0; increment < recDtls.dataSet.Tables[0].Rows.Count; increment++)
                        {
                            DataRow dr = recDtls.dataSet.Tables[0].Rows[increment];
                            if (dr[0].ToString() == "108")
                            {
                                cashAmt = decimal.Parse(dr[1].ToString());
                                paidAmt += cashAmt;
                            }
                            else if (dr[0].ToString() == "110")
                            {
                                cardAmt = decimal.Parse(dr[1].ToString());
                                paidAmt += cardAmt;
                            }
                        }
                    }
                    else
                    {
                        owingAmt = (charges - discount);
                    }
                }
                else
                {
                    owingAmt = (charges - discount);
                }

                owingAmt = (charges - discount) - paidAmt;

                if (Row[11].ToString() != "1")
                {
                    panelAmt = owingAmt;
                    owingAmt = 0;
                }
                totalOwing += owingAmt; totalCash += cashAmt; totalCard += cardAmt; totalPanel += panelAmt;
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("HH:mm") + "</td><td>" + ((DateTime)Row[1]).ToString("HH:mm") + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + " (" + Row[5] + ")</td><td>" + Row[6] + "</td><td align='right'>" + ((decimal)Row[7]).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)Row[8]).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)cashAmt).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)cardAmt).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)panelAmt).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)owingAmt).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                cardAmt = 0; cashAmt = 0; panelAmt = 0; owingAmt = 0; charges = 0; discount = 0; paidAmt = 0;
                //rptStr += "<tr><td>" + Row[11] + "</td><td>" + ((DateTime)Row[0]).ToString("HH:mm") + "</td><td>" + ((DateTime)Row[1]).ToString("HH:mm") + "</td><td>" + Row[2] + "</td><td>" + Row[3] + "</td><td>" + Row[4] + " (" + Row[5] + ")</td><td>" + Row[6] + "</td><td align='right'>" + (discount==0?"":((decimal)discount).ToString("N", new CultureInfo("en-US"))) + "</td><td align='right'>" + (charges==0?"":((decimal)charges).ToString("N", new CultureInfo("en-US"))) + "</td><td align='right'>" + (cashAmt==0?"":((decimal)cashAmt).ToString("N", new CultureInfo("en-US"))) + "</td><td align='right'>" + (cardAmt==0?"":((decimal)cardAmt).ToString("N", new CultureInfo("en-US"))) + "</td><td align='right'>" + (panelAmt==0?"":((decimal)panelAmt).ToString("N", new CultureInfo("en-US"))) + "</td><td align='right'>" + (owingAmt==0?"":((decimal)owingAmt).ToString("N", new CultureInfo("en-US"))) + "</td></tr>";
            }
            rptStr += "<tr><td colspan='7'>Total Amount</td><td align='right'>" + totalDisc.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalCash.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalCard.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalPanel.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalOwing.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string printReceipt(string strDate)
    {
        // Receipt No
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='0' cellpadding='2' cellspacing='2' class='tbl'>";
        rptStr += "<tr><td width='15%'>&nbsp;</td><td width='25%'>&nbsp;</td><td width='10%'>&nbsp;</td><td width='20%'>&nbsp;</td></tr>";

        string rec_number = "";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, RECEIPT_NO, REC_DATE, PAID_AMT, REMARKS  FROM RECEIPT_MST JOIN PATIENT_REGISTRATION ON RECEIPT_MST.PAT_ID=PATIENT_REGISTRATION.PAT_ID WHERE RECEIPT_NO = '" + strDate + "' OR VISIT_ID = '" + strDate + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataRow Row = objdl.dataSet.Tables[0].Rows[0];
            rec_number = Row[2].ToString();
            rptStr += "<tr><td>Date : </td><td>" + ((DateTime)Row[3]).ToString("dd/MM/yyyy") + "</td><td>&nbsp;</td><td align='right'>Receipt No." + Row[2] + "</td></tr>";
            rptStr += "<tr><td colspan='4'>&nbsp;</td></tr>";
            rptStr += "<tr><td>Received from :</td><td colspan='3'>" + Row[0] + "</td></tr>";
            rptStr += "<tr><td>IC No.:</td><td colspan='3'>" + Row[1] + "</td></tr>";
            //rptStr += "<tr><td>Ringgit :</td><td></td></tr>";
            rptStr += "<tr><td>Being payment for :</td><td colspan='3'>Medical Fees</td></tr>";
            rptStr += "<tr><td colspan='4'>&nbsp;</td></tr>";
            rptStr += "<tr><td align='right' colspan='4'>RM " + ((decimal)Row[4]).ToString("0.00") + "</td></tr>";
        }
        rptStr += "<tr><td colspan='4'>Breakdown</td></tr>";
        rptStr += "<tr><td>#</td><td>Pay Mode</td><td>Amount</td><td>Remarks</td></tr>";
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PARAM_NAME, PAMT, PDTLS FROM RECEIPT_DTLS JOIN PARAMETERS_INFO ON PMODE = PARAM_ID WHERE RECEIPT_NO = '" + rec_number + "'");
        if (objdl.flaG == true)
        {
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                rptStr += "<tr><td>" + (inc + 1) + "</td><td>" + objdl.dataSet.Tables[0].Rows[inc][0].ToString() + "</td><td align='right'>" + ((decimal)objdl.dataSet.Tables[0].Rows[inc][1]).ToString("0.00") + "</td><td>" + objdl.dataSet.Tables[0].Rows[inc][2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string printReceiptBreakdown(string strDate)
    {
        // Receipt No
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='0' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='15%'>&nbsp;</td><td width='25%'>&nbsp;</td><td width='10%'>&nbsp;</td><td width='20%'>&nbsp;</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, VISIT_ID, VISIT_DATE  FROM PATIENT_VISIT_MST JOIN PATIENT_REGISTRATION ON PATIENT_VISIT_MST.PAT_ID=PATIENT_REGISTRATION.PAT_ID WHERE VISIT_ID = '" + strDate + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataRow Row = objdl.dataSet.Tables[0].Rows[0];
            rptStr += "<tr><td>Name :</td><td>" + Row[0] + "</td><td>INVOICE No.</td><td>" + Row[2] + "</td></tr>";
            rptStr += "<tr><td>IC No.:</td><td>" + Row[1] + "</td><td>Date : </td><td>" + ((DateTime)Row[3]).ToString("dd/MM/yyyy") + "</td></tr>";
        }

        //Drugs
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, MED_UNIT_PRICE, MED_QTY, MED_AMT FROM VISIT_MEDICINE_DTLS JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID = VISIT_MEDICINE_DTLS.MED_ID WHERE VISIT_ID = '" + strDate + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<tr><td colspan='4'><u>List of Drugs</u></td></tr>";
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                DataRow Col = objdl.dataSet.Tables[0].Rows[inc];
                rptStr += "<tr><td colspan='2'>" + Col[0] + "</td><td align='right'>" + Col[2] + "</td><td align='right'>" + ((decimal)Col[3]).ToString("0.00") + "</td></tr>";
            }
        }

        rptStr += "<tr><td colspan='4'>&nbsp;</td></tr>";
        rptStr += "<tr><td colspan='4'>&nbsp;</td></tr>";

        //Services--Charges
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SERVICE_NAME, SERVICE_MOD_AMT FROM VISIT_SERVICE_DTLS JOIN SERVICE_MST ON SERVICE_MST.SERVICE_ID = VISIT_SERVICE_DTLS.SERVICE_ID WHERE VISIT_ID = '" + strDate + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            decimal totAmt = 0;
            rptStr += "<tr><td colspan='4'><u>List of Services/Charges</u></td></tr>";
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                DataRow Col = objdl.dataSet.Tables[0].Rows[inc];
                rptStr += "<tr><td colspan='2'>" + Col[0] + "</td><td colspan='2' align='right'>" + ((decimal)Col[1]).ToString("0.00") + "</td></tr>";
                totAmt += (decimal)Col[1];
            }
            rptStr += "<tr><td colspan='4'>&nbsp;</td></tr>";
            rptStr += "<tr><td>Nett Amount</td><td colspan='3' align='right'>" + totAmt.ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string printDrugDispense(string strDate)
    {
        // Receipt No
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        decimal totAmt = 0;

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='2%'>&nbsp;</td><td width='10%'>Visit Date</td><td width='68%'>Patient Name</td><td width='5%'>S.Price</td><td width='5%'>Qty</td><td width='5%'>Amount</td></tr>";
        //Drugs
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, MED_NAME, MED_UNIT_PRICE, MED_QTY, MED_AMT, VISIT_DATE FROM VISIT_MEDICINE_DTLS JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID = VISIT_MEDICINE_DTLS.MED_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=VISIT_MEDICINE_DTLS.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_VISIT_MST.PAT_ID = PATIENT_REGISTRATION.PAT_ID WHERE VISIT_DATE BETWEEN '" + fdate + "' AND '" + tdate + "' AND VISIT_MEDICINE_DTLS.MED_ID='" + strDate.Split('.')[1] + "' ORDER BY VISIT_DATE, PAT_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            rptStr += "<tr><td colspan='6'>" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "</td></tr>";

            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                DataRow Col = objdl.dataSet.Tables[0].Rows[inc];
                rptStr += "<tr><td>" + (inc + 1) + "</td><td>" + ((DateTime)Col[6]).ToString("dd/MM/yyyy") + "</td><td>" + Col[0] + "  [" + Col[1] + "]</td><td align='right'>" + ((decimal)Col[3]).ToString("0.00") + "</td><td align='right'>" + Col[4] + "</td><td align='right'>" + ((decimal)Col[5]).ToString("0.00") + "</td></tr>";
                totAmt += (decimal)Col[5];
            }
            rptStr += "<tr><td colspan='2'>Total</td><td align='right' colspan='4'>" + ((decimal)totAmt).ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string printDrugsDispense(string strDate)
    {
        // Receipt No
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        decimal totAmt = 0;
        string strF = "";
        string strN = "";

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='2%'>&nbsp;</td><td width='10%'>Visit Date</td><td width='68%'>Patient Name</td><td width='5%'>S.Price</td><td width='5%'>Qty</td><td width='5%'>Amount</td></tr>";
        //Drugs
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, MED_NAME, MED_UNIT_PRICE, MED_QTY, MED_AMT, VISIT_DATE FROM VISIT_MEDICINE_DTLS JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID = VISIT_MEDICINE_DTLS.MED_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=VISIT_MEDICINE_DTLS.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_VISIT_MST.PAT_ID = PATIENT_REGISTRATION.PAT_ID WHERE VISIT_DATE BETWEEN '" + fdate + "' AND '" + tdate + "' ORDER BY MED_NAME, VISIT_DATE, PAT_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                DataRow Col = objdl.dataSet.Tables[0].Rows[inc];

                strF = Col[2].ToString();
                if (strF != strN)
                {
                    rptStr += "<tr><td colspan='6'>" + Col[2] + "</td></tr>";
                    strN = strF;
                }
                rptStr += "<tr><td>" + (inc + 1) + "</td><td>" + ((DateTime)Col[6]).ToString("dd/MM/yyyy") + "</td><td>" + Col[0] + "  [" + Col[1] + "]</td><td align='right'>" + ((decimal)Col[3]).ToString("0.00") + "</td><td align='right'>" + Col[4] + "</td><td align='right'>" + ((decimal)Col[5]).ToString("0.00") + "</td></tr>";
                totAmt += (decimal)Col[5];
            }
            rptStr += "<tr><td colspan='2'>Total</td><td align='right' colspan='4'>" + ((decimal)totAmt).ToString("0.00") + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPaymentCard(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><th width='5%'>#</th><th width='5%'>Cash Bill</th><th width='20%'>Visit Date</th><th width='50%'>Patient Name/IC</th><th width='10%'>Discount</th><th width='10%' align='right'>Amount</th></tr>";

        decimal totalAmt = 0; decimal totalDisc = 0;

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_TIME, CASH_BILL_NO, PAT_NAME, PAT_IC_NO, VISIT_DISCOUNT, PAID_AMT FROM PATIENT_VISIT_MST, PATIENT_REGISTRATION, RECEIPT_MST WHERE PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID AND PATIENT_VISIT_MST.VISIT_ID = RECEIPT_MST.VISIT_ID AND PAYMENT_MODE =2 AND VISIT_DATE BETWEEN '" + fdate + " 00:00:00.0000' AND '" + tdate + " 23:59:59.0000' ORDER BY VISIT_TIME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[1] + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy HH:mm") + "</td><td>" + Row[2] + "[" + Row[3] + "]</td><td align='right'>" + ((decimal)Row[4]).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)Row[5]).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                totalDisc += decimal.Parse(Row[4].ToString());
                totalAmt += decimal.Parse(Row[5].ToString());
            }
            rptStr += "<tr><td colspan='4'>Total Amount</td><td align='right'>" + totalDisc.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPaymentPanel(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><th width='5%'>#</th><th width='5%'>Cash Bill</th><th width='20%'>Visit Date</th><th width='50%'>Patient Name/IC</th><th width='10%'>Discount</th><th width='10%' align='right'>Amount</th></tr>";

        decimal totalAmt = 0; decimal totalDisc = 0; string strF = ""; string strN = "";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_TIME, CASH_BILL_NO, PAT_NAME, PAT_IC_NO, VISIT_DISCOUNT, (VISIT_TOT_AMT- IF (PAID_AMT IS NULL, 0,PAID_AMT)), COMPANY_NAME FROM PATIENT_VISIT_MST JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID LEFT JOIN RECEIPT_MST ON RECEIPT_MST.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN COMPANY_MST ON COMPANY_MST.COMPANY_ID = PATIENT_VISIT_MST.COMPANY_ID WHERE VISIT_DATE BETWEEN '" + fdate + " 00:00:00.0000' AND '" + tdate + " 23:59:59.0000' AND PATIENT_VISIT_MST.COMPANY_ID != 1 ORDER BY COMPANY_NAME, VISIT_TIME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];

                strF = Row[6].ToString();
                if (strF != strN)
                {
                    rptStr += "<tr><td colspan='6'>" + Row[6] + "</td></tr>";
                    strN = strF;
                }
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[1] + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy HH:mm") + "</td><td>" + Row[2] + "[" + Row[3] + "]</td><td align='right'>" + ((decimal)Row[4]).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)Row[5]).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                totalDisc += decimal.Parse(Row[4].ToString());
                totalAmt += decimal.Parse(Row[5].ToString());
            }
            rptStr += "<tr><td colspan='4'>Total Amount</td><td align='right'>" + totalDisc.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPaymentCash(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='1' cellspacing='0' class='tbl'>";
        rptStr += "<tr><th width='5%'>#</th><th width='5%'>Cash Bill</th><th width='20%'>Visit Date</th><th width='50%'>Patient Name/IC</th><th width='10%'>Discount</th><th width='10%' align='right'>Amount</th></tr>";

        decimal totalAmt = 0; decimal totalDisc = 0;

        //Details
        //objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_TIME, CASH_BILL_NO, PAT_NAME, PAT_IC_NO, VISIT_DISCOUNT, PAID_AMT FROM PATIENT_VISIT_MST, PATIENT_REGISTRATION, RECEIPT_MST WHERE PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID AND PATIENT_VISIT_MST.VISIT_ID = RECEIPT_MST.VISIT_ID AND PAYMENT_MODE = '" + strDate.Split('.')[1] + "' AND VISIT_DATE BETWEEN '" + fdate + " 00:00:00.0000' AND '" + tdate + " 23:59:59.0000' ORDER BY VISIT_TIME");
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_TIME, CASH_BILL_NO, PAT_NAME, PAT_IC_NO, VISIT_DISCOUNT, PAID_AMT FROM PATIENT_VISIT_MST, PATIENT_REGISTRATION, RECEIPT_MST, RECEIPT_DTLS WHERE PATIENT_REGISTRATION.PAT_ID = PATIENT_VISIT_MST.PAT_ID AND PATIENT_VISIT_MST.VISIT_ID = RECEIPT_MST.VISIT_ID AND RECEIPT_MST.RECEIPT_NO = RECEIPT_DTLS.RECEIPT_NO AND PMODE = '" + strDate.Split('.')[1] + "' AND VISIT_DATE BETWEEN '" + fdate + " 00:00:00.0000' AND '" + tdate + " 23:59:59.0000' ORDER BY VISIT_TIME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[1] + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy HH:mm") + "</td><td>" + Row[2] + "[" + Row[3] + "]</td><td align='right'>" + ((decimal)Row[4]).ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + ((decimal)Row[5]).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                totalDisc += decimal.Parse(Row[4].ToString());
                totalAmt += decimal.Parse(Row[5].ToString());
            }
            rptStr += "<tr><td colspan='4'>Total Amount</td><td align='right'>" + totalDisc.ToString("N", new CultureInfo("en-US")) + "</td><td align='right'>" + totalAmt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getDrugPriceList()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='65%'>Drug Name</td><td align='right'>U.Cost[RM]</td><td align='right' width='10%'>Out [RM]</td><td align='right' width='10%'>In [RM]</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, MED_OUT_SELLING_COST, MED_SELLING_PRICE, MED_UNIT_COST FROM MEDICINE_MST ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td align='right'>" + ((decimal)Row[3]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[1]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[2]).ToString("0.00") + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getServicesPriceList()
    {

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='85%'>SERVICE NAME</td><td align='right' width='10%'>Charge[RM]</td></tr>";


        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SERVICE_NAME, SERVICE_CHARGE FROM SERVICE_MST ORDER BY SERVICE_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td align='right'>" + ((decimal)Row[1]).ToString("0.00") + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getPatientList()
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='35%'>Patient Name</td><td width='10%'>IC No</td><td width='10%'>Folder No</td><td width='10%'>Nationality</td><td width='10%'>Regn. Date</td><td width='10%'>Birth Date</td><td width='10%'>Visits</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, PAT_REG_NO, PAT_NATIONALITY, PAT_REG_DATE, PAT_BIRTH_DATE, (SELECT COUNT(1) FROM PATIENT_VISIT_MST WHERE PATIENT_VISIT_MST.PAT_ID = PATIENT_REGISTRATION.PAT_ID) AS NOVISITS FROM PATIENT_REGISTRATION ORDER BY PAT_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[1].ToString() + "</td><td>" + Row[2].ToString() + "</td><td>" + Row[3].ToString() + "</td><td>" + Row[4].ToString() + "</td><td>" + Row[5].ToString() + "</td><td>" + Row[6].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getLoginDetails(string strDate)
    {
        // 01122015 01122015
        string fdate = strDate.Substring(4, 4) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(0, 2);
        string tdate = strDate.Substring(12, 4) + "-" + strDate.Substring(10, 2) + "-" + strDate.Substring(8, 2);

        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='40%'>Date</td><td width='30%'>Login ID</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT USER_LOGIN_ID, LOG_TIME, getLogStatus(LOG_TYPE) FROM USER_LOG WHERE LOG_TIME BETWEEN '" + fdate + " 00:00' AND '" + fdate + " 23:59' ORDER BY TRAN_ID DESC");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy HH:mm") + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getUsersList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='30%'>User Name</td><td width='20%'>Login ID</td><td width='20%'>User Type</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT USER_NAME, USER_LOGIN_ID, USER_TYPE, USER_STATUS FROM USER_MST ORDER BY USER_LOGIN_ID");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[1].ToString() + "</td><td>" + getUserType((int)Row[2]) + "</td><td>" + Row[3].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getUserType(int uType)
    {
        string returnVal = "";
        switch (uType)
        {
            case 0:
                returnVal = "Super Admin";
                break;
            case 1:
                returnVal = "Administrator";
                break;
            case 2:
                returnVal = "Doctor";
                break;
            case 3:
                returnVal = "Cashier";
                break;
            case 4:
                returnVal = "Pharmacy";
                break;
            case 5:
                returnVal = "Reception";
                break;
            case 6:
                returnVal = "Office";
                break;
            case 7:
                returnVal = "Inventory";
                break;
            case 8:
                returnVal = "Billing";
                break;
            case 9:
                returnVal = "Patient";
                break;
            default:
                returnVal = "N/A";
                break;
        }
        return returnVal;
    }
    private string getComponentsList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='50%'>Description</td><td width='20%'>Type</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMP_NAME, COMP_TYPE, 'A' AS COMP_STATUS FROM COMP_MST ORDER BY COMP_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + getComponentType(Row[1].ToString()) + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getComponentType(string cType)
    {
        string returnVal = "";

        switch (cType)
        {
            case "0":
                returnVal = "Diagnosis";
                break;
            case "3":
                returnVal = "Discipline";
                break;
            case "5":
                returnVal = "Nationality";
                break;
            default:
                returnVal = "N/A";
                break;
        }
        return returnVal;
    }
    private string getDoctorsList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='50%'>Name</td><td width='20%'>Specialization</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT DOC_NAME, (SELECT COMP_NAME FROM COMP_MST WHERE COMP_ID = DOC_SPECIALIZATION), 'A' AS DOC_STATUS FROM DOCTOR_MST ORDER BY DOC_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[1].ToString() + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getServicesList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='50%'>Description</td><td width='30%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT SERVICE_NAME, SERVICE_TYPE, 'A' AS SERVICE_STATUS FROM SERVICE_MST ORDER BY SERVICE_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getParametersList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='50%'>Description</td><td width='20%'>Type</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PARAM_NAME, PARAM_TYPE, 'A' AS PARAM_STATUS FROM PARAMETERS_INFO ORDER BY PARAM_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[1].ToString() + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getAllergiesList(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='10%'>#</td><td width='50%'>Description</td><td width='20%'>&nbsp;</td><td width='10%'>Status</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT ALLERGY_NAME, '', 'A' AS ALLERGY_STATUS FROM ALLERGIES_MST ORDER BY ALLERGY_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td>" + Row[1].ToString() + "</td><td>" + Row[2].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getDrugsMarkup(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='30%'>Drug Name</td><td align='right'>Cost[RM]</td><td align='right' width='10%'>Unit [RM]</td><td align='right' width='10%'>Out [RM]</td><td align='right' width='10%'>Markup [%]</td><td align='right' width='10%'>In [RM]</td><td align='right' width='10%'>Markup [%]</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, MED_COST_PRICE, MED_UNIT_COST, MED_OUT_SELLING_COST, MED_OUT_MARK_UP, MED_SELLING_PRICE, MED_MARK_UP FROM MEDICINE_MST WHERE MED_TYPE != 223 ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td align='right'>" + ((decimal)Row[1]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[2]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[3]).ToString("0.00") + "</td><td align='right'>" + Row[4].ToString() + "</td><td align='right'>" + ((decimal)Row[5]).ToString("0.00") + "</td><td align='right'>" + Row[6].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getItemsMarkup(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' cellpadding='2' cellspacing='0' class='tbl'>";
        rptStr += "<tr><td width='5%'>#</td><td width='30%'>Drug Name</td><td align='right'>Cost[RM]</td><td align='right' width='10%'>Unit [RM]</td><td align='right' width='10%'>Out [RM]</td><td align='right' width='10%'>Markup [%]</td><td align='right' width='10%'>In [RM]</td><td align='right' width='10%'>Markup [%]</td></tr>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT MED_NAME, MED_COST_PRICE, MED_UNIT_COST, MED_OUT_SELLING_COST, MED_OUT_MARK_UP, MED_SELLING_PRICE, MED_MARK_UP FROM MEDICINE_MST WHERE MED_TYPE = 223 ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0].ToString() + "</td><td align='right'>" + ((decimal)Row[1]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[2]).ToString("0.00") + "</td><td align='right'>" + ((decimal)Row[3]).ToString("0.00") + "</td><td align='right'>" + Row[4].ToString() + "</td><td align='right'>" + ((decimal)Row[5]).ToString("0.00") + "</td><td align='right'>" + Row[6].ToString() + "</td></tr>";
            }
        }
        rptStr += "</table>";
        return rptStr;
    }
    private string getMedicalExam(string strDate)
    {
        string rptStr = "";
        objDL objdl = new objDL();

        rptStr += "<table border='1' width='100%' cellpadding='0' cellspacing='0'><tr><td>";
        rptStr += "<table border='0' cellpadding='2' cellspacing='5' class='tbl'>";
        rptStr += "<tr><td></td>";

        //Details
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAT_NAME, PAT_IC_NO, PAT_SEX, PAT_BIRTH_DATE AS AGE, PAT_NATIONALITY, PAT_HANDPHONE, VISIT_DATE, DOC_ID AS DOC, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27 FROM PAT_ME_INFO JOIN PATIENT_VISIT_MST ON PAT_ME_INFO.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN PATIENT_REGISTRATION ON PATIENT_VISIT_MST.PAT_ID = PATIENT_REGISTRATION.PAT_ID WHERE PAT_ME_INFO.VISIT_ID = '" + strDate + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];

                rptStr += "<tr><td>Name</td><td>" + Row["PAT_NAME"].ToString() + "</td><td>Gender</td><td>" + Row["PAT_SEX"].ToString() + "</td><td>Age</td><td>" + Row["AGE"] + "</td></tr>";
                rptStr += "<tr><td>NRIC No.</td><td>" + Row["PAT_IC_NO"].ToString() + "</td><td>Occupation</td><td>" + Row["F1"] + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td>Nationality</td><td>" + Row["PAT_NATIONALITY"].ToString() + "</td><td>Contact No.</td><td>" + Row["PAT_HANDPHONE"].ToString() + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td>Date of Exam</td><td>" + ((DateTime)Row["VISIT_DATE"]).ToString("dd/MM/yyyy") + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";

                rptStr += "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";

                rptStr += "<tr><td>Family History</td><td colspan='5'>" + Row["F3"].ToString() + "</td></tr>";
                rptStr += "<tr><td>Past History</td><td colspan='5'>" + Row["F4"].ToString() + "</td></tr>";
                rptStr += "<tr><td>Allergy History</td><td colspan='5'>" + Row["F5"] + "</td></tr>";
                rptStr += "<tr><td>Present Complaints</td><td colspan='5'>" + Row["F6"] + "</td></tr>";
                rptStr += "<tr><td>Height (cm)</td><td>" + Row["F7"] + "</td><td>Weight (kg)</td><td>" + Row["F8"] + "</td><td>BMI</td><td>" + Row["F9"] + "</td></tr>";
                rptStr += "<tr><td>VISION</td><td>LEFT</td><td>RIGHT</td><td>&nbsp;</td><td>Blood Pressure</td><td>" + Row["F19"] + "</td></tr>";
                rptStr += "<tr><td>UnCorrected</td><td>" + Row["F10"] + "</td><td>" + Row["F11"] + "</td><td>&nbsp;</td><td>Pulse Rate /min</td><td>" + Row["F20"] + "</td></tr>";
                rptStr += "<tr><td>Corrected</td><td>" + Row["F12"] + "</td><td>" + Row["F13"] + "</td><td>&nbsp;</td><td>Colour Vision</td><td>" + Row["F21"] + "</td></tr>";
                //Physical Exam 
                rptStr += "<tr><td>Y If Normal</td><td>N If Abnormal</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td colspan='4'>";
                rptStr += "<table width='100%' cellspacing='0' cellpadding='5' border='1'>";

                objDL objX = new objDL();
                objX = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMP_NAME, RESULTS FROM PAT_ME_INFO1 JOIN COMP_MST ON COMP_ID = PARAM_ID WHERE VISIT_ID = '" + strDate + "'"); ;

                for (int m = 0; m < objX.dataSet.Tables[0].Rows.Count; m++)
                {
                    rptStr += "<tr><td>" + objX.dataSet.Tables[0].Rows[m]["COMP_NAME"].ToString() + "</td><td>" + objX.dataSet.Tables[0].Rows[m]["RESULTS"].ToString() + "</td>";
                    m++;
                    if (m <= (objX.dataSet.Tables[0].Rows.Count-1))
                    {
                        rptStr += "<td>" + objX.dataSet.Tables[0].Rows[m]["COMP_NAME"].ToString() + "</td><td>" + objX.dataSet.Tables[0].Rows[m]["RESULTS"].ToString() + "</td></tr>";
                    }
                    else
                    {
                        rptStr += "<td>&nbsp;</td><td>&nbsp;</td></tr>";
                    }
                }
                rptStr += "</table>";
                rptStr += "</td><td>Remarks</td><td colspan='2'>" + Row["F26"] + "</td></tr>";
                //End
                rptStr += "<tr><td>Urine:</td><td>Protien</td><td>" + Row["F14"] + "</td><td>pH</td><td>" + Row["F15"] + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td>&nbsp;</td><td>Sugar</td><td>" + Row["F16"] + "</td><td>SG</td><td>" + Row["F17"] +"</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td>&nbsp;</td><td>Micro</td><td>" + Row["F18"] + "</td><td>Drug Screen<br/>[Qualitative]</td><td>" + Row["F22"] + "</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
                rptStr += "<tr><td>Other Test</td><td colspan='5'>" + Row["F25"] + "</td></tr>";
                rptStr += "<tr><td>Chest X-Ray Report</td><td colspan='5'>" + Row["F23"] + "</td></tr>";
                rptStr += "<tr><td>Findings/ Recommendations</td><td colspan='5'>" + Row["F24"] + "</td></tr>";
                rptStr += "<tr><td>Conclusion</td><td colspan='5'>" + Row["F27"] + "</td></tr>";
                rptStr += "<tr><td colspan='7'>&nbsp;</td></tr>";
                rptStr += "<tr><td colspan='7'>&nbsp;</td></tr>";
                rptStr += "<tr><td colspan='7'>&nbsp;</td></tr>";
                rptStr += "<tr><td>" + Row["DOC"] + "</td><td>Signature</td><td>&nbsp;</td><td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td><td>&nbsp;</td><td>Clinic Stamp</td></tr>";
            }
        }
        rptStr += "</table>";
        rptStr += "</td></tr></table>";
        return rptStr;
    }
    private string getTitle(string sTitle, string strDate)
    {
        string rptStr = "";

        string fdt = "";
        string tdt = "";

        if (strDate != "" && strDate.Count() >= 16)
        {
            fdt = strDate.Substring(0, 2) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(4, 4);
            tdt = strDate.Substring(0, 2) + "-" + strDate.Substring(2, 2) + "-" + strDate.Substring(4, 4);
        }
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT CLINIC_NAME, CLINIC_ADDR1, CLINIC_ADDR2, CLINIC_ADDR3, CLINIC_PHONE_O, CLINIC_FAX, CLINIC_EMAIL, CLINIC_WEBSITE FROM CLINIC_MST");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            //Clinic or Company details
            rptStr += "<table border='0' cellpadding='0' cellspacing='0' class='tbl'>";
            rptStr += "<tr><th align='center'><font size='2'>" + objdl.dataSet.Tables[0].Rows[0][0].ToString().ToUpper() + "</font></th></tr>";
            rptStr += "<tr><td align='center'><font size='1'>" + objdl.dataSet.Tables[0].Rows[0][1].ToString().ToUpper() + ", " + objdl.dataSet.Tables[0].Rows[0][2].ToString().ToUpper() + ", " + objdl.dataSet.Tables[0].Rows[0][3].ToString().ToUpper() + ", Tel: " + objdl.dataSet.Tables[0].Rows[0][4].ToString() + " Fax: " + objdl.dataSet.Tables[0].Rows[0][5].ToString() + "</font></td></tr>";
            rptStr += "<tr><td align='center'><font size='1'>Email: " + objdl.dataSet.Tables[0].Rows[0][6].ToString() + ", Web: " + objdl.dataSet.Tables[0].Rows[0][7].ToString() + "</td></tr>";
            rptStr += "<tr><th><br/><br/>" + sTitle + "</th></tr>";
            if (strDate != "" && strDate.Count() >= 16)
            {
                rptStr += "<tr><th>For the period of " + fdt + " - " + tdt + "</th></tr>";
            }
            rptStr += "</table>";
        }

        return rptStr;
    }
    private string getFooter()
    {
        string rptStr = "";

        rptStr += "<div id='divFooter'>";
        rptStr += "<table border='0' cellpadding='1' cellspacing='1' class='foot'>";
        rptStr += "<tr><td align='left'>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</td><td align='right'>User: " + Session["username"].ToString() + "</td></tr>";
        rptStr += "</table>";
        rptStr += "</div>";

        return rptStr;
    }
    [System.Web.Services.WebMethod]
    public static string getCurrentTime()
    {
        return DateTime.Now.ToString("dddd dd MMMM yyyy hh:mm:ss tt");
    }
}
