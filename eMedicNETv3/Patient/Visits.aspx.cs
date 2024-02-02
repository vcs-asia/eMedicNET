using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;
using System.Data;

public partial class Patient_Visits : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT PAT_ID, VISIT_ID, VISIT_DATE, VISIT_TIME, VISIT_TOT_AMT, DOC_NAME, COMP_NAME AS DISC FROM PATIENT_VISIT_MST JOIN DOCTOR_MST ON PATIENT_VISIT_MST.DOC_ID=DOCTOR_MST.DOC_ID JOIN COMP_MST ON COMP_MST.COMP_ID=DOCTOR_MST.DOC_SPECIALIZATION WHERE PAT_ID = '" + Request.QueryString["PatID"].ToString() + "' ORDER BY VISIT_DATE DESC");
        if (objdl.flaG == true)
        {
            LstVisit.DataSource = new DataView(objdl.dataSet.Tables[0]);
            LstVisit.DataBind();
        }
    }
}