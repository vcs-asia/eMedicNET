using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Billing_Invoices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.ToString("yyyy");
            txtMonth.SelectedValue = DateTime.Now.ToString("MM");
        }
    }
    protected void viewInvoies(object sender, EventArgs e)
    {
        fillGrid("COMPANY_NAME", "ASC");
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT INV_NO, INV_COMPANY_ID, COMPANY_NAME, INV_MONTH, INV_YEAR, IF(INV_AMT IS NULL,0,INV_AMT) AS INV_AMT, INV_DATE, INV_TDAT, INV_FDAT FROM INV_MST JOIN COMPANY_MST ON COMPANY_ID=INV_COMPANY_ID WHERE INV_AMT IS NOT NULL AND  LPAD(INV_MONTH,2,0) = '" + txtMonth.SelectedValue + "' AND INV_YEAR = '" + txtYear.Text + "' ORDER BY COMPANY_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
            Lst.DataSource = dv;
            Lst.DataBind();
        }
    }
    protected void Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortDirection = "ASC";

        string lastDirection = ViewState["SortDirection"] as string;

        if ((lastDirection != null) && (lastDirection == "ASC"))
        {
            sortDirection = "DESC";
        }
        ViewState["SortDirection"] = sortDirection;
        fillGrid(e.SortExpression.ToString(), sortDirection);
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lst.PageIndex = e.NewPageIndex;
        fillGrid("INV_NO", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT PAT_ID, PAT_NAME, VISIT_ID, VISIT_DATE, VISIT_TIME, VISIT_TOT_AMT, DOC_NAME, 'POLIKLINIK' AS DISC FROM PATIENT_VISIT_MST JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=PATIEINT_VISIT_MST.VISIT_ID JOIN DOCTOR_MST ON PATIENT_VISIT_MST.DOC_ID=DOCTOR_MST.DOC_ID WHERE INV_NO = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' ORDER BY VISIT_DATE DESC");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstVisit") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }
        }
    }
    protected void viewInvoices(object sender, EventArgs e)
    {
        fillGrid("COMPANY_NAME", "ASC");
    }
    protected void generateInvoices(object sender, EventArgs e)
    {
        try
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            List<dbParam> objparams = new List<dbParam>();

            string msg = "";

            objparams.Add(new dbParam { col = "INVMONTH", dType = "S", image = null, val = txtMonth.SelectedValue });
            objparams.Add(new dbParam { col = "INVYEAR", dType = "S", image = null, val = txtYear.Text });
            objparams.Add(new dbParam { col = "INVUSER", dType = "S", image = null, val = Session["userid"].ToString() });

            msg = dA.runStoredProcedure("GENERATEINVOICES", objparams, null);
            //msg = dA.run("CALL GENERATEINVOICES('" + txtMonth.SelectedValue + "','" + txtYear.Text + "','" + Session["user_login_id"].ToString() + "')");
            if (msg.StartsWith("ERROR"))
            {
                pnlError.Visible = true;
            }
            else
            {
                pnlSuccess.Visible = true;
                fillGrid("COMPANY_NAME", "ASC");
            }
        }
        catch(Exception ex)
        {
            lblError.Text = ex.ToString();
            pnlError.Visible = true;
        }
    }
}