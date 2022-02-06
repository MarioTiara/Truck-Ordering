using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ReplenishmentService.APITransaction.Model
{
    class ModelUser
    {
        private USerEntity user = new USerEntity();
        public ModelUser()
        {
            this.user.username = ConfigurationManager.AppSettings.Get("username");
            this.user.password = ConfigurationManager.AppSettings.Get("password");
        }

        public string GetUserdataJson()
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this.user);          
            return serializedResult.ToString();
        }

        public class USerEntity
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        
    }
}
