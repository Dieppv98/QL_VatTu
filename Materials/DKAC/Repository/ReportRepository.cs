using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class ReportRepository : IReportRepository
    {
        MaterialsContext db = new MaterialsContext();

        public PermissionActionInfo GetPermissionActionInfo()
        {
            var perInfo = new PermissionActionInfo();
            perInfo.ListPage = db.Pages.ToList();
            if (perInfo.ListPage == null) perInfo.ListPage = new List<Page>();
            perInfo.ListRole = db.Roles.ToList();
            if (perInfo.ListRole == null) perInfo.ListRole = new List<Role>();
            perInfo.ListAction = db.Actions.ToList();
            if (perInfo.ListAction == null) perInfo.ListAction = new List<Models.EntityModel.Action>();
            perInfo.ListModul = db.Moduls.ToList();
            if (perInfo.ListModul == null) perInfo.ListModul = new List<Modul>();
            perInfo.ListPermissionActions = db.PermissionActions.ToList();
            if (perInfo.ListPermissionActions == null) perInfo.ListPermissionActions = new List<PermissionAction>();
            return perInfo;
        }

        public int SavePermission(PermissionAction model)
        {
            var perAction = db.PermissionActions.FirstOrDefault(x => x.PageId == model.PageId && x.RoleId == model.RoleId);
            if (perAction != null)
            {
                perAction.ActionKey = model.ActionKey;
            }
            else
            {
                db.PermissionActions.Add(model);
            }
            return db.SaveChanges();
        }

        public List<Role> GetAllRole()
        {
            var Roles = db.Roles.ToList();
            if (Roles == null) { Roles = new List<Role>(); }
            return Roles;
        }

        public List<User> GetAllUser()
        {
            var Users = db.Users.ToList();
            if (Users == null) { Users = new List<User>(); }
            return Users;
        }

        public List<UserRole> GetAllUserRole()
        {
            var ur = db.UserRoles.ToList();
            if (ur == null) ur = new List<UserRole>();
            return ur;
        }

        public int SaveUserRole(UserRoleInfo model)
        {
            var userRole = db.UserRoles.FirstOrDefault(x => x.UserId == model.UserId && x.RoleId == model.RoleId);
            var role = db.Roles.FirstOrDefault(x => x.Id == model.RoleId);
            var uR = new UserRole();
            uR.RoleId = model.RoleId;
            uR.UserId = model.UserId;
            uR.Description = role?.Description;
            if (model.Check == true)
            {
                db.UserRoles.Add(uR);
            }
            if (model.Check == false && userRole != null)
            {
                db.UserRoles.Remove(userRole);
            }
            return db.SaveChanges();
        }

        public int SaveRole(Role model)
        {
            var uR = new Role();
            uR.Id = model.Id;
            uR.RoleName = model.RoleName;
            uR.Description = model.Description;
            db.Roles.Add(uR);
            return db.SaveChanges();
        }

        public List<DonHang> GetAllSo_LSX()
        {
            return db.DonHang.ToList();
        }

        public List<DonHang> SearchSoLSX(DonHangRequestModel request, int pageIndex, int recordPerPage, out int totalRecord)
        {
            pageIndex = pageIndex - 1;
            var query = db.DonHang.Where(t => (string.IsNullOrEmpty(request.Keywords) || t.so_lenh_sx.Contains(request.Keywords.Trim())));
            totalRecord = query.Count();
            return query.OrderByDescending(x => x.id).Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
        }

        public List<DonHangInfo> ThongKe(string lsx, DateTime? fromDate, DateTime? toDate)
        {
            var query = (from d in db.DonHang
                         where (string.IsNullOrEmpty(lsx) || d.so_lenh_sx.ToLower().Contains(lsx.ToLower().Trim()))
                         && ((!fromDate.HasValue && !toDate.HasValue)
                         || (!fromDate.HasValue && d.ngay_giao_hang.Value <= toDate.Value)
                         || (d.ngay_giao_hang.Value >= fromDate.Value && !toDate.HasValue)
                         || (d.ngay_giao_hang.Value >= fromDate.Value && d.ngay_giao_hang.Value <= toDate.Value))
                         select new DonHangInfo()
                         {
                             id = d.id,
                             so_lenh_sx = d.so_lenh_sx,
                             ten_san_pham = d.ten_san_pham,
                             ma_san_pham = d.ma_san_pham,
                             ten_khach_hang = d.ten_khach_hang,
                             ma_khach_hang = d.ma_khach_hang,
                             so_luong_tong = d.so_luong_tong,
                             phieu_dnsx_so = d.phieu_dnsx_so,
                             ngay_giao_hang = d.ngay_giao_hang,

                             lstthongKes = db.ChiTietThongKes.Where(x => x.MaDonHang == d.id).ToList(),
                             //lstVatTus = db.VatTu.Where(x => x.don_hang_id == d.id).ToList(),
                             //lstChiTietDuToans = db.ChiTietDuToan.Where(x => x.don_hang_id == d.id).ToList(),
                             //lstChiTietCheBans = db.ChiTietCheBan.Where(x => x.don_hang_id == d.id).ToList(),
                             //lstChiTietIns = db.ChiTietIn.Where(x => x.don_hang_id == d.id).ToList(),
                         }).ToList();
            query = query.Where(x => x.lstthongKes.Count() > 0).ToList();
            //ListChiTietNhomVatTu
            var ListChiTietThongKes = new List<ChiTietThongKeInfo>();
            foreach (var chiTietInfo in query)
            {
                ListChiTietThongKes = new List<ChiTietThongKeInfo>();
                // Bìa
                var lstNhomBias = chiTietInfo.lstthongKes.Where(x => x.LoaiVatTu.Trim().ToLower() == "bìa").ToList();
                var lstQuyCachBias = lstNhomBias.GroupBy(x => new { x.KGI_Cao, x.KGI_Rong, x.KI_Cao, x.KI_Rong }).Select(y => new
                {
                    KGI_Cao = y.Key.KGI_Cao,
                    KGI_Rong = y.Key.KGI_Rong,
                    KI_Cao = y.Key.KI_Cao,
                    KI_Rong = y.Key.KI_Rong,

                    So_Trang = y.Sum(i => i.KTP_SoTrang),
                    SL_Giay_Chinh = y.Sum(i => i.SLGiayChinh),
                    SLInThucTe = y.Sum(i => i.SLInThucTe),
                    // mau in
                    lstMauIn = lstNhomBias.Where(x => x.KGI_Cao == y.Key.KGI_Cao &&
                                                    x.KGI_Rong == y.Key.KGI_Rong &&
                                                    x.KI_Cao == y.Key.KI_Cao &&
                                                    x.KI_Rong == y.Key.KI_Rong)
                                          .GroupBy(z => new { z.KGI_Cao, z.KGI_Rong, z.KI_Cao, z.KI_Rong, z.MI_Tren, z.MI_Duoi })
                                          .Select(t => new
                                          {
                                              MI_Tren = t.Key.MI_Tren,
                                              MI_Duoi = t.Key.MI_Duoi,
                                              Trang_Mau_In = t.Sum(u => u.KTP_SoTrang),
                                              Tay_In = t.OrderBy(i => i.VatTuID).Select(x => x.TayIn).ToList(),
                                              lstKemIn = lstNhomBias.Where(v => v.KGI_Cao == y.Key.KGI_Cao &&
                                                                                v.KGI_Rong == y.Key.KGI_Rong &&
                                                                                v.KI_Cao == y.Key.KI_Cao &&
                                                                                v.KI_Rong == y.Key.KI_Rong &&
                                                                                v.MI_Tren == t.Key.MI_Tren &&
                                                                                v.MI_Duoi == t.Key.MI_Duoi
                                                                                )
                                                                    .GroupBy(c => new { c.KGI_Cao, c.KGI_Rong, c.KI_Cao, c.KI_Rong, c.MI_Tren, c.MI_Duoi, c.KhoKem }).Select(i => new
                                                                    {
                                                                        KhoKem = db.KhoKem.Where(x => x.id == i.Key.KhoKem).Select(m => m.value).FirstOrDefault(),
                                                                        TongKhoKem = i.Sum(x => x.TongKhoKem)
                                                                    })
                                          })
                });

                foreach (var item1 in lstQuyCachBias)
                {
                    foreach (var item2 in item1.lstMauIn)
                    {
                        var kho_kem_600 = item2.lstKemIn.Where(x => x.KhoKem == 600).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_800 = item2.lstKemIn.Where(x => x.KhoKem == 800).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_900 = item2.lstKemIn.Where(x => x.KhoKem == 900).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_608 = item2.lstKemIn.Where(x => x.KhoKem == 608).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_680 = item2.lstKemIn.Where(x => x.KhoKem == 680).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_607 = item2.lstKemIn.Where(x => x.KhoKem == 607).Select(x => x.TongKhoKem).FirstOrDefault();
                        var tay_in = String.Join("+", item2.Tay_In.Distinct().ToList());

                        var newThongKeInfo = new ChiTietThongKeInfo
                        {
                            LoaiVatTu = "Bìa",
                            TayIn = tay_in,
                            KTP_SoTrang = item1.So_Trang,
                            KGI_Rong = item1.KGI_Rong,
                            KGI_Cao = item1.KGI_Cao,
                            SLGiayChinh = item1.SL_Giay_Chinh,
                            KI_Rong = item1.KI_Rong,
                            KI_Cao = item1.KI_Cao,
                            SLInThucTe = item1.SLInThucTe,
                            MI_Tren = item2.MI_Tren,
                            MI_Duoi = item2.MI_Duoi,
                            kho_kem_600 = kho_kem_600,
                            kho_kem_800 = kho_kem_800,
                            kho_kem_900 = kho_kem_900,
                            kho_kem_608 = kho_kem_608,
                            kho_kem_680 = kho_kem_680,
                            kho_kem_607 = kho_kem_607
                        };
                        ListChiTietThongKes.Add(newThongKeInfo);
                    }
                }
                // Ruột
                var lstNhomRuots = chiTietInfo.lstthongKes.Where(x => x.LoaiVatTu.Trim().ToLower() == "ruột").ToList();
                var lstQuyCachRuots = lstNhomRuots.GroupBy(x => new { x.KGI_Cao, x.KGI_Rong, x.KI_Cao, x.KI_Rong }).Select(y => new
                {
                    KGI_Cao = y.Key.KGI_Cao,
                    KGI_Rong = y.Key.KGI_Rong,
                    KI_Cao = y.Key.KI_Cao,
                    KI_Rong = y.Key.KI_Rong,

                    So_Trang = y.Sum(i => i.KTP_SoTrang),
                    SL_Giay_Chinh = y.Sum(i => i.SLGiayChinh),
                    SLInThucTe = y.Sum(i => i.SLInThucTe),
                    // mau in
                    lstMauIn = lstNhomRuots.Where(x => x.KGI_Cao == y.Key.KGI_Cao &&
                                                    x.KGI_Rong == y.Key.KGI_Rong &&
                                                    x.KI_Cao == y.Key.KI_Cao &&
                                                    x.KI_Rong == y.Key.KI_Rong)
                                          .GroupBy(z => new { z.KGI_Cao, z.KGI_Rong, z.KI_Cao, z.KI_Rong, z.MI_Tren, z.MI_Duoi })
                                          .Select(t => new
                                          {
                                              MI_Tren = t.Key.MI_Tren,
                                              MI_Duoi = t.Key.MI_Duoi,
                                              Trang_Mau_In = t.Sum(u => u.KTP_SoTrang),
                                              Tay_In = t.OrderBy(i => i.VatTuID).Select(x => x.TayIn).ToList(),
                                              lstKemIn = lstNhomRuots.Where(v => v.KGI_Cao == y.Key.KGI_Cao &&
                                                                                v.KGI_Rong == y.Key.KGI_Rong &&
                                                                                v.KI_Cao == y.Key.KI_Cao &&
                                                                                v.KI_Rong == y.Key.KI_Rong &&
                                                                                v.MI_Tren == t.Key.MI_Tren &&
                                                                                v.MI_Duoi == t.Key.MI_Duoi
                                                                                )
                                                                    .GroupBy(c => new { c.KGI_Cao, c.KGI_Rong, c.KI_Cao, c.KI_Rong, c.MI_Tren, c.MI_Duoi, c.KhoKem }).Select(i => new
                                                                    {
                                                                        KhoKem = db.KhoKem.Where(x => x.id == i.Key.KhoKem).Select(m => m.value).FirstOrDefault(),
                                                                        TongKhoKem = i.Sum(x => x.TongKhoKem)
                                                                    })
                                          })
                });

                foreach (var item1 in lstQuyCachRuots)
                {
                    foreach (var item2 in item1.lstMauIn)
                    {
                        var kho_kem_600 = item2.lstKemIn.Where(x => x.KhoKem == 600).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_800 = item2.lstKemIn.Where(x => x.KhoKem == 800).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_900 = item2.lstKemIn.Where(x => x.KhoKem == 900).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_608 = item2.lstKemIn.Where(x => x.KhoKem == 608).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_680 = item2.lstKemIn.Where(x => x.KhoKem == 680).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_607 = item2.lstKemIn.Where(x => x.KhoKem == 607).Select(x => x.TongKhoKem).FirstOrDefault();
                        var tay_in = "Ruột";

                        var newThongKeInfo = new ChiTietThongKeInfo
                        {
                            LoaiVatTu = "Ruột",
                            TayIn = tay_in,
                            KTP_SoTrang = item1.So_Trang,
                            KGI_Rong = item1.KGI_Rong,
                            KGI_Cao = item1.KGI_Cao,
                            SLGiayChinh = item1.SL_Giay_Chinh,
                            KI_Rong = item1.KI_Rong,
                            KI_Cao = item1.KI_Cao,
                            SLInThucTe = item1.SLInThucTe,
                            MI_Tren = item2.MI_Tren,
                            MI_Duoi = item2.MI_Duoi,
                            kho_kem_600 = kho_kem_600,
                            kho_kem_800 = kho_kem_800,
                            kho_kem_900 = kho_kem_900,
                            kho_kem_608 = kho_kem_608,
                            kho_kem_680 = kho_kem_680,
                            kho_kem_607 = kho_kem_607
                        };
                        ListChiTietThongKes.Add(newThongKeInfo);
                    }
                }
                // Khác
                var lstNhomThuongs = chiTietInfo.lstthongKes.Where(x => x.LoaiVatTu.Trim().ToLower() != "bìa" && x.LoaiVatTu.Trim().ToLower() != "ruột").ToList();
                var lstQuyCachThuongs = lstNhomThuongs.GroupBy(x => new { x.KGI_Cao, x.KGI_Rong, x.KI_Cao, x.KI_Rong }).Select(y => new
                {
                    KGI_Cao = y.Key.KGI_Cao,
                    KGI_Rong = y.Key.KGI_Rong,
                    KI_Cao = y.Key.KI_Cao,
                    KI_Rong = y.Key.KI_Rong,

                    So_Trang = y.Sum(i => i.KTP_SoTrang),
                    SL_Giay_Chinh = y.Sum(i => i.SLGiayChinh),
                    SLInThucTe = y.Sum(i => i.SLInThucTe),
                    // mau in
                    lstMauIn = lstNhomThuongs.Where(x => x.KGI_Cao == y.Key.KGI_Cao &&
                                                    x.KGI_Rong == y.Key.KGI_Rong &&
                                                    x.KI_Cao == y.Key.KI_Cao &&
                                                    x.KI_Rong == y.Key.KI_Rong)
                                          .GroupBy(z => new { z.KGI_Cao, z.KGI_Rong, z.KI_Cao, z.KI_Rong, z.MI_Tren, z.MI_Duoi })
                                          .Select(t => new
                                          {
                                              MI_Tren = t.Key.MI_Tren,
                                              MI_Duoi = t.Key.MI_Duoi,
                                              Trang_Mau_In = t.Sum(u => u.KTP_SoTrang),
                                              Tay_In = t.OrderBy(i => i.VatTuID).Select(x => x.TayIn).ToList(),
                                              lstKemIn = lstNhomThuongs.Where(v => v.KGI_Cao == y.Key.KGI_Cao &&
                                                                                v.KGI_Rong == y.Key.KGI_Rong &&
                                                                                v.KI_Cao == y.Key.KI_Cao &&
                                                                                v.KI_Rong == y.Key.KI_Rong &&
                                                                                v.MI_Tren == t.Key.MI_Tren &&
                                                                                v.MI_Duoi == t.Key.MI_Duoi
                                                                                )
                                                                    .GroupBy(c => new { c.KGI_Cao, c.KGI_Rong, c.KI_Cao, c.KI_Rong, c.MI_Tren, c.MI_Duoi, c.KhoKem }).Select(i => new
                                                                    {
                                                                        KhoKem = db.KhoKem.Where(x => x.id == i.Key.KhoKem).Select(m => m.value).FirstOrDefault(),
                                                                        TongKhoKem = i.Sum(x => x.TongKhoKem)
                                                                    })
                                          })
                });

                foreach (var item1 in lstQuyCachThuongs)
                {
                    foreach (var item2 in item1.lstMauIn)
                    {
                        var kho_kem_600 = item2.lstKemIn.Where(x => x.KhoKem == 600).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_800 = item2.lstKemIn.Where(x => x.KhoKem == 800).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_900 = item2.lstKemIn.Where(x => x.KhoKem == 900).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_608 = item2.lstKemIn.Where(x => x.KhoKem == 608).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_680 = item2.lstKemIn.Where(x => x.KhoKem == 680).Select(x => x.TongKhoKem).FirstOrDefault();
                        var kho_kem_607 = item2.lstKemIn.Where(x => x.KhoKem == 607).Select(x => x.TongKhoKem).FirstOrDefault();
                        var tay_in = String.Join("+", item2.Tay_In.Distinct().ToList());

                        var newThongKeInfo = new ChiTietThongKeInfo
                        {
                            LoaiVatTu = "",
                            TayIn = tay_in,
                            KTP_SoTrang = item1.So_Trang,
                            KGI_Rong = item1.KGI_Rong,
                            KGI_Cao = item1.KGI_Cao,
                            SLGiayChinh = item1.SL_Giay_Chinh,
                            KI_Rong = item1.KI_Rong,
                            KI_Cao = item1.KI_Cao,
                            SLInThucTe = item1.SLInThucTe,
                            MI_Tren = item2.MI_Tren,
                            MI_Duoi = item2.MI_Duoi,
                            kho_kem_600 = kho_kem_600,
                            kho_kem_800 = kho_kem_800,
                            kho_kem_900 = kho_kem_900,
                            kho_kem_608 = kho_kem_608,
                            kho_kem_680 = kho_kem_680,
                            kho_kem_607 = kho_kem_607
                        };
                        ListChiTietThongKes.Add(newThongKeInfo);
                    }
                }
                // thay đổi list thống kê
                chiTietInfo.lstthongKeInfos = ListChiTietThongKes;

            }

            return query ?? new List<DonHangInfo>();
        }

        public DonHangInfo ExportExcelDonHang(int id)
        {
            var query = (from d in db.DonHang.Where(x => x.id == id)
                         select new DonHangInfo()
                         {
                             id = d.id,
                             so_lenh_sx = d.so_lenh_sx,
                             ngay_giao_hang = d.ngay_giao_hang,
                             ten_can_bo_kt = d.ten_can_bo_kt,
                             ten_can_bo_ql = d.ten_can_bo_ql,
                             ten_khach_hang = d.ten_khach_hang,
                             ten_san_pham = d.ten_san_pham,
                             ma_khach_hang = d.ma_khach_hang,
                             ma_san_pham = d.ma_san_pham,
                             phieu_dnsx_so = d.phieu_dnsx_so,
                             nv_kinh_doanh = d.nv_kinh_doanh,
                             phone_number = d.phone_number,
                             loai = d.loai,
                             lan_dieu_chinh = d.lan_dieu_chinh,
                             kho_doc = d.kho_doc,
                             kho_ngang = d.kho_ngang,
                             kho_tp = d.kho_tp,
                             created_date = d.created_date,
                             email = d.email,
                             quy_cach_chung = d.quy_cach_chung,
                             nha_cc = d.nha_cc,
                             so_luong_tong = d.so_luong_tong,
                             cb_ghi_chu = d.cb_ghi_chu,
                             cb_thoi_gian_giao = d.cb_thoi_gian_giao,
                             in_ghi_chu = d.in_ghi_chu,
                             in_thoi_gian_giao = d.in_thoi_gian_giao,
                             quy_cach_san_pham = d.quy_cach_san_pham,
                             thanh_pham_chung = d.thanh_pham_chung,
                             chi_tiet_sl_tong = d.chi_tiet_sl_tong,
                             tp_ghi_chu = d.tp_ghi_chu,
                             tp_cao = d.tp_cao,
                             tp_dai = d.tp_dai,
                             tp_rong = d.tp_rong,
                             tp_sl_bangkeo = d.tp_sl_bangkeo,
                             tp_socuon_thung = d.tp_socuon_thung,
                             tp_soluong = d.tp_soluong,
                             tp_thoi_han = d.tp_thoi_han,
                             tp_vat_tu = d.tp_vat_tu,

                             lstVatTus = db.VatTu.Where(x => x.don_hang_id == d.id).ToList(),
                             lstChiTietDuToans = db.ChiTietDuToan.Where(x => x.don_hang_id == d.id).ToList(),
                             lstChiTietCheBans = db.ChiTietCheBan.Where(x => x.don_hang_id == d.id).ToList(),
                             lstChiTietIns = db.ChiTietIn.Where(x => x.don_hang_id == d.id).ToList(),
                         }).FirstOrDefault();
            if (query.chi_tiet_sl_tong != null)
            {
                query.lst_sl_tong = JsonConvert.DeserializeObject<List<TongSoLuongInfo>>(query.chi_tiet_sl_tong ?? string.Empty)
                        .Select(o => new TongSoLuongInfo()
                        {
                            Values = o.Values
                        }).ToList() ?? new List<TongSoLuongInfo>();
            }
            else { query.lst_sl_tong = new List<TongSoLuongInfo>(); }

            query.lstCheBanInfo = (from cb in db.ChiTietCheBan.Where(x => x.don_hang_id == query.id)
                                   join k in db.KhoKem on cb.kho_kem equals k.id into lk
                                   from lkcb in lk.DefaultIfEmpty()
                                   join p in db.PPIn on cb.phuong_phap_in equals p.id into lp
                                   from lpcb in lp.DefaultIfEmpty()
                                   select new ChiTietCheBanInfo()
                                   {
                                       id = cb.id,
                                       don_hang_id = cb.don_hang_id,
                                       kho_in_dai = cb.kho_in_dai,
                                       kho_in_rong = cb.kho_in_rong,
                                       kho_kem = cb.kho_kem,
                                       kho_kem_name = lkcb.value,
                                       phuong_phap_in = cb.phuong_phap_in,
                                       phuong_phap_in_name = lpcb.Name,
                                       so_bo = cb.so_bo,
                                       so_kem = cb.so_kem,
                                       ten_tay_in = cb.ten_tay_in,
                                       tong = cb.tong,
                                       trang = cb.trang,
                                   }).ToList() ?? new List<ChiTietCheBanInfo>();
            query.lstInInfo = (from i in db.ChiTietIn.Where(x => x.don_hang_id == query.id)
                               join p in db.PPIn on i.phuong_phap_in equals p.id into lp
                               from lpi in lp.DefaultIfEmpty()
                               select new ChiTietInInfo()
                               {
                                   id = i.id,
                                   ten_tay_in = i.ten_tay_in,
                                   ten_loai = i.ten_loai,
                                   bu_in = i.bu_in,
                                   bu_phat_hanh = i.bu_phat_hanh,
                                   bu_thanh_pham = i.bu_thanh_pham,
                                   chinh = i.chinh,
                                   dinh_luong_giay_in = i.dinh_luong_giay_in,
                                   don_hang_id = i.don_hang_id,
                                   kho_giay_dai = i.kho_giay_dai,
                                   kho_giay_rong = i.kho_giay_rong,
                                   kho_in_dai = i.kho_in_dai,
                                   kho_in_rong = i.kho_in_rong,
                                   phuong_phap_in = i.phuong_phap_in,
                                   phuong_phap_in_name = lpi.Name,
                                   sl_tong = i.sl_tong,
                                   so_kg = i.so_kg,
                                   so_luot_in_quy_doi = i.so_luot_in_quy_doi,
                                   so_luot_in_thuc_te = i.so_luot_in_thuc_te,
                               }).ToList() ?? new List<ChiTietInInfo>();

            return query ?? new DonHangInfo();
        }

        public List<DonHang> SearchLSXAutoComplete(string key)
        {
            return db.DonHang.AsNoTracking().Where(x => x.so_lenh_sx.ToLower().Contains(key.ToLower())).Distinct().ToList();
        }
    }
}