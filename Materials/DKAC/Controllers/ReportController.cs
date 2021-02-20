﻿using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Models.Enum;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using DKAC.Repository;
using FlexCel.Report;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DKAC.Controllers
{
    public class ReportController : BaseController
    {
        ReportRepository _rep = new ReportRepository();
        // GET: Report
        public ActionResult Index()
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            ViewBag.hasViewPermission = CheckPermission((int)PageId.ThongKeDuToan, (int)Actions.Xem, currentUser); // check quyền xem page
            if (!ViewBag.hasViewPermission) { return RedirectToAction("NotPermission", "Home"); }

            var lstDH = _rep.GetAllSo_LSX();
            DonHangInfo dh = new DonHangInfo();
            dh.lstSo_LSX = lstDH.ConvertAll(a => new SelectListItem()
            {
                Value = a.id.ToString(),
                Text = a.so_lenh_sx.ToString(),
            });
            return View(dh);
        }

        [HttpGet]
        public ActionResult SearchLSX(string q)
        {
            var model = new DonHangRequestModel()
            {
                Keywords = q,
                page = 1,
                pageSize = 50
            };
            var result = _rep.SearchSoLSX(model, model.page, model.pageSize, out var totalCount);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMeasurEquipAfterEvaluate(int? id, DateTime? fromDate, DateTime? toDate, bool exportExcel)
        {
            var model = _rep.ThongKe(id, fromDate, toDate);
            //var lstEvalua = (from d in model.SelectMany(o => o.lstEvaluateAfterverificationInfo)
            //                 select new
            //                 {
            //                     d.Id,
            //                     d.MeasuringEquipmentId,
            //                     d.Conclusion,
            //                     d.Date,
            //                     d.Evaluation,
            //                     d.MaxTolerance,
            //                     d.Note,
            //                     d.Parameter,
            //                     d.Tolerance,
            //                     d.UnitName,
            //                 }).ToList();

            var path = Path.Combine(Server.MapPath("~/FileTemplate"), "ThongKeMaterials.xlsx");
            var file = new FileInfo(path);
            var excel = new ExcelPackage(file);
            var fr = new FlexCelReport();
            var result = CreateXlsFile(excel);

            fr.SetValue("Date", DateTime.Now.Day);
            fr.SetValue("Month", DateTime.Now.Month);
            fr.SetValue("Year", DateTime.Now.Year);

            fr.AddTable("evaluateMeasur", model.OrderBy(o => o.id));
            //fr.AddTable("lstEvaluaMeasur", lstEvalua.OrderBy(o => o.MeasuringEquipmentId));
            fr.Run(result);
            fr.Dispose();
            return ViewReport(result, "ThongKeMaterials", exportExcel);
        }

        [HttpGet]
        public ActionResult ExportExcelBangDuToan(int id, bool exportExcel)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var model = _rep.ExportExcelDonHang(id);

            var lstDuToan = model.lstChiTietDuToans ?? new List<ChiTietDuToan>();

            var path = Path.Combine(Server.MapPath("~/FileTemplate"), "ExprotDuToanVatTu.xlsx");
            var file = new FileInfo(path);
            var excel = new ExcelPackage(file);
            var fr = new FlexCelReport();

            var result = CreateXlsFile(excel);

            fr.SetValue("ten_khach_hang", model.ten_khach_hang);
            fr.SetValue("ten_san_pham", model.ten_san_pham);
            fr.SetValue("ma_san_pham", model.ma_san_pham);
            fr.SetValue("lan_dieu_chinh", model.lan_dieu_chinh);
            fr.SetValue("quy_cach_chung", model.quy_cach_chung);
            fr.SetValue("ten_can_bo_ql", model.ten_can_bo_ql);
            fr.SetValue("ten_can_bo_kt", model.ten_can_bo_kt);
            fr.SetValue("created_date", (model.created_date.HasValue ? model.created_date.Value.ToString("dd/MM/yyyy") : ""));

            fr.AddTable("lstDuToan", lstDuToan.OrderBy(o => o.id));
            fr.Run(result);
            fr.Dispose();
            if (exportExcel == false)
            {
                return ExportPDF(result, "ExprotDuToanVatTu", exportExcel);
            }
            return ViewReport(result, "ExprotDuToanVatTu", exportExcel);
        }

        [HttpGet]
        public ActionResult ExportExcelBangLSX(int id, bool exportExcel)
        {
            var model = _rep.ExportExcelDonHang(id);
            var lstvatTu = model.lstVatTus;
            var lstsltong = model.lst_sl_tong ?? new List<TongSoLuongInfo>();
            var lstCheBanInfo = model.lstCheBanInfo ?? new List<ChiTietCheBanInfo>();
            var lstInInfo = model.lstInInfo ?? new List<ChiTietInInfo>();

            var lstQuycachBia = new List<VatTu>();
            var lstQuycachThuong = new List<VatTu>();
            var lstQuycachNull = new List<VatTu>();
            var lstQuyCachInfo = new List<QuyCachInfo>();

            if (lstvatTu.Count > 0)
            {
                for (int i = 0; i < lstvatTu.Count; i++)
                {
                    if (lstvatTu[i].ten_nhom_vat_tu != null && lstvatTu[i].ten_nhom_vat_tu.Trim().ToLower().StartsWith("bìa"))
                    {
                        lstQuycachBia.Add(lstvatTu[i]);
                    }
                    //if (lstvatTu[i].nhom_vat_tu_id == null)
                    //{
                    //    lstQuycachNull.Add(lstvatTu[i]);
                    //}
                    else { lstQuycachThuong.Add(lstvatTu[i]); }
                }

                var lstIds = lstQuycachBia.OrderBy(x => x.nhom_vat_tu_id).Select(x => x.nhom_vat_tu_id).Distinct().ToList();
                for (int i = 0; i < lstIds.Count; i++)
                {
                    var vattu = lstQuycachBia.Where(x => x.nhom_vat_tu_id == lstIds[i]).ToList();
                    if (vattu.Count > 0)
                    {
                        foreach (var item in vattu)
                        {
                            if (item.loai_giay != null)
                                item.loai_giay = item.loai_giay.Trim();
                        }

                        var quycachinfo = new QuyCachInfo()
                        {
                            id = vattu[0].nhom_vat_tu_id,
                            count = vattu.Count,
                            ten_nhom_vat_tu = vattu[0].ten_nhom_vat_tu,
                            lstvatTus = vattu,
                        };
                        lstQuyCachInfo.Add(quycachinfo);
                    }
                }

                var lstIdsThuong = lstQuycachThuong.OrderByDescending(x => x.nhom_vat_tu_id).Select(x => x.nhom_vat_tu_id).Distinct().ToList();
                for (int i = 0; i < lstIdsThuong.Count; i++)
                {
                    var vattuThuong = lstQuycachThuong.Where(x => x.nhom_vat_tu_id == lstIdsThuong[i]).ToList();
                    if (vattuThuong.Count > 0)
                    {
                        foreach (var item in vattuThuong)
                        {
                            if (item.loai_giay != null)
                                item.loai_giay = item.loai_giay.Trim();
                        }

                        var quycachinfoThuong = new QuyCachInfo()
                        {
                            id = vattuThuong[0].nhom_vat_tu_id,
                            count = vattuThuong.Count,
                            ten_nhom_vat_tu = vattuThuong[0].ten_nhom_vat_tu,
                            lstvatTus = vattuThuong,
                        };
                        lstQuyCachInfo.Add(quycachinfoThuong);
                    }
                }

                //if (lstQuycachNull.Count > 0)
                //{
                //    foreach (var item in lstQuycachNull)
                //    {
                //        item.nhom_vat_tu_id = 0;
                //    }
                //    var quycachinfoNull = new QuyCachInfo()
                //    {
                //        id = 0,
                //        ten_nhom_vat_tu = "",
                //        count = lstQuycachNull.Count,
                //        lstvatTus = lstQuycachNull,
                //    };
                //    lstQuyCachInfo.Add(quycachinfoNull);
                //}

            }
            var lstQuyCachChitiet = lstQuyCachInfo.SelectMany(x => x.lstvatTus).ToList() ?? new List<VatTu>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var path = Path.Combine(Server.MapPath("~/FileTemplate"), "Export-LSX.xlsx");
            var file = new FileInfo(path);
            var excel = new ExcelPackage(file);
            var fr = new FlexCelReport();
            var result = CreateXlsFile(excel);

            fr.SetValue("so_lenh_sx", model.so_lenh_sx);
            fr.SetValue("phieu_dnsx_so", model.phieu_dnsx_so);
            fr.SetValue("nv_kinh_doanh", model.nv_kinh_doanh);
            fr.SetValue("loai", model.loai);
            fr.SetValue("nha_cc", model.nha_cc);
            fr.SetValue("ma_khach_hang", model.ma_khach_hang);
            fr.SetValue("ten_khach_hang", model.ten_khach_hang);
            fr.SetValue("ten_san_pham", model.ten_san_pham);
            fr.SetValue("ma_san_pham", model.ma_san_pham);
            fr.SetValue("lan_dieu_chinh", model.lan_dieu_chinh);
            fr.SetValue("so_luong_tong", model.so_luong_tong);
            fr.SetValue("quy_cach_chung", model.quy_cach_chung);
            fr.SetValue("ten_can_bo_ql", model.ten_can_bo_ql);
            fr.SetValue("ten_can_bo_kt", model.ten_can_bo_kt);
            fr.SetValue("created_date", (model.created_date.HasValue ? model.created_date.Value.ToString("dd/MM/yyyy") : ""));
            fr.SetValue("ngay_giao_hang", (model.ngay_giao_hang.HasValue ? model.ngay_giao_hang.Value.ToString("dd/MM/yyyy") : ""));
            fr.SetValue("cb_ghi_chu", model.cb_ghi_chu);
            fr.SetValue("cb_thoi_gian_giao", model.cb_thoi_gian_giao);
            fr.SetValue("in_ghi_chu", model.in_ghi_chu);
            fr.SetValue("tp_dai", model.tp_dai);
            fr.SetValue("tp_rong", model.tp_rong);
            fr.SetValue("tp_cao", model.tp_cao);
            fr.SetValue("tp_soluong", model.tp_soluong);
            fr.SetValue("tp_socuon_thung", model.tp_socuon_thung);
            fr.SetValue("tp_ghi_chu", model.tp_ghi_chu);
            fr.SetValue("tp_thoi_han", model.tp_thoi_han);
            fr.SetValue("tp_sl_bangkeo", model.tp_sl_bangkeo);
            fr.SetValue("tp_vat_tu", model.tp_vat_tu);

            fr.AddTable("lstsltong", lstsltong);
            fr.AddTable("lstQuyCach", lstQuyCachInfo);
            fr.AddTable("lstQuyCachChitiet", lstQuyCachChitiet);
            fr.AddTable("lstCheBanInfo", lstCheBanInfo);
            fr.AddTable("lstInInfo", lstInInfo);
            fr.Run(result);
            fr.Dispose();

            return ViewReport(result, "Export-LSX", exportExcel);
        }
    }
}