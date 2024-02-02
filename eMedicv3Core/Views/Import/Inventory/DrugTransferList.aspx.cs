using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugTransferList : System.Web.UI.Page
{
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sortExpression"] = "TRAN_REF_ID";
            Session["sortDirection"] = "DESC";
            fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
        }
        else
        {
            fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
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

        objdl = dA.returnList("SELECT TRAN_REF_ID, TRAN_REF_NO, TRAN_DATE, TRAN_POST_FLAG, getStatus('STOCK_TRANSFER_INFO', TRAN_REF_ID) AS FLAG, TRAN_OUT_OUTLET, TRAN_IN_OUTLET, (SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID = TRAN_IN_OUTLET) AS IN_NAME, (SELECT OUTLET_NAME FROM OUTLET_MST WHERE OUTLET_ID = TRAN_OUT_OUTLET) AS OUT_NAME FROM STOCK_TRANSFER_INFO  " + searchKeyword + " ORDER BY TRAN_REF_ID DESC");
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

        Session["sortExpression"] = e.SortExpression;
        Session["sortDirection"] = sortDirection;
    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lst.PageIndex = e.NewPageIndex;
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected string getStatus(string flag)
    {
        string str = "";
        if (Convert.ToInt32(flag) == 0)
        {
            str = "<span class='label label-success'>O</span>";
        }
        else if (Convert.ToInt32(flag) == 1)
        {
            str = "<span class='label label-important'>C</span>";
        }
        else if (Convert.ToInt32(flag) == 2)
        {
            str = "<span class='label label-warning'>P</span>";
        }
        else if (Convert.ToInt32(flag) == 3)
        {
            str = "<span class='label label-inverse'>N</span>";
        }
        else if (Convert.ToInt32(flag) == 10)
        {
            str = "<span class='label label-inverse'>L</span>";
        }

        return str;
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Lst.DataKeys[e.Row.RowIndex].Values[2].ToString() == "1")
            {
                e.Row.Cells[7].Controls[1].Visible = false;
                e.Row.Cells[8].Controls[1].Visible = false;
                e.Row.Cells[9].Controls[1].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT TRANS_ID, MED_NAME, CONCAT(TRAN_QTY, ' ', MED_SMALL_UOM)  AS RQTY FROM STOCK_TRANSFER_DTLS JOIN MEDICINE_MST ON MED_ID=TRAN_MED_ID WHERE STK_REF_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstPODrugs") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]) { Sort = "MED_NAME  ASC" };
                gv.DataBind();
            }
        }
    }
    [System.Web.Services.WebMethod]
    public static string delRec(string id)
    {
        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM STOCK_TRANSFER_DTLS WHERE STK_REF_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg == "")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM STOCK_TRANSFER_INFO WHERE TRAN_REF_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string posRec(string id)
    {
        List<dbParam> objparams = new List<dbParam>();

        objparams.Add(new dbParam { col = "ID", image = null, dType = "I", val = id });
        objparams.Add(new dbParam { col = "UserID", image = null, dType = "S", val = HttpContext.Current.Session["userid"].ToString() });

        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).runStoredProcedure("proc_postTransfer", objparams, null);
        return msg;
    }
}