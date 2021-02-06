using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DKAC.Models.EntityModel
{
    [Table("KhoKem")]
    public partial class KhoKem
    {
        public int id { get; set; }
        public int? value { get; set; }
    }
}