﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugOAdjList : System.Web.UI.Page
{
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid(Session["sortExpression"].ToString(), Session["sortDirection"].ToString());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["sortExpression"] = "ADJ_REF_ID";
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

        objdl = dA.returnList("SELECT ADJ_REF_ID, ADJ_REF_NO, ADJ_DATE, ADJ_POST_FLAG, getStatus('OUTLET_STOCK_ADJUSTMENT_INFO', ADJ_REF_ID) AS FLAG, ADJ_OUTLET, OUTLET_NAME FROM OUTLET_STOCK_ADJUSTMENT_INFO JOIN OUTLET_MST ON ADJ_OUTLET = OUTLET_ID " + searchKeyword + " ORDER BY ADJ_REF_ID DESC");
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
                e.Row.Cells[5].Controls[1].Visible = false;
                e.Row.Cells[6].Controls[1].Visible = false;
                e.Row.Cells[7].Controls[1].Visible = false;
                e.Row.Cells[8].Controls[1].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT TRANS_ID, MED_NAME, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_QTY) AS RQTY, getMedQuantity(ADJ_MED_ID, ADJ_PACK, ADJ_PHY_QTY) AS PQTY FROM OUTLET_STOCK_ADJUSTMENT_DTLS JOIN MEDICINE_MST ON MED_ID=ADJ_MED_ID WHERE STK_REF_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
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

        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM OUTLET_STOCK_ADJUSTMENT_DTLS WHERE STK_REF_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        if (msg == "")
        {
            msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM OUTLET_STOCK_ADJUSTMENT_INFO WHERE ADJ_REF_ID = '" + id + "'", HttpContext.Current.Session["userid"].ToString());
        }
        return msg;
    }
    [System.Web.Services.WebMethod]
    public static string posRec(string id)
    {
        List<dbParam> objparams = new List<dbParam>();

        objparams.Add(new dbParam { col = "AdjID", image = null, dType = "I", val = id });
        objparams.Add(new dbParam { col = "UserID", image = null, dType = "S", val = HttpContext.Current.Session["userid"].ToString() });

        string msg = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).runStoredProcedure("proc_postOAdjust", objparams, null);
        return msg;
    }
}