using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using DKAC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DKAC.Models.Enum;

namespace DKAC.Controllers
{
    public class MaterialController : BaseController
    {
        MaterialRepository _mater = new MaterialRepository();
        // GET: Material
        public ActionResult Index(DonHangRequestModel model)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            model.page = model.page == 0 ? 1 : model.page;
            model.pageSize = 10;
            int totalCount = 0;
            ViewBag.hasViewPermission = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            ViewBag.hasAddPermission = CheckPermission((int)PageId.NhapLieu, (int)Actions.Them, currentUser);
            ViewBag.hasUpdatePermission = CheckPermission((int)PageId.NhapLieu, (int)Actions.Sua, currentUser);
            ViewBag.hasDeletePermission = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xoa, currentUser);
            if (!ViewBag.hasViewPermission)
            {
                return RedirectToAction("NotPermission", "Home");
            }
            var rs = _mater.Search(model, model.page, model.pageSize, out totalCount);
            model.totalRecord = totalCount;
            model.totalPage = (int)Math.Ceiling((decimal)model.totalRecord / model.pageSize);
            model.lstDonHang = rs;
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];

            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            var lstPPin = _mater.GetAllPPIn() ?? new List<PPIn>();
            var lstKhokem = _mater.GetAllKhoKem() ?? new List<KhoKem>();

            DonHangInfo donHang = new DonHangInfo();
            donHang.lstMaterialType = lstMaterial;

            if (id == 0)
            {
                var checkPermissonThem = CheckPermission((int)PageId.NhapLieu, (int)Actions.Them, currentUser); ///check quyền thêm
                if (checkPermissonThem == false) { return RedirectToAction("NotPermission", "Home"); }

                donHang.lstMaterialType = lstMaterial;
                donHang.lstPPIn = lstPPin;
                donHang.lstVatTus = new List<VatTu>();
                donHang.lstkhoKem = lstKhokem;
                return View("Edit", donHang);
            }

            var checkPermissonSua = CheckPermission((int)PageId.NhapLieu, (int)Actions.Sua, currentUser); ///check quyền sủa
            if (checkPermissonSua == false) { return RedirectToAction("NotPermission", "Home"); }

            donHang = _mater.GetbyId(id);
            donHang.lstMaterialType = lstMaterial;
            donHang.lstPPIn = lstPPin;
            donHang.lstkhoKem = lstKhokem;
            return View("EditReceipt", donHang);
        }

        public ActionResult Details(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền xem

            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            DonHangInfo donHang = new DonHangInfo();
            //gọi hàm
            donHang = _mater.GetbyId(id);
            donHang.lstMaterialType = lstMaterial;
            return View(donHang);
        }

        public ActionResult DetailsLSX(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền xem

            var lstPPin = _mater.GetAllPPIn() ?? new List<PPIn>();
            var lstKhokem = _mater.GetAllKhoKem() ?? new List<KhoKem>();
            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            DonHangInfo donHang = new DonHangInfo();
            //gọi hàm
            donHang = _mater.GetbyId(id);
            donHang.lstMaterialType = lstMaterial;
            donHang.lstMaterialType = lstMaterial;
            donHang.lstPPIn = lstPPin;
            return View(donHang);
        }

        [HttpGet]
        public ActionResult Search(string q)
        {
            var model = new MaterialTypeRequestModel()
            {
                Keywords = q,
                page = 1,
                pageSize = 50
            };
            var result = _mater.SearchMaterial(model, model.page, model.pageSize, out var totalCount);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult QuickAddManagementLevel()
        {
            return PartialView();
        }

        public int UpdateMaterialType(MaterialType model)
        {
            var rs = _mater.UpdateMaterialType(model);
            return rs;
        }

        [HttpPost]
        public ActionResult CheckDuplicatedMaterialName(string MaterialTypeName, int? id)
        {
            var name = _mater.CheckDupplicateMaterial(MaterialTypeName, id);
            if (name == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchMaKHAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllKhachHang(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.ma_khach_hang).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchTenKHAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllKhachHang(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.ten_khach_hang).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchMaSPAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllSanPham(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.ma_san_pham).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchTenSPAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllSanPham(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.ten_san_pham).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchLoaiGiayAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllLoaiGiay(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.loai_giay.Trim()).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddorUpdateDonHang(DonHangInfo model)
        {
            User user = (User)Session[CommonConstants.USER_SESSION];
            var checkPermissonSua = CheckPermission((int)PageId.NhapLieu, (int)Actions.Sua, user); ///check quyền
            if (checkPermissonSua == false) { return RedirectToAction("NotPermission", "Home"); }

            model.last_updated = DateTime.Now;
            var result = _mater.AddorUpdate(model);
            if (result == 1)
            {
                return Json(new { status = 1, message = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0, message = "Thêm thất bại" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            User user = (User)Session[CommonConstants.USER_SESSION];
            var checkPermissonXoa = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xoa, user); ///check quyền
            if (checkPermissonXoa == false) { return RedirectToAction("NotPermission", "Home"); }

            var result = _mater.Delete(id);
            if (result == 1)
            {
                return Json(new { status = 1, message = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DonHangDetails(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền xem

            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            TongQuanDonHangInfo donHangInfo = new TongQuanDonHangInfo();
            //gọi hàm
            donHangInfo = _mater.GetTongQuanbyId(id);
            donHangInfo.lstMaterialType = lstMaterial;
            return View(donHangInfo);
        }

        public ActionResult KHVTDetails(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền xem

            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            DonHangInfo donHangInfo = new DonHangInfo();
            //gọi hàm
            donHangInfo = _mater.GetbyId(id);
            donHangInfo.lstMaterialType = lstMaterial;
            return View(donHangInfo);
        }

        public ActionResult LenhSXDetails(int id)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            var checkPer = CheckPermission((int)PageId.NhapLieu, (int)Actions.Xem, currentUser);
            if (checkPer == false) { return RedirectToAction("NotPermission", "Home"); }  ////check quyền xem

            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            LenhSanXuatInfo donHangInfo = new LenhSanXuatInfo();
            //gọi hàm
            donHangInfo = _mater.GetLenhSanXuatbyId(id);
            donHangInfo.lstMaterialType = lstMaterial;
            return View(donHangInfo);
        }
        [HttpPost]
        public ActionResult CheckDuplicatedSoLenhSX(string soLenhSX)
        {
            var name = _mater.GetBySoLenhSX(soLenhSX);
            if (name == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}