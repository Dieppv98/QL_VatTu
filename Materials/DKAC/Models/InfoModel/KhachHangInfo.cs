using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DKAC.Models.InfoModel
{
    public class KhachHangInfo
    {
        public int id { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập mã khách hàng")]
        [Remote("CheckDuplicatedMaKhachHang", "KhachHang", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Mã khách hàng đã tồn tại")]
        public string ma_khach_hang { get; set; }

        [StringLength(100)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [Remote("CheckDuplicatedTenKhachHang", "KhachHang", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Tên khách hàng đã tồn tại")]
        public string ten_khach_hang { get; set; }
        
        public string ten_cong_ty { get; set; }
        
        public string dia_chi { get; set; }
        
        public string ma_so_thue { get; set; }
        
        public string dien_thoai { get; set; }
        
        public string fax { get; set; }

        [StringLength(300)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng Email")]
        public string email { get; set; }
        
        public string nguoi_dai_dien { get; set; }
        
        public string chuc_vu { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }
    }
}