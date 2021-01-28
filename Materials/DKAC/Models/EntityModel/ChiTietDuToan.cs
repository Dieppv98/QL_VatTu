namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDuToan")]
    public partial class ChiTietDuToan
    {
        public int id { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }

        [StringLength(500)]
        public string ten { get; set; }

        public float? dinh_luong { get; set; }

        public int? dinh_luong_thuc_te { get; set; }

        public float? so_luong { get; set; }

        [StringLength(20)]
        public string ma_vat_tu { get; set; }

        public float? trang { get; set; }

        public float? dinh_muc { get; set; }

        public float? sl_chinh { get; set; }

        public float? sl_bu_in { get; set; }

        public float? sl_bu_tp { get; set; }

        public float? sl_tong { get; set; }

        [StringLength(50)]
        public string don_vi_tinh { get; set; }

        [StringLength(500)]
        public string muc_dich_sd { get; set; }

        [StringLength(50)]
        public string cc_vat_tu { get; set; }
        public float? kho_giay_dai { get; set; }
        public float? kho_giay_rong { get; set; }
        public float? quy_ky { get; set; } 

        public DateTime? thoi_han_cc { get; set; }

        public int don_hang_id { get; set; }
        public string loai_giay { get; set; }
    }
}
