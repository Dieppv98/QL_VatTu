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

        public string so_kem { get; set; }

        public string so_bo { get; set; }

        public string tong { get; set; }

        public string trang { get; set; }
        
        public string kho_in_dai { get; set; }

        public string kho_in_rong { get; set; }
        
        public string kho_kem { get; set; }
        
        public string phuong_phap_in { get; set; }

        public int? don_hang_id { get; set; }
    }
}
