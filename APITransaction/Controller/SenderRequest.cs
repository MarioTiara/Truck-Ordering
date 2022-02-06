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
    class SenderRequest
    {
        public string GetReplenishmentJson(Queue<ModelIPEreplenishment> quereplenishment)
        {
            ModelIPEreplenishment Obejct = quereplenishment.Dequeue();
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(Obejct);
            return serializedResult.ToString();
        }
        public void HitRequest(String inputJson)
        {
            bool HitSuccess = false;
            ModelRespon responClient = new ModelRespon();
            while (!HitSuccess)
            {
                try
                {
                    Authentication authenticationproperties = new Authentication();
                    var IpeURl = ConfigurationManager.AppSettings.Get("IpeHitURl");
                    var httpRequest = (HttpWebRequest)WebRequest.Create(IpeURl);
                    httpRequest.Method = "POST";
                    httpRequest.Headers["Authorization"] = authenticationproperties.GetAuthorization();
                    httpRequest.ContentType = "application/json";
                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        Console.WriteLine("/// data sent ////");
                        Console.WriteLine(inputJson);
                        streamWriter.Write(inputJson);
                    }
                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    if (httpResponse.StatusCode.ToString().Equals("OK"))
                    {
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            Console.WriteLine("/// respons ////");
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
                        Log.log(responClient.message, "ApiTransaction.HitRequest");
                        Thread.Sleep(1 * 1000);
                    }
                }
                catch (Exception err)
                {
                    Log.log(err.Message, "ApiTransaction.HitRequest");
                    Thread.Sleep(1 * 1000);
                }
            }
        }
    }
}
