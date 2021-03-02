using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DKAC.Models.EntityModel
{
    [Table("ChiTietThongKe")]
    public class ChiTietThongKe
    {
        public int ID { get; set; }

        public int? MaDonHang { get; set; }

        public string LoaiVatTu { get; set; }

        public int? NhomVatTuID { get; set; }

        public string TenNhomVatTu { get; set; }

        public int? VatTuID { get; set; }

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
        public int? KhoKem { get; set; }
        public int? TongKhoKem { get; set; }
    }
}