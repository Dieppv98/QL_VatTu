using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DKAC.Models.RequestModel
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20)]
        public string OldPassWord { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20)]
        public string NewPassWord { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("NewPassWord", ErrorMessage = "Xác nhận mật khẩu không đúng, mời nhập lại")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(20)]
        public string ConfirmPassWord { get; set; }
    }
}