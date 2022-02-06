using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
    public class EntityOrderFullversion:EntityOrder
    {       
        public EntityOrderItem item { get; set;}
        public EntityUangJalan uangJalan { get; set;}
        public EntityMsImage imageOrder { get; set; }
    }
}
