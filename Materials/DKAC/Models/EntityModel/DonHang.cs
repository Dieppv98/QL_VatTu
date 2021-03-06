namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
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

        public double? kho_ngang { get; set; }

        public double? kho_doc { get; set; }
        
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
        public string in_ghi_chu { get; set; }
        
        public string thanh_pham_chung { get; set; }
        
        public string tp_ghi_chu { get; set; }
        
        public string tp_thoi_han { get; set; }
        
        public string nha_cc { get; set; }
        
        public string kho_tp { get; set; }

        public int? so_luong_tong { get; set; }
        public string chi_tiet_sl_tong { get; set; }
        public int? tp_dai { get; set; }
        public int? tp_rong { get; set; }
        public int? tp_cao { get; set; }
        public int? tp_soluong { get; set; }
        public int? tp_socuon_thung { get; set; }
        public int? tp_sl_bangkeo { get; set; }
        public int? tp_vat_tu { get; set; }

    }
}
