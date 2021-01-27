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
            model.pageSize = 5;
            int totalCount = 0;
            ViewBag.hasViewPermission = CheckPermission(3, 1, currentUser);
            ViewBag.hasAddPermission = CheckPermission(3, 2, currentUser);
            ViewBag.hasUpdatePermission = CheckPermission(3, 4, currentUser);
            ViewBag.hasDeletePermission = CheckPermission(3, 8, currentUser);
            if (!ViewBag.hasViewPermission)
            {
                return RedirectToAction("NotPermission", "Home");
            }
            var rs = _mater.Search(model, model.page, model.pageSize, out totalCount);
            model.totalRecord = rs.Count();
            model.totalPage = (int)Math.Ceiling((decimal)rs.Count() / model.pageSize);
            model.lstDonHang = rs;
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var lstMaterial = _mater.GetAllMaterial() ?? new List<MaterialType>();
            DonHangInfo donHang = new DonHangInfo();
            //gọi hàm
            donHang = _mater.GetbyId(id);
            donHang.lstMaterialType = lstMaterial;
            return View(donHang);
        }

        //public ActionResult EditMaterial(int id, DonHangInfo model)
        //{
        //    if (model.id == (int)0)
        //    {
        //        return View(model);
        //    }
        //    //gọi hàm
        //    return View(model);
        //}

        //public ActionResult EditProductionOrder(int id)
        //{
        //    if (id == (int)0)
        //    {
        //        UserInfo u = new UserInfo();
        //        return View(u);
        //    }
        //    //gọi hàm
        //    return View();
        //}


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

        public ActionResult Test()
        {
            return View();
        }
    }
}