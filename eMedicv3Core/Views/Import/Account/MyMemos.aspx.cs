using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Account_MyMemos : System.Web.UI.Page
{
    protected void newMemo(object sender, EventArgs e)
    {
        Response.Redirect("~/Inventory/sDrug.aspx");
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

        objdl = dA.returnList("SELECT MED_ID, MED_NAME, MED_GENERIC_NAME, getMedQuantity(MED_ID, MED_PACKING, MED_CURRENT_STOCK) AS BAL, MED_STOCK_CODE, MED_FLAG FROM MEDICINE_MST WHERE MED_TYPE = 223" + searchKeyword);
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
        fillGrid("MED_NAME", "ASC");
    }
    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            dbAction dA = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString());
            objDL objdl = new objDL();

            objdl = dA.returnList("SELECT BATCH_NO, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM,CUR_BAL_STK, MED_PACKING) AS BAL, EXP_DATE, BATCH_ID FROM STOCK_DTLD_INFO JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID=STOCK_DTLD_INFO.MED_ID WHERE STOCK_DTLD_INFO.MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' AND CUR_BAL_STK !=0");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstBatch") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }

            objdl = dA.returnList("SELECT OUTLET_NAME, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM, OUTLET_QTY, MED_PACKING) AS BAL, OUTLET_MST.OUTLET_ID FROM OUTLET_STOCK_INFO JOIN OUTLET_MST ON OUTLET_STOCK_INFO.OUTLET_ID = OUTLET_MST.OUTLET_ID JOIN MEDICINE_MST ON MED_ID=OUTLET_MED_ID WHERE OUTLET_MED_ID='" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' AND OUTLET_QTY != 0");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstOutLet") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }

            objdl = dA.returnList("SELECT TRANS_ID, TRAN_DATE, REF_NO, TRAN_TYPE, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM, QTY_IN, MED_PACKING) AS QIN, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM, QTY_OUT, MED_PACKING) AS QUT, getMedQuantity(MED_BIG_UOM, MED_SMALL_UOM, CURRENT_BAL, MED_PACKING) AS BAL, BATCH_NO, STOCK_MOVEMENT_INFO.MED_ID FROM STOCK_MOVEMENT_INFO JOIN MEDICINE_MST ON MEDICINE_MST.MED_ID=STOCK_MOVEMENT_INFO.MED_ID WHERE STOCK_MOVEMENT_INFO.MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "'");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstMovement") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }
            objdl = dA.returnList("SELECT TOP 10 MED_ID, SUPPLIER_NAME, PURCHASE_ORDER_INFO.PO_ID, PURCHASE_ORDER_INFO.PO_NO, PO_DATE, MED_ORD_QTY_P, MED_COST_PRICE FROM PURCHASE_ORDER_INFO JOIN PURCHASE_ORDER_DTLS ON PURCHASE_ORDER_INFO.PO_ID = PURCHASE_ORDER_DTLS.PO_ID JOIN SUPPLIER_MST ON SUPPLIER_ID=PO_SUPPLIER_ID WHERE MED_ID = '" + Lst.DataKeys[e.Row.RowIndex].Values[0].ToString() + "' ORDER BY PO_DATE DESC");
            if (objdl.flaG == true)
            {
                GridView gv = e.Row.FindControl("LstPO") as GridView;
                gv.DataSource = new DataView(objdl.dataSet.Tables[0]);
                gv.DataBind();
            }
        }
    }
}