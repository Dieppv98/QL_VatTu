using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DKAC.Models.EntityModel
{
    [Table("MaterialType")]
    public partial class MaterialType
    {
        public int Id { get; set; }
        public string MaterialTypeName { get; set; }
    }
}