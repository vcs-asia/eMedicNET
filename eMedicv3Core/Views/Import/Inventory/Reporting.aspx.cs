using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vijay;

public partial class Inventory_Reporting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getOutlets();
    }
    private void getOutlets()
    {
        objDL objdl = new objDL();
        objdl = new dbAction(HttpContext.Current.Session["dT"].ToString(), HttpContext.Current.Session["cS"].ToString()).returnList("SELECT OUTLET_NAME, OUTLET_ID FROM OUTLET_MST ORDER BY OUTLET_NAME");

        lstOutlets.DataSource = objdl.dataSet.Tables[0];
        lstOutlets.DataTextField = "OUTLET_NAME";
        lstOutlets.DataValueField = "OUTLET_ID";
        lstOutlets.DataBind();
    }
}