namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VatTu")]
    public partial class VatTu
    {
        public int id { get; set; }

        public int? don_hang_id { get; set; }

        public int? nhom_vat_tu_id { get; set; }

        [StringLength(1000)]
        public string ten { get; set; }

        public int? trang { get; set; }

        [StringLength(200)]
        public string loai_giay { get; set; }

        public int? dinh_luong_giay { get; set; }

        [StringLength(100)]
        public string kieu_in_1 { get; set; }

        [StringLength(100)]
        public string kieu_in_2 { get; set; }

        [StringLength(500)]
        public string ghi_chu { get; set; }
    }
}
