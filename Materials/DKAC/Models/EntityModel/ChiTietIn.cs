namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietIn")]
    public partial class ChiTietIn
    {
        public int id { get; set; }
        
        public string ten_tay_in { get; set; }
        
        public string ten_loai { get; set; }

        public string kho_giay_dai { get; set; }
        public string kho_giay_rong { get; set; }
        public string kho_in_dai { get; set; }
        public string kho_in_rong { get; set; }

        public string chinh { get; set; }

        public string bu_thanh_pham { get; set; }

        public string bu_in { get; set; }
        public string bu_phat_hanh { get; set; }

        public string sl_tong { get; set; }

        public int? phuong_phap_in { get; set; }

        public string so_kg { get; set; }

        public string so_luot_in_quy_doi { get; set; }

        public string so_luot_in_thuc_te { get; set; }

        public int? don_hang_id { get; set; }
        public int? dinh_luong_giay_in { get; set; }
        public int? vat_tu_id { get; set; }
    }
}
