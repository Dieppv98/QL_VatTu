using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Models.Enum;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using DKAC.Repository;
using System;
using System.Web.Mvc;

namespace DKAC.Controllers
{
    public class KhachHangController : BaseController
    {
        private KhachHangRepository _khachHangRepository = new KhachHangRepository();
        // Index
        public ActionResult Index(string KeySearch, int page = 1, int pageSize = 20)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];

            ViewBag.hasViewPermission = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Xem, currentUser);
            ViewBag.hasAddPermission = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Them, currentUser);
            ViewBag.hasUpdatePermission = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Sua, currentUser);
            ViewBag.hasDeletePermission = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Xoa, currentUser);
            if (!ViewBag.hasViewPermission)
            {
                return RedirectToAction("NotPermission", "Home");
            }

            KhachHangRequestModel model = _khachHangRepository.GetByKhachHang(KeySearch, page, pageSize);
            return View(model);
        }
        // Add
        public ActionResult Add()
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Them, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền

            return View();
        }
        [HttpPost]
        public ActionResult Add(KhachHang khachHang)
        {
            khachHang.created_date = DateTime.Now;
            khachHang.last_updated = DateTime.Now;
            var result = _khachHangRepository.Add(khachHang);
            return Json(result == 1 ? new { status = 1, message = "Thêm thành công" } : new { status = 0, message = "Thêm thất bại" }, JsonRequestBehavior.AllowGet);
        }
        // Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền

            KhachHang khachHang = _khachHangRepository.GetById(id);
            KhachHangInfo khachHangInfo = new KhachHangInfo()
            {
                id = khachHang.id,
                ma_khach_hang = khachHang.ma_khach_hang,
                ten_khach_hang = khachHang.ten_khach_hang,
                dia_chi = khachHang.dia_chi,
                ma_so_thue = khachHang.ma_so_thue,
                dien_thoai = khachHang.dien_thoai,
                fax = khachHang.fax,
                email = khachHang.email,
                nguoi_dai_dien = khachHang.nguoi_dai_dien,
                chuc_vu = khachHang.chuc_vu,
                created_date = khachHang.created_date,
                last_updated = khachHang.last_updated
        };
            return View("Details", khachHangInfo);
        }
        // Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Sua, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền

            KhachHang khachHang = _khachHangRepository.GetById(id);
            KhachHangInfo khachHangInfo = new KhachHangInfo()
            {
                id = khachHang.id,
                ma_khach_hang = khachHang.ma_khach_hang,
                ten_khach_hang = khachHang.ten_khach_hang,
                dia_chi = khachHang.dia_chi,
                ma_so_thue = khachHang.ma_so_thue,
                dien_thoai = khachHang.dien_thoai,
                fax = khachHang.fax,
                email = khachHang.email,
                nguoi_dai_dien = khachHang.nguoi_dai_dien,
                chuc_vu = khachHang.chuc_vu,
                created_date = khachHang.created_date,
                last_updated = khachHang.last_updated
            };
            return View("Edit", khachHangInfo);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang khachHang)
        {
            khachHang.last_updated = DateTime.Now;
            var result = _khachHangRepository.Update(khachHang);
            return Json(result > 0 ? new { status = 1, message = "Cập nhật thành công" } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }
        // Delete
        public ActionResult Delete(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.QuanLyKhachHang, (int)Actions.Xoa, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền

            var result = _khachHangRepository.Delete(id);
            return Json(result == 1 ? new { status = 1, message = "Xóa thành công" } : new { status = 1, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckDuplicatedMaKhachHang(string ma_khach_hang)
        {
            var ma = _khachHangRepository.GetByMaKhachHang(ma_khach_hang);
            return Json(ma == null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckDuplicatedTenKhachHang(string ten_khach_hang)
        {
            var name = _khachHangRepository.GetByTenKhachHang(ten_khach_hang);
            return Json(name == null, JsonRequestBehavior.AllowGet);
        }
    }
}