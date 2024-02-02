using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace emedic3.Reports
{
    public partial class reportingUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            drpMonth.SelectedValue = DateTime.Now.ToString("M");
            txtYear.Text = DateTime.Now.ToString("yyyy");
        }
    }
}