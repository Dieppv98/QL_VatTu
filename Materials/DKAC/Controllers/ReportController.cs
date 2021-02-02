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
            if(exportExcel == false)
            {
                return ExportPDF(result, "ExprotDuToanVatTu", exportExcel);
            }
            return ViewReport(result, "ExprotDuToanVatTu", exportExcel);
        }
    }
}