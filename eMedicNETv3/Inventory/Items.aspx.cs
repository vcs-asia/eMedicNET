using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_Items : System.Web.UI.Page
{
    public string getFlagIcon(string flag)
    {
        string str = "";
        if (flag=="0")
        {
            str = "<i class='icon-ok-sign'></i>";
        }
        else
        {
            str = "<i class='icon-ban-circle'></i>";
        }
        return str;
    }
    protected void newDrug(object sender, EventArgs e)
    {
        Response.Redirect("~/Inventory/Drug.aspx?id=0");
    }
    protected void searchKeyword(object sender, EventArgs e)
    {
        fillGrid("MED_ID", "ASC");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid("MED_ID", "ASC");
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

        //string sQuery = "SELECT MED_ID, MED_NAME, MED_GENERIC_NAME, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM, MED_CURRENT_STOCK, MED_PACKING) AS BAL, MED_STOCK_CODE, MED_FLAG FROM MEDICINE_MST WHERE MED_TYPE != 223";
        string sQuery = "SELECT MED_ID, MED_NAME, MED_GENERIC_NAME, MED_STOCK_CODE, MED_FLAG, MED_COST_PRICE, MED_SELLING_PRICE, MED_OUT_SELLING_COST, MED_MARK_UP, MED_OUT_MARK_UP, MED_UNIT_COST FROM MEDICINE_MST WHERE MED_TYPE = 223";

        objdl = dA.returnList( sQuery + searchKeyword);
        DataView dv = new DataView(objdl.dataSet.Tables[0]) { Sort = sortCol + " " + sortDir };
        Lst.DataSource = dv;
        Lst.DataBind();
    }

    protected void deleteMe(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible=false;
        string message = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("DELETE FROM MEDICINE_MST WHERE MED_ID='" + int.Parse(hdnID.Value) + "'", HttpContext.Current.Session["userid"].ToString());
        if (message.StartsWith("ERROR"))
        {
            lblError.Text = message;
            pnlError.Visible = true;
        }
    }
    protected void cancelDelete(object sender, EventArgs e)
    {
        pnlDeleteAlert.Visible = false;
    }
    protected void OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="bEdit")
        {
            Response.Redirect("~/Inventory/Drug.aspx?id=" + e.CommandArgument.ToString());
        }
        else if (e.CommandName =="delete")
        {
            hdnID.Value = e.CommandArgument.ToString();
            pnlDeleteAlert.Visible = true;
        }
        else if (e.CommandName=="bActivate")
        {
            string message = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).run("UPDATE MEDICINE_MST SET MED_FLAG=0 WHERE MED_ID='" + int.Parse(e.CommandArgument.ToString()) + "'", HttpContext.Current.Session["userid"].ToString());
            if (message.StartsWith("ERROR"))
            {
                lblError.Text = message;
                pnlError.Visible = true;
            }
            else
            {
                int pageIndex = Lst.PageIndex;
                fillGrid("MED_NAME", "ASC");
                Lst.PageIndex = pageIndex;
            }
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
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Lst.PageIndex = e.NewPageIndex;
        fillGrid("MED_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Lst.DataKeys[e.Row.RowIndex].Values[1].ToString()=="0")
            {
                e.Row.Cells[14].Controls[1].Visible = false;
            }

            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT BATCH_NO, getMedQuantity(MED_ID ,MED_PACK,CUR_BAL_STK) AS BAL, EXP_DATE, BATCH_ID, getMedPacking(MED_ID, MED_PACK) AS PACKING FROM STOCK_DTLD_INFO WHERE MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
            //objdl = dA.returnList("SELECT BATCH_NO, getMedQuantity(MED_ID ,MED_PACK,CUR_BAL_STK) AS BAL, EXP_DATE, BATCH_ID, getMedPacking(MED_ID, MED_PACK) AS PACKING FROM STOCK_DTLD_INFO WHERE MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' AND CUR_BAL_STK !=0");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstBatch") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }

            objdl = dA.returnList("SELECT OUTLET_STOCK_INFO.OUTLET_ID, OUTLET_MED_ID, OUTLET_NAME, getMedQuantity(OUTLET_MED_ID, MED_PACK, OUTLET_QTY) AS BAL, OUTLET_MST.OUTLET_ID AS OUTLETID FROM OUTLET_STOCK_INFO JOIN OUTLET_MST ON OUTLET_STOCK_INFO.OUTLET_ID = OUTLET_MST.OUTLET_ID WHERE OUTLET_MED_ID='" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
            //objdl = dA.returnList("SELECT OUTLET_STOCK_INFO.OUTLET_ID, OUTLET_MED_ID, OUTLET_NAME, getMedQuantity(OUTLET_MED_ID, MED_PACK, OUTLET_QTY) AS BAL, OUTLET_MST.OUTLET_ID AS OUTLETID FROM OUTLET_STOCK_INFO JOIN OUTLET_MST ON OUTLET_STOCK_INFO.OUTLET_ID = OUTLET_MST.OUTLET_ID WHERE OUTLET_MED_ID='" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' AND OUTLET_QTY > 0");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstOutLet") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }

            objdl = dA.returnList("SELECT MED_ID, MED_PACK, getMedQuantity(MED_ID, MED_PACK, SUM(CUR_BAL_STK)) AS BAL, getMedPacking(MED_ID, MED_PACK) AS PACKING FROM STOCK_DTLD_INFO WHERE MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' GROUP BY MED_ID, MED_PACK");
            //objdl = dA.returnList("SELECT MED_ID, MED_PACK, getMedQuantity(MED_ID, MED_PACK, SUM(CUR_BAL_STK)) AS BAL, getMedPacking(MED_ID, MED_PACK) AS PACKING FROM STOCK_DTLD_INFO WHERE MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' GROUP BY MED_ID, MED_PACK HAVING SUM(CUR_BAL_STK) >0");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstMovement") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }

            objdl = dA.returnList("SELECT  PO_MED_ID, SUPPLIER_NAME, PURCHASE_ORDER_INFO.PO_ID, PURCHASE_ORDER_INFO.PO_NO, PO_DATE, getMedQuantity(PO_MED_ID, PO_MED_PACK, MED_ORD_QTY) AS MED_ORD_QTY_P, PO_MED_COST FROM PURCHASE_ORDER_INFO JOIN PURCHASE_ORDER_DTLS ON PURCHASE_ORDER_INFO.PO_ID = PURCHASE_ORDER_DTLS.PO_ID JOIN SUPPLIER_MST ON SUPPLIER_ID=PO_SUPPLIER_ID WHERE PO_MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' ORDER BY PO_DATE DESC");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstPO") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }
        }
    }
}