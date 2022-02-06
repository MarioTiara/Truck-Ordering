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
    class ControllerTicket
    {
        private ContextIpeReplenishment ipeContext = new ContextIpeReplenishment();
        public bool booking()
        {
            LinkedList<EntityTicketbook> ticketSlots = ipeContext.getTicketSlot();
            Console.WriteLine("ticketSlots:" + ticketSlots.Count);
            if (ticketSlots.Count > 0)
            {
                bool bookingSuccess = false;
                while (!bookingSuccess)
                {
                    try
                    {
                        var transactions = ticketSlots.GroupBy(c => c.transaction_id);
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
                        bookingSuccess = true;
                    }
                    catch (Exception err)
                    {
                        Log.log(err.Message, "Ordering.Controller.ControllerTicket.booking");
                    }
                }
                return bookingSuccess;
            }
            else
                Console.WriteLine("No new booking");         
            return true;
        }
    }
}
