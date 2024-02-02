using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;

public partial class Patient_Reporting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lstReport.Focus();
        fillDrugs();
        fillPMode();
    }
    private void fillDrugs()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT MED_ID, MED_NAME FROM MEDICINE_MST ORDER BY MED_NAME");
        if (objdl.flaG==true)
        {
            lstDrugs.DataSource = objdl.dataSet.Tables[0];
            lstDrugs.DataTextField = "MED_NAME";
            lstDrugs.DataValueField = "MED_ID";
            lstDrugs.DataBind();
        }
    }

    private void fillPMode()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT PARAM_NAME, PARAM_ID FROM PARAMETERS_INFO WHERE PARAM_TYPE = 10");
        if (objdl.flaG == true)
        {
            lstPMode.DataSource = objdl.dataSet.Tables[0];
            lstPMode.DataTextField = "PARAM_NAME";
            lstPMode.DataValueField = "PARAM_ID";
            lstPMode.DataBind();
        }
    }
        
}