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

        [StringLength(200)]
        public string ten_tay_in { get; set; }

        [StringLength(100)]
        public string ten_loai { get; set; }
        
        public string kho_giay { get; set; }
        public string kho_in_dai { get; set; }
        public string kho_in_rong { get; set; }

        public double? chinh { get; set; }

        public double? bu_thanh_pham { get; set; }

        public double? bu_in { get; set; }

        public double? sl_tong { get; set; }
        
        public string phuong_phap_in { get; set; }

        public double? so_kg { get; set; }

        public double? so_luot_in_quy_doi { get; set; }

        public double? so_luot_in_thuc_te { get; set; }

        public int? don_hang_id { get; set; }
    }
}
