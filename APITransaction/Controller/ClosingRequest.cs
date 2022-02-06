using Newtonsoft.Json.Linq;
using ReplenishmentService.APITransaction.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ReplenishmentService.APITransaction.Controller
{
    class ClosingRequest
    {
        public void CloseRequest(String inputJson)
        {
            
            bool HitSuccess = false;
            ModelRespon responClient = new ModelRespon();
            while (!HitSuccess)
            {
                try
                {
                    Authentication authenticationproperties = new Authentication();
                    var IpeURl = ConfigurationManager.AppSettings.Get("IpeHitCloseURl");
                    var httpRequest = (HttpWebRequest)WebRequest.Create(IpeURl);
                    httpRequest.Method = "POST";
                    httpRequest.Headers["Authorization"] = authenticationproperties.GetAuthorization();
                    httpRequest.ContentType = "application/json";
                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        streamWriter.Write(inputJson);
                    }
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                    if (httpResponse.StatusCode.ToString().Equals("OK"))
                    {
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            Console.WriteLine(result);
                            JObject jObject = JObject.Parse(result);
                            responClient.message = jObject["message"].ToString();
                            responClient.success = Convert.ToBoolean(jObject["success"]);
                        }
                    }
                    if (responClient.success)
                        HitSuccess = true;
                    else if (!responClient.success)
                    {
                        Log.log(responClient.message, "ApiTransaction.CloseRequest");
                        Thread.Sleep(1 * 1000);
                    }
                }
                catch (Exception err)
                {
                    Log.log(err.Message, "ApiTransaction.CloseRequest");
                    Thread.Sleep(1 * 1000);
                }
            }
        }

        public string GetCloseReplenishmentJson(ModelCloseHit closeobject)
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(closeobject);
            return serializedResult.ToString();
        }
    }
}
