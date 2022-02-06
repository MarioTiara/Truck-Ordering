using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
   public  class EntityOrderItem
    {
       public string OrderNo { get; set; }
       public string PONo { get; set; }
       public string ItemCode { get; set; }
       public float ItemQty { get; set; }
       public float Weight { get; set; }
       public string ItemUnit = "LITER";
       public DateTime CreateDate { get; set; }
       public string CreateBy = "system";
       public DateTime UpdateDate { get; set; }
       public string UpdateBy = "system";

    }
}
