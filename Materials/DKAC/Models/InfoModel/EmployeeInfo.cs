using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Models.InfoModel
{
    public class EmployeeInfo
    {
        private const string RegexCode = @"^[a-zA-Z0-9]+$";

        public int id { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte? IsDeleted { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(200)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [RegularExpression(RegexCode, ErrorMessage = "Tài khoản không được chứa kí tự đặc biệt")]
        [Remote("CheckDuplicatedUserName", "Account", AdditionalFields = "id", HttpMethod = "POST", ErrorMessage = "Tên tài khoản đã tồn tại")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [StringLength(20)]
        public string PassWord { get; set; }
        
        public string Position { get; set; }

        public int? UserGroupId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }
        
        public int? Gender { get; set; }
        
        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Tel { get; set; }
        
        public string CMND { get; set; }
    }
}