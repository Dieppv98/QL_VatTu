using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DKAC.Models.InfoModel
{
    public class SanPhamInfo
    {
        public int id { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập mã sản phẩm")]
        [Remote("CheckDuplicatedMaSanPham", "SanPham", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Mã sản phẩm đã tồn tại")]
        public string ma_san_pham { get; set; }

        [StringLength(500)]
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Remote("CheckDuplicatedTenSanPham", "SanPham", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Tên sản phẩm đã tồn tại")]
        public string ten_san_pham { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }
    }
}