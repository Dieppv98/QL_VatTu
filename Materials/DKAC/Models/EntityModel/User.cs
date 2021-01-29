namespace DKAC.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte? IsDeleted { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        [StringLength(200)]
        public string Position { get; set; }

        public int? UserGroupId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int? Gender { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string CMND { get; set; }
    }
}
