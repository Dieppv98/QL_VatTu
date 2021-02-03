using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DKAC.Models.EntityModel
{
    [Table("PPIn")]
    public partial class PPIn
    {
        public int id { get; set; }
        public string Name { get; set; }
    }
}