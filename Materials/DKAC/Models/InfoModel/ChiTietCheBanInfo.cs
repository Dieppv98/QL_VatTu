using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Models.InfoModel
{
    public class ChiTietCheBanInfo
    {
        public int id { get; set; }

        public string ten_tay_in { get; set; }

        public int? so_kem { get; set; }

        public int? so_bo { get; set; }

        public int? tong { get; set; }

        public int? trang { get; set; }

        public double? kho_in_dai { get; set; } 

        public double? kho_in_rong { get; set; }

        public int? kho_kem { get; set; }
        public int? kho_kem_name { get; set; }

        public int? phuong_phap_in { get; set; }
        public string phuong_phap_in_name { get; set; }

        public int? don_hang_id { get; set; }
        public int? vat_tu_id { get; set; }
    }
}