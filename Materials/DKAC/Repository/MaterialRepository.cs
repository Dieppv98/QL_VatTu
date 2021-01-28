using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class MaterialRepository
    {
        MaterialsContext db = new MaterialsContext();

        public List<MaterialType> GetAllMaterial()
        {
            return db.MaterialTypes.AsNoTracking().ToList();
        }

        public DonHangInfo GetbyId(int id)
        {
            var query = (from d in db.DonHang.Where(x => x.id == id)
                         select new DonHangInfo()
                         {
                             id = d.id,
                             cb_ghi_chu = d.cb_ghi_chu,
                             cb_thoi_gian_giao = d.cb_thoi_gian_giao,
                             created_date = d.created_date,
                             email = d.email,
                             in_thoi_gian_giao = d.in_thoi_gian_giao,
                             kho_doc = d.kho_doc,
                             kho_ngang = d.kho_ngang,
                             kho_tp = d.kho_tp,
                             lan_dieu_chinh = d.lan_dieu_chinh,
                             last_updated = d.last_updated,
                             loai = d.loai,
                             ma_khach_hang = d.ma_khach_hang,
                             ma_san_pham = d.ma_san_pham,
                             ngay_giao_hang = d.ngay_giao_hang,
                             nha_cc = d.nha_cc,
                             nv_kinh_doanh = d.nv_kinh_doanh,
                             phieu_dnsx_so = d.phieu_dnsx_so,
                             phone_number = d.phone_number,
                             quy_cach_chung = d.quy_cach_chung,
                             quy_cach_san_pham = d.quy_cach_san_pham,
                             so_lenh_sx = d.so_lenh_sx,
                             status = d.status,
                             ten_can_bo_kt = d.ten_can_bo_kt,
                             ten_can_bo_ql = d.ten_can_bo_ql,
                             ten_che_ban = d.ten_che_ban,
                             ten_khach_hang = d.ten_khach_hang,
                             ten_san_pham = d.ten_san_pham,
                             thanh_pham_chung = d.thanh_pham_chung,
                             tp_ghi_chu = d.tp_ghi_chu,
                             tp_thoi_han = d.tp_thoi_han
                         }).FirstOrDefault() ?? new DonHangInfo();
            query.lstVatTus = db.VatTu.Where(x => x.don_hang_id == query.id).ToList() ?? new List<VatTu>();
            query.lstChiTietDuToans = db.ChiTietDuToan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietDuToan>();
            query.lstChiTietCheBans = db.ChiTietCheBan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietCheBan>();
            query.lstChiTietIns = db.ChiTietIn.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietIn>();
            return query;
        }

        public List<DonHang> Search(DonHangRequestModel request, int pageIndex, int recordPerPage, out int totalRecord)
        {
            pageIndex = pageIndex - 1;
            var query = db.DonHang.Where(t => (string.IsNullOrEmpty(request.Keywords) || t.so_lenh_sx.Contains(request.Keywords)));
            totalRecord = query.Count();
            return query.OrderByDescending(x => x.id).Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
        }

        public List<MaterialType> SearchMaterial(MaterialTypeRequestModel request, int pageIndex, int recordPerPage, out int totalRecord)
        {
            pageIndex = pageIndex - 1;
            var query = db.MaterialTypes.Where(t => (string.IsNullOrEmpty(request.Keywords) || t.MaterialTypeName.Contains(request.Keywords)));
            totalRecord = query.Count();
            return query.OrderByDescending(x => x.Id).Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
        }

        public int UpdateMaterialType(MaterialType model)
        {
            try
            {
                var material = new MaterialType()
                {
                    MaterialTypeName = model.MaterialTypeName,
                };
                db.MaterialTypes.Add(material);
                db.SaveChanges();
                return 1;
            }
            catch
            {
            }
            return 0;
        }

        public MaterialType CheckDupplicateMaterial(string MaterialTypeName, int? id)
        {
            var data = db.MaterialTypes.Where(x => x.Id != id && x.MaterialTypeName == MaterialTypeName).FirstOrDefault();
            return data;
        }

        public List<KhachHang> GetAllKhachHang(string key)
        {
            return db.KhachHang.AsNoTracking().Where(x => x.ma_khach_hang.ToLower().Contains(key.ToLower()) || x.ten_khach_hang.ToLower().Contains(key.ToLower())).ToList();
        }

        public List<SanPham> GetAllSanPham(string key)
        {
            return db.SanPham.AsNoTracking().Where(x => x.ma_san_pham.ToLower().Contains(key.ToLower()) || x.ten_san_pham.ToLower().Contains(key.ToLower())).ToList();
        }

        public List<VatTu> GetAllLoaiGiay(string key)
        {
            return db.VatTu.AsNoTracking().Where(x => x.loai_giay.ToLower().Contains(key.ToLower())).ToList();
        }
    }
}