using System;
using System.Collections.Generic;
using ReplenishmentService.APITransaction.DbAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Model
{
    public class ModelDelivery
    {
        private DbIPEDelivery dbdelevery = new DbIPEDelivery();
        public int TICKET_ID { get; set; }
        public string DMS_TRANSFER_DOC_NO { get; set; }
        public string SAP_NUMBER { get; set; }
        public string SPM_NUMBER { get; set; }
        public string ACTUAL_QUANTITY { get; set; }
        public string TRUCK_NO { get; set; }
        public string DRIVER_NAME { get; set; }
        public string SEAL_NO { get; set; }
        public string DEPO_STRUK_PHOTO { get; set; }
        public string SITE_RECEIPT_STRUK_PHOTO { get; set; }

        

        public List<Dictionary<String,Object>> getDelevery()
        {
            List<ModelDelivery> deliveries = dbdelevery.selectDeleveryIsReady();
            List<Dictionary<string, object>> listdelivery = new List<Dictionary<string, object>>();
            foreach( var data in deliveries)
            {
                Dictionary<string, object> delivery = new Dictionary<string, object>();
                delivery.Add("TICKET_ID", data.TICKET_ID);
                delivery.Add("DMS_TRANSFER_DOC_NO", data.DMS_TRANSFER_DOC_NO);
                delivery.Add("SAP_NUMBER", data.SAP_NUMBER);
                delivery.Add("SPM_NUMBER", data.SPM_NUMBER);
                delivery.Add("ACTUAL_QUANTITY", data.ACTUAL_QUANTITY);
                delivery.Add("TRUCK_NO", data.TRUCK_NO);
                delivery.Add("DRIVER_NAME", data.DRIVER_NAME);
                delivery.Add("SEAL_NO", data.SEAL_NO);
                delivery.Add("DEPO_STRUK_PHOTO", data.DEPO_STRUK_PHOTO);
                delivery.Add("SITE_RECEIPT_STRUK_PHOTO", data.SITE_RECEIPT_STRUK_PHOTO);
                listdelivery.Add(delivery);               
            }
            return listdelivery;
        }

        public List<ModelDelivery> getDeliveryList()
        {
            return dbdelevery.selectDeleveryIsReady();
        }
    }
}
