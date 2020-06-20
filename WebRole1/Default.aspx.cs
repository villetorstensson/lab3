using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// This is where the application starts, the user can choose to include the hotel reservation or not

namespace WebRole
{
    public partial class Default : Page
    {

        bool HotelBox = false;
        List<string> listValues = new List<string>();

        protected void BtnContinue(object sender, EventArgs e)
        {
            foreach (ListItem Item in CheckAdditionalBox.Items)
            {
                if (Item.Selected)
                {
                    listValues.Add(Item.Value);
                }
            }

            if (listValues.Contains("hrsbox"))
            {
                HotelBox = true;
            }
            Response.Redirect("/Flight.aspx?hrsbox=" + this.HotelBox, false);
        }
        protected void BtnReport(object sender, EventArgs e)
        {
            Response.Redirect("/Reports.aspx");
        }
    }
}