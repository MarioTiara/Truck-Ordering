using ReplenishmentService.Ordering.Data;
using ReplenishmentService.Ordering.DbContext;
using ReplenishmentService.Ordering.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Controller
{
    class ControllerOrder
    {
        private ContextLms lmscontext = new ContextLms();
        private ContextIpeReplenishment ipeContext = new ContextIpeReplenishment();
        public void proccess() 
        {
            LinkedList<EntityAllocatedOrder> allocatedorders = ipeContext.getAllocatedOrder();
            if (allocatedorders.Count > 0)
            {
                var ordersGroupedbyTransactionId = allocatedorders.GroupBy(c => c.transactionId);
                foreach (var transactions in ordersGroupedbyTransactionId)
                {
                    Console.WriteLine("transactions.Key: " + transactions.Key);
                    var orders = transactions.GroupBy(s => s.groupNo);
                    foreach (var order in orders)
                    {
                        Console.WriteLine("order.Key " + order.Key);
                        Order neworder = new Order(order.First().ticketId);
                        int orderRoute = 1;
                        foreach (var data in order)
                        {
                            EntityOrderFullversion lmsorder = neworder.makeOrder(data);
                            lmsorder.TripRoute = orderRoute;
                            lmscontext.saveOrder(lmsorder);
                            orderRoute += 1;
                        }
                    }
                }
            }
            else
                Console.WriteLine("No allocated Order");

           
        }
    }
}
