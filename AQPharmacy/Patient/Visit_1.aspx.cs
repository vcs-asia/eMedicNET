using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Script.Serialization;
using Vijay;
using System.Text.RegularExpressions;
using System.Data;

public partial class Patient_Visit_1 : System.Web.UI.Page
{
    public string xDrugs = "";
    public string xServices = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtVisitTime.Text = DateTime.Now.ToString("HH:mm:ss");
            txtVisitDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            getCompanies();
            getAllDoctors();
            getVisitInfo();

            xDrugs = getAllDrugs();
            xServices = getAllServices();

            generateDynamicControls();
        }
        else
        {
            getbackDynamicControls();
        }
    }
    private void getCompanies()
    {
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMPANY_NAME, COMPANY_ID FROM COMPANY_MST ORDER BY COMPANY_NAME");

        txtCompany.DataSource = objdl.dataSet.Tables[0];
        txtCompany.DataValueField = "COMPANY_ID";
        txtCompany.DataTextField = "COMPANY_NAME";
        txtCompany.DataBind();
    }

    protected void gotoME(object sender, EventArgs e)
    {
        Response.Redirect("~/Patient/ME.aspx?_iD=" + Request.QueryString["PatID"].ToString() + "&_vD" + Request.QueryString["_vD"].ToString());
    }
    protected string getVisitLinks(string patientID)
    {
        //<li><a href="#"><i class="icon-pencil"></i> Edit</a></li>
        string str = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT VISIT_DATE, COMP_NAME, VISIT_ID FROM PATIENT_VISIT_MST JOIN COMP_MST ON COMP_ID = VISIT_DISC WHERE PAT_ID = '" + patientID + "' ORDER BY VISIT_DATE DESC");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                str += "<li><a href='" + ResolveUrl("~/Patient/PatientVisit.aspx") + "?PatID=" + Request.QueryString["PatID"].ToString() + "&_vD=" + objdl.dataSet.Tables[0].Rows[inc][2].ToString() + "'>" + ((DateTime)objdl.dataSet.Tables[0].Rows[inc][0]).ToString("dd/MM/yyyy") + " [" + objdl.dataSet.Tables[0].Rows[inc][1].ToString() + "]</a></li>";
            }
        }
        return str;
    }
    protected void generateDynamicControls()
    {
        string str = "";
        for (int row = 0; row < 1; row++)
        {
            str += "$('#tblServices > tbody:last').append('<tr><td><select id=\"service[]\" name=\"service[]\" style=\"width:90%\" onchange=\"getServiceDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + xServices + "</select></td><td><input type=\"text\" id=\"charge[]\" name=\"charge[]\"  style=\"width:90%\" value=\"0\"/></td><td><input type=\"text\" id=\"discount[]\" name=\"discount[]\" style=\"width:90%\" value=\"0\" onblur=\"scal(" + row + ")\" readonly/></td><td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:90%\" value=\"0\" onblur=\"calculate()\" readonly/><input type=\"hidden\" id=\"stype[]\" name=\"stype[]\"/></td></tr>');";
        }
        for (int row = 0; row < 5; row++)
        {
            str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + xDrugs + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"\"/><input type=\"hidden\" id=\"ucost[]\" name=\"ucost[]\" value=\"0\"/></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%\" value=\"0\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%\" value=\"0\" onblur=\"drugcal(" + row + ")\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%\" value=\"0\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%\" value=\"0\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td></tr>');";
        }
        str += "$('#txtDiagnosis').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getDiagnosis() + ", create: false });";
        str += "$('#txtExam').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getPhysicalExams() + ", create: false });";

        Page page = HttpContext.Current.CurrentHandler as Page;
        page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
    }
    protected void getbackDynamicControls()
    {
        if (Request.Form.Count > 0)
        {
            if (Request.Form.GetValues("service[]") == null) return;

            string str = "";

            var serv = Request.Form.GetValues("service[]");
            var scst = Request.Form.GetValues("charge[]");
            var sdsc = Request.Form.GetValues("discount[]");
            var samt = Request.Form.GetValues("amt[]");
            var styp = Request.Form.GetValues("stype[]");

            var drug = Request.Form.GetValues("drug[]");
            var ddes = Request.Form.GetValues("mdesc[]");
            var dcst = Request.Form.GetValues("ucost[]");
            var ddsc = Request.Form.GetValues("mdisc[]");
            var damt = Request.Form.GetValues("mamt[]");
            var dtyp = Request.Form.GetValues("dtype[]");
            var dsel = Request.Form.GetValues("scost[]");
            var dqty = Request.Form.GetValues("mqty[]");
            var tran = Request.Form.GetValues("tran[]");

            for (int row = 0; row < serv.Count(); row++)
            {
                str += "$('#tblServices > tbody:last').append('<tr><td><select id=\"service[]\" name=\"service[]\" style=\"width:90%\" onchange=\"getServiceDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllServices(serv[row]) + "</select></td><td><input type=\"text\" id=\"charge[]\" name=\"charge[]\"  style=\"width:90%\" value=\"" + scst[row] + "\"/></td><td><input type=\"text\" id=\"discount[]\" name=\"discount[]\" style=\"width:90%\" value=\"" + sdsc[row] + "\" onblur=\"scal(" + row + ")\" readonly/></td><td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:90%\" value=\"" + samt[row] + "\" onblur=\"calculate()\" readonly/><input type=\"hidden\" id=\"stype[]\" name=\"stype[]\" value=\"" + styp[row] + "\"/></td></tr>');";
            }
            for (int row = 0; row < drug.Count(); row++)
            {
                str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(drug[row]) + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"" + ddes[row] + "\"/><input type=\"hidden\" id=\"ucost[]\" name=\"ucost[]\" value=\"" + dcst[row] + "\"/></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%\" value=\"" + dsel[row] + "\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%\" value=\"" + dqty[row] + "\" onblur=\"drugcal(" + row + ")\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%\" value=\"" + damt[row] + "\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%\" value=\"" + ddsc[row] + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + tran[row] + "\"/></td></tr>');";
            }
            str += "$('#txtDiagnosis').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getDiagnosis() + ", create: false });";
            str += "$('#txtExam').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getPhysicalExams() + ", create: false });";

            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "$('.txtService').attr('data-source','" + getAllServices() + "');</script>");
        }
    }
    protected void getAllDoctors()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();
        
        objdl = dA.returnList("SELECT DOC_ID, DOC_NAME FROM DOCTOR_MST ORDER BY DOC_NAME");
        txtDoctor.DataSource = objdl.dataSet;
        txtDoctor.DataTextField = "DOC_NAME";
        txtDoctor.DataValueField = "DOC_ID";
        txtDoctor.DataBind();
    }

    protected string getAllServices()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT SERVICE_NAME, SERVICE_ID FROM SERVICE_MST ORDER BY SERVICE_NAME ASC");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
        }
        return str;
    }
    protected string getAllServices(string serviceID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";


        objdl = dA.returnList("SELECT SERVICE_NAME, SERVICE_ID FROM SERVICE_MST ORDER BY SERVICE_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count>0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                if (serviceID==objdl.dataSet.Tables[0].Rows[row][1].ToString())
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\" selected>" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
                else
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
        }
        return str;
    }
    protected string getAllDrugs()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT MED_NAME, MED_ID FROM MEDICINE_MST ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
                str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
        }
        return str;
    }
    protected string getAllDrugs(string medID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT MED_NAME, MED_ID FROM MEDICINE_MST ORDER BY MED_NAME");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count; row++)
            {
                if (objdl.dataSet.Tables[0].Rows[row][1].ToString() == medID)
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\" selected>" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";
                else
                    str += "<option value=\"" + objdl.dataSet.Tables[0].Rows[row][1].ToString() + "\">" + Regex.Replace(objdl.dataSet.Tables[0].Rows[row][0].ToString(), @"[^0-9a-zA-Z]+", " ") + "</option>";

            }
        }
        return str;
    }
    [System.Web.Services.WebMethod]
    public static string getDrugDtls(string medID)
    {
        string rVal = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (medID != "")
        {
            objdl = dA.returnList("SELECT MED_ID, MED_UNIT_COST, MED_OUT_SELLING_COST, '' AS BAL, MED_TYPE FROM MEDICINE_MST WHERE MED_ID='" + medID + "'");
            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0][1].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][2].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][0].ToString();
            }
        }
        return rVal;
    }
    [System.Web.Services.WebMethod]
    public static string getServiceDtls(string serviceID)
    {
        string rVal = "";

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        if (serviceID != "")
        {
            objdl = dA.returnList("SELECT SERVICE_ID, SERVICE_CHARGE, SERVICE_TYPE FROM SERVICE_MST WHERE SERVICE_ID='" + serviceID + "'");
            if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
            {
                rVal = objdl.dataSet.Tables[0].Rows[0][0].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][1].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][2].ToString();
            }
        }
        return rVal;
    }
    protected void getVisitInfo()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string sQuery = "";

        if (Request.QueryString.Count > 0)
        {
            if (Request.QueryString["_vD"].ToString() == "" || Request.QueryString["_vD"].ToString() == "0")
            {
                sQuery = "SELECT PAT_NAME, PAT_IC_NO, COMPANY_ID, COMPANY_NAME, PAT_BIRTH_DATE, PAT_PREV_HISTORY, getAge(PAT_ID) AS AGE  FROM PATIENT_REGISTRATION JOIN COMPANY_MST ON COMPANY_MST.COMPANY_ID=PAT_COMPANY_ID WHERE PAT_ID='" + Request.QueryString["PatID"].ToString() + "'";
                objdl = dA.returnList(sQuery);
                if (objdl.flaG == true)
                {
                    txtPatName.Text = objdl.dataSet.Tables[0].Rows[0]["PAT_NAME"].ToString() + "\r\n" +
                    objdl.dataSet.Tables[0].Rows[0]["PAT_IC_NO"].ToString() + "\r\n" +
                    ((DateTime)objdl.dataSet.Tables[0].Rows[0]["PAT_BIRTH_DATE"]).ToString("dd/MM/yyyy") + "\r\n" +
                    objdl.dataSet.Tables[0].Rows[0]["AGE"].ToString();
                    txtCompany.SelectedValue = objdl.dataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString();
                    txtDoctor.SelectedValue = Session["docid"].ToString();
                    txtPrevHistory.Text = objdl.dataSet.Tables[0].Rows[0]["PAT_PREV_HISTORY"].ToString();
                    vID.Value = Request.QueryString["_vD"].ToString();
                    pID.Value = Request.QueryString["PatID"].ToString();
                    qID.Value = Request.QueryString["_qD"].ToString();
                    pDS.Value = Request.QueryString["_pS"].ToString();
                    uID.Value = HttpContext.Current.Session["userid"].ToString();
                    
                    return;
                }
            }
            else
            {
                //sQuery = "SELECT getAge(PATIENT_VISIT_MST.PAT_ID) AS AGE, VISIT_DATE, VISIT_TIME, PAT_PREV_HISTORY, PATIENT_VISIT_MST.DOC_ID AS DOC_ID, DOC_NAME, VISIT_TOT_AMT, PAT_NAME, PAT_IC_NO, PAT_BIRTH_DATE, PATIENT_VISIT_MST.COMPANY_ID AS COMPANY_ID, COMPANY_NAME, MC_DAYS, VISIT_DISCOUNT, MC_DATE, VISIT_NEXT_APPT_DATE, CASH_BILL_NO, VISIT_ALLERGIES, VISIT_REMARKS, VISIT_HGT, VISIT_WGT, VISIT_BMI, VISIT_DISC FROM PATIENT_VISIT_MST JOIN COMPANY_MST ON COMPANY_MST.COMPANY_ID=PATIENT_VISIT_MST.COMPANY_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=PATIENT_VISIT_MST.PAT_ID JOIN DOCTOR_MST ON DOCTOR_MST.DOC_ID=PATIENT_VISIT_MST.DOC_ID WHERE VISIT_ID='" + Request.QueryString["_vD"].ToString() + "'";
                sQuery = "SELECT getAge(PATIENT_VISIT_MST.PAT_ID) AS AGE, VISIT_DATE, VISIT_DISC, VISIT_TIME, PAT_PREV_HISTORY, PATIENT_VISIT_MST.DOC_ID AS DOC_ID, DOC_NAME, VISIT_TOT_AMT, PAT_NAME, PAT_IC_NO, PAT_BIRTH_DATE, PATIENT_VISIT_MST.COMPANY_ID AS COMPANY_ID, COMPANY_NAME, MC_DAYS, VISIT_DISCOUNT, MC_DATE, VISIT_NEXT_APPT_DATE, CASH_BILL_NO, VISIT_ALLERGIES, VISIT_REMARKS, VISIT_HGT, VISIT_WGT, VISIT_BMI, VISIT_US FROM PATIENT_VISIT_MST JOIN COMPANY_MST ON COMPANY_MST.COMPANY_ID=PATIENT_VISIT_MST.COMPANY_ID JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID=PATIENT_VISIT_MST.PAT_ID JOIN DOCTOR_MST ON DOCTOR_MST.DOC_ID=PATIENT_VISIT_MST.DOC_ID WHERE VISIT_ID='" + Request.QueryString["_vD"].ToString() + "'";
                objdl = dA.returnList(sQuery);
                if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
                {
                    vID.Value = Request.QueryString["_vD"].ToString();
                    pID.Value = Request.QueryString["PatID"].ToString();
                    if (Request.QueryString["_qD"] != null)
                        qID.Value = Request.QueryString["_qD"].ToString();
                    else
                        qID.Value = "0";
                    pDS.Value = objdl.dataSet.Tables[0].Rows[0]["VISIT_DISC"].ToString();
                    txtPatName.Text = objdl.dataSet.Tables[0].Rows[0]["PAT_NAME"].ToString() + "\r\n" +
                    objdl.dataSet.Tables[0].Rows[0]["PAT_IC_NO"].ToString() + "\r\n" +
                    ((DateTime)objdl.dataSet.Tables[0].Rows[0]["PAT_BIRTH_DATE"]).ToString("dd/MM/yyyy") + "\r\n" +
                    objdl.dataSet.Tables[0].Rows[0]["AGE"].ToString();
                    txtCompany.SelectedValue = objdl.dataSet.Tables[0].Rows[0]["COMPANY_ID"].ToString();
                    txtPrevHistory.Text = objdl.dataSet.Tables[0].Rows[0]["PAT_PREV_HISTORY"].ToString();
                    txtVisitDate.Text = ((DateTime)objdl.dataSet.Tables[0].Rows[0]["VISIT_DATE"]).ToString("dd/MM/yyyy");
                    txtVisitTime.Text = ((DateTime)objdl.dataSet.Tables[0].Rows[0]["VISIT_TIME"]).ToString("hh:mm");
                    txtDoctor.SelectedValue = objdl.dataSet.Tables[0].Rows[0]["DOC_ID"].ToString();
                    txtNApptDate.Text = ((objdl.dataSet.Tables[0].Rows[0]["VISIT_NEXT_APPT_DATE"].ToString() == objdl.dataSet.Tables[0].Rows[0]["VISIT_DATE"].ToString()) ? "" : ((DateTime)objdl.dataSet.Tables[0].Rows[0]["VISIT_NEXT_APPT_DATE"]).ToString("dd/MM/yyyy"));
                    txtBillNo.Text = objdl.dataSet.Tables[0].Rows[0]["CASH_BILL_NO"].ToString();
                    decimal totAmt = decimal.Parse(objdl.dataSet.Tables[0].Rows[0]["VISIT_TOT_AMT"].ToString());
                    txtRemarks.Text = objdl.dataSet.Tables[0].Rows[0]["VISIT_REMARKS"].ToString();
                    txtAmount.Text = totAmt.ToString("0.00");
                    if (objdl.dataSet.Tables[0].Rows[0]["VISIT_DISCOUNT"] == System.DBNull.Value)
                        txtVisitDiscount.Text = "0.00";
                    else
                    {
                        decimal discount = decimal.Parse(objdl.dataSet.Tables[0].Rows[0]["VISIT_DISCOUNT"].ToString());
                        txtVisitDiscount.Text = discount.ToString("0.00");
                    }
                    txtMCDays.Text = objdl.dataSet.Tables[0].Rows[0]["MC_DAYS"].ToString();
                    txtPHeight.Text = objdl.dataSet.Tables[0].Rows[0]["VISIT_HGT"].ToString();
                    txtPWeight.Text = objdl.dataSet.Tables[0].Rows[0]["VISIT_WGT"].ToString();
                    txtBMI.Text = objdl.dataSet.Tables[0].Rows[0]["VISIT_BMI"].ToString();
                    if (!DBNull.Value.Equals(objdl.dataSet.Tables[0].Rows[0]["VISIT_US"]))
                        lstUrineStatus.SelectedValue = objdl.dataSet.Tables[0].Rows[0]["VISIT_US"].ToString();

                    if (objdl.dataSet.Tables[0].Rows[0]["MC_DATE"].ToString() != "")
                        txtMCDate.Text = ((DateTime)objdl.dataSet.Tables[0].Rows[0]["MC_DATE"]).ToString("dd/MM/yyyy");

                    dbAction dAS = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
                    objDL objdlS = new objDL();

                    string str = "";

                    decimal totalServicesAmount = 0;
                    decimal totalServicesDiscountAmount = 0;

                    sQuery = "SELECT VISIT_SERVICE_DTLS.SERVICE_ID, SERVICE_ORG_AMT, SERVICE_MOD_AMT, SERVICE_DISC_AMT, SERVICE_CHARGE, SERVICE_TYPE FROM VISIT_SERVICE_DTLS JOIN SERVICE_MST ON SERVICE_MST.SERVICE_ID=VISIT_SERVICE_DTLS.SERVICE_ID WHERE VISIT_ID=" + Request.QueryString["_vD"].ToString();
                    objdlS = dAS.returnList(sQuery);
                    if (objdlS.flaG == true && objdlS.dataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int row = 0; row < 1; row++)
                        {
                            if (row < objdlS.dataSet.Tables[0].Rows.Count)
                            {
                                DataRow Col = objdlS.dataSet.Tables[0].Rows[row];
                                str += "$('#tblServices > tbody:last').append('<tr><td><select id=\"service[]\" name=\"service[]\" style=\"width:90%\" onchange=\"getServiceDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllServices(Col[0].ToString()) + "</select></td><td><input type=\"text\" id=\"charge[]\" name=\"charge[]\"  style=\"width:90%;text-align:right\" value=\"" + ((decimal)Col[1]).ToString("0.00") + "\"/></td><td><input type=\"text\" id=\"discount[]\" name=\"discount[]\" style=\"width:90%;text-align:right\" value=\"" + ((decimal)Col[3]).ToString("0.00") + "\" onblur=\"scal(" + row + ")\" readonly/></td><td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:90%;text-align:right\" value=\"" + ((decimal)Col[2]).ToString("0.00") + "\" onblur=\"calculate()\" readonly/><input type=\"hidden\" id=\"stype[]\" name=\"stype[]\" val=\"" + Col[5].ToString() + "\"/></td></tr>');";
                                totalServicesAmount += (decimal)Col[2];
                                totalServicesDiscountAmount += (decimal)Col[3];
                            }
                            else
                            {
                                str += "$('#tblServices > tbody:last').append('<tr><td><select id=\"service[]\" name=\"service[]\" style=\"width:90%\" onchange=\"getServiceDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllServices() + "</select></td><td><input type=\"text\" id=\"charge[]\" name=\"charge[]\"  style=\"width:90%;text-align:right\" value=\"0\"/></td><td><input type=\"text\" id=\"discount[]\" name=\"discount[]\" style=\"width:90%;text-align:right\" value=\"0\" onblur=\"scal(" + row + ")\" readonly/></td><td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:90%;text-align:right\" value=\"0\" onblur=\"calculate()\" readonly/><input type=\"hidden\" id=\"stype[]\" name=\"stype[]\"/></td></tr>');";
                            }
                        }
                        str += "$('#txtDiagnosis').val('" + getVisitDiagnosis(int.Parse(Request.QueryString["_vD"].ToString())) + "');$('#txtExam').val('" + getVisitExam(int.Parse(Request.QueryString["_vD"].ToString())) + "');";
                        str += "$('#txtDiagnosis').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getDiagnosis() + ", create: false });";
                        str += "$('#txtExam').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getPhysicalExams() + ", create: false });";
                    }
                    else
                    {
                        for (int row = 0; row < 1; row++)
                        {
                            str += "$('#tblServices > tbody:last').append('<tr><td><select id=\"service[]\" name=\"service[]\" style=\"width:90%\" onchange=\"getServiceDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllServices() + "</select></td><td><input type=\"text\" id=\"charge[]\" name=\"charge[]\"  style=\"width:90%;text-align:right\" value=\"0\"/></td><td><input type=\"text\" id=\"discount[]\" name=\"discount[]\" style=\"width:90%;text-align:right\" value=\"0\" onblur=\"scal(" + row + ")\" readonly/></td><td><input type=\"text\" id=\"amt[]\" name=\"amt[]\" style=\"width:90%;text-align:right\" value=\"0\" onblur=\"calculate()\" readonly/><input type=\"hidden\" id=\"stype[]\" name=\"stype[]\"/></td></tr>');";
                        }
                        str += "$('#txtDiagnosis').val('" + getVisitDiagnosis(int.Parse(Request.QueryString["_vD"].ToString())) + "');$('#txtExam').val('" + getVisitExam(int.Parse(Request.QueryString["_vD"].ToString())) + "');";
                        str += "$('#txtDiagnosis').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getDiagnosis() + ", create: false });";
                        str += "$('#txtExam').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getPhysicalExams() + ", create: false });";

                    }

                    //Getting Drugs.
                    dbAction dDS = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
                    objDL objdlD = new objDL();

                    decimal totalDrugsAmount = 0;
                    decimal totalDrugsDiscount = 0;

                    sQuery = "SELECT VISIT_MEDICINE_DTLS.MED_ID, MED_UNIT_PRICE, MED_AMT, 0.00 AS MED_DISC_AMT, MED_QTY, MED_DESC, TRAN_ID FROM VISIT_MEDICINE_DTLS JOIN MEDICINE_MST ON VISIT_MEDICINE_DTLS.MED_ID=MEDICINE_MST.MED_ID WHERE VISIT_MEDICINE_DTLS.VISIT_ID=" + Request.QueryString["_vD"].ToString();
                    objdlD = dDS.returnList(sQuery);
                    if (objdlD.flaG == true && objdlD.dataSet.Tables[0].Rows.Count > 0)
                    {

                        for (int row = 0; row < 5; row++)
                        {
                            if (row < objdlD.dataSet.Tables[0].Rows.Count)
                            {
                                DataRow Col = objdlD.dataSet.Tables[0].Rows[row];
                                //str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option>[Please select...]</option>" + getAllDrugs(Col[0].ToString()) + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"" + Col[5].ToString() + "\"/></td><td><input type=\"text\" id=\"ucost[]\" name=\"ucost[]\" style=\"width:90%\" value=\"" + ((decimal)Col[1]).ToString("0.00") + "\" readonly/></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%\" value=\"" + ((decimal)Col[1]).ToString("0.00") +"\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%\" value=\"" + int.Parse(Col[4].ToString()) + "\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%\" value=\"" + ((decimal)Col[2]).ToString("0.00") + "\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%\" value=\"" + ((decimal)Col[3]).ToString("0.00") + "\"/></td></tr>');";
                                str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs(Col[0].ToString()) + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"" + Col[5] + "\"/><input type=\"hidden\" id=\"ucost[]\" name=\"ucost[]\" value=\"" + ((decimal)Col[1]).ToString("0.00") + "\" /></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%;text-align:right\" value=\"" + ((decimal)Col[1]).ToString("0.00") + "\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%;text-align:right\" value=\"" + Col[4].ToString() + "\" onblur=\"drugcal(" + row + ")\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%;text-align:right\" value=\"" + ((decimal)Col[2]).ToString("0.00") + "\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%;text-align:right\" value=\"" + Col[3] + "\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"" + Col[6] + "\"/></td></tr>');";
                                totalDrugsAmount += (decimal)Col[2];
                                totalDrugsDiscount += (decimal)Col[3];
                            }
                            else
                            {
                                str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"\"/><input type=\"hidden\" id=\"ucost[]\" name=\"ucost[]\" value=\"0\"/></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%;text-align:right\" value=\"0\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%;text-align:right\" value=\"0\" onblur=\"drugcal(" + row + ")\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%;text-align:right\" value=\"0\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%;text-align:right\" value=\"0\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td></tr>');";
                            }
                        }
                    }
                    else
                    {
                        for (int row = 0; row < 5; row++)
                        {
                            str += "$('#tblDrugs > tbody:last').append('<tr><td><select id=\"drug[]\" name=\"drug[]\" style=\"width:90%\" onchange=\"getDrugDtls(" + row + ")\"><option value=\"\">[Please select...]</option>" + getAllDrugs() + "</select></td><td><input type=\"text\" id=\"mdesc[]\" name=\"mdesc[]\"  style=\"width:90%\" value=\"\"/><input type=\"hidden\" id=\"ucost[]\" name=\"ucost[]\" value=\"0\"/></td><td><input type=\"text\" id=\"scost[]\" name=\"scost[]\" style=\"width:90%\" value=\"0\"/></td><td><input type=\"text\" id=\"mqty[]\" name=\"mqty[]\" style=\"width:90%\" value=\"0\" onblur=\"drugcal(" + row + ")\"/></td><td><input type=\"text\" id=\"mamt[]\" name=\"mamt[]\" style=\"width:90%\" value=\"0\"/></td><td><input type=\"text\" id=\"mdisc[]\" name=\"mdisc[]\" style=\"width:90%\" value=\"0\"/><input type=\"hidden\" id=\"tran[]\" name=\"tran[]\" value=\"\"/></td></tr>');";
                        }

                    }

                    str += "$('#txtTCharges').val((" + totalServicesAmount + ").toFixed(2));$('#txtTDiscount').val((" + totalServicesDiscountAmount + ").toFixed(2));$('#txtTSAmount').val((" + totalServicesAmount + ").toFixed(2));$('#txtTDAmount').val((" + totalDrugsAmount + ").toFixed(2));$('#txtTDDiscount').val((" + totalDrugsDiscount + ").toFixed(2));";

                    Page page = HttpContext.Current.CurrentHandler as Page;
                    page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
                }
            }
        }
    }
    protected void saveInfo(object sender, EventArgs e)
    {
        string msg = "";
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        try
        {
            List<string> mainInfo = new List<string>();
            mainInfo.Add(Request.QueryString[0].ToString()); //0
            mainInfo.Add(txtVisitDate.Text); //1
            mainInfo.Add(txtVisitTime.Text); //2
            mainInfo.Add(txtCompany.SelectedValue); //3
            mainInfo.Add(txtDoctor.SelectedValue); //4
            mainInfo.Add(txtBillNo.Text); //5
            mainInfo.Add(txtNApptDate.Text); //6
            mainInfo.Add(txtAmount.Text); //7
            mainInfo.Add(txtVisitDiscount.Text); //8
            mainInfo.Add(txtMCDays.Text); //9
            mainInfo.Add(txtMCDate.Text); //10

            mainInfo.Add(""); //11
            mainInfo.Add(""); //12
            mainInfo.Add(txtDiagnosis.Text); //13

            mainInfo.Add("");//14
            mainInfo.Add(txtRemarks.Text);//15
            mainInfo.Add("");//16
            mainInfo.Add("");//17
            mainInfo.Add("");//18

            if (Request.QueryString["_qD"]!=null)
                mainInfo.Add(Request.QueryString["_qD"].ToString());//19
            else
                mainInfo.Add("");//19
            mainInfo.Add(Request.QueryString["_vD"].ToString());//20
            mainInfo.Add(txtPrevHistory.Text); //21
            mainInfo.Add(""); //22

            mainInfo.Add(txtPHeight.Text); //23
            mainInfo.Add(txtPWeight.Text); //24
            mainInfo.Add(txtBMI.Text); //25

            ArrayList row = new ArrayList();

            if (Request.Form.GetValues("service[]") != null)
            {
                var serviceID = Request.Form.GetValues("service[]"); //Service ID
                var charges = Request.Form.GetValues("charge[]"); //Charges
                var discount = Request.Form.GetValues("discount[]");//Discount

                decimal gTotal = 0;

                if (serviceID.Count() > 0)
                {
                    for (int i = 0; i < serviceID.Count(); i++)
                    {
                        if (serviceID[i].ToString() != "")
                        {
                            List<string> col = new List<string>();

                            col.Add(serviceID[i].ToString()); //0
                            col.Add(charges[i].ToString()); //1
                            col.Add(discount[i].ToString()); //2

                            col.Add(""); //3
                            col.Add(""); //4
                            col.Add(""); //5
                            col.Add(""); //6
                            col.Add("S"); //7
                            col.Add("");//8

                            gTotal += (decimal.Parse(charges[i]) - decimal.Parse(discount[i]));

                            row.Add(col);
                        }
                    }
                    mainInfo[7] = gTotal.ToString();
                }
            }
            if (Request.Form.GetValues("drug[]") != null)
            {
                var drug = Request.Form.GetValues("drug[]"); //Drug Name for ID
                var damt = Request.Form.GetValues("mamt[]"); //Amount
                var sdisc = Request.Form.GetValues("mdisc[]"); //Discount
                var sell = Request.Form.GetValues("scost[]"); //Selling cost
                var cost = Request.Form.GetValues("ucost[]"); //Unit Cost
                var mdesc = Request.Form.GetValues("mdesc[]"); //Dispensing instructions
                var dqty = Request.Form.GetValues("mqty[]"); //Dispensed quantity
                var tran = Request.Form.GetValues("tran[]"); //Transaction ID

                if (drug.Count() > 0)
                {
                    for (int i = 0; i < drug.Count(); i++)
                    {
                        if (drug[i].ToString() != "")
                        {
                            List<string> col = new List<string>();

                            col.Add(drug[i].ToString()); //0
                            col.Add(damt[i].ToString()); //1
                            col.Add(sdisc[i].ToString()); //2

                            col.Add(sell[i].ToString()); //3
                            col.Add(cost[i].ToString()); //4
                            col.Add(mdesc[i].ToString()); //5
                            col.Add(dqty[i].ToString()); //6
                            col.Add("D"); //7
                            col.Add(tran[i].ToString()); //7

                            row.Add(col);
                        }
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            pnlError.Visible = true;
        }
    }
    protected string getVisitDiagnosis(int visitID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT COMP_ID FROM VISIT_COMP_DTLS WHERE COMP_TYPE=0 AND VISIT_ID=" + visitID);
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    str += objdl.dataSet.Tables[0].Rows[i]["COMP_ID"].ToString();
                }
                else
                {
                    str += "," + objdl.dataSet.Tables[0].Rows[i]["COMP_ID"].ToString();
                }
            }
        }
        return str;
    }
    protected string getPatientAllergies(int patID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT ALLERGY_ID FROM PATIENT_ALLERGIES WHERE PAT_ID=" + patID);
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    str += objdl.dataSet.Tables[0].Rows[i]["ALLERGY_ID"].ToString();
                }
                else
                {
                    str += "," + objdl.dataSet.Tables[0].Rows[i]["ALLERGY_ID"].ToString();
                }
            }
        }
        return str;
    }
    protected string getVisitExam(int visitID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string str = "";

        objdl = dA.returnList("SELECT COMP_ID FROM VISIT_COMP_DTLS  WHERE COMP_TYPE=1 AND VISIT_ID=" + visitID);
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    str += objdl.dataSet.Tables[0].Rows[i]["COMP_ID"].ToString();
                }
                else
                {
                    str += "," + objdl.dataSet.Tables[0].Rows[i]["COMP_ID"].ToString();
                }
            }
        }
        return str;
    }
    protected string getDiagnosis()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        var eachDiagnosis = new List<Diagnosis>();
        objdl = dA.returnList("SELECT COMP_ID, COMP_NAME FROM COMP_MST WHERE COMP_TYPE=0");
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                eachDiagnosis.Add(new Diagnosis() { ID = objdl.dataSet.Tables[0].Rows[i][0].ToString(), Name = objdl.dataSet.Tables[0].Rows[i][1].ToString() });
            }
        }

        var serializer = new JavaScriptSerializer();
        var serializedResult = serializer.Serialize(eachDiagnosis);

        return serializedResult;
    }
    protected string getPhysicalExams()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        var eachExam = new List<Diagnosis>();
        objdl = dA.returnList("SELECT COMP_ID, COMP_NAME FROM COMP_MST WHERE COMP_TYPE=1");
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                eachExam.Add(new Diagnosis() { ID = objdl.dataSet.Tables[0].Rows[i][0].ToString(), Name = objdl.dataSet.Tables[0].Rows[i][1].ToString() });
            }
        }

        var serializer = new JavaScriptSerializer();
        var serializedResult = serializer.Serialize(eachExam);

        return serializedResult;
    }
    public class Diagnosis
    {
        public string ID { get; set; }
        public string Name { get; set; }

    }
    public class Allergy
    {
        public string ID { get; set; }
        public string Name { get; set; }
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
    protected void deleteVisit(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = true;
    }
    protected void deleteMe(object sender, EventArgs e)
    {
        string msg = "";

        try
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            msg = dA.run("DELETE FROM PATIENT_VISIT_MST WHERE VISIT_ID='" + Request.QueryString["_vD"] + "'", HttpContext.Current.Session["userid"].ToString());
            if (!msg.StartsWith("SUCCESS"))
            {
                pnlError.Visible = true;
                lblError.Text = msg;
            }
            else
            {
                msg = dA.run("UPDATE PAT_QUEUE SET VISIT_ID=0 WHERE PAT_QID = '" + Request.QueryString["_qD"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
                if (!msg.StartsWith("SUCCESS"))
                {
                    pnlError.Visible = true;
                    lblError.Text = msg;
                }
                else
                {
                    pnlSuccess.Visible = true;
                    lblSuccess.Text = "Deletion operation is succeeded. Please refresh the Patients List Page.";
                    pnlDeleteAlert.Visible = false;
                }
            }
        }
        catch(Exception ex)
        {
            lblError.Text = ex.ToString();
            pnlError.Visible = true;
        }
    }
    protected void cancelDelete(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = false;
    }
    private string getAllergies()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        var eachAllergy = new List<Allergy>();

        objdl = dA.returnList("SELECT ALLERGY_ID, ALLERGY_NAME FROM ALLERGY_MST ORDER BY ALLERGY_NAME");
        if (objdl.flaG = true && objdl.dataSet.Tables[0].Rows.Count >0)
        {
            for (int i=0; i < objdl.dataSet.Tables[0].Rows.Count;i++)
            {
                eachAllergy.Add(new Allergy() { ID = objdl.dataSet.Tables[0].Rows[i][0].ToString(), Name = objdl.dataSet.Tables[0].Rows[i][1].ToString() });
            }
        }

        var serializer = new JavaScriptSerializer();
        var serializedResult = serializer.Serialize(eachAllergy);

        return serializedResult;
    }

    [System.Web.Services.WebMethod]
    public static string unlockRecord(string qID, string tblName)
    {
        dbAction db = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = db.run("DELETE FROM RECORD_LOCK_INFO WHERE ID='" + qID + "' AND TBLNAME = '" + tblName + "' AND USER_ID = '" + HttpContext.Current.Session["uid"].ToString() + "'", HttpContext.Current.Session["userid"].ToString());
        return msg;
    }

    [System.Web.Services.WebMethod]
    public static string saveVisit(NameValue[] formVars)
    {
        string msg = "";
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        List<string> col = new List<string>();
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        vGeneral vG = new vGeneral();

        string refno = formVars.Form("ctl00$contentForm$vID");
        List<string> gID = new List<string> { refno, "PATIENT_VISIT_MST", "VISIT_ID", HttpContext.Current.Session["userid"].ToString() };

        decimal drugsTotalAmount = 0; decimal serviceTotalAmount = 0;

        //PATIENT_VISIT_MST
        objD.xTable = "PATIENT_VISIT_MST";
        objD.Delete = false;

        objD.Column = new List<string>() {"VISIT_ID", "PAT_ID", "VISIT_DATE", "VISIT_TIME", "DOC_ID", "VISIT_REMARKS", "VISIT_TOT_AMT", "COMPANY_ID",
        "VISIT_DISC", "VISIT_DISCOUNT", "CASH_BILL_NO", "VISIT_TIME_OUT", "VISIT_NEXT_APPT_DATE", "MC_DAYS", "MC_DATE", "USER_ID", "VISIT_ALLERGIES", 
        "VISIT_HGT", "VISIT_WGT", "VISIT_BMI", "VISIT_US"};

        objD.KeyCol = new List<string>() { "VISIT_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.CValue = new ArrayList();

        col = new List<string>() 
        { 
            refno, 
            formVars.Form("ctl00$contentForm$pID"),
            new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtVisitDate")),
            String.Concat(new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtVisitDate")), " ", formVars.Form("ctl00$contentForm$txtVisitTime")),
            formVars.Form("ctl00$contentForm$txtDoctor"),
            formVars.Form("ctl00$contentForm$txtRemarks"),
            new vGeneral().getNumberD(formVars.Form("ctl00$contentForm$txtAmount")).ToString(),
            formVars.Form("ctl00$contentForm$txtCompany"),
            formVars.Form("ctl00$contentForm$pDS"),
            formVars.Form("ctl00$contentForm$txtVisitDiscount"),
            formVars.Form("ctl00$contentForm$txtBillNo"),
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            (formVars.Form("ctl00$contentForm$txtNAppDate")=="" ? DateTime.Now.ToString("yyyy-MM-dd") : new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtNAppDate"))),
            formVars.Form("ctl00$contentForm$txtMCDays"),
            (formVars.Form("ctl00$contentForm$txtMCDate")=="" ? DateTime.Now.ToString("yyyy-MM-dd") : new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtMCDate"))),
            formVars.Form("ctl00$contentForm$uID"),
            formVars.Form("ctl00$contentForm$txtAllergies"),
            new vGeneral().getNumberV(formVars.Form("ctl00$contentForm$txtPHeight")).ToString(),
            new vGeneral().getNumberV(formVars.Form("ctl00$contentForm$txtPWeight")).ToString(),
            new vGeneral().getNumberV(formVars.Form("ctl00$contentForm$txtBMI")).ToString(),
            new vGeneral().getNumberV(formVars.Form("ctl00$contentForm$txtAge")).ToString()
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        //VISIT_CASE_DTLS
        objD = new objData();
        col = new List<string>();

        objD.xTable = "VISIT_CASE_DTLS";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "VISIT_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "VISIT_ID", "PAT_ID", "VISIT_SYMPTOMS", "VISIT_FINDINGS", "VISIT_TREATMENT_NOTES", "VISIT_SURGERY_NOTES" };
        objD.CValue = new ArrayList();

        col = new List<string>()
        {
            refno,
            formVars.Form("ctl00$contentForm$pID"),
            formVars.Form("ctl00$contentForm$txtSigns"),
            formVars.Form("ctl00$contentForm$txtFindings"),
            formVars.Form("ctl00$contentForm$txtTreatmentNotes"),
            formVars.Form("ctl00$contentForm$txtSurgeryNotes")
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        //VISIT_CHILD_DTLS
        /*
        objD = new objData();

        objD.xTable = "VISIT_CHILD_DTLS";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "VISIT_ID" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "VISIT_ID", "PAT_ID", "CHILD_AGE", "CHILD_LENGTH", "CHILD_WEIGHT", "CHILD_HEAD_CIRC" };
        objD.CValue = new ArrayList();
        col = new List<string>()
        {
            refno,
            formVars.Form("ctl00$contentForm$pID"),
            formVars.Form("ctl00$contentForm$txtAge"),
            formVars.Form("ctl00$contentForm$txtLength"),
            formVars.Form("ctl00$contentForm$txtWeight"),
            formVars.Form("ctl00$contentForm$txtCirc")
        };
        objD.CValue.Add(col);
        objdata.Add(objD);
        */

        //VISIT_COMP_DTLS - DIAGNOSIS
        if (formVars.Form("ctl00$contentForm$txtDiagnosis").ToString().Trim() != "")
        {
            objD = new objData();
            objD.xTable = "VISIT_COMP_DTLS";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "VISIT_ID", "COMP_TYPE" };
            objD.KeyVal = new List<string>() { refno, "0" };

            objD.Column = new List<string>() { "VISIT_ID", "PAT_ID", "COMP_ID", "COMP_TYPE" };
            objD.CValue = new ArrayList();

            foreach (string eachdiagnosis in formVars.Form("ctl00$contentForm$txtDiagnosis").Split(new char[] { ',' }))
            {
                col = new List<string>()
                {
                    refno,
                    formVars.Form("ctl00$contentForm$pID"),
                    eachdiagnosis,
                    "0"
                };
                objD.CValue.Add(col);
            }
            objdata.Add(objD);
        }

        //VISIT_COMP_DTLS - HEALTH COMPLAINTS
        if (formVars.Form("ctl00$contentForm$txtExam").ToString().Trim() != "")
        {
            objD = new objData();
            objD.xTable = "VISIT_COMP_DTLS";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "VISIT_ID", "COMP_TYPE" };
            objD.KeyVal = new List<string>() { refno, "1" };

            objD.Column = new List<string>() { "VISIT_ID", "PAT_ID", "COMP_ID", "COMP_TYPE" };
            objD.CValue = new ArrayList();

            foreach (string eachcomplaint in formVars.Form("ctl00$contentForm$txtExam").Split(new char[] { ',' }))
            {
                col = new List<string>()
                {
                    refno,
                    formVars.Form("ctl00$contentForm$pID"),
                    eachcomplaint,
                    "1"
                };
                objD.CValue.Add(col);
            }
            objdata.Add(objD);
        }

        //PATIENT_ALLERGIES
        if (formVars.Form("ctl00$contentForm$txtPAllergies").ToString().Trim() != "")
        {
            objD = new objData();
            objD.xTable = "PATIENT_ALLERGIES";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "PAT_ID" };
            objD.KeyVal = new List<string>() { formVars.Form("ctl00$contentForm$pID") };

            objD.Column = new List<string>() { "PAT_ID", "ALLERGY_ID" };
            objD.CValue = new ArrayList();

            foreach (string eachallergy in formVars.Form("ctl00$contentForm$txtPAllergies").Split(new char[] { ',' }))
            {
                col = new List<string>()
                {
                    formVars.Form("ctl00$contentForm$pID"),
                    eachallergy
                };
                objD.CValue.Add(col);
            }
            objdata.Add(objD);
        }

        //VISIT_MEDICINE_DTLS
        var drug = formVars.FormMultiple("drug[]");
        var damt = formVars.FormMultiple("mamt[]");
        var sdisc = formVars.FormMultiple("mdisc[]");
        var sell = formVars.FormMultiple("scost[]");
        var cost = formVars.FormMultiple("ucost[]");
        var mdesc = formVars.FormMultiple("mdesc[]");
        var dqty = formVars.FormMultiple("mqty[]");
        var tran = formVars.FormMultiple("tran[]");

        if (drug.Count() > 0)
        {
            objD = new objData();
            col = new List<string>();

            objD.xTable = "VISIT_MEDICINE_DTLS";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "VISIT_ID" };
            objD.KeyVal = new List<string>() { refno };

            objD.Column = new List<string>() { "VISIT_ID", "TRAN_ID", "PAT_ID", "MED_ID", "MED_UNIT_PRICE", "MED_QTY", "MED_AMT", "MED_DESC", "MED_SELL_PRICE" };
            objD.CValue = new ArrayList();

            for (int i = 0; i < drug.Count(); i++)
            {
                if (drug[i].ToString() != "")
                {
                    col = new List<string>() 
                    { 
                        refno,
                        (tran[i]=="" ? "0" : tran[i]),
                        formVars.Form("ctl00$contentForm$pID"),
                        drug[i],
                        cost[i],
                        dqty[i],
                        damt[i],
                        mdesc[i],
                        //sdisc[i].ToString(),
                        sell[i]
                    };
                    drugsTotalAmount += vG.getNumberD(damt[i]);
                    objD.CValue.Add(col);
                }
            }
            objdata.Add(objD);
        }

        //VISIT_SERVICE_DTLS
        var serviceID = formVars.FormMultiple("service[]");
        var charges = formVars.FormMultiple("charge[]");
        var discount = formVars.FormMultiple("discount[]");

        if (serviceID.Count() > 0)
        {
            objD = new objData();
            col = new List<string>();

            objD.xTable = "VISIT_SERVICE_DTLS";
            objD.Delete = true;

            objD.KeyCol = new List<string>() { "VISIT_ID" };
            objD.KeyVal = new List<string>() { refno };

            objD.Column = new List<string>() { "VISIT_ID", "PAT_ID", "SERVICE_ID", "SERVICE_ORG_AMT", "SERVICE_MOD_AMT", "SERVICE_DISC_AMT" };
            objD.CValue = new ArrayList();

            if (drugsTotalAmount > 0)
            {
                col = new List<string>()
                {
                    refno,
                    formVars.Form("ctl00$contentForm$pID"),
                    "283",
                    drugsTotalAmount.ToString(),
                    drugsTotalAmount.ToString(),
                    "0"
                };
                objD.CValue.Add(col);
                serviceTotalAmount += drugsTotalAmount;
            }
            for (int i = 0; i < serviceID.Count(); i++)
            {
                if (serviceID[i].ToString() != "")
                {
                    if (serviceID[i] != "283")
                    {
                        col = new List<string>()
                        {
                            refno,
                            formVars.Form("ctl00$contentForm$pID"),
                            serviceID[i],
                            charges[i],
                            (vG.getNumberD(charges[i]) - vG.getNumberD(discount[i])).ToString(),
                            discount[i]
                        };
                        serviceTotalAmount += (vG.getNumberD(charges[i]) - vG.getNumberD(discount[i]));
                        objD.CValue.Add(col);
                    }
                }
            }
            objdata.Add(objD);
        }

        //PATIENT_REGISTRATION
        objD = new objData();

        objD.xTable = "PATIENT_REGISTRATION";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "PAT_ID" };
        objD.KeyVal = new List<string>() { formVars.Form("ctl00$contentForm$pID") };

        objD.Column = new List<string>() { "PAT_PREV_HISTORY" };
        objD.CValue = new ArrayList();
        col = new List<string>() { formVars.Form("ctl00$contentForm$txtPrevHistory") };
        objD.CValue.Add(col);

        objdata.Add(objD);

        //PAT_QUEUE
        objD = new objData();

        objD.xTable = "PAT_QUEUE";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "PAT_QID" };
        objD.KeyVal = new List<string>() { formVars.Form("ctl00$contentForm$qID") };

        objD.Column = new List<string>() { "VISIT_ID", "PAT_QUEUE_STATUS" };
        objD.CValue = new ArrayList();

        if (HttpContext.Current.Session["usertype"].ToString() == "2")
            col = new List<string>() { refno, "Pharmacy" };
        else if (HttpContext.Current.Session["usertype"].ToString() == "4")
            col = new List<string>() { refno, "Cash" };
        else if (HttpContext.Current.Session["usertype"].ToString() == "3")
            col = new List<string>() { refno, "Over" };
        else
            col = new List<string>() { refno, "Over" };
        objD.CValue.Add(col);

        objdata.Add(objD);

        try
        {
            msg = dbaction.saveCollection(objdata, gID);
        }
        catch (Exception ex)
        {
            msg = "ERROR " + ex.Message;
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string saveDiagnosis(string nm)
    {
        // string str = "";
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO COMP_MST(COMP_NAME, COMP_TYPE) VALUES('" + nm + "','0')", HttpContext.Current.Session["userid"].ToString());
        /*
        str += "$('#txtDiagnosis').selectize({ maxitems: null, valueField: 'ID', labelField: 'Name', searchField: 'Name', options: " + getAllDiagnosis() + ", create: false });";

        Page page = HttpContext.Current.CurrentHandler as Page;
        page.ClientScript.RegisterStartupScript(typeof(Page), "DIV", "<script type='text/javascript'>" + str + "</script>");
        */
        return msg;
    }
    [System.Web.Services.WebMethod]
    private static string getAllDiagnosis()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        var eachDiagnosis = new List<Diagnosis>();
        objdl = dA.returnList("SELECT COMP_ID, COMP_NAME FROM COMP_MST WHERE COMP_TYPE=0");
        if (objdl.flaG == true)
        {
            for (int i = 0; i < objdl.dataSet.Tables[0].Rows.Count; i++)
            {
                eachDiagnosis.Add(new Diagnosis() { ID = objdl.dataSet.Tables[0].Rows[i][0].ToString(), Name = objdl.dataSet.Tables[0].Rows[i][1].ToString() });
            }
        }

        var serializer = new JavaScriptSerializer();
        var serializedResult = serializer.Serialize(eachDiagnosis);

        return serializedResult;
    }
}