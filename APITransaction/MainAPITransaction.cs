using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using ReplenishmentService.APITransaction.Model;
using ReplenishmentService.APITransaction.Controller;
using ReplenishmentService.APITransaction.DbAccess;

namespace ReplenishmentService.APITransaction
{
    public class MainAPITransaction
    {

        private DeliveriesSender delivery = new DeliveriesSender();
        private GroupedOrderSender groupedOrder = new GroupedOrderSender();
        public async void process()
        {
            delivery.sendDelivery();
            groupedOrder.sendGroupedorder();
        }
       
       
    }
}
