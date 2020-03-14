using RestSharp;
using System;
using System.Net;
using System.Windows;

namespace WAYDN
{
    public class TweetModel
    {
        private string message = "";
        public string Message {
            get { return message; } 
            set {
                message = value; 
            } 
        }

        public void Tweet()
        {
            var client = new RestClient();
            var request = new RestRequest();

            client.BaseUrl = new Uri(Properties.Settings.Default.WebhookURL);

            message = message.Replace(Environment.NewLine, "\n");

            request.Method = Method.POST;
            request.AddHeader("content-type", "application/json");

            request.AddJsonBody(new {text = message});

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
                MessageBox.Show("error!");
        }
    }
}
