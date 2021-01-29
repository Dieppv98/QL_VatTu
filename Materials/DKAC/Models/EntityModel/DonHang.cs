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

        [StringLength(10)]
        public string ma_khach_hang { get; set; }

        [StringLength(500)]
        public string ten_khach_hang { get; set; }

        [StringLength(100)]
        public string ma_san_pham { get; set; }

        [StringLength(500)]
        public string ten_san_pham { get; set; }

        [StringLength(50)]
        public string loai { get; set; }

        public string kho_ngang { get; set; }

        public string kho_doc { get; set; }

        [StringLength(200)]
        public string ten_che_ban { get; set; }

        [Column(TypeName = "text")]
        public string quy_cach_chung { get; set; }

        [StringLength(200)]
        public string ten_can_bo_ql { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(200)]
        public string ten_can_bo_kt { get; set; }

        [StringLength(20)]
        public string so_lenh_sx { get; set; }

        [StringLength(50)]
        public string phieu_dnsx_so { get; set; }

        [StringLength(200)]
        public string nv_kinh_doanh { get; set; }

        public DateTime? ngay_giao_hang { get; set; }

        [Column(TypeName = "text")]
        public string quy_cach_san_pham { get; set; }

        [StringLength(100)]
        public string cb_thoi_gian_giao { get; set; }

        [Column(TypeName = "text")]
        public string cb_ghi_chu { get; set; }

        [StringLength(100)]
        public string in_thoi_gian_giao { get; set; }

        [Column(TypeName = "text")]
        public string thanh_pham_chung { get; set; }

        [Column(TypeName = "text")]
        public string tp_ghi_chu { get; set; }

        [StringLength(100)]
        public string tp_thoi_han { get; set; }

        [StringLength(200)]
        public string nha_cc { get; set; }

        [StringLength(200)]
        public string kho_tp { get; set; }

        public string so_luong_tong { get; set; }
        public string chi_tiet_sl_tong { get; set; }

    }
}
