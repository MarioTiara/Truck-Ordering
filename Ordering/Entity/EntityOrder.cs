using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
   public  class EntityOrder
    {
        private string _Resi = null;
        private int _ChargeType = 1;
        private string _CustomerCode = ConfigurationManager.AppSettings.Get("CustomerCode");
        private string _Descryption = "";
        private short _CargoFlag = 1;
        private short _CombineFlag = 1;
        private short _MultiDropFlag = 0;
        private short _ReturnFlag = 0;
        private string _CancelReason = "";
        private int _bCancel = 0;
        private short _bShipment = 0;
        private short _bApprove = 0;
        private string _CustomerAdminName = ConfigurationManager.AppSettings.Get("CustomerAdminName");
       
        public string Number { get; set; }
        public string Resi { get { return _Resi; } set { this._Resi = value; } }
        public string OrderNo { get; set; }
        public string TripDate { get; set; }
        public int TripRoute { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime AllocateDate { get; set; }
        public string RealeaseDate { get; set; }
        public string CloseDate { get; set; }
        public string UnitCode { get; set; }
        public string DriverCode { get; set; }
        public string CustomerCode { get { return _CustomerCode; } set {this.CustomerCode=value ;} }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public DateTime OriginAppointedTime { get; set; }
        public DateTime DestinationAppointedTime { get; set; }
        public int ChargeType { get { return _ChargeType; } set {this._ChargeType=value;} }
        public string BillPayerCode { get { return _CustomerCode; } set {this._CustomerCode=value;}}
        public string PONo { get; set; }
        public string Descryption { get { return _Descryption; } set {this._Descryption=value;} }
        public short CargoFlag { get { return _CargoFlag; } set { this.CargoFlag=value;} }
        public short CombineFlag { get { return _CombineFlag; } set { this._CombineFlag=value;} }
        public short MultiDropFlag { get {return _MultiDropFlag;} set {this._MultiDropFlag=value;} }
        public short ReturnFlag { get { return _ReturnFlag; } set { this._ReturnFlag=value;} }
        public string CancelReason { get { return _CancelReason; } set { this._CancelReason=value;} }
        public int bCancel { get { return _bCancel; } set { this._bCancel=value;} }
        public short bShipment { get { return _bShipment; } set {this._bShipment=value;} }
        public short bApprove { get { return _bApprove; } set { this._bApprove=value;} }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get { return _CustomerAdminName; } set {this._CustomerAdminName=value ;} }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get { return _CustomerAdminName; } set { this._CustomerAdminName = value; } }
    }
}
