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
public partial class Patient_DrugDispensing : System.Web.UI.Page
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
        PdfWriter.GetInstance(document, new FileStream(base.Server.MapPath("_PublicData") + "/report.pdf", FileMode.Create));
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

            decimal totalAmount = 0;

            rptStr += "<table border='1' cellpadding='2' cellspacing='2' style='font-size:8px'>";
            rptStr += "<tr><td colspan='6' align='center'><b>" + objH.dataSet.Tables[0].Rows[0][0].ToString() + "</b>";
            rptStr += "<br/><font size='6px'>" + objH.dataSet.Tables[0].Rows[0][1].ToString() + "," + objH.dataSet.Tables[0].Rows[0][2].ToString() + "," + objH.dataSet.Tables[0].Rows[0][3].ToString() + "</font>";
            rptStr += "<tr><td colspan='6'>Drug Dispensing report for the period of " + convertDateForForm(fdate) + " - " + convertDateForForm(tdate) + "</td></tr>";
            rptStr += "<tr><td width='10%'>S.No.</td><td width='20%'>Patient Name</td><td width='10%' align='right'>Visit Date</td><td width='10%'>Quantity</td><td width='10%'>U. Price</td></tr>";

            objdl = dA.returnList("SELECT PAT_NAME, VISIT_DATE, MED_NAME, VISIT_MEDICINE_DTLS.MED_QTY, MED_UNIT_PRICE FROM PATIENT_VISIT_MST JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=PATIENT_VISIT_MST.PAT_ID JOIN VISIT_MEDICINE_DTLS ON PATIENT_VISIT_MST.VISIT_ID=VISIT_MEDICINE_DTLS.VISIT_ID JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID=VISIT_MEDICINE_DTLS.MED_ID WHERE VISIT_DATE BETWEEN '" + fdate + " 00:00' AND '" + tdate + " 23:59' AND VISIT_MEDICINE_DTLS.MED_ID='" + Request.QueryString["mID"].ToString() + "' ORDER BY VISIT_DATE, VISIT_TIME");
            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count>0)
            {
                for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                {
                    DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                    DateTime dt = (DateTime) Row[1];
                    if (row == 0)
                    {
                        rptStr += "<tr><td colspan='6'>" + Row[2] + "</td></tr>";
                    }
                    decimal amt = ((int)Row[3]) * ((decimal)Row[4]);
                    totalAmount += amt;
                    rptStr += "<tr><td>" + (row + 1) + "</td><td>" + Row[0] + "</td><td align='right'>" + ((DateTime)Row[1]).ToString("dd/MM/yyyy") + "</td><td align='right'>" + Row[3] + "</td><td align='right'>" + ((decimal)Row[4]).ToString("0.00") + "</td><td align='right'>" + ((decimal)amt).ToString("0.00") + "</td></tr>";
                }
                rptStr += "<tr><td colspan='5' align='right'>Total Amount </td><td align='right'>" + ((decimal)totalAmount).ToString("0.00") + "</td></tr>";
            }
        }
        rptStr += "</table>";

        return rptStr;
    }
    protected string convertDateForForm(string dt)
    {
        string returnDate = "";
        returnDate = dt.Substring(8, 2) + "/" + dt.Substring(5, 2) + "/" + dt.Substring(0, 4);
        return returnDate;
    }
}