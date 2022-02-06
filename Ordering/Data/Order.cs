using ReplenishmentService.Ordering.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplenishmentService.Ordering.DbContext;
using System.Configuration;

namespace ReplenishmentService.Ordering.Data
{
    class Order
    {
       // private string CustomerCode = "PRO0000106";
        private ContextLms lmscontext = new ContextLms();
        private ContextIpeReplenishment ipecontext = new ContextIpeReplenishment();
        private EntityAllocatedOrder allocatedorder = new EntityAllocatedOrder();
        private EntityOrderFullversion lmsorder = new EntityOrderFullversion();
        DateTime timeNow = DateTime.Now;        
        public Order(Int64 ticketId)
        {
            ipecontext.UpdateticketStatus(2, ticketId);
            lmscontext.upatebValidTikect(1, ticketId);
        }

        public EntityOrderFullversion makeOrder(EntityAllocatedOrder allocatedorder)
        {
            this.allocatedorder = allocatedorder;
            generateOrder();
            return lmsorder;
        }
        private void generateOrder()
        {             
            lmsorder.Number = allocatedorder.ticketNumber;
            lmsorder.Resi = generateResi();
            lmsorder.OrderNo = generateOrderNo();
            lmsorder.TripDate = generateTripDate();
            lmsorder.TripRoute = 0;
            lmsorder.OrderDate = timeNow;
            lmsorder.AllocateDate = allocatedorder.UpdateDate;
            lmsorder.UnitCode = allocatedorder.noUnit;
            lmsorder.DriverCode = allocatedorder.driverCode;
            //lmsorder.CustomerCode = this.CustomerCode;
            lmsorder.OriginCode = allocatedorder.origincode;
            lmsorder.DestinationCode = allocatedorder.destinationCode;
            lmsorder.OriginAppointedTime = estimateOriginApointedTime();
            lmsorder.DestinationAppointedTime = estimateDestinAppointedTime();
            //lmsorder.ChargeType = 1;
            //lmsorder.BillPayerCode = this.CustomerCode;
            lmsorder.PONo = allocatedorder.transferDocNo;
            //lmsorder.Descryption = "";
            //lmsorder.CargoFlag = 1;
            //lmsorder.CombineFlag = 1;
            //lmsorder.MultiDropFlag = 0;
            //lmsorder.ReturnFlag = 0;
            //lmsorder.CancelReason = "";
            //lmsorder.bCancel = 0;
            //lmsorder.bShipment = 0;
            //lmsorder.bApprove = 0;
            lmsorder.CreateDate = timeNow;
            //lmsorder.CreateBy = "system";
            lmsorder.UpdateDate = timeNow;
            //lmsorder.UpdateBy = "system";
            lmsorder.item = generateOrderItem();
            lmsorder.uangJalan = generateUangJalan();
            lmsorder.imageOrder = generateOrderImage();
        }
        private EntityOrderItem generateOrderItem()
        {
            EntityOrderItem item = new EntityOrderItem
            {
                OrderNo = lmsorder.OrderNo,
                PONo = allocatedorder.transferDocNo,
                ItemCode = ConfigurationManager.AppSettings.Get("ItemCode"),
                ItemQty = allocatedorder.quantity / 1000,
                Weight = allocatedorder.quantity / 1000,
                CreateDate = timeNow,
                UpdateDate = timeNow
            };
            return item;
        }
        private EntityUangJalan generateUangJalan()
        {
            string unitType = ConfigurationManager.AppSettings.Get("unitType");
            EntityUangJalan uangjalan= lmscontext.getUangJalan(lmsorder.CustomerCode, 
                                            lmsorder.OriginCode, 
                                            lmsorder.DestinationCode, 
                                            unitType,                                              
                                            lmsorder.DriverCode, 
                                            lmsorder.ChargeType, 
                                            lmsorder.item.Weight, 
                                            lmsorder.item.ItemCode);
            uangjalan.OrderNo = lmsorder.OrderNo;
            uangjalan.CreateDate = lmsorder.CreateDate;
            uangjalan.CreateBy = lmsorder.CreateBy;
            uangjalan.UpdateDate = lmsorder.UpdateDate;
            uangjalan.UpdateBy = lmsorder.UpdateBy;
            return uangjalan;
        }
        private EntityMsImage generateOrderImage()
        {
            return new EntityMsImage()
            {
                KodeDokumen="order",
                NoReferensi=lmsorder.Number,
                NoReferensi2=lmsorder.Resi,
                NoReferensi3=lmsorder.OrderNo,
                DibuatOleh=lmsorder.CreateBy,
                WaktuDibuat=lmsorder.CreateDate,
                DiubahOleh=lmsorder.UpdateBy,
                WaktuDiubah=lmsorder.UpdateDate
            };
        }
        private string generateResi()
        {
            int shortCustomerCode = Int32.Parse(lmsorder.CustomerCode.Substring(3, 7));
            string datekey = allocatedorder.ticketNumber.Substring(4, 8);
            string uniqkey = allocatedorder.ticketNumber.Substring(12, 5);         
            return string.Concat("RSIL", datekey, shortCustomerCode.ToString(), uniqkey);
        }
        private string generateOrderNo()
        {           
            if (string.IsNullOrEmpty(lmsorder.OrderNo))
            {
                string resiKey = lmsorder.Resi.Substring(4, 11);
                int firstNumber = 1;
                return resiKey + firstNumber.ToString("00000");              
            }
            Int64 newOrderNumber = Int64.Parse(lmsorder.OrderNo) + 1;
            return newOrderNumber.ToString();
        }
        private string generateTripDate()
        {
            string datekey = allocatedorder.ticketNumber.Substring(4, 8);
            return string.Join("-", datekey.Substring(0, 4), datekey.Substring(4, 2), datekey.Substring(6, 2));
        }
        private DateTime estimateOriginApointedTime()
        {
            DateTime ReadyTime = lmsorder.AllocateDate;
            Mapbox mapbox = new Mapbox(allocatedorder.poolCordinate, allocatedorder.loadCoordinate);
            double Mileagehour = mapbox.getMileage();
            return ReadyTime.AddHours(Mileagehour);
        }
        private DateTime estimateDestinAppointedTime()
        {
            DateTime ReadyTime = lmsorder.OriginAppointedTime;
            Mapbox mapbox = new Mapbox(allocatedorder.loadCoordinate, allocatedorder.undloadCoordinte);
            double Mileagehour = mapbox.getMileage();
            return ReadyTime.AddHours(Mileagehour);
        }
    }
}
