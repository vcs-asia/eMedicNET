using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Vijay;

public partial class Patient_ME : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["_vD"].ToString() != "0" || Request.QueryString["_vD"].ToString() != "")
            {
                hdnVisitID.Value = Request.QueryString["_vD"].ToString();
            }

        }
    }

    [System.Web.Services.WebMethod]
    public static string[] getPhysicalExams()
    {
        objDL objdl = new objDL();
        string[] rVal = null;

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMP_NAME, COMP_ID FROM COMP_MST WHERE COMP_TYPE = 8");
        if (objdl.flaG==true)
        {
            rVal = new string[objdl.dataSet.Tables[0].Rows.Count];
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                rVal[inc] = objdl.dataSet.Tables[0].Rows[inc][0].ToString() + "|" + objdl.dataSet.Tables[0].Rows[inc][1].ToString();
            }
        }
        else
        {
            rVal = new string[1];
            rVal[0] = objdl.Msg;
        }

        return rVal;
    }

    [System.Web.Services.WebMethod]
    public static string saveInfo(NameValue[] formVars)
    {
        string retValue = "";
        dbAction da = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        try
        {
            List<dbParam> objparams = new List<dbParam>();

            List<string> mainInfo = new List<string>();

            mainInfo.Add(formVars.Form("ctl00$contentForm$txtOccupation")); //0
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtFamilyHistory")); //1
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtPastHistory")); //2
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtAllergyHistory")); //3
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtPresentComplaints")); //4
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtHeight")); //5
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtWeight")); //6
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtBMI")); //7
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtBP")); //8
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtPulse")); //9
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtLCorrected")); //10
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtRCorrected")); //11
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtLUncorrected")); //12
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtRUncorrected")); //13
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtColourVision")); //14
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtRemarks")); //15
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtProtein")); //16
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtPH")); //17
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtSugar")); //18
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtSG")); //19
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtMicro")); //20
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtDrugScreen")); //21
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtOtherTest")); //22
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtChestXRay")); //23
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtFindings")); //24
            mainInfo.Add(formVars.Form("ctl00$contentForm$txtConclusion")); //25

            mainInfo.Add(formVars.Form("ctl00$contentForm$hdnVisitID")); //26
            var phExam = formVars.FormMultiple("chk[]");

            ArrayList tbl = new ArrayList();

            for (int inc = 0; inc < phExam.Count(); inc++ )
            {
                List<string> eachPh = new List<string>();
                eachPh.Add(phExam[inc]);
                tbl.Add(eachPh);
            }
        }
        catch(Exception ex)
        {
            retValue = ex.Message.ToString();
        }

        return retValue;
    }
}