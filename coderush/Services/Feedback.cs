using coderush.Models.AccountViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace coderush.Services
{
    public class Feedback : IFeedback
    {
        private SlackOptions _slackOptions { get; }
        public Feedback(IOptions<SlackOptions> slackOptions) 
        {
            _slackOptions = slackOptions.Value;
        }
        public void SendUserFeedback(SlackFeedbackModel slackFeedbackModel)
        {
            string url = _slackOptions.url;
            string token = _slackOptions.token;
            string channel = _slackOptions.channel;

            slackFeedbackModel.channel = channel;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // add authenitication details
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = JsonConvert.SerializeObject(slackFeedbackModel);
            HttpContent requestContent = new StringContent(content, Encoding.UTF8, "application/json");

            string urlParameters = "api/chat.postMessage";
            // List data response.
            HttpResponseMessage response = client.PostAsync(urlParameters, requestContent).Result; // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("");
            }
            var dataObjects = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            var jObject = JObject.Parse(dataObjects);

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
