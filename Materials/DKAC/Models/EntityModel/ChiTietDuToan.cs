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

        public string ten { get; set; }

        public double? dinh_luong { get; set; }

        public double? dinh_luong_thuc_te { get; set; }

        public int? so_luong { get; set; }

        public string ma_vat_tu { get; set; }

        public int? trang { get; set; }

        public double? dinh_muc { get; set; }

        public double? sl_chinh { get; set; }

        public int? sl_bu_in { get; set; }

        public int? sl_bu_tp { get; set; }
        public int? bu_phat_hanh { get; set; }

        public double? sl_tong { get; set; }

        public string don_vi_tinh { get; set; }

        public string muc_dich_sd { get; set; }

        public string cc_vat_tu { get; set; }
        public double? kho_giay_dai { get; set; }
        public double? kho_giay_rong { get; set; }
        public double? quy_ky { get; set; }

        public DateTime? thoi_han_cc { get; set; }

        public int? don_hang_id { get; set; }
        public string loai_giay { get; set; }
        public int? vat_tu_id { get; set; }
    }
}
