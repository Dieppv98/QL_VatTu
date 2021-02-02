﻿using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Models.InfoModel
{
    public class LenhSanXuatInfo
    {
        public int id { get; set; }

        public DateTime? created_date { get; set; }

        public DateTime? last_updated { get; set; }

        public short? status { get; set; }

        public int? lan_dieu_chinh { get; set; }

        [StringLength(10)]
        public string ma_khach_hang { get; set; }

        [StringLength(500)]
        public string ten_khach_hang { get; set; }

        [StringLength(100)]
        public string ma_san_pham { get; set; }

        [StringLength(500)]
        public string ten_san_pham { get; set; }

        [StringLength(50)]
        public string loai { get; set; }

        public string kho_ngang { get; set; }

        public string kho_doc { get; set; }

        [StringLength(200)]
        public string ten_che_ban { get; set; }

        public string quy_cach_chung { get; set; }

        [StringLength(200)]
        public string ten_can_bo_ql { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(200)]
        public string ten_can_bo_kt { get; set; }

        [StringLength(20)]
        public string so_lenh_sx { get; set; }

        [StringLength(50)]
        public string phieu_dnsx_so { get; set; }

        [StringLength(200)]
        public string nv_kinh_doanh { get; set; }

        public DateTime? ngay_giao_hang { get; set; }

        public string quy_cach_san_pham { get; set; }

        [StringLength(100)]
        public string cb_thoi_gian_giao { get; set; }

        public string cb_ghi_chu { get; set; }

        [StringLength(100)]
        public string in_thoi_gian_giao { get; set; }

        public string in_ghi_chu { get; set; }

        public string thanh_pham_chung { get; set; }

        public string tp_ghi_chu { get; set; }

        [StringLength(100)]
        public string tp_thoi_han { get; set; }

        public string nha_cc { get; set; }

        [StringLength(200)]
        public string kho_tp { get; set; }
        public string so_luong_tong { get; set; }
        public string chi_tiet_sl_tong { get; set; }

        public List<MaterialType> lstMaterialType { get; set; }
        public List<VatTu> lstVatTus { get; set; }
        public List<ChiTietCheBan> lstChiTietCheBans { get; set; }
        public List<ChiTietIn> lstChiTietIns { get; set; }
        public List<TongSoLuongInfo> lst_sl_tong { get; set; }
        public List<SelectListItem> lstSo_LSX { get; set; }
        public List<NhomVatTu> lstnhomVatTus { get; set; }
    }
    public class NhomVatTu
    {
        public int? nhom_vat_tu_id { get; set; }
        public string ten_nhom_vat_tu { get; set; }
        public List<VatTu> lstVatTus{get;set;}
    }
}