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

        [StringLength(500)]
        public string ten_tay_in { get; set; }

        public int? so_kem { get; set; }

        public int? so_bo { get; set; }

        public int? tong { get; set; }

        public int? trang { get; set; }

        [StringLength(20)]
        public string kho_in_dai { get; set; }

        public string kho_in_rong { get; set; }

        [StringLength(20)]
        public string kho_kem { get; set; }

        [StringLength(20)]
        public string phuong_phap_in { get; set; }

        public int? don_hang_id { get; set; }
    }
}
