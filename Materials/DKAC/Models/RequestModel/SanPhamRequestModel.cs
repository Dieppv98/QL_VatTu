using System.Collections.Generic;
using System.Web.Mvc;
using DKAC.Models.EntityModel;

namespace DKAC.Models.RequestModel
{
    public class SanPhamRequestModel: BaseModel
    {
        [AllowHtml]
        public string Keywords { get; set; }
        public List<SanPham> data { get; set; }
    }
}