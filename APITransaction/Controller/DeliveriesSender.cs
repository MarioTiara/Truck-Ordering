using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using ReplenishmentService.APITransaction.DbAccess;
using ReplenishmentService.APITransaction.Model;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ReplenishmentService.APITransaction.Controller
{
   public  class DeliveriesSender
    {
       private ModelDelivery deliveryModel = new ModelDelivery();
       public async void sendDelivery()
       {          
           await Task.Run(() =>
           {
               //Console.WriteLine("================== DeliveriesSender ====================================");
               List<Dictionary<string, object>> deliveries = deliveryModel.getDelevery();
               if (deliveries.Count > 0)
               {
                   foreach (Dictionary<string, object> delivery in deliveries)
                   {
                       Int64 ticketId = Convert.ToInt64(delivery["TICKET_ID"]);
                       string data = getMultiFrom(delivery);
                       ModelRespon respons = postMultiForm(data);
                       if (respons.success)
                           DbIPEDelivery.updateTicketStatus(3, ticketId);
                   }
               }
               else
                   Console.WriteLine("No Delivery");
             
           });
           
       }

       private ModelRespon postMultiForm(string data)
       {
           bool HitSucess = false;
           ModelRespon responClient = new ModelRespon();
           while (!HitSucess)
           {
               try
               {
                   Authentication authenticationproperties = new Authentication();
                   var IpeURl = ConfigurationManager.AppSettings.Get("urlvelivery");
                   var httpRequest = (HttpWebRequest)WebRequest.Create(IpeURl);
                   httpRequest.Method = "POST";
                   httpRequest.Headers["Authorization"] = authenticationproperties.GetAuthorization();
                   httpRequest.ContentType = "application/x-www-form-urlencoded";
                   using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                   {
                       Console.WriteLine("/// data sent ////");
                       streamWriter.Write(data);
                   }
                   var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                   if (httpResponse.StatusCode.ToString().Equals("OK"))
                   {
                       using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                       {
                           var result = streamReader.ReadToEnd();
                           Console.WriteLine("/// respons ////");
                           Console.WriteLine(result);
                           JObject jObject = JObject.Parse(result);
                           responClient.message = jObject["message"].ToString();
                           responClient.success = Convert.ToBoolean(jObject["success"]);
                       }
                   } if (responClient.success)
                       HitSucess = true;
               }
               catch (Exception err)
               {
                   Log.log(err.Message, "APITransaction.DeliveriesSender.postMultiForm");
               }
           }
          

           return responClient;
       }


       private string getMultiFrom(Dictionary<string, object> postParameters)
       {
           string formdataTemplate = "{0}:{1}\n";
           var  sbuilder = new StringBuilder();
           if (postParameters != null)
           {
               foreach (string key in postParameters.Keys)
               {
                   if (!key.Equals("TICKET_ID"))
                        sbuilder.Append(string.Format(formdataTemplate, key, postParameters[key]));
               }
               return sbuilder.ToString();

           }

          return null;
       }

       private byte[] getBytesdata( string txt)
       {
           byte[] bytes = Encoding.UTF8.GetBytes(txt);
           return bytes;
       }

       private void WriteToStream(Stream s, byte[] bytes)
       {
           s.Write(bytes, 0, bytes.Length);
       }
     
    }
}
