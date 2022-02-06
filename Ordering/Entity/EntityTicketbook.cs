using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
    public class EntityTicketbook
    {
        public int Id { get; set; }
        public int transaction_id { get; set; }
        public int group_no { get; set; }
        public int ticket_id { get; set; }
        public int ticket_status { get; set; }
        public int totalSlot { get; set; }
        public string destinationCode { get; set; }
        public string originCode { get; set; }

    }
}
