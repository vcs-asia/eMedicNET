using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Account_Users : System.Web.UI.Page
{
    protected void newOLet(object sender, EventArgs e)
    {
        Response.Redirect("~/Manage/User.aspx");
    }
    protected void getDoctors()
    {
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT DOC_ID, DOC_NAME FROM DOCTOR_MST ORDER BY DOC_NAME");

        lstDoctor.DataSource = objdl.dataSet;
        lstDoctor.DataTextField = "DOC_NAME";
        lstDoctor.DataValueField = "DOC_ID";
        lstDoctor.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("USER_ID", "ASC");
            getDoctors();
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
            searchKeyword = " AND " + lstFields.SelectedValue + " LIKE '" + txtKeyword.Text + "%'";
        }

        objdl = dA.returnList("SELECT USER_ID, USER_NAME, USER_LOGIN_ID, getUserType(USER_TYPE) AS USER_TYPE, IF (USER_SEX = 0, 'MALE', 'FEMALE') AS USER_SEX, USER_EMAIL FROM USER_MST WHERE USER_TYPE !=0 " + searchKeyword);
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
        fillGrid("USER_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    [System.Web.Services.WebMethod]
    public static string[] getDetails(string id)
    {
        objDL objdl = new objDL();
        string[] data = new string[7];
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT USER_NAME, USER_LOGIN_ID, USER_SEX, USER_TYPE, USER_EMAIL, DOC_ID, USER_STATUS FROM USER_MST WHERE USER_ID = '" + id + "'");
        if (objdl.flaG == true)
        {
            DataRow Col = objdl.dataSet.Tables[0].Rows[0];
            data[0] = Col[0].ToString();
            data[1] = Col[1].ToString();
            data[2] = Col[2].ToString();
            data[3] = Col[3].ToString();
            data[4] = Col[4].ToString();
            data[5] = Col[5].ToString();
            data[6] = Col[6].ToString();
        }

        return data;
    }
    [System.Web.Services.WebMethod]
    public static string saveDetails(string nm, string ld, string sx, string tp, string em, string dc, string st, string id)
    {
        string msg = "";
        if (id=="0")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("INSERT INTO USER_MST(USER_NAME, USER_LOGIN_ID, USER_SEX, USER_TYPE, USER_EMAIL, DOC_ID, USER_STATUS) VALUES('" + nm + "', '" + ld + "', '" + sx + "', '" + tp + "', '" + em + "', '" + dc + "', '" + st + "')", HttpContext.Current.Session["userid"].ToString());
        }
        else 
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE USER_MST SET USER_NAME='" + nm + "', USER_LOGIN_ID='" + ld + "', USER_SEX ='" + sx + "', USER_TYPE = '" + tp + "', USER_EMAIL = '" + em + "', DOC_ID ='" + dc + "', USER_STATUS = '" + st + "' WHERE USER_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }

        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string resetPassword(string userid)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE USER_MST SET USER_PWD = MD5('12345678') WHERE USER_ID = '" + userid + "'", HttpContext.Current.Session["userid"].ToString());
        return msg;
    }

    protected void OnSearch(object sender, EventArgs e)
    {
        fillGrid("USER_ID", "ASC");
    }
}