using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReplenishmentService.APITransaction
{
    class APITransactionService
    {
        private bool runningFlag = true;
        public void APITransactionElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (this.runningFlag)
                {
                    this.runningFlag = false;
                    MainAPITransaction APItransaction= new  MainAPITransaction();
                    APItransaction.process();
                    this.runningFlag = true;
                }
                
            }
            catch (Exception error)
            {
                
            }
        }
   }
}

