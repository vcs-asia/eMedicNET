using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;
using System.Data;
public partial class Patient_ReceiptList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void processList(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT RECEIPT_NO, VISIT_ID, RECEIPT_MST.PAT_ID, PAT_NAME, REC_DATE, TOT_AMT, PAID_AMT, IF (PAYMENT_MODE=0,'CASH', 'CARD') AS PMODE FROM RECEIPT_MST JOIN PATIENT_REGISTRATION ON PATIENT_REGISTRATION.PAT_ID = RECEIPT_MST.PAT_ID WHERE REC_DATE BETWEEN '" + new vGeneral().convertDateForDB(txtDate.Text) + " 00:00:00' AND '" + new vGeneral().convertDateForDB(txtDate.Text) + " 23:59:59' ORDER BY RECEIPT_NO");
        if (objdl.flaG == true)
        {
            LstVisit.DataSource = new DataView(objdl.dataSet.Tables[0]);
            LstVisit.DataBind();
        }
    }
    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        LstVisit.EditIndex = e.NewEditIndex;
        fillGrid();
    }
    [System.Web.Services.WebMethod]
    public static string[] getReceiptDetails(string recID, string vID)
    {
        string[] response = new string[6];

        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        objdl = dA.returnList("SELECT RECEIPT_NO, VISIT_ID, PAT_ID, REC_DATE, TOT_AMT, PAID_AMT, PAYMENT_MODE, REMARKS,  (SELECT PAT_NAME FROM PATIENT_REGISTRATION WHERE PAT_ID = RECEIPT_MST.PAT_ID) AS PAT_NAME FROM RECEIPT_MST WHERE RECEIPT_NO = '" + recID + "'");
        if (objdl.flaG==true && objdl.dataSet.Tables[0].Rows.Count > 0)
        {
            response[0] = ((DateTime) objdl.dataSet.Tables[0].Rows[0][3]).ToString("dd/MM/yyyy");
            response[1] = ((decimal)objdl.dataSet.Tables[0].Rows[0][4]).ToString("0.00");
            response[2] = ((decimal)objdl.dataSet.Tables[0].Rows[0][5]).ToString("0.00");
            response[3] = objdl.dataSet.Tables[0].Rows[0][6].ToString();
            response[4] = objdl.dataSet.Tables[0].Rows[0][7].ToString();
            response[5] = objdl.dataSet.Tables[0].Rows[0][8].ToString();
        }
        else
        {
            dbAction dH = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objDH = new objDL();

            string vAmt = "0";

            objDH = dH.returnList("SELECT VISIT_TOT_AMT FROM PATIENT_VISIT_MST WHERE VISIT_ID = '" + vID + "'");
            if (objDH.flaG == true && objDH.dataSet.Tables[0].Rows.Count > 0)
            {
                vAmt = objDH.dataSet.Tables[0].Rows[0][0].ToString();
            }

            response[0] = DateTime.Now.ToString("dd/MM/yyyy");
            response[1] = vAmt;
            response[2] = "0.00";
            response[3] = "0";
            response[4] = "";
        }

        return response;
    }
    protected void saveReceipt(object sender, EventArgs e)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        if (hdnRecNo.Value == "0")
        {
            string msg = dA.run("INSERT INTO RECEIPT_MST(VISIT_ID, PAT_ID, REC_DATE, TOT_AMT, PAID_AMT, PAYMENT_MODE, REMARKS, USER_ID) VALUES('" + Request.QueryString["_vD"].ToString() + "','" + Request.QueryString["_pD"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "','" + txtAmt.Text + "','" + txtPaid.Text + "','" + payMode.SelectedValue + "','" + txtRemarks.Text + "','" + Session["user_log_id"] + "')", HttpContext.Current.Session["userid"].ToString());
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg;
                pnlError.Visible = true;
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }
        else
        {
            string msg = dA.run("UPDATE RECEIPT_MST SET TOT_AMT='" + txtAmt.Text + "',PAID_AMT='" + txtPaid.Text + "',PAYMENT_MODE='" + payMode.SelectedValue + "',REMARKS='" + txtRemarks.Text + "' WHERE RECEIPT_NO = '" + hdnRecNo.Value + "'", HttpContext.Current.Session["userid"].ToString());
            if (msg.StartsWith("ERROR"))
            {
                lblError.Text = msg;
                pnlError.Visible = true;
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }
    [System.Web.Services.WebMethod]
    public static string deleteReceipt(string rNo)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM RECEIPT_MST WHERE RECEIPT_NO='" + rNo + "'", HttpContext.Current.Session["userid"].ToString());
        return msg;
    }

}