using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;


namespace ReplenishmentService.Ordering.Entity
{
    public class EntityAllocatedOrder
    {
        public int transactionId { get; set; }
        public int groupNo { get; set; }
        public string itemCode { get; set; }
        public Int64 ticketId { get; set; }
        public string ticketNumber { get; set; }
        public string origincode { get; set; }
        public GeoCoordinate loadCoordinate { get; set; }
        public string destinationCode { get; set; }
        public GeoCoordinate undloadCoordinte { get; set; }
        public int quantity { get; set; }
        public string transferDocNo { get; set; }
        public string noUnit { get; set; }
        public string driverCode { get; set; }
        public string TipeUnit { get; set; }
        public GeoCoordinate poolCordinate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime originAppointedTime { get; set; }
        public DateTime destionatinAppointedTime { get; set; }

    }
}
