using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


// This is the last page where the customer gets the confirmation of that the payment is done

namespace WebRole
{
    public partial class Last : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelName.Text = "Thank you for your booking!";
        }

        //protected void BtnReport(object sender, EventArgs e)
        //{
        //    Response.Redirect("/Reports.aspx");
        //}
    }
}