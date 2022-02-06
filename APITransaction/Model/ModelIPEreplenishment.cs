using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplenishmentService.APITransaction.DbAccess;

namespace ReplenishmentService.APITransaction.Model
{
    public class ModelIPEreplenishment
    {
        private int ID_HDR { get; set; }
        private List<DbIPEGroped> DbListP = new List<DbIPEGroped>();
        public string REPLENISHMENT_GROUP_SYS_NO { get; set; }
        public string SEINO_GROUPING_NUMBER { get; set; }
        public string SEINO_GROUPING_DOC_NO { get; set; }
        public string CREATION_DATETIME { get; set; }
        public string BATCH_NO { get; set; }
        public string TOTAL_BATCH { get; set; }
        public string COUNT_OF_DETAILS { get; set; }
        public LinkedList<EntityDetails> DETAILS = new LinkedList<EntityDetails>();
        public ModelIPEreplenishment(List<DbIPEGroped> DbListP)
        {
            //this.DbListP = DbIPEGroped.GetReplenishmentList();
            this.DbListP = DbListP;
            if (this.DbListP.Count>0)
                this.ID_HDR = DbListP.First().Id_hdr;          
        }

        public ModelIPEreplenishment()
        {

        }
        public Queue<ModelIPEreplenishment> GetData()
        {                        
            int BatchNumber = 0;
            Queue<ModelIPEreplenishment> replenishmentQueue = new Queue<ModelIPEreplenishment>();
            var DbListgrouped = this.DbListP.GroupBy(c => c.group_no);
            foreach (var data in DbListgrouped){
                BatchNumber++;
                List<DbIPEGroped> groupedList = data.ToList();
                ModelIPEreplenishment objectreplenishment = new ModelIPEreplenishment();
                objectreplenishment.SEINO_GROUPING_NUMBER = data.Key.ToString();
                objectreplenishment.CREATION_DATETIME = groupedList.First().create_date;
                objectreplenishment.REPLENISHMENT_GROUP_SYS_NO = groupedList.First().transaction_no.ToString();
                objectreplenishment.SEINO_GROUPING_DOC_NO = groupedList.First().group_doc_no;
                objectreplenishment.BATCH_NO = BatchNumber.ToString();
                objectreplenishment.TOTAL_BATCH = DbListgrouped.Count().ToString();
                foreach (var datagrouped in groupedList){                  
                    EntityDetails objectdetails = new EntityDetails();
                    objectdetails.REPLENISHMENT_GROUP_LINE_NO = datagrouped.line_no.ToString();
                    objectdetails.SITE_CODE = datagrouped.site_no;
                    objectdetails.SOURCE_DEPO = datagrouped.terminal_no;
                    objectdetails.ITEM_CODE = datagrouped.item_code.ToString();
                    objectdetails.QUANTITY = datagrouped.quantity.ToString();
                    objectdetails.REVISED_QUANTITY = datagrouped.revised_quantity.ToString();
                    objectreplenishment.DETAILS.AddLast(objectdetails);
                }
                objectreplenishment.COUNT_OF_DETAILS = objectreplenishment.DETAILS.Count.ToString();        
                replenishmentQueue.Enqueue(objectreplenishment);              
            }
            return replenishmentQueue;        
        }

        public void UpdateHdrState()
        {          
            DbIPEGroped.UpdateState(this.ID_HDR);
        }

        public void Updateregistry()
        {
            DbIPEGroped.UpdateRegistry(this.ID_HDR);
        }
        
    }
}
