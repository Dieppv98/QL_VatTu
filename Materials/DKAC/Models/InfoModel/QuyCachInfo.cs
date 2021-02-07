using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Models.InfoModel
{
    public class QuyCachInfo
    {
        public int? id { get; set; }
        public string ten_nhom_vat_tu { get; set; }
        public List<VatTu> lstvatTus { get; set; }
    }
}