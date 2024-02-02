using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;
using System.Text.RegularExpressions;

public partial class Patient_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
            getPatientInfo();
        }
    }
    protected void loadData()
    {
        string str = "";

        objDL objdl = new objDL();

        objdl = getCompanies();
        if (objdl.flaG==true)
        {
            txtCompany.DataSource = objdl.dataSet;
            txtCompany.DataTextField = "COMPANY_NAME";
            txtCompany.DataValueField = "COMPANY_ID";
            txtCompany.DataBind();
        }
        objdl = getParams("6");
        if (objdl.flaG == true)
        {
            txtNationality.DataSource = objdl.dataSet;
            txtNationality.DataTextField = "PARAM_NAME";
            txtNationality.DataValueField = "PARAM_ID";
            txtNationality.DataBind();
        }
        objdl = getParams("7");
        if (objdl.flaG == true)
        {
            txtRace.DataSource = objdl.dataSet;
            txtRace.DataTextField = "PARAM_NAME";
            txtRace.DataValueField = "PARAM_ID";
            txtRace.DataBind();
        }
        objdl = getParams("3");
        if (objdl.flaG == true)
        {
            txtRelation.DataSource = objdl.dataSet;
            txtRelation.DataTextField = "PARAM_NAME";
            txtRelation.DataValueField = "PARAM_ID";
            txtRelation.DataBind();
        }
        objdl = getParams("3");
        if (objdl.flaG == true)
        {
            txtNKRelation.DataSource = objdl.dataSet;
            txtNKRelation.DataTextField = "PARAM_NAME";
            txtNKRelation.DataValueField = "PARAM_ID";
            txtNKRelation.DataBind();
        }
        objdl =  getParams("3");
        if (objdl.flaG == true)
        {
            txtGRelation.DataSource = objdl.dataSet;
            txtGRelation.DataTextField = "PARAM_NAME";
            txtGRelation.DataValueField = "PARAM_ID";
            txtGRelation.DataBind();
        }

        Page page = HttpContext.Current.CurrentHandler as Page;
        page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
    }
    protected void goToVisit(object sender, EventArgs e)
    {
        Response.Redirect("~/Patient/PatientVisit.aspx?PatID=" + getPatID() + "&type=NEW");
    }
    protected void goBack(object sender, EventArgs e)
    {
        Response.Redirect("Patients");
    }
    protected int getPatID()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        int myID = 0;

        objdl = dA.returnList("SELECT MAX(PAT_ID) FROM PATIENT_REGISTRATION");
        if (objdl.flaG==true)
        {
            myID = (int)objdl.dataSet.Tables[0].Rows[0][0];
        }
        return myID;
    }
    protected void getPatientInfo()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT PAT_ID, PAT_NAME, PAT_IC_NO, PAT_REG_NO, PAT_REG_DATE, PAT_BIRTH_DATE, PAT_SEX, PAT_FATHER_HUSBAND_NAME, PAT_ADDR1, PAT_ADDR2, PAT_ADDR3, PAT_EMAIL, PAT_PHONE_R, PAT_PHONE_O, PAT_HANDPHONE, PAT_OCCUPATION, PAT_RACE, PAT_NATIONALITY, PAT_BLOOD_GROUP, PAT_PREV_HISTORY, PAT_COMPANY_ID, PAT_STATUS, PAT_EMP_NO, PAT_RELATION, PAT_RELATED_TO, PAT_COMP_OUTLET, PAT_COMPANY_OTH_DTLS, PAT_KIN_NAME, PAT_KIN_ICNO, PAT_KIN_RELATION, PAT_KIN_OCCUPATION, PAT_KIN_PHONES, PAT_KIN_ADDRESS, PAT_GRN_NAME, PAT_GRN_ICNO, PAT_GRN_RELATION, PAT_GRN_OCCUPATION, PAT_GRN_PHONES, PAT_GRN_ADDRESS, COMPANY_NAME, (SELECT PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_ID = PAT_NATIONALITY) AS PARAM_NATIONALITY, (SELECT PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_ID = PAT_RACE) AS PARAM_RACE, (SELECT PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_ID = PAT_RELATION) AS PARAM_RELATION, (SELECT PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_ID = PAT_KIN_RELATION) AS PARAM_KRELATION, (SELECT PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_ID = PAT_GRN_RELATION) AS PARAM_GRELATION FROM PATIENT_REGISTRATION JOIN COMPANY_MST ON COMPANY_ID=PAT_COMPANY_ID WHERE PAT_ID='" + Request.QueryString[0].ToString() + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            DataRow Row = objdl.dataSet.Tables[0].Rows[0];

            txtPatientName.Text = Row["PAT_NAME"].ToString();
            txtICNo.Text = Row["PAT_IC_NO"].ToString();
            txtRegnNo.Text = Row["PAT_ID"].ToString();
            txtFolderNo.Text = Row["PAT_REG_NO"].ToString();
            txtRegnDate.Text = ((DateTime)Row["PAT_REG_DATE"]).ToString("dd/MM/yyyy");
            txtBirthDate.Text = ((DateTime)Row["PAT_BIRTH_DATE"]).ToString("dd/MM/yyyy");
            lstSex.SelectedValue = Row["PAT_SEX"].ToString();
            txtNextKin.Text = Row["PAT_KIN_NAME"].ToString();
            txtAddr1.Text = Row["PAT_ADDR1"].ToString();
            txtAddr2.Text = Row["PAT_ADDR2"].ToString();
            txtAddr3.Text = Row["PAT_ADDR3"].ToString();
            txtPhones.Text = Row["PAT_PHONE_R"].ToString();
            txtHandPhone.Text = Row["PAT_HANDPHONE"].ToString();
            txtRace.Text = Row["PAT_RACE"].ToString();
            lstStatus.SelectedValue = Row["PAT_STATUS"].ToString();
            txtNationality.Text = Row["PAT_NATIONALITY"].ToString();
            lstBloodGroup.SelectedValue = Row["PAT_BLOOD_GROUP"].ToString();
            txtRemarks.Text = Row["PAT_PREV_HISTORY"].ToString();
            txtCompany.Text = Row["PAT_COMPANY_ID"].ToString();
            txtEmployee.Text = Row["PAT_RELATED_TO"].ToString();
            txtEmpNo.Text = Row["PAT_EMP_NO"].ToString();
            txtNKRelation.Text = Row["PAT_KIN_RELATION"].ToString();
            txtNKRelation.Text = Row["PAT_GRN_RELATION"].ToString();
            hdnPatID.Value = Row["PAT_ID"].ToString();
        }
    }
    protected void DeleteInfo(object sender, CommandEventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";

        msg = dA.run("DELETE FROM PATIENT_REGISTRATION WHERE PAT_ID='" + Request.QueryString["PatID"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg.StartsWith("ERROR")==true)
        {
            lblError.Text = msg;
            pnlError.Visible = true;
        }
        else
        {
            pnlSuccess.Visible = true;
        }
    }
    [System.Web.Services.WebMethod]
    public static string SaveInfo(NameValue[] frmV)
    {
        string msg = "";
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        List<string> gID = new List<string>() { frmV.Form("ctl00$contentForm$hdnPatID"), "PATIENT_REGISTRATION", "PAT_ID", HttpContext.Current.Session["userid"].ToString() };

        objD.xTable = "PATIENT_REGISTRATION";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "PAT_ID" };
        objD.KeyVal = new List<string>() { frmV.Form("ctl00$contentForm$hdnPatID") };

        objD.Column = new List<string>() { "PAT_NAME", "PAT_IC_NO", "PAT_REG_NO", "PAT_REG_DATE", "PAT_BIRTH_DATE", "PAT_SEX", "PAT_FATHER_HUSBAND_NAME", "PAT_ADDR1", "PAT_ADDR2", 
            "PAT_ADDR3", "PAT_EMAIL", "PAT_PHONE_R", "PAT_PHONE_O", "PAT_HANDPHONE", "PAT_OCCUPATION", "PAT_RACE", "PAT_NATIONALITY", "PAT_BLOOD_GROUP", "PAT_PREV_HISTORY",
            "PAT_COMPANY_ID", "PAT_STATUS", "PAT_EMP_NO", "PAT_RELATION", "PAT_RELATED_TO", "PAT_COMP_OUTLET", "PAT_COMPANY_OTH_DTLS", "PAT_KIN_NAME", "PAT_KIN_ICNO",
            "PAT_KIN_RELATION", "PAT_KIN_OCCUPATION", "PAT_KIN_PHONES", "PAT_KIN_ADDRESS", "PAT_GRN_NAME", "PAT_GRN_ICNO", "PAT_GRN_RELATION", "PAT_GRN_OCCUPATION", "PAT_GRN_PHONES",
            "PAT_GRN_ADDRESS", "USER_ID" };

        objD.CValue = new System.Collections.ArrayList();
        List<string> col = new List<string>()
        {
           frmV.Form("ctl00$contentForm$txtPatientName"),
           frmV.Form("ctl00$contentForm$txtICNo"),
           frmV.Form("ctl00$contentForm$txtFolderNo"),
           new vGeneral().convertDateForDB(frmV.Form("ctl00$contentForm$txtRegnDate")),
           new vGeneral().convertDateForDB(frmV.Form("ctl00$contentForm$txtBirthDate")),
           frmV.Form("ctl00$contentForm$lstSex"),
           frmV.Form("ctl00$contentForm$txtNextKin"),
           frmV.Form("ctl00$contentForm$txtAddr1"),
           frmV.Form("ctl00$contentForm$txtAddr2"),
           frmV.Form("ctl00$contentForm$txtAddr3"),
           "",
           frmV.Form("ctl00$contentForm$txtPhones"),
           "",
           frmV.Form("ctl00$contentForm$txtHandPhone"),
           "",
           frmV.Form("ctl00$contentForm$txtRace"),
           frmV.Form("ctl00$contentForm$txtNationality"),
           frmV.Form("ctl00$contentForm$lstBloodGroup"),
           frmV.Form("ctl00$contentForm$txtRemarks"),
           frmV.Form("ctl00$contentForm$txtCompany"),
           frmV.Form("ctl00$contentForm$lstStatus"),
           frmV.Form("ctl00$contentForm$txtEmpNo"),
           frmV.Form("ctl00$contentForm$txtRelation"),
           frmV.Form("ctl00$contentForm$txtEmployee"),
           frmV.Form("ctl00$contentForm$txtOutlet"),
           "",
           frmV.Form("ctl00$contentForm$txtNextKin"),
           frmV.Form("ctl00$contentForm$txtNKICNo"),
           frmV.Form("ctl00$contentForm$txtNKRelation"),
           frmV.Form("ctl00$contentForm$txtNKOccupation"),
           frmV.Form("ctl00$contentForm$txtNKPhones"),
           frmV.Form("ctl00$contentForm$txtNKAddress"),
           frmV.Form("ctl00$contentForm$txtGuarantor"),
           frmV.Form("ctl00$contentForm$txtGICNo"),
           frmV.Form("ctl00$contentForm$txtGRelation"),
           frmV.Form("ctl00$contentForm$txtGOccupation"),
           frmV.Form("ctl00$contentForm$txtGPhones"),
           frmV.Form("ctl00$contentForm$txtGAddress"),
           HttpContext.Current.Session["userid"].ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        msg = dbaction.saveCollection(objdata, gID);
        return msg;
    }
    protected objDL getCompanies()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        return dA.returnList("SELECT COMPANY_ID, COMPANY_NAME FROM COMPANY_MST ORDER BY COMPANY_NAME");
    }
    protected objDL getParams(string type)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        return dA.returnList("SELECT PARAM_ID, PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_TYPE='" + int.Parse(type) + "'");
    }
    protected string convertDateForDB(string dt)
    {
        string returnDate = "";

        returnDate = dt.Substring(6, 4) + "-" + dt.Substring(3, 2) + "-" + dt.Substring(0, 2);
        return returnDate;
    }
    protected string convertDateForForm(string dt)
    {
        string returnDate = "";
        returnDate = dt.Substring(0, 2) + "/" + dt.Substring(3, 2) + "/" + dt.Substring(6, 4);
        return returnDate;
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {

    }
}