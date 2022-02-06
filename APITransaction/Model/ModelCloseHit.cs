using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Model
{
    class ModelCloseHit
    {
        public string REPLENISHMENT_GROUP_SYS_NO { get; set; }
        public string COUNT_OF_DETAILS { get; set; }
        public string STATUS { get; set; }
        public string CREATION_DATETIME { get; set; }
        public ModelCloseHit(Queue<ModelIPEreplenishment> quereplenishment)
        {
            int count = 0;
            this.REPLENISHMENT_GROUP_SYS_NO = quereplenishment.First().REPLENISHMENT_GROUP_SYS_NO;
            foreach (var data in quereplenishment)
            {                
                count = count + data.DETAILS.Count;
                this.COUNT_OF_DETAILS = count.ToString();              
            }
        }
    }
}
