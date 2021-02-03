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

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Position { get; set; }

        public int? UserGroupId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public int? Gender { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public string CMND { get; set; }
    }
}
