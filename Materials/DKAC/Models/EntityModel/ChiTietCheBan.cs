namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCheBan")]
    public partial class ChiTietCheBan
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
        
        public int? phuong_phap_in { get; set; }

        public int? don_hang_id { get; set; }
        public int? vat_tu_id { get; set; }
    }
}
