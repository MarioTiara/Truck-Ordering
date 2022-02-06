using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplenishmentService.Ordering.Controller;

namespace ReplenishmentService.Ordering
{
    class MainOrdering
    {
        private ControllerTicket ticket = new ControllerTicket();
        private ControllerOrder orders = new ControllerOrder();
        public void order()
        {
            bool ticketready = ticket.booking();
            if (ticketready)
                orders.proccess();
        }
    }
}
