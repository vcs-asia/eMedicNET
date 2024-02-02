using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Vijay;
using System.Globalization;

public partial class Billing_PrintInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.convertToPDF();
    }
    private void SaveToPDF()
    {
        /*
        StringWriter writer = new StringWriter();
        HtmlTextWriter writer2 = new HtmlTextWriter(writer);
        this.Page.RenderControl(writer2);
        StringReader reader = new StringReader(writer.ToString());
        Document document = new Document(PageSize.A4.Rotate(), 20f, 20f, 20f, 20f);
        HTMLWorker worker = new HTMLWorker(document);
        PdfWriter.GetInstance(document, new FileStream(base.Server.MapPath("_PublicData") + "/xray.pdf", FileMode.Create));
        document.Open();
        worker.Parse(reader);
        document.Close();*/
    }
    protected string getInvoiceData()
    {
        string strData = "";
        if (Request.QueryString["type"].ToString()=="0")
        {
            strData = getData();
        }
        else if(Request.QueryString["type"].ToString()=="1")
        {
            strData = getDataE();
        }
        else if (Request.QueryString["type"].ToString()=="2")
        {
            strData = getDataD();
        }
        return strData;
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
        Document document = new Document(PageSize.A4.Rotate(), 50f, 50f, 50f, 50f);
        HTMLWorker worker = new HTMLWorker(document);
        PdfWriter.GetInstance(document, base.Response.OutputStream);
        document.Open();
        worker.Parse(reader);
        document.Close();
        base.Response.Write(document);
        base.Response.End();*/
    }
    private objDL getServices(int VisitID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT SERVICE_NAME, SERVICE_MOD_AMT FROM VISIT_SERVICE_DTLS JOIN SERVICE_MST ON SERVICE_MST.SERVICE_ID=VISIT_SERVICE_DTLS.SERVICE_ID WHERE SERVICE_MST.SERVICE_ID NOT IN(109,1,51) AND VISIT_ID=" + VisitID);
        return objdl;
    }
    private string getDiagnosis(int VisitID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string patDiagnosis = "";

        objdl = dA.returnList("select GROUP_CONCAT(COMP_NAME) AS COMP_NAME FROM VISIT_COMP_DTLS JOIN COMP_MST ON COMP_MST.COMP_ID=VISIT_COMP_DTLS.COMP_ID WHERE VISIT_ID='" + VisitID + "' AND COMP_MST.COMP_TYPE=0");
        if (objdl.flaG==true)
        {
            patDiagnosis = objdl.dataSet.Tables[0].Rows[0]["COMP_NAME"].ToString();
        }
        return patDiagnosis;
    }
    private objDL getCompanyInfo(string InvNo)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT COMPANY_ID, COMPANY_NAME, COMPANY_ADDR1, COMPANY_ADDR2, COMPANY_ADDR3, COMPANY_EMAIL, COMPANY_PHONE1, COMPANY_PHONE2, COMPANY_FAX, COMPANY_CONT_PERSON, COMPANY_CONT_PERSON_HP, COMPANY_INVOICE_FORMAT, MSG_FLAG, MSGEMP, MSGDEP, INV_DATE, INV_NO, INV_MONTH, INV_YEAR FROM COMPANY_MST JOIN INV_MST ON INV_COMPANY_ID=COMPANY_ID WHERE INV_NO='" + InvNo + "'");
        return objdl;
    }
    public string getData()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objDL cmpDL = new objDL();

        string rptStr = "";

        if (Request.QueryString.Count > 0)
        {
            // 01122015 01122015

            cmpDL = getCompanyInfo(Request.QueryString["id"].ToString());

            objDL objH = new objDL();
            objH = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT CLINIC_NAME, CLINIC_ADDR1, CLINIC_ADDR2, CLINIC_ADDR3, CLINIC_EMAIL, CLINIC_PHONE_O FROM CLINIC_MST");

            rptStr += "<table border='0' cellpadding='1' cellspacing='1' style='width:100%;font-size:8px'>";
            rptStr += "<tr><td><font size='6px'><u>Panel Company Details</u></font></td><td style='text-align:right'><font size='14px'>" + objH.dataSet.Tables[0].Rows[0][0].ToString() + "</font></td></tr>";
            rptStr += "<tr><td><font size='14px'><b>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_NAME"].ToString() + "</b></font></td><td style='text-align:right'><font size='8px'>" + objH.dataSet.Tables[0].Rows[0][1].ToString() + "</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR1"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>" + objH.dataSet.Tables[0].Rows[0][2].ToString() + "</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR2"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>" + objH.dataSet.Tables[0].Rows[0][3].ToString() + "</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR3"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>" + objH.dataSet.Tables[0].Rows[0][5].ToString() + "</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>Tel :" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE1"].ToString() + ',' + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE2"].ToString() + "Fax:" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_FAX"].ToString() + "</font></td><td style='text-align:right'>&nbsp;</td></tr>";
            rptStr += "<tr><td><font size='8px'>BILL NO: " + Request.QueryString["id"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>DATE :" + ((DateTime)cmpDL.dataSet.Tables[0].Rows[0]["INV_DATE"]).ToString("dd/MM/yyyy") + "</font></td></tr>";
            rptStr += "<tr><td colspan='2' style='text-align:center'><font size='10px'><b>CHARGES FOR THE MONTH OF </b>" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(cmpDL.dataSet.Tables[0].Rows[0]["INV_MONTH"].ToString())) + " - " + cmpDL.dataSet.Tables[0].Rows[0]["INV_YEAR"].ToString() + "</font></td></tr>";
            rptStr += "</table>";
            rptStr += "<br/>";

            if (cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString().Trim() == "99999")
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='20%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='17%'>Employee Name</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' ORDER BY INV_VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }

                        rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='8' style='text-align:right'>Total Amount</td><td align='right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
            else
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='20%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='10%'>Employee Name</td><td width='8%'>Diagnosis</td><td width='5%'>MC</td><td width='7%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Other Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID, MC_DATE FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' ORDER BY VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }
                        if (Row[9].ToString()=="0")
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>&nbsp;</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        else
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>" + Row[9] + "(" + ((DateTime)Row[11]).ToString("dd/MM/yyyy") + ") </td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='10' style='text-align:right'>Total Amount</td><td style='text-align:right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
        }
        return rptStr;
    }
    public string getDataE()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objDL cmpDL = new objDL();

        string rptStr = "";

        if (Request.QueryString.Count > 0)
        {
            // 01122015 01122015

            cmpDL = getCompanyInfo(Request.QueryString["id"].ToString());

            rptStr += "<table border='0' cellpadding='1' cellspacing='1' style='width:100%;font-size:8px'>";
            rptStr += "<tr><td><font size='6px'><u>Panel Company Details</u></font></td><td style='text-align:right'><font size='14px'>POLIKLINI NEGARA</font></td></tr>";
            rptStr += "<tr><td><font size='14px'><b>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_NAME"].ToString() + "</b></font></td><td style='text-align:right'><font size='8px'>11A, JALAN BERANANG SATU</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR1"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>TAMAN BUNGA NEGARA</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR2"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>SECTION 27/14A, 40000 SHAH ALAM</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR3"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>PHONE: 03-51920436, FAX: 03-51920436</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>Tel :" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE1"].ToString() + ',' + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE2"].ToString() + "Fax:" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_FAX"].ToString() + "</font></td><td style='text-align:right'>&nbsp;</td></tr>";
            rptStr += "<tr><td><font size='8px'>BILL NO: " + Request.QueryString["id"].ToString() + "E</font></td><td style='text-align:right'><font size='8px'>DATE :" + ((DateTime)cmpDL.dataSet.Tables[0].Rows[0]["INV_DATE"]).ToString("dd/MM/yyyy") + "</font></td></tr>";
            rptStr += "<tr><td colspan='2' style='text-align:center'><font size='10px'><b>CHARGES FOR THE MONTH OF </b>" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(cmpDL.dataSet.Tables[0].Rows[0]["INV_MONTH"].ToString())) + " - " + cmpDL.dataSet.Tables[0].Rows[0]["INV_YEAR"].ToString() + "</font></td></tr>";
            rptStr += "</table>";
            rptStr += "<br/>";
            if (cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString().Trim() == "99999")
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='20%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='17%'>Employee Name</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' AND PAT_RELATION='86' ORDER BY VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }

                        rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='8' style='text-align:right'>Total Amount</td><td align='right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
            else
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='16%'>Employee/Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='8%'>Diagnosis</td><td width='8%'>MC (Date)</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Other Services</td><td width='5%' align='right'>Amount</td></tr>";
                //rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='20%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='10%'>Employee Name</td><td width='5%'>Diagnosis</td><td width='2%'>MC</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Other Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID, MC_DATE FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' AND PAT_RELATION='86' ORDER BY VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }
                        if (Row[9].ToString()=="0")
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>&nbsp;</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        else
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>" + Row[9] + "(" + ((DateTime)Row[11]).ToString("dd/MM/yyyy") + ") </td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='9' style='text-align:right'>Total Amount</td><td align='right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
        }
        return rptStr;
    }
    public string getDataD()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objDL cmpDL = new objDL();

        string rptStr = "";

        if (Request.QueryString.Count > 0)
        {
            // 01122015 01122015

            cmpDL = getCompanyInfo(Request.QueryString["id"].ToString());

            rptStr += "<table border='0' cellpadding='1' cellspacing='1' style='width:100%;font-size:8px'>";
            rptStr += "<tr><td><font size='6px'><u>Panel Company Details</u></font></td><td style='text-align:right'><font size='14px'>POLIKLINI NEGARA</font></td></tr>";
            rptStr += "<tr><td><font size='14px'><b>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_NAME"].ToString() + "</b></font></td><td style='text-align:right'><font size='8px'>11A, JALAN BERANANG SATU</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR1"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>TAMAN BUNGA NEGARA</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR2"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>SECTION 27/14A, 40000 SHAH ALAM</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ADDR3"].ToString() + "</font></td><td style='text-align:right'><font size='8px'>PHONE: 03-51920436, FAX: 03-51920436</font></td></tr>";
            rptStr += "<tr><td><font size='8px'>Tel :" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE1"].ToString() + ',' + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_PHONE2"].ToString() + "Fax:" + cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_FAX"].ToString() + "</font></td><td style='text-align:right'>&nbsp;</td></tr>";
            rptStr += "<tr><td><font size='8px'>BILL NO: " + Request.QueryString["id"].ToString() + "D</font></td><td style='text-align:right'><font size='8px'>DATE :" + ((DateTime)cmpDL.dataSet.Tables[0].Rows[0]["INV_DATE"]).ToString("dd/MM/yyyy") + "</font></td></tr>";
            rptStr += "<tr><td colspan='2' style='text-align:center'><font size='10px'><b>CHARGES FOR THE MONTH OF </b>" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(cmpDL.dataSet.Tables[0].Rows[0]["INV_MONTH"].ToString())) + " - " + cmpDL.dataSet.Tables[0].Rows[0]["INV_YEAR"].ToString() + "</font></td></tr>";
            rptStr += "</table>";
            rptStr += "<br/>";
            if (cmpDL.dataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString().Trim() == "99999")
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='20%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='17%'>Employee Name</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' AND PAT_RELATION!='86' ORDER BY VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }

                        rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='8' style='text-align:right'>Total Amount</td><td align='right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
            else
            {
                rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
                rptStr += "<tr><td width='3%'>#</td><td width='7%'>Date</td><td width='16%'>Patient Name</td><td width='10%'>NRIC<br/>Staff No.</td><td width='10%'>Employee Name</td><td width='8%'>Diagnosis</td><td width='8%'>MC (Date)</td><td width='8%'>Consultation</td><td width='5%'>Medicine</td><td width='15%'>Other Services</td><td width='5%' align='right'>Amount</td></tr>";

                decimal total = 0;

                objdl = dA.returnList("SELECT INV_VISIT_DATE, PAT_NAME, PAT_EMP_NO, PAT_IC_NO, PAT_RELATED_TO, INV_SER_AMT, INV_MED_AMT, '', INV_VISIT_AMT, MC_DAYS, INV_VISIT_ID, MC_DATE FROM INV_DTLS JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=INV_DTLS.INV_PAT_ID JOIN PATIENT_VISIT_MST ON PATIENT_VISIT_MST.VISIT_ID=INV_DTLS.INV_VISIT_ID WHERE INV_NO='" + Request.QueryString["id"].ToString() + "' AND PAT_RELATION!='86' ORDER BY VISIT_DATE");
                if (objdl.flaG == true)
                {
                    for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                    {
                        DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                        decimal samt = (decimal)Row[5];
                        decimal mamt = (decimal)Row[6];
                        decimal tamt = (decimal)Row[8];

                        string rptSub = "";

                        objDL services = new objDL();

                        services = getServices(int.Parse(Row[10].ToString()));

                        if (services.flaG == true)
                        {
                            rptSub += "<table border='0' cellspacing='1' cellpadding='1' style='font-size:6px;width:100%'>";
                            for (int i = 0; i < services.dataSet.Tables[0].Rows.Count; i++)
                            {
                                decimal serviceAmt = (decimal)services.dataSet.Tables[0].Rows[i]["SERVICE_MOD_AMT"];
                                rptSub += "<tr><td style='width:90%'>" + services.dataSet.Tables[0].Rows[i]["SERVICE_NAME"].ToString() + "</td><td style='width:10%;text-align:right'>" + serviceAmt.ToString("0.00") + "</td></tr>";
                            }
                            rptSub += "</table>";
                        }
                        if (Row[9].ToString()=="0")
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>&nbsp;</td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        else
                            rptStr += "<tr><td>" + (row + 1) + "</td><td>" + ((DateTime)Row[0]).ToString("dd/MM/yyyy") + "</td><td>" + Row[1] + "</td><td>" + Row[3] + "<br/>" + Row[2] + " </td><td>" + Row[4] + "</td><td>" + getDiagnosis(int.Parse(Row[10].ToString())) + "</td><td>" + Row[9] + "(" + ((DateTime)Row[11]).ToString("dd/MM/yyyy") + ") </td><td style='text-align:right'>" + samt.ToString("N", new CultureInfo("en-US")) + "</td><td style='text-align:right'>" + mamt.ToString("N", new CultureInfo("en-US")) + "</td><td>" + rptSub + "</td><td style='text-align:right'>" + tamt.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                        total += decimal.Parse(Row[8].ToString());
                    }
                    rptStr += "<tr><td colspan='10' style='text-align:right'>Total Amount</td><td align='right'>" + (total).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    rptStr += "</table>";
                    rptStr += "<font size='8px'><i>Please mark our Reference No when making payment.</i></font>";
                }

            }
        }
        return rptStr;
    }
    protected string convertDateForForm(string dt)
    {
        string returnDate = "";
        returnDate = dt.Substring(8, 2) + "/" + dt.Substring(5, 2) + "/" + dt.Substring(0, 4);
        return returnDate;
    }
}