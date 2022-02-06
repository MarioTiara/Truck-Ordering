using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
   public class EntityTicket
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Tipe { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Descryption { get; set; }
        public string NoUnit { get; set; }
        public int bConfirm { get; set; }
        public int bValid { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
