using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vijay;

public partial class Inventory_DrugSP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            generateDynamicItems(int.Parse(pages.SelectedValue.Split('.')[0]), int.Parse(pages.SelectedValue.Split('.')[0]));
        }
        else
        {
            getbackSubmittedItems();
        }
    }
    private void getbackSubmittedItems()
    {
        string str = "";

        string[] desc = Request.Form.GetValues("desc[]");
        string[] drug = Request.Form.GetValues("drug[]");
        string[] ucst = Request.Form.GetValues("ucst[]");
        string[] suom = Request.Form.GetValues("suom[]");
        string[] seli = Request.Form.GetValues("seli[]");
        string[] mrki = Request.Form.GetValues("mrki[]");
        string[] selo = Request.Form.GetValues("selo[]");
        string[] mrko = Request.Form.GetValues("mrko[]");

        for (int row = 0; row < drug.Count() - 1; row++ )
        {
            str += "$('#tblDrugs').append('";
            str += "<tr>";
            str += "<td>" + (row + 1) + "</td>";
            str += "<td><input type=\"text\" id=\"desc[]\" name=\"desc[]\" value=\"" + desc[row] + "\" style=\"width:90%\" ReadOnly=\"true\"/><input type=\"hidden\" id=\"drug[]\" name=\"drug[]\" value=\"" + drug[row] + "\"/></td>";
            str += "<td><input type=\"text\" id=\"ucst[]\" name=\"ucst[]\" value=\"" + ucst[row] + "\" style=\"width:90%;text-align:right\" ReadOnly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" value=\"" + suom[row] + "\" style=\"width:90%;\" ReadOnly=\"true\"/></td>";
            str += "<td><input type=\"text\" id=\"seli[]\" name=\"seli[]\" value=\"" + seli[row] + "\" style=\"width:90%;text-align:right\"/></td>";
            str += "<td><input type=\"text\" id=\"mrki[]\" name=\"mrki[]\" value=\"" + mrki[row] + "\" style=\"width:90%;text-align:right\"/></td>";
            str += "<td><input type=\"text\" id=\"selo[]\" name=\"selo[]\" value=\"" + selo[row] + "\" style=\"width:90%;text-align:right\"/></td>";
            str += "<td><input type=\"text\" id=\"mrko[]\" name=\"mrko[]\" value=\"" + mrko[row] + "\" style=\"width:90%;text-align:right\"/></td>";
            str += "</tr>";
            str += "');";
        }

            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "<DIV>", "<script type='text/javascript'>" + str + "</script>");
    }
    protected void onChange(object sender, EventArgs e)
    {
        generateDynamicItems(int.Parse(pages.SelectedValue.Split('.')[0]), int.Parse(pages.SelectedValue.Split('.')[0]));
    }
    private void generateDynamicItems(int start, int end)
    {
        string str = "";
        objDL objdl = new objDL();

        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("WITH ITEMS AS (SELECT ROW_NUMBER() OVER (ORDER BY MED_NAME) AS ROW, MED_ID, MED_NAME, MED_UNIT_COST, MED_SMALL_UOM, MED_SELLING_PRICE, MED_MARK_UP, MED_OUT_SELLING_COST, MED_OUT_MARK_UP FROM MEDICINE_MST WHERE MED_TYPE!=223 AND MED_FLAG=1) SELECT ROW, MED_ID, MED_NAME, MED_UNIT_COST, MED_SMALL_UOM, MED_SELLING_PRICE, MED_MARK_UP, MED_OUT_SELLING_COST, MED_OUT_MARK_UP FROM ITEMS WHERE ROW BETWEEN " + start + " AND " + end + " ORDER BY MED_NAME");
        if (objdl.flaG==true)
        {
            for (int row = 0; row < objdl.dataSet.Tables[0].Rows.Count - 1; row++ )
            {
                DataRow Row = objdl.dataSet.Tables[0].Rows[row];
                str += "$('#tblDrugs').append('";
                str += "<tr>";
                str += "<td>" + Row["ROW"].ToString() + "</td>";
                str += "<td><input type=\"text\" id=\"desc[]\" name=\"desc[]\" value=\"" + Row["MED_NAME"].ToString() + "\" style=\"width:90%\" ReadOnly=\"true\"/><input type=\"hidden\" id=\"drug[]\" name=\"drug[]\" value=\"" + Row["MED_ID"].ToString() + "\"/></td>";
                str += "<td><input type=\"text\" id=\"ucst[]\" name=\"ucst[]\" value=\"" + Row["MED_UNIT_COST"].ToString() + "\" style=\"width:90%;text-align:right\" ReadOnly=\"true\"/></td>";
                str += "<td><input type=\"text\" id=\"suom[]\" name=\"suom[]\" value=\"" + Row["MED_SMALL_UOM"].ToString() + "\" style=\"width:90%;\" ReadOnly=\"true\"/></td>";
                str += "<td><input type=\"text\" id=\"seli[]\" name=\"seli[]\" value=\"" + Row["MED_SELLING_PRICE"].ToString() + "\" style=\"width:90%;text-align:right\"/></td>";
                str += "<td><input type=\"text\" id=\"mrki[]\" name=\"mrki[]\" value=\"" + Row["MED_MARK_UP"].ToString() + "\" style=\"width:90%;text-align:right\"/></td>";
                str += "<td><input type=\"text\" id=\"selo[]\" name=\"selo[]\" value=\"" + Row["MED_OUT_SELLING_COST"].ToString() + "\" style=\"width:90%;text-align:right\"/></td>";
                str += "<td><input type=\"text\" id=\"mrko[]\" name=\"mrko[]\" value=\"" + Row["MED_OUT_MARK_UP"].ToString() + "\" style=\"width:90%;text-align:right\"/></td>";
                str += "</tr>";
                str += "');";
            }

            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "<DIV>", "<script type='text/javascript'>" + str + "</script>");
        }
        else
        {
            lblError.Text = objdl.Msg;
            pnlError.Visible = true;
        }
    }
}