using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Manage_Doctors : System.Web.UI.Page
{
    protected void newDoctor(object sender, EventArgs e)
    {
        Response.Redirect("~/Patient/Doctor.aspx");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("DOC_ID", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("DOC_ID", "ASC");
            fillSpecialization();
        }
        else
        {
        }
    }
    private void fillGrid(string sortCol, string sortDir)
    {
        dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
        objDL objdl = new objDL();

        string searchKeyword = "";

        if (txtKeyword.Text != "")
        {
            searchKeyword = " WHERE " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT DOC_ID, DOC_NAME, DOC_HANDPHONE, DOC_EMAIL, DOC_TYPE, DOC_QUALIFICATION, COMP_NAME FROM DOCTOR_MST JOIN COMP_MST ON DOC_SPECIALIZATION = COMP_ID " + searchKeyword);
        DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
        Lst.DataSource = dv;
        Lst.DataBind();
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
        fillGrid("DOC_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void fillSpecialization()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT COMP_ID, COMP_NAME FROM COMP_MST WHERE COMP_TYPE = 3");

        lstSpec.DataSource = objdl.dataSet;
        lstSpec.DataTextField = "COMP_NAME";
        lstSpec.DataValueField = "COMP_ID";
        lstSpec.DataBind();
    }
    [System.Web.Services.WebMethod]
    public static string[] getInfo(string docID)
    {
        string[] data = new string[7];

        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT DOC_NAME, DOC_SEX, DOC_QUALIFICATION, DOC_EMAIL, DOC_SPECIALIZATION, DOC_TYPE, DOC_HANDPHONE FROM DOCTOR_MST WHERE DOC_ID = '" + docID + "'");
        if (objdl.flaG==true)
        {
            data[0] = objdl.dataSet.Tables[0].Rows[0][0].ToString();
            data[1] = objdl.dataSet.Tables[0].Rows[0][1].ToString();
            data[2] = objdl.dataSet.Tables[0].Rows[0][2].ToString();
            data[3] = objdl.dataSet.Tables[0].Rows[0][3].ToString();
            data[4] = objdl.dataSet.Tables[0].Rows[0][4].ToString();
            data[5] = objdl.dataSet.Tables[0].Rows[0][5].ToString();
            data[6] = objdl.dataSet.Tables[0].Rows[0][6].ToString();
        }

        return data;
    }
    [System.Web.Services.WebMethod]
    public static string saveDoctor(string docID, string docName, string docSex, string docQualification, string docEmail, string docSpecialization, string docType, string docHP)
    {
        string msg = "";
        if (docID == "0")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO DOCTOR_MST(DOC_NAME, DOC_SEX, DOC_QUALIFICATION, DOC_EMAIL, DOC_SPECIALIZATION, DOC_TYPE, DOC_HANDPHONE) VALUES('" + docName + "','" + docSex + "','" + docQualification + "','" + docEmail + "','" + docSpecialization + "','" + docType + "','" + docHP + "')", HttpContext.Current.Session["userid"].ToString());
        }
        else
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE DOCTOR_MST SET DOC_NAME = '" + docName + "', DOC_SEX = '" + docSex + "', DOC_QUALIFICATION = '" + docQualification + "', DOC_EMAIL = '" + docEmail + "' , DOC_SPECIALIZATION = '" + docSpecialization + "', DOC_TYPE = '" + docType + "', DOC_HANDPHONE = '" + docHP + "' WHERE DOC_ID = '" + docID + "'", HttpContext.Current.Session["userid"].ToString());
        }
        return msg;
    }
}