using System.Collections.Generic;
using System.Web.Mvc;
using DKAC.Models.EntityModel;

namespace DKAC.Models.RequestModel
{
    public class KhachHangRequestModel: BaseModel
    {
        [AllowHtml]
        public string Keywords { get; set; }
        public List<KhachHang> data { get; set; }
    }
}