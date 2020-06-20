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
                //Account and key are already initialized
                StorageCredentials creds = new StorageCredentials(storageName, storageKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                //Create an instance of a Cloud QueueClient object to access your queue in the storage
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                // Retrieve a reference to a specific queue
                CloudQueue queue = queueClient.GetQueueReference("frsqueue");

                // Create the queue if it doesn't already exist
                queue.CreateIfNotExists();

                //remove any existing messages (just in case)
                queue.Clear();

                // Create a message and add it to the queue.
                string s = from + " -- " + to + " -- " + infants + " -- " + children + " -- " + adults + " -- " + seniors + " -- " + name;

                CloudQueueMessage message = new CloudQueueMessage(s);

                queue.AddMessage(message);

                //Show in the console that some activity is going on in the Web Role
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