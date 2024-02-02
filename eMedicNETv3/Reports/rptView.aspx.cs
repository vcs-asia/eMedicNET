using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Packaging;

using NReco.PivotData;
using NReco.PivotData.Output;

using Vijay;

namespace emedic3.Reports
{
    public partial class rptView : System.Web.UI.Page
    {
        public string _strValue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            objDL objdl = new objDL();
            objdl = new dbAction(System.Configuration.ConfigurationManager.AppSettings["dbType"].ToString(), System.Configuration.ConfigurationManager.AppSettings["con"].ToString() + System.Configuration.ConfigurationManager.AppSettings["dbname"].ToString() + System.Configuration.ConfigurationManager.AppSettings["cred"].ToString()).returnList("SELECT PATIENT_VISIT_MST.VISIT_DISC AS DISC, VISIT_SERVICE_DTLS.SERVICE_ID AS SERVICE, SERVICE_MST.SERVICE_NAME AS SERVICE_NAME, COMP_MST.COMP_NAME AS COMP_NAME, SUM(VISIT_SERVICE_DTLS.SERVICE_MOD_AMT) AS AMT FROM PATIENT_VISIT_MST JOIN VISIT_SERVICE_DTLS ON PATIENT_VISIT_MST.VISIT_ID = VISIT_SERVICE_DTLS.VISIT_ID JOIN SERVICE_MST ON SERVICE_MST.SERVICE_ID = VISIT_SERVICE_DTLS.SERVICE_ID JOIN COMP_MST ON COMP_MST.COMP_ID = PATIENT_VISIT_MST.VISIT_DISC WHERE MONTH(PATIENT_VISIT_MST.VISIT_DATE) = '" + Request.QueryString["_m"].ToString() + "' AND YEAR(PATIENT_VISIT_MST.VISIT_DATE) = '" +  Request.QueryString["_y"].ToString() +"' GROUP BY VISIT_SERVICE_DTLS.SERVICE_ID, PATIENT_VISIT_MST.VISIT_DISC");

            DataTable tbl = objdl.dataSet.Tables[0];

            PivotData pvtData = new PivotData(new[] { "COMP_NAME", "SERVICE_NAME", "AMT" }, new SumAggregatorFactory("AMT"));
            pvtData.ProcessData(new DataTableReader(tbl));

            var pvtTbl = new PivotTable (
                new[] {"COMP_NAME"}, //rows
                new[] {"SERVICE_NAME"}, //columns
                pvtData);


            var sb = new StringBuilder();
            sb.Append("<table style='font-family:arial;font-size:12px' border='1' colspan='0' cellpadding='2' cellspacing='0'>");
            //column labels
            sb.Append("<tr><th>&nbsp;</th>");
            foreach(var colKey in pvtTbl.ColumnKeys)
            {
                sb.AppendFormat("<th width='200'>{0}</th>", colKey.ToString());

            }
            sb.Append("<th>Total</th>");
            sb.Append("</tr>");
            // rows
            for (var r=0; r<pvtTbl.RowKeys.Length; r++)
            {
                var rowKey = pvtTbl.RowKeys[r];
                sb.Append("<tr>");
                sb.AppendFormat("<th width='200'>{0}</th>", rowKey.ToString()); //row label
                for (var c=0; c < pvtTbl.ColumnKeys.Length; c++)
                {
                    sb.AppendFormat("<td align='right'>{0}</td>",  (Convert.ToDecimal(pvtTbl[r, c].Value).ToString("0.00")=="0.00" ? "&nbsp" : Convert.ToDecimal(pvtTbl[r, c].Value).ToString("0.00")));
                }
                sb.AppendFormat("<td align='right'>{0}</td>", Convert.ToDecimal(pvtTbl[r, null].Value).ToString("0.00")); //row total
            }
            //row for column totals
            sb.Append("<tr>");
            sb.Append("<th>Totals</th>");
            for (var c=0; c < pvtTbl.ColumnKeys.Length; c++)
            {
                sb.AppendFormat("<td align='right'>{0}</td>", Convert.ToDecimal(pvtTbl[null, c].Value).ToString("0.00"));
            }
            sb.AppendFormat("<td align='right'>{0}</td>", Convert.ToDecimal(pvtTbl[null, null].Value).ToString("0.00")); //grand total
            sb.Append("<tr>");
            sb.Append("</table>");
            _strValue = sb.ToString();
            //Console.WriteLine(sb.ToString());
            /*
            ds = objdl.dataSet;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("~/Reports/sales.rpt"));
            rpt.SetDataSource(ds);
            crptViewer.ReportSource = rpt;
            crptViewer.DataBind();*/
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            objDL objdl = new objDL();
            objdl = new dbAction(System.Configuration.ConfigurationManager.AppSettings["dbType"].ToString(), System.Configuration.ConfigurationManager.AppSettings["con"].ToString() + System.Configuration.ConfigurationManager.AppSettings["dbname"].ToString() + System.Configuration.ConfigurationManager.AppSettings["cred"].ToString()).returnList("SELECT PATIENT_VISIT_MST.VISIT_DISC AS DISC, VISIT_SERVICE_DTLS.SERVICE_ID AS SERVICE, SERVICE_MST.SERVICE_NAME AS SERVICE_NAME, COMP_MST.COMP_NAME AS COMP_NAME, SUM(VISIT_SERVICE_DTLS.SERVICE_MOD_AMT) AS AMT FROM PATIENT_VISIT_MST JOIN VISIT_SERVICE_DTLS ON PATIENT_VISIT_MST.VISIT_ID = VISIT_SERVICE_DTLS.VISIT_ID JOIN SERVICE_MST ON SERVICE_MST.SERVICE_ID = VISIT_SERVICE_DTLS.SERVICE_ID JOIN COMP_MST ON COMP_MST.COMP_ID = PATIENT_VISIT_MST.VISIT_DISC WHERE MONTH(PATIENT_VISIT_MST.VISIT_DATE) = '" + Request.QueryString["_m"].ToString() + "' AND YEAR(PATIENT_VISIT_MST.VISIT_DATE) = '" + Request.QueryString["_y"].ToString() + "' GROUP BY VISIT_SERVICE_DTLS.SERVICE_ID, PATIENT_VISIT_MST.VISIT_DISC");

            DataTable tbl = objdl.dataSet.Tables[0];

            PivotData pvtData = new PivotData(new[] { "COMP_NAME", "SERVICE_NAME", "AMT" }, new SumAggregatorFactory("AMT"));
            pvtData.ProcessData(new DataTableReader(tbl));

            var pvtTbl = new PivotTable(
                new[] { "COMP_NAME" }, //rows
                new[] { "SERVICE_NAME" }, //columns
                pvtData);

            var pkg = new ExcelPackage();
            pkg.Compression = CompressionLevel.Default;
            var wsPvt = pkg.Workbook.Worksheets.Add("Sheet");
            Stream output = new MemoryStream();
            var pvtExcelWr = new PivotTableExcelWriter(wsPvt);
            pvtExcelWr.Write(pvtTbl);

            using (var excelFs = new FileStream(Server.MapPath("~/_PublicData/sales.xlsx"), FileMode.Create, FileAccess.Write))
            {
                pkg.SaveAs(excelFs);
            }
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"sales.xlsx\"");
            Response.BufferOutput = false;
            Response.TransmitFile(Server.MapPath("~/_PublicData/sales.xlsx"));
            Response.End();
        }
    }
}