using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Models.InfoModel
{
    public class ChiTietThongKeInfo
    {
        public string LoaiVatTu { get; set; }

        public int? NhomVatTuID { get; set; }

        public string TenNhomVatTu { get; set; }


        public string TayIn { get; set; }

        public int? KTP_SoTrang { get; set; }

        public double? KGI_Rong { get; set; }

        public double? KGI_Cao { get; set; }

        public int? SLGiayChinh { get; set; }

        public double? KI_Rong { get; set; }

        public double? KI_Cao { get; set; }
        public int? SLInThucTe { get; set; }
        public int? MI_Tren { get; set; }
        public int? MI_Duoi { get; set; }
        public int? kho_kem_600 { get; set; }
        public int? kho_kem_800 { get; set; }
        public int? kho_kem_900 { get; set; }
        public int? kho_kem_608 { get; set; }
        public int? kho_kem_680 { get; set; }
        public int? kho_kem_607 { get; set; }
    }
}