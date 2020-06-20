using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure;
using System.Diagnostics;

// Here is where the different routes a user can use is showed in flight.aspx using SQL

namespace WebRole
{
    public partial class Flight : System.Web.UI.Page
    {

        private string storageName = "villetorstenssonstorage3";
        private string storageKey = "u042KcqxSZ3GgWwMGg500SMSAjj5ZuUVAuFaIPU90RvqBYGeFkjB8lh58qbVQRBWclWef7NyFUpvgosncufLdQ==";

        bool hrsbox;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["hrsbox"] != null)
            {
                hrsbox = bool.Parse(Request.QueryString["hrsbox"]);
            }

        }

        protected void BtnContinue(object sender, EventArgs e)
        {
            int infants = int.Parse(TxtInfants.Text);
            int children = int.Parse(TxtChildren.Text);
            int seniors = int.Parse(TxtSeniors.Text);
            int adults = int.Parse(TxtAdults.Text);
            string from = RadioFrom.SelectedValue;
            string to = RadioTo.SelectedValue;
            string name = TxtCustomer.Text;

            try
            {
  
                StorageCredentials creds = new StorageCredentials(storageName, storageKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

              
                CloudQueue queue = queueClient.GetQueueReference("frsqueue");

                queue.CreateIfNotExists();

                queue.Clear();

                
                string s = from + " -- " + to + " -- " + infants + " -- " + children + " -- " + adults + " -- " + seniors + " -- " + name;

                CloudQueueMessage message = new CloudQueueMessage(s);

                queue.AddMessage(message);

           
                Debug.WriteLine("Message : '" + message + " 'stored in Queue");
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.StackTrace);
            }


            if (hrsbox == true)
            {
                Response.Redirect("Hotel.aspx", false);
            }
            else
            {
                Response.Redirect("Payment.aspx", false);
            }
        }


        protected void BtnCancel(object sender, EventArgs e)
        {

            Response.Redirect("Default.aspx", false);

        }

        protected void BtnReport(object sender, EventArgs e)
        {

            Response.Redirect("Reports.aspx");

        }
    }
}