using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;
using System.Collections;


public partial class Patient_Queue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            getDisciplines();
            setDoctorValues();
            fillGrid("PAT_QUEUE_DATE", "ASC");
        }
        else
        {
        }
    }
    protected void deleteQueue(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        msg = dA.run("DELETE FROM PAT_QUEUE WHERE PAT_QID = '" + hdnQID.Value + "'", HttpContext.Current.Session["userid"].ToString());
    }
    [System.Web.Services.WebMethod]
    public static string printDLabel(string id, string drugid)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());

        string[] drugID = drugid.Split(',');
        string retMsg = "";

        for (int inc = 0; inc < drugID.Length; inc++)
        {
            if (drugID[inc]!="0")
            {
                string msg = dA.run("INSERT INTO DRUGL(VISIT_ID, MED_ID) VALUES('" + id + "','" + drugID[inc] + "')", HttpContext.Current.Session["userid"].ToString());
                if (msg.StartsWith("ERROR"))
                {
                    retMsg = "ERROR";
                }
            }
        }
        return retMsg;
    }
    [System.Web.Services.WebMethod]
    public static string printNLabel(string patID, string nCopies)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        msg = dA.run("INSERT INTO NORML(PAT_ID, CPS) VALUES('" + patID + "', '" + nCopies + "')", HttpContext.Current.Session["userid"].ToString());

        return msg;
    }
    private void setDoctorValues()
    {
        if (Session["usertype"].ToString()=="2")
        {
            drpDiscipline.SelectedValue = Session["disc"].ToString();
            chkDiscipline.Checked = true;
        }
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

            lstDiscipline.DataSource = objdl.dataSet.Tables[0];
            lstDiscipline.DataTextField = "COMP_NAME";
            lstDiscipline.DataValueField = "COMP_ID";
            lstDiscipline.DataBind();
        }
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string strDisc = "";
        string strStatus = "";

        if (chkDiscipline.Checked == true)
        {
            strDisc = " AND PAT_DISC = '" + drpDiscipline.SelectedValue + "'";
        }
        if (chkStatus.Checked == true)
        {
            strStatus = " AND PAT_QUEUE_STATUS = '" + queueStatus.SelectedValue + "'";
        }

        objdl = dA.returnList("SELECT PAT_QUEUE_DATE, PAT_QUEUE_STATUS, PAT_QUEUE_NEW_OLD, PAT_REG_NO, PAT_QUEUE.PAT_ID AS PAT_ID, PAT_NAME, PAT_IC_NO, PAT_REG_DATE, PAT_BIRTH_DATE, PAT_REG_NO, COMP_NAME, PAT_QID, VISIT_ID, getAge(PAT_QUEUE.PAT_ID) AS PAT_AGE, (SELECT USER_NAME FROM USER_MST WHERE USER_ID = (SELECT USER_ID FROM RECORD_LOCK_INFO WHERE ID = PAT_QID)) AS CUSER, PAT_DISC, PAT_QID FROM PAT_QUEUE JOIN COMP_MST ON PAT_DISC=COMP_ID JOIN PATIENT_REGISTRATION ON PAT_QUEUE.PAT_ID = PATIENT_REGISTRATION.PAT_ID WHERE DATE_FORMAT(PAT_QUEUE_DATE, '%Y-%m-%d') = '" + new vGeneral().convertDateForDB(txtFromDate.Text) + "'" + strDisc + strStatus + " ORDER BY PAT_QUEUE_DATE");
        if (objdl.flaG == true)
        {
            //DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
            DataView dv = new DataView(objdl.dataSet.Tables[0]);
            Lst.DataSource = dv;
            Lst.DataBind();
        }
        else
        {
            Lst.DataSource = null;
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
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Lst.DataKeys[e.Row.RowIndex].Values[1].ToString() != "Waiting")
            {
                e.Row.Cells[10].Controls[7].Visible = false;
            }
            if (Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "0" || Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "")
            {
                e.Row.Cells[10].Controls[5].Visible = false;
                e.Row.Cells[10].Controls[9].Visible = false;
                e.Row.Cells[10].Controls[13].Visible = false;
                e.Row.Cells[10].Controls[15].Visible = false;
            }
            if (Session["usertype"].ToString() == "0" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "6")
            {
                e.Row.Cells[4].Controls[1].Visible = true;
            }
            else
            {
                e.Row.Cells[4].Controls[1].Visible = false;
            }
            /*
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT PAT_ID, VISIT_ID, VISIT_DATE, VISIT_TIME, VISIT_TOT_AMT, DOC_NAME, COMP_NAME AS DISC FROM PATIENT_VISIT_MST JOIN DOCTOR_MST ON PATIENT_VISIT_MST.DOC_ID=DOCTOR_MST.DOC_ID JOIN COMP_MST ON COMP_MST.COMP_ID=DOCTOR_MST.DOC_SPECIALIZATION WHERE PAT_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' ORDER BY VISIT_DATE DESC");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstVisit") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }
             */ 
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
    [System.Web.Services.WebMethod]
    public static string lockQueue(string qID)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        string msg = "";
        string st = "";
        if (dA .checkRecordLockStatus(int.Parse(qID), "PAT_QUEUE", int.Parse(HttpContext.Current.Session["uid"].ToString())) == false)
        {
            st = "N";
            msg = dA.lockRecord(int.Parse(qID), "PAT_QUEUE", int.Parse(HttpContext.Current.Session["uid"].ToString()), HttpContext.Current.Session["userid"].ToString());
        }
        else
        {
            st = "Y";
        }
        return st;
    }
    [System.Web.Services.WebMethod]
    public static string[] getReceiptDetails(string vID)
    {
        string[] response = new string[5];

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT RECEIPT_NO, VISIT_ID, PAT_ID, REC_DATE, TOT_AMT, PAID_AMT, (SELECT PAT_NAME FROM PATIENT_REGISTRATION WHERE PAT_ID = RECEIPT_MST.PAT_ID) FROM RECEIPT_MST WHERE VISIT_ID = '" + vID + "'");
        if (objdl.flaG == true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            response[0] = ((DateTime)objdl.dataSet.Tables[0].Rows[0][3]).ToString("dd/MM/yyyy");
            response[1] = ((decimal)objdl.dataSet.Tables[0].Rows[0][4]).ToString("0.00");
            response[2] = ((decimal)objdl.dataSet.Tables[0].Rows[0][5]).ToString("0.00");
            response[3] = objdl.dataSet.Tables[0].Rows[0][6].ToString();
            response[4] = objdl.dataSet.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            dbAction dH = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objDH = new objDL();

            string vAmt = "0";
            string nm = "";
            string dt = "";

            objDH = dH.returnList("SELECT VISIT_TOT_AMT, (SELECT PAT_NAME FROM PATIENT_REGISTRATION WHERE PAT_ID = PATIENT_VISIT_MST.PAT_ID), VISIT_DATE FROM PATIENT_VISIT_MST WHERE VISIT_ID = '" + vID + "'");
            if (objDH.flaG == true && objDH.dataSet.Tables[0].Rows.Count > 0)
            {
                vAmt = objDH.dataSet.Tables[0].Rows[0][0].ToString();
                nm = objDH.dataSet.Tables[0].Rows[0][1].ToString();
                dt = ((DateTime)objDH.dataSet.Tables[0].Rows[0][2]).ToString("dd/MM/yyyy");
            }

            response[0] = dt;
            //response[0] = DateTime.Now.ToString("dd/MM/yyyy");
            response[1] = vAmt;
            response[2] = vAmt;
            response[3] = nm;
            response[4] = "NEW RECEIPT";
        }

        return response;
    }
    [System.Web.Services.WebMethod]
    public static string saveReceipt(NameValue[] formVars)
    {
        string msg = "";
        dbAction dbaction = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        List<objData> objdata = new List<objData>();
        objData objD = new objData();
        string refno = (formVars.Form("ctl00$contentForm$txtRecNo")=="NEW RECEIPT" ? "0" : formVars.Form("ctl00$contentForm$txtRecNo"));
        List<string> gID = new List<string>() { refno, "RECEIPT_MST", "RECEIPT_NO", HttpContext.Current.Session["userid"].ToString() };
        bool saveFlag = false;

        objD.xTable = "RECEIPT_MST";
        objD.Delete = false;

        objD.KeyCol = new List<string>() { "RECEIPT_NO" };
        objD.KeyVal = new List<string>() { refno };

        objD.Column = new List<string>() { "RECEIPT_NO", "PAT_ID", "VISIT_ID", "REC_DATE", "TOT_AMT", "PAID_AMT", "PAYMENT_MODE", "REMARKS", "USER_ID" };
        objD.CValue = new ArrayList();
        List<string> col = new List<string>() 
        { 
            refno, 
            formVars.Form("ctl00$contentForm$hdnPatID"), 
            formVars.Form("ctl00$contentForm$hdnVisitID"), 
            new vGeneral().convertDateForDB(formVars.Form("ctl00$contentForm$txtRecDate")), 
            new vGeneral().getNumberD(formVars.Form("ctl00$contentForm$txtAmt")).ToString(), 
            new vGeneral().getNumberD(formVars.Form("ctl00$contentForm$txtPaid")).ToString(), 
            "0",
            formVars.Form("ctl00$contentForm$txtRemarks"), 
            HttpContext.Current.Session["userid"].ToString() 
        };
        objD.CValue.Add(col);
        objdata.Add(objD);

        objD = new objData();

        objD.xTable = "RECEIPT_DTLS";
        objD.Delete = true;
        objD.Column = new List<string>() { "RECEIPT_NO", "PMODE", "PAMT", "PDTLS" };
        objD.KeyCol = new List<string>() { "RECEIPT_NO" };
        objD.KeyVal = new List<string>() { refno };

        var pmod = formVars.FormMultiple("pmode[]");
        var pamt = formVars.FormMultiple("pdamt[]");
        var prmk = formVars.FormMultiple("rmrks[]");

        objD.CValue = new ArrayList();

        decimal paidAmount = 0;

        for (int inc = 0; inc < pmod.Count(); inc++)
        {
            if (decimal.Parse(pamt[inc]) > 0)
            {
                col = new List<string>() { refno, pmod[inc], pamt[inc], prmk[inc] };
                objD.CValue.Add(col);
                saveFlag = true;
                paidAmount += new vGeneral().getNumberD(pamt[inc]);
            }
        }
        objdata.Add(objD);

        if (paidAmount > new vGeneral().getNumberD(formVars.Form("ctl00$contentForm$txtAmt")))
        {
            msg = "ERROR: Paid Amount cannot be more than total charges.";
            saveFlag = false;
        }

        if (saveFlag==true)
        {
            objD = new objData();
            objD.xTable = "PATIENT_VISIT_MST";

            objD.KeyCol = new List<string>() { "VISIT_ID" };
            objD.KeyVal = new List<string>() { formVars.Form("ctl00$contentForm$hdnVisitID") };

            objD.Column = new List<string>() { "VISIT_PENDING_AMT" };
            objD.CValue = new ArrayList();

            col = new List<string>() { new vGeneral().getNumberD(formVars.Form("ctl00$contentForm$txtPaid")).ToString() };

            objD.CValue.Add(col);

            objdata.Add(objD);
        }

        if (saveFlag==true)
        {
            msg = dbaction.saveCollection(objdata, gID);
        }
        else
        {
            msg = "ERROR: Receipt details are not found. Please check.";
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string saveDiscipline(string qid, string vid, string did)
    {
        string msg = "";

        msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PAT_QUEUE SET PAT_DISC = '" + did + "' WHERE PAT_QID = '" + qid + "'", HttpContext.Current.Session["userid"].ToString());
        msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE PATIENT_VISIT_MST SET VISIT_DISC = '" + did + "' WHERE VISIT_ID = '" + vid + "'", HttpContext.Current.Session["userid"].ToString());

        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string[] getPaymentModes(string recNo)
    {
        string[] data = {};

        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PARAM_ID, PARAM_NAME FROM PARAMETERS_INFO WHERE PARAM_TYPE = 10");
        if (objdl.flaG==true)
        {
            data = new string[objdl.dataSet.Tables[0].Rows.Count];
            for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
            {
                data[inc] = objdl.dataSet.Tables[0].Rows[inc][0].ToString() + "|" + objdl.dataSet.Tables[0].Rows[inc][1].ToString() + "|" + getPaymentDetails(recNo, objdl.dataSet.Tables[0].Rows[inc][0].ToString());
            }
        }
        else
        {
            data = new string[1];
            data[0] = objdl.Msg;
        }
        return data;
    }
    [System.Web.Services.WebMethod]
    private static string getPaymentDetails(string recNo, string pmode)
    {
        string result = "";

        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT PAMT, PDTLS FROM RECEIPT_DTLS WHERE RECEIPT_NO = '" + recNo + "' AND PMODE = '" + pmode + "'");
        if (objdl.flaG==true)
        {
            result = objdl.dataSet.Tables[0].Rows[0][0].ToString() + "|" + objdl.dataSet.Tables[0].Rows[0][1].ToString();
        }
        else
        {
            result = "0.00| ";
        }
        return result;
    }
    [System.Web.Services.WebMethod]
    public static string getLatestAmount(string vD)
    {
        string result = "";

        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT VISIT_TOT_AMT FROM PATIENT_VISIT_MST WHERE VISIT_ID = '" + vD + "'");
        if (objdl.flaG==true)
        {
            result = objdl.dataSet.Tables[0].Rows[0][0].ToString();
        }

        return result;
    }
    [System.Web.Services.WebMethod]
    public static string getDrugsOfAPatient(string visitID)
    {
        string retValue = "";
        
        try
        {
            objDL objdl = new objDL();
            objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT TRAN_ID, MED_NAME, MED_DESC FROM VISIT_MEDICINE_DTLS JOIN MEDICINE_MST ON VISIT_MEDICINE_DTLS.MED_ID = MEDICINE_MST.MED_ID WHERE VISIT_ID = '" + visitID + "'");
            if (objdl.flaG == true)
            {
                for (int inc = 0; inc < objdl.dataSet.Tables[0].Rows.Count; inc++)
                {
                    retValue += "$('#myDrugTable > tbody:last').append(<tr><td><input type=\"checkbox\" id=\"chkid[]\" name=\"chkid[]\" value=\"" + objdl.dataSet.Tables[0].Rows[inc][0].ToString() + "\" checked /></td><td>" + objdl.dataSet.Tables[0].Rows[inc][1].ToString() + "</td><td>" + objdl.dataSet.Tables[0].Rows[inc][2].ToString() + "</td></tr>)";
                }
            }
        }
        catch(Exception ex)
        {
            retValue = ex.Message.ToString();
        }
        return retValue;
    }
}