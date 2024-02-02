using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Patient_Patients_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("PAT_REG_DATE", "DESC");
            getDisciplines();
            txtYear.Text = DateTime.Now.ToString("yyyy");
            txtDay.Text = DateTime.Now.ToString("dd");
            txtMonth.SelectedValue = DateTime.Now.ToString("MM");
        }
        else
        {

        }
    }
    protected string getVisitLinks(string patientID)
    {
        //<li><a href="#"><i class="icon-pencil"></i> Edit</a></li>
        string str = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT VISIT_DATE, COMP_NAME, PATIENT_VISIT_MST.VISIT_ID FROM PATIENT_VISIT_MST JOIN PAT_QUEUE ON PAT_QUEUE.VISIT_ID = PATIENT_VISIT_MST.VISIT_ID JOIN COMP_MST ON COMP_ID = PAT_QUEUE.PAT_DISC WHERE PATIENT_VISIT_MST.PAT_ID = '" + patientID + "' ORDER BY VISIT_DATE DESC");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["vType"].ToString() == "1")
                    str += "<li><a href='javascript:window.open(\"" + ResolveUrl("~/Patient/PatientVisit.aspx") + "?PatID=" + patientID + "&_vD=" + objdl.dataSet.Tables[0].Rows[inc][2].ToString() + "\", \"_blank\")'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[inc][0]).ToString("dd/MM/yyyy") + " [" + objdl.dataSet.Tables[0].Rows[inc][1].ToString() + "]</a></li>";
                else if(System.Configuration.ConfigurationManager.AppSettings["vType"].ToString() == "2")
                    str += "<li><a href='javascript:window.open(\"" + ResolveUrl("~/Patient/Visit_1.aspx") + "?PatID=" + patientID + "&_vD=" + objdl.dataSet.Tables[0].Rows[inc][2].ToString() + "\", \"_blank\")'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[inc][0]).ToString("dd/MM/yyyy") + " [" + objdl.dataSet.Tables[0].Rows[inc][1].ToString() + "]</a></li>";
            }
        }
        return str;
    }
    protected void SendToQueue(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        string qry = "INSERT INTO PAT_QUEUE(PAT_ID, PAT_QUEUE_STATUS, PAT_QUEUE_DATE, PAT_DISC, PAT_STATE, VISIT_ID) VALUES('" + hdnPatID.Value + "', 'Waiting', '" + txtYear.Text + "-" + txtMonth.SelectedValue + "-" + txtDay.Text + " " + DateTime.Now.ToString("HH:mm") + "', '" + drpDiscipline.SelectedValue + "','0','0')";
        msg = dA.run("INSERT INTO PAT_QUEUE(PAT_ID, PAT_QUEUE_STATUS, PAT_QUEUE_DATE, PAT_DISC, PAT_STATE, VISIT_ID) VALUES('" + hdnPatID.Value + "', 'Waiting', '" + txtYear.Text + "-" + txtMonth.SelectedValue + "-" + txtDay.Text + " " + DateTime.Now.ToString("HH:mm") + "', '" + drpDiscipline.SelectedValue + "','0','0')", HttpContext.Current.Session["userid"].ToString());
    }
    protected void getDisciplines()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT COMP_ID, COMP_NAME FROM COMP_MST WHERE COMP_TYPE=3");

        if (objdl.flaG==true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            drpDiscipline.DataSource = objdl.dataSet.Tables[0];
            drpDiscipline.DataTextField = "COMP_NAME";
            drpDiscipline.DataValueField = "COMP_ID";
            drpDiscipline.DataBind();
            
        }
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        sortCol = "PAT_NAME";
        sortDir = "ASC";

        string searchKeyword = "";

        if (txtKeyword.Text != "")
        {
            searchKeyword = " AND " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT PAT_ID, PAT_NAME, PAT_IC_NO, PAT_REG_DATE, PAT_BIRTH_DATE, PAT_REG_NO, COMPANY_NAME, PAT_PREV_HISTORY FROM PATIENT_REGISTRATION JOIN COMPANY_MST ON PAT_COMPANY_ID=COMPANY_ID WHERE PAT_NAME !=''" + searchKeyword + "");
        if (objdl.flaG == true)
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
        fillGrid("PAT_NAME", "ASC");
    }
    protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName=="visitPatient")
        {
            Response.Redirect("Visit?PatID=" + Lst.DataKeys[row].Values[0].ToString() + "&type=NEW");
        }
        else if (e.CommandName=="editPatient")
        {
            Response.Redirect("Registration?PatID=" + Lst.DataKeys[row].Values[0].ToString());
        }
    }

    protected void btnSearch_Command(object sender, EventArgs e)
    {
        fillGrid("PAT_NAME", "ASC");
    }

    protected void btnNew_Command(object sender, EventArgs e)
    {
        Response.Redirect("~/Patient/Registration.aspx?PatID=0");
    }
}