using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplenishmentService.Ordering.DbContext;
using ReplenishmentService.Ordering.Entity;
using ReplenishmentService.Ordering.Data;

namespace ReplenishmentService.Ordering
{
    class TicketingApp
    {
        private ContextIpeReplenishment ipeContext = new ContextIpeReplenishment();
        private ContextLms lmscontext = new ContextLms();
     
        public  void bookingTicket()
        {
            LinkedList<EntityTicketbook> ticketSlots = ipeContext.getTicketSlot();
            if (ticketSlots.Count > 0)
            {
                var transactions=ticketSlots.GroupBy(c => c.transaction_id);
                foreach (var transactionSlot in transactions)
                {
                    var ticketGroups = transactionSlot.GroupBy(s => s.group_no);
                    foreach (var ticketGroup in ticketGroups)
                    {                      
                        foreach (var ticketbook in ticketGroup)
                        {                          
                            Ticket lmsTicket = new Ticket(ticketbook);
                            ticketbook.ticket_id = lmsTicket.bookNewTicket();
                            ticketbook.ticket_status = 1;
                            ipeContext.KeepTicketId(ticketbook);
                        }
                    }
                }
            }
        }

        public void processOrder()
        {
            LinkedList<EntityAllocatedOrder> allocatedorders = ipeContext.getAllocatedOrder();
            var ordersGroupedbyTransactionId=allocatedorders.GroupBy(c => c.transactionId);
            foreach (var transactions in ordersGroupedbyTransactionId)
            {
                Console.WriteLine("transactions.Key: " + transactions.Key);
                var orders = transactions.GroupBy(s => s.groupNo);
                foreach( var order in orders){
                    Console.WriteLine("order.Key " + order.Key);
                    Order neworder = new Order();
                    int orderRoute = 1;
                    foreach (var data in order)
                    {                       
                        EntityLmsOrder lmsorder = neworder.makeOrder(data);                     
                        lmsorder.TripRoute = orderRoute;
                        lmscontext.saveOrder(lmsorder);
                        orderRoute +=1;
                    }
                }
            }

        }
    }
}
