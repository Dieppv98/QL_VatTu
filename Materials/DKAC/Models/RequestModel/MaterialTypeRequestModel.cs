using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Models.RequestModel
{
    public class MaterialTypeRequestModel : BaseModel
    {
        [AllowHtml]
        public string Keywords { get; set; }
        public int Id { get; set; }
        public string MaterialTypeName { get; set; }
    }
}