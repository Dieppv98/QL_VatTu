namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        public int id { get; set; }

        [StringLength(10)]
        public string ma_khach_hang { get; set; }

        [StringLength(500)]
        public string ten_cong_ty { get; set; }

        [Column(TypeName = "text")]
        public string dia_chi { get; set; }

        [StringLength(20)]
        public string ma_so_thue { get; set; }

        [StringLength(20)]
        public string dien_thoai { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(300)]
        public string email { get; set; }

        [StringLength(500)]
        public string nguoi_dai_dien { get; set; }

        [StringLength(200)]
        public string chuc_vu { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }
    }
}
