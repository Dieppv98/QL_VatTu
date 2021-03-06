namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        public int id { get; set; }

        public string ma_san_pham { get; set; }

        public string ten_san_pham { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }
    }
}
