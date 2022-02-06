using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Data
{
    class Mapbox
    {
        private string apiLink = "https://api.mapbox.com/directions/v5/mapbox/driving/";
        private string token = "?access_token=pk.eyJ1Ijoic2Vpbm9rYWhmaSIsImEiOiJja285aWY5NHcwNHlyMm9xbWY3ZWhlcm0wIn0.kUzmIB4Vzg0A9XKE6O1ipA";
        private GeoCoordinate origin;
        private GeoCoordinate destination;
        private string response { get; set; }
        public Mapbox(GeoCoordinate origin, GeoCoordinate destination)
        {
            this.origin = origin;
            this.destination = destination;
            this.response = getResponse();            
        }
        private string  getResponse()
        {
            bool requestsuccess = false;
            string result = null;
            var url = string.Concat(apiLink, 
                                     origin.Longitude.ToString().Replace(",", "."),
                                    ",",
                                    origin.Latitude.ToString().Replace(",","."),
                                    ";",
                                    destination.Longitude.ToString().Replace(",", "."), 
                                    ",",
                                    destination.Latitude.ToString().Replace(",", "."),
                                    token);
           // Console.WriteLine(url);
            while (!requestsuccess)
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                if (httpResponse.StatusCode.ToString().Equals("OK"))
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                        requestsuccess = true;                                                                                  
                    }
                }
            }
            return result;
        }

        public double getMileage()
        {
            JObject jObject = JObject.Parse(this.response);
            double mileageinHour = Convert.ToDouble(jObject["routes"][0]["duration"]) / 3600;
            return mileageinHour;            
        }
    }
}
