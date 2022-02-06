using Newtonsoft.Json.Linq;
using ReplenishmentService.APITransaction.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Controller
{
    public class Authentication
    {
        private string DataStream { get; set; }
        private string access_token { get; set; }
        public Authentication()
        {
            RequestToken();
        }
        private void RequestToken()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ModelUser modeluser = new ModelUser();
            String inputJson = modeluser.GetUserdataJson();
            var url = ConfigurationManager.AppSettings.Get("urlToken");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(inputJson);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpResponse.StatusCode.ToString().Equals("OK"))
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    this.DataStream = streamReader.ReadToEnd();
                    JObject jObject = JObject.Parse(this.DataStream);
                    this.access_token = jObject["token"].ToString();
                }
            }

        }

        public string GetAuthorization()
        {
            return "Bearer " + this.access_token;
        }
    }
}
