using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
    public class EntityUangJalan
    {
        public string OrderNo { get; set; }
        public decimal KM { get; set; }
        public string StatusTrip { get; set; }
        public int Leadtime { get; set; }
        public decimal UangBensin { get; set; }
        public decimal UangTol { get; set; }
        public decimal Ratio { get; set; }
        public decimal Spare { get; set; }
        public decimal Parkir { get; set; }
        public decimal Mel { get; set; }
        public decimal Komisi { get; set; }
        public decimal Bongkar { get; set; }
        public decimal Muat { get; set; }
        public decimal Timbangan { get; set; }
        public decimal Penyebrangan { get; set; }
        public decimal Mutidrop { get; set; }
        public decimal Makan { get; set; }
        public decimal Endapan { get; set; }
        public decimal DSFee { get; set; }
        public decimal Revenue { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal LiterBensin { get; set; }
        public string KodelKlaim { get; set; }
        public string NoUrut { get; set; }
        public string NoSPMKlaim { get; set; }
        public decimal NominalKlaim { get; set; }
        public string Keterangan { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
