﻿using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Models.InfoModel
{
    public class UserInfo
    {
        private const string RegexCode = @"^[a-zA-Z0-9]+$";

        public int id { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte? IsDeleted { get; set; }
       
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

        [StringLength(200)]
        public string Position { get; set; }

        public int? UserGroupId { get; set; }

        public List<SelectListItem> lstMaterial { get; set; }
        public List<MaterialType> lstMaterialType { get; set; }
    }
}