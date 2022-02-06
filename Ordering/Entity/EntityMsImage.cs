using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Entity
{
    public class EntityMsImage
    {
        public string KodeCabang { get; set; }
        public string KodeDokumen { get; set; }
        public string NoReferensi { get; set; }
        public string NoReferensi2 { get; set; }
        public string NoReferensi3 { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string DibuatOleh { get; set; }
        public DateTime WaktuDibuat { get; set; }
        public string DiubahOleh { get; set; }
        public DateTime WaktuDiubah { get; set; }
    }
}
