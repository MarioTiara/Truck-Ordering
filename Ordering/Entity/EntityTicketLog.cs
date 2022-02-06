using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
    public class EntityLmsTicketLog
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public int bConfirm { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

    }
}
