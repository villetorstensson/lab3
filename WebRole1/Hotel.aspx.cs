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

// This class uses no database functionality but in the workerrole I added some anyway

namespace WebRole
{
    public partial class Hotel : System.Web.UI.Page
    {
        private string storageName = "villetorstenssonstorage3";
        private string storageKey = "u042KcqxSZ3GgWwMGg500SMSAjj5ZuUVAuFaIPU90RvqBYGeFkjB8lh58qbVQRBWclWef7NyFUpvgosncufLdQ==";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnCalculate(object sender, EventArgs e)
        {

            try
            {
                StorageCredentials creds = new StorageCredentials(storageName, storageKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue queue = queueClient.GetQueueReference("crsqueue");

                queue.CreateIfNotExists();

                queue.Clear();

                string s = TxtTravellers.Text + "*" + TxtNights.Text + "*" + TxtSeniors.Text + "*" + TxtPrimaryGuest.Text;

                if (CheckSingle.Checked == true)
                {
                    s = s + "*" + "True";
                }
                else
                {
                    s = s + "*" + "False";
                }
                if (CheckDouble.Checked == true)
                {
                    s = s + "*" + "True";
                }
                else
                {
                    s = s + "*" + "False";
                }

                CloudQueueMessage message = new CloudQueueMessage(s);

                queue.AddMessage(message);

                Debug.WriteLine("Message : '" + message + " 'stored in Queue");
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.StackTrace);
            }

            Response.Redirect("Payment.aspx", false);



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