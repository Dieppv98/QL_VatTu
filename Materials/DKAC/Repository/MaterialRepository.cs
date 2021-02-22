using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public List<PPIn> GetAllPPIn()
        {
            return db.PPIn.AsNoTracking().ToList();
        }

        public List<KhoKem> GetAllKhoKem()
        {
            return db.KhoKem.AsNoTracking().ToList();
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
                             in_ghi_chu = d.in_ghi_chu,
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
                             tp_thoi_han = d.tp_thoi_han,
                             chi_tiet_sl_tong = d.chi_tiet_sl_tong,
                             so_luong_tong = d.so_luong_tong,
                             tp_vat_tu = d.tp_vat_tu,
                             tp_soluong = d.tp_soluong,
                             tp_socuon_thung = d.tp_socuon_thung,
                             tp_rong = d.tp_rong,
                             tp_sl_bangkeo = d.tp_sl_bangkeo,
                             tp_dai = d.tp_dai,
                             tp_cao = d.tp_cao,

                         }).FirstOrDefault();
            if (query == null)
            {
                query = new DonHangInfo()
                {
                    id = 0,
                };
                return query;
            }
            query.lstVatTus = db.VatTu.Where(x => x.don_hang_id == query.id).ToList() ?? new List<VatTu>();
            query.lstChiTietDuToans = db.ChiTietDuToan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietDuToan>();
            query.lstChiTietCheBans = db.ChiTietCheBan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietCheBan>();
            query.lstChiTietIns = db.ChiTietIn.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietIn>();
            query.lstPPIn = db.PPIn.ToList();
            query.lstkhoKem = db.KhoKem.ToList();
            if (query.chi_tiet_sl_tong != null)
            {
                query.lst_sl_tong = JsonConvert.DeserializeObject<List<TongSoLuongInfo>>(query.chi_tiet_sl_tong ?? string.Empty)
                        .Select(o => new TongSoLuongInfo()
                        {
                            Values = o.Values
                        }).ToList() ?? new List<TongSoLuongInfo>();
            }
            else { query.lst_sl_tong = new List<TongSoLuongInfo>(); }
            return query;
        }

        public List<DonHang> Search(DonHangRequestModel request, int pageIndex, int recordPerPage, out int totalCount)
        {
            pageIndex = pageIndex - 1;

            var query = from d in db.DonHang
                        where (string.IsNullOrEmpty(request.Keywords) || d.so_lenh_sx.Contains(request.Keywords))
                        select (d);
            totalCount = query.Count();
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

        public int AddorUpdate(DonHangInfo model)
        {
            var dh = db.DonHang.Where(x => x.id == model.id).FirstOrDefault();
            if (dh == null) dh = new DonHang();

            dh.cb_ghi_chu = model.cb_ghi_chu;
            dh.cb_thoi_gian_giao = model.cb_thoi_gian_giao;
            dh.created_date = model.created_date;
            dh.email = model.email;
            dh.in_thoi_gian_giao = model.in_thoi_gian_giao;
            dh.in_ghi_chu = model.in_ghi_chu;
            dh.kho_doc = model.kho_doc;
            dh.kho_ngang = model.kho_ngang;
            dh.kho_tp = model.kho_tp;
            dh.nv_kinh_doanh = model.nv_kinh_doanh;
            dh.lan_dieu_chinh = model.lan_dieu_chinh;
            dh.last_updated = DateTime.Now;
            dh.loai = model.loai;
            dh.ma_khach_hang = model.ma_khach_hang;
            dh.ma_san_pham = model.ma_san_pham;
            dh.ngay_giao_hang = model.ngay_giao_hang;
            dh.nha_cc = model.nha_cc;
            dh.phieu_dnsx_so = model.phieu_dnsx_so;
            dh.phone_number = model.phone_number;
            dh.quy_cach_chung = model.quy_cach_chung;
            dh.quy_cach_san_pham = model.quy_cach_san_pham;
            dh.so_lenh_sx = model.so_lenh_sx;
            dh.so_luong_tong = model.so_luong_tong;
            dh.ten_can_bo_kt = model.ten_can_bo_kt;
            dh.ten_can_bo_ql = model.ten_can_bo_ql;
            dh.ten_che_ban = model.ten_che_ban;
            dh.ten_khach_hang = model.ten_khach_hang;
            dh.ten_san_pham = model.ten_san_pham;
            dh.thanh_pham_chung = model.thanh_pham_chung;
            dh.tp_ghi_chu = model.tp_ghi_chu;
            dh.tp_thoi_han = model.tp_thoi_han;
            dh.so_luong_tong = model.so_luong_tong;
            dh.tp_dai = model.tp_dai;
            dh.tp_rong = model.tp_rong;
            dh.tp_cao = model.tp_cao;
            dh.tp_soluong = model.tp_soluong;
            dh.tp_socuon_thung = model.tp_socuon_thung;
            dh.tp_sl_bangkeo = model.tp_sl_bangkeo;
            dh.tp_vat_tu = model.tp_vat_tu;
            if (model.lst_sl_tong != null)
            {
                dh.chi_tiet_sl_tong = JsonConvert.SerializeObject(model.lst_sl_tong);
            }
            if (model.id <= 0)
            {
                db.DonHang.Add(dh);
            }
            db.SaveChanges();
            /// xóa hết bảng con
            var lstVatTu = db.VatTu.Where(x => x.don_hang_id == dh.id).ToList();
            db.VatTu.RemoveRange(lstVatTu);
            var lstDuToan = db.ChiTietDuToan.Where(x => x.don_hang_id == dh.id).ToList();
            db.ChiTietDuToan.RemoveRange(lstDuToan);
            var lstCheban = db.ChiTietCheBan.Where(x => x.don_hang_id == dh.id).ToList();
            db.ChiTietCheBan.RemoveRange(lstCheban);
            var lstIn = db.ChiTietIn.Where(x => x.don_hang_id == dh.id).ToList();
            db.ChiTietIn.RemoveRange(lstIn);
            ///add lại bảng con
            if (model.lstVatTus != null)
            {
                foreach (var item in model.lstVatTus)
                {
                    item.don_hang_id = dh.id;
                    if (item.loai_giay != null)
                    {
                        item.loai_giay = item.loai_giay.Trim();
                    }
                }
                db.VatTu.AddRange(model.lstVatTus);
            }
            if (model.lstChiTietDuToans != null)
            {
                foreach (var item in model.lstChiTietDuToans)
                {
                    item.don_hang_id = dh.id;
                }
                db.ChiTietDuToan.AddRange(model.lstChiTietDuToans);
            }
            if (model.lstChiTietCheBans != null)
            {
                foreach (var item in model.lstChiTietCheBans)
                {
                    item.don_hang_id = dh.id;
                }
                db.ChiTietCheBan.AddRange(model.lstChiTietCheBans);
            }
            if (model.lstChiTietIns != null)
            {
                foreach (var item in model.lstChiTietIns)
                {
                    item.don_hang_id = dh.id;
                }
                db.ChiTietIn.AddRange(model.lstChiTietIns);
            }

            db.SaveChanges();
            AddorUpdateKhachHang(dh.ma_khach_hang, dh.ten_khach_hang);
            AddorUpdateSanPham(dh.ma_san_pham, dh.ten_san_pham);
            return dh.id;
        }

        private void AddorUpdateKhachHang(string maKH, string tenKH)
        {
            var kh = db.KhachHang.Where(x => x.ma_khach_hang == maKH).FirstOrDefault();
            if (kh == null)
            {
                kh = new KhachHang();
                kh.ma_khach_hang = maKH;
                kh.ten_khach_hang = tenKH;
                db.KhachHang.Add(kh);
            }
            else
            {
                kh.ma_khach_hang = maKH;
                kh.ten_khach_hang = tenKH;
            }
            db.SaveChanges();
        }

        private void AddorUpdateSanPham(string maSP, string tenSP)
        {
            var sp = db.SanPham.Where(x => x.ma_san_pham == maSP).FirstOrDefault();
            if (sp == null)
            {
                sp = new SanPham();
                sp.ma_san_pham = maSP;
                sp.ten_san_pham = tenSP;
                db.SanPham.Add(sp);
            }
            else
            {
                sp.ma_san_pham = maSP;
                sp.ten_san_pham = tenSP;
            }
            db.SaveChanges();
        }

        public int Delete(int id)
        {
            try
            {
                var dh = db.DonHang.Where(x => x.id == id).FirstOrDefault();
                if (dh == null) return 0;

                var lstVatTu = db.VatTu.Where(x => x.don_hang_id == dh.id).ToList();
                db.VatTu.RemoveRange(lstVatTu);
                var lstDuToan = db.ChiTietDuToan.Where(x => x.don_hang_id == dh.id).ToList();
                db.ChiTietDuToan.RemoveRange(lstDuToan);
                var lstCheban = db.ChiTietCheBan.Where(x => x.don_hang_id == dh.id).ToList();
                db.ChiTietCheBan.RemoveRange(lstCheban);
                var lstIn = db.ChiTietIn.Where(x => x.don_hang_id == dh.id).ToList();
                db.ChiTietIn.RemoveRange(lstIn);

                db.DonHang.Remove(dh);
                db.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public TongQuanDonHangInfo GetTongQuanbyId(int id)
        {
            var query = (from d in db.DonHang.Where(x => x.id == id)
                         select new TongQuanDonHangInfo()
                         {
                             id = d.id,
                             cb_ghi_chu = d.cb_ghi_chu,
                             cb_thoi_gian_giao = d.cb_thoi_gian_giao,
                             created_date = d.created_date,
                             email = d.email,
                             in_thoi_gian_giao = d.in_thoi_gian_giao,
                             in_ghi_chu = d.in_ghi_chu,
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
                             tp_thoi_han = d.tp_thoi_han,
                             chi_tiet_sl_tong = d.chi_tiet_sl_tong,
                             so_luong_tong = d.so_luong_tong,
                         }).FirstOrDefault() ?? new TongQuanDonHangInfo();
            query.lstVatTus = db.VatTu.Where(x => x.don_hang_id == query.id).ToList() ?? new List<VatTu>();
            return query;
        }
        public LenhSanXuatInfo GetLenhSanXuatbyId(int id)
        {
            var query = (from d in db.DonHang.Where(x => x.id == id)
                         select new LenhSanXuatInfo()
                         {
                             id = d.id,
                             cb_ghi_chu = d.cb_ghi_chu,
                             cb_thoi_gian_giao = d.cb_thoi_gian_giao,
                             created_date = d.created_date,
                             email = d.email,
                             in_thoi_gian_giao = d.in_thoi_gian_giao,
                             in_ghi_chu = d.in_ghi_chu,
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
                             tp_thoi_han = d.tp_thoi_han,
                             chi_tiet_sl_tong = d.chi_tiet_sl_tong,
                             so_luong_tong = d.so_luong_tong,
                         }).FirstOrDefault() ?? new LenhSanXuatInfo();
            query.lstVatTus = db.VatTu.Where(x => x.don_hang_id == query.id).ToList() ?? new List<VatTu>();
            var queryNhomVT = query.lstVatTus.Select(x => x.nhom_vat_tu_id).Distinct().OrderBy(x => x.Value).ToArray();
            query.lstnhomVatTus = new List<NhomVatTu>();
            foreach (var data in queryNhomVT)
            {
                var quycach = query.lstVatTus.Where(x => x.nhom_vat_tu_id == data);
                query.lstnhomVatTus.Add(new NhomVatTu
                {
                    nhom_vat_tu_id = data,
                    ten_nhom_vat_tu = quycach.FirstOrDefault().ten_nhom_vat_tu,
                    lstVatTus = quycach.OrderBy(x => x.id).ToList(),
                });
            }
            query.lstChiTietCheBans = db.ChiTietCheBan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietCheBan>();
            query.lstChiTietIns = db.ChiTietIn.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietIn>();
            if (query.chi_tiet_sl_tong != null)
            {
                query.lst_sl_tong = JsonConvert.DeserializeObject<List<TongSoLuongInfo>>(query.chi_tiet_sl_tong ?? string.Empty)
                        .Select(o => new TongSoLuongInfo()
                        {
                            Values = o.Values
                        }).ToList() ?? new List<TongSoLuongInfo>();
            }
            else { query.lst_sl_tong = new List<TongSoLuongInfo>(); }
            return query;
        }
        public DuToanVatTuInfo GetKHVTbyId(int id)
        {
            var query = (from d in db.DonHang.Where(x => x.id == id)
                         select new DuToanVatTuInfo()
                         {
                             id = d.id,
                             cb_ghi_chu = d.cb_ghi_chu,
                             cb_thoi_gian_giao = d.cb_thoi_gian_giao,
                             created_date = d.created_date,
                             email = d.email,
                             in_thoi_gian_giao = d.in_thoi_gian_giao,
                             in_ghi_chu = d.in_ghi_chu,
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
                             tp_thoi_han = d.tp_thoi_han,
                             chi_tiet_sl_tong = d.chi_tiet_sl_tong,
                             so_luong_tong = d.so_luong_tong,
                         }).FirstOrDefault() ?? new DuToanVatTuInfo();
            query.lstChiTietDuToans = db.ChiTietDuToan.Where(x => x.don_hang_id == query.id).ToList() ?? new List<ChiTietDuToan>();
            return query;
        }
        public DonHang GetBySoLenhSX(string soLenhSX)
        {
            var data = db.DonHang.Where(x => x.so_lenh_sx.Trim() == soLenhSX).FirstOrDefault();
            return data;
        }
    }
}