using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Vijay;
using System.Data;
using System.Globalization;
public partial class Patient_PanelSummary : System.Web.UI.Page
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
        base.Response.End();
         */
    }
    public string getData()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();
        string rptStr = "";

        objDL objH = new objDL();
        objH = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT CLINIC_NAME, CLINIC_ADDR1, CLINIC_ADDR2, CLINIC_ADDR3, CLINIC_EMAIL, CLINIC_PHONE_O FROM CLINIC_MST");

        if (Request.QueryString.Count > 0)
        {
            // 01122015 01122015
            string fdate = Request.QueryString["param"].ToString().Substring(4, 4) + "-" + Request.QueryString["param"].ToString().Substring(2, 2) + "-" + Request.QueryString["param"].ToString().Substring(0, 2);
            string tdate = Request.QueryString["param"].ToString().Substring(12, 4) + "-" + Request.QueryString["param"].ToString().Substring(10, 2) + "-" + Request.QueryString["param"].ToString().Substring(8, 2);

            rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
            rptStr += "<tr><td colspan='6' align='center'><b>" + objH.dataSet.Tables[0].Rows[0][0].ToString() + "</b>";
            rptStr += "<br/><font size='6px'>" + objH.dataSet.Tables[0].Rows[0][1].ToString() + "," + objH.dataSet.Tables[0].Rows[0][2].ToString() + "," + objH.dataSet.Tables[0].Rows[0][3].ToString() + "</font>";
            rptStr += "<br/>Panel Companies Summary for the period of " + convertDateForForm(fdate) + " - " + convertDateForForm(tdate) + "</td></tr>";
            rptStr += "<tr><td width='10%'>S.No.</td><td colspan='3' width='60%'>Company</td><td width='15%' align='right'>No. Cases</td><td width='15%' align='right'>Amount</td></tr>";

            decimal totalCash = 0; decimal totalCompany = 0;

            objdl = dA.returnList("SELECT '', '', COUNT(1) AS NO_CASES, SUM(VISIT_TOT_AMT) AS VISIT_TOT_AMT, COMPANY_NAME FROM PATIENT_VISIT_MST JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=PATIENT_VISIT_MST.PAT_ID JOIN COMPANY_MST ON PATIENT_VISIT_MST.COMPANY_ID=COMPANY_MST.COMPANY_ID WHERE VISIT_DATE BETWEEN '" + fdate + "' AND ' 00:00" + tdate + " 23:59' GROUP BY COMPANY_MST.COMPANY_ID");
            if (objdl.flaG == true)
            {
                for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                {
                    DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                    decimal amount = (decimal)Row[3];
                    rptStr += "<tr><td>" + (row + 1) + "</td><td colspan='3'>" + Row[4] + "</td><td align='right'>" + Row[2] + "</td><td align='right'>" + amount.ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                    totalCompany += decimal.Parse(Row[3].ToString());
                }
                rptStr += "<tr><td colspan='5'>Grand Total</td><td align='right'>" + (totalCompany + totalCash).ToString("N", new CultureInfo("en-US")) + "</td></tr>";
                rptStr += "</table>";
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