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

        public string ma_khach_hang { get; set; }

        public string ten_khach_hang { get; set; }

        public string ten_cong_ty { get; set; }

        [Column(TypeName = "text")]
        public string dia_chi { get; set; }

        public string ma_so_thue { get; set; }

        public string dien_thoai { get; set; }

        public string fax { get; set; }

        public string email { get; set; }

        public string nguoi_dai_dien { get; set; }

        public string chuc_vu { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }
    }
}
