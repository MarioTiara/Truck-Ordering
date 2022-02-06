using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ReplenishmentService.Ordering
{
    class OrderingService
    {
        private bool runningFlag = true;
        public void OrderingElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (this.runningFlag)
                {
                    this.runningFlag = false;
                    MainOrdering ordering = new MainOrdering();
                    ordering.order();
                    this.runningFlag = true;
                }

            }
            catch (Exception error)
            {

            }
        }
    }
}
