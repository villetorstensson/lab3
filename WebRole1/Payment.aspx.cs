using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


// in this payment class I use the NoSQL to save Transaction and Customer information to the database. 

namespace WebRole
{
    public partial class Payment : System.Web.UI.Page
    {
        private string connectionString = "mongodb+srv://ville:hejhej@cluster0-7lagi.mongodb.net/test";
        private string accountName = "villetorstenssonstorage3";
        private string accountKey = "u042KcqxSZ3GgWwMGg500SMSAjj5ZuUVAuFaIPU90RvqBYGeFkjB8lh58qbVQRBWclWef7NyFUpvgosncufLdQ==";
        private StorageCredentials creds;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;
        private CloudQueue calculate1, calculate2;
        private CloudQueueMessage messageCalc1, messageCalc2;
        double finalCost;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnPay(object sender, EventArgs e)
        {
            string name = TxtHolderName.Text;
            string creditCard = TxtCreditCard.Text;
            int balance = 999;
            string expirationDate = "2023-07-06";
            string date = DateTime.Now.ToString();

            MongoClient dbClient = new MongoClient(connectionString);
            var database = dbClient.GetDatabase("lab3cosmosdb");
            var customerTable = database.GetCollection<BsonDocument>("Customer");
            var transactionTable = database.GetCollection<BsonDocument>("Transactions");

            var transDoc = new BsonDocument
            {
                {"name", name },
                {"transaction", balance },
                {"date", date },
                {"creditcard", creditCard }

            };

            var custDoc = new BsonDocument
            {
                {"name", name },
                {"balance", balance },
                {"expirydate", expirationDate },
                {"creditcard", creditCard },
            };
            
            transactionTable.InsertOneAsync(transDoc);
            customerTable.InsertOneAsync(custDoc);
            
            Response.Redirect("Last.aspx", false);

        }

        protected void BtnOffer(object sender, EventArgs e)
        {
            creds = new StorageCredentials(accountName, accountKey);
            storageAccount = new CloudStorageAccount(creds, useHttps: true);

            queueClient = storageAccount.CreateCloudQueueClient();

            calculate1 = queueClient.GetQueueReference("calculate1");
            calculate2 = queueClient.GetQueueReference("calculate2");

            calculate1.CreateIfNotExists();
            calculate2.CreateIfNotExists();

            messageCalc1 = calculate1.GetMessage();
            messageCalc2 = calculate2.GetMessage();

            double calcOne = 0;
            double calcTwo = 0;

            if (messageCalc1 != null)
            {
                calcOne = double.Parse(messageCalc1.AsString);
            }
            if (messageCalc2 != null)
            {
                calcTwo = double.Parse(messageCalc2.AsString);
            }

            calculate1.Clear();
            calculate2.Clear();

            finalCost = calcOne + calcTwo;
            LabelTotalCost.Text = "Total cost " + finalCost.ToString() + " SEK";

        }
    }
}