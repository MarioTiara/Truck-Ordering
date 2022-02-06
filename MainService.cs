using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ReplenishmentService.APITransaction;
using ReplenishmentService.Ordering;

namespace ReplenishmentService
{
    class MainService
    {
        private APITransactionService APITransaction = new APITransactionService();
        private OrderingService ordering = new OrderingService();
        private Timer _timerAPItransaction;
        private Timer _timerOrdering;

        public MainService()
        {
            this._timerAPItransaction = new Timer(30 * 1000);
            this._timerAPItransaction.Elapsed += new ElapsedEventHandler(APITransaction.APITransactionElapsed);
            this._timerOrdering = new Timer(30 * 1000);
            this._timerOrdering.Elapsed += new ElapsedEventHandler(ordering.OrderingElapsed);
            Start();
        }

        public void Start()
        {
            this._timerAPItransaction.Start();
            this._timerOrdering.Start();  
        }

        public void Stop()
        {
            this._timerAPItransaction.Stop();
            this._timerOrdering.Stop();
        }


    }
}
