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
    public class senderDelivery
    {
        private ModelDelivery deliveryModel = new ModelDelivery();
        public void sendDelivery()
        {
            List<Dictionary<string, string>> deliveries = deliveryModel.getDelevery();
            //Console.WriteLine(deliveries.Count);
            foreach (Dictionary<string, string> delivery in deliveries)
            {
                byte[] data = getMultiFrom(delivery);
                Console.WriteLine(data.Count());
                postMultiForm(data);
            }
        }

        private void postMultiForm(byte[] formData)
        {
            ModelRespon responClient = new ModelRespon();
            Authentication authenticationproperties = new Authentication();
            var IpeURl = ConfigurationManager.AppSettings.Get("urlvelivery");
            var httpRequest = (HttpWebRequest)WebRequest.Create(IpeURl);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = authenticationproperties.GetAuthorization();
            httpRequest.ContentType = "multipart/form-data";
            using (Stream requestStream = httpRequest.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            var resnponse = httpRequest.GetResponse() as HttpWebResponse;
        }

        private byte[] getMultiFrom(Dictionary<string, string> postParameters)
        {
            string formdataTemplate = "{0}:{1}\n";
            var sbuilder = new StringBuilder();
            if (postParameters != null)
            {
                foreach (string key in postParameters.Keys)
                {
                    sbuilder.Append(string.Format(formdataTemplate, key, postParameters[key]));
                }
                Console.WriteLine(sbuilder);
                return getBytesdata( sbuilder.ToString());

            }

            return null;
        }

        private byte[] getBytesdata(string txt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(txt);
            return bytes;
        }
    }
}
