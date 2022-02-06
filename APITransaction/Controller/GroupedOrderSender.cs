using ReplenishmentService.APITransaction.DbAccess;
using ReplenishmentService.APITransaction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Controller
{
    public class GroupedOrderSender
    {
        private ClosingRequest closing = new ClosingRequest();
        private SenderRequest sender = new SenderRequest();
        private void HitIpe(List<DbIPEGroped> hdr)
        {
            ModelIPEreplenishment modelreplenishment = new ModelIPEreplenishment(hdr);
            Queue<ModelIPEreplenishment> queuereplenishment = modelreplenishment.GetData();
            ModelCloseHit closeobject = new ModelCloseHit(queuereplenishment);
            while (queuereplenishment.Count > 0)
            {
                string inputJson = sender.GetReplenishmentJson(queuereplenishment);
                sender.HitRequest(inputJson);
            }
            if (queuereplenishment.Count <= 0)
            {
                closeobject.STATUS = "CLOSED";
                DateTime now = DateTime.Now;
                closeobject.CREATION_DATETIME = now.Year + "-" + now.Month + "-" + now.Day;
                string CloseInputJson = closing.GetCloseReplenishmentJson(closeobject);
                modelreplenishment.UpdateHdrState();
                closing.CloseRequest(CloseInputJson);
            }
        }

        public void sendGroupedorder()
        {
            //Console.WriteLine("================== GroupedOrderSender ====================================");
            List<DbIPEGroped> DbListP = DbIPEGroped.GetReplenishmentList();
            if (DbListP.Count > 0)
            {
                var groupedbyHdr = DbListP.GroupBy(c => c.Id_hdr);
                foreach (var data in groupedbyHdr)
                {
                    List<DbIPEGroped> hdr = data.ToList();
                    Console.WriteLine(hdr.Count);
                    HitIpe(hdr);
                }
            }
            else
            {
                Console.WriteLine("No Data Grouped");
                Thread.Sleep(5 * 1000);
            }

        }
    }
}
