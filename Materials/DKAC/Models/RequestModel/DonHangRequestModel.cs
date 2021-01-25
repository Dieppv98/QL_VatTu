using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Models.RequestModel
{
    public class DonHangRequestModel : BaseModel
    {
        [AllowHtml]
        public string Keywords { get; set; }
        public override object RouteValues => new
        {
            this.Keywords,
        };
        public int id { get; set; }
        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }

        public short? status { get; set; }

        public int? lan_dieu_chinh { get; set; }

        public string ma_khach_hang { get; set; }

        public string ten_khach_hang { get; set; }

        public string ma_san_pham { get; set; }

        public string ten_san_pham { get; set; }

        public string loai { get; set; }

        public decimal? kho_ngang { get; set; }

        public decimal? kho_doc { get; set; }

        public string ten_che_ban { get; set; }

        public string quy_cach_chung { get; set; }

        public string ten_can_bo_ql { get; set; }

        public string phone_number { get; set; }

        public string email { get; set; }

        public string ten_can_bo_kt { get; set; }

        public string so_lenh_sx { get; set; }

        public string phieu_dnsx_so { get; set; }

        public string nv_kinh_doanh { get; set; }

        public DateTime? ngay_giao_hang { get; set; }

        public string quy_cach_san_pham { get; set; }

        public string cb_thoi_gian_giao { get; set; }

        public string cb_ghi_chu { get; set; }

        public string in_thoi_gian_giao { get; set; }

        public string thanh_pham_chung { get; set; }

        public string tp_ghi_chu { get; set; }

        public string tp_thoi_han { get; set; }

        public string nha_cc { get; set; }

        public string kho_tp { get; set; }

        public List<DonHang> lstDonHang { get; set; }
    }
}