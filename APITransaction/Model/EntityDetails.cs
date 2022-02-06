using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Model
{
    public class EntityDetails
    {
        public string REPLENISHMENT_GROUP_LINE_NO { get; set; }
        public string SITE_CODE { get; set; }
        public string SOURCE_DEPO { get; set; }
        public string ITEM_CODE { get; set; }
        public string QUANTITY { get; set; }
        public string REVISED_QUANTITY { get;set;}
    }
}
