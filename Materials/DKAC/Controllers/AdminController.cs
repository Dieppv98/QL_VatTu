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
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DKAC.Controllers
{
    public class AdminController : BaseController
    {
        ReportRepository _reportRepo = new ReportRepository();

        // GET: Report
        public ActionResult PermissionManagerment()
        {
            var model = _reportRepo.GetPermissionActionInfo();
            if (model == null) model = new PermissionActionInfo();
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            ViewBag.hasViewPermission = CheckPermission(12, 1, currentUser);
            ViewBag.hasAddPermission = CheckPermission(12, 2, currentUser);
            ViewBag.hasUpdatePermission = CheckPermission(12, 4, currentUser);
            ViewBag.hasDeletePermission = CheckPermission(12, 8, currentUser);
            if (!ViewBag.hasViewPermission)
            {
                return RedirectToAction("NotPermission", "Home");
            }
            return View(model);
        }

        public ActionResult RoleManagerment()
        {
            var model = new PermissionActionInfo();
            model.ListRole = _reportRepo.GetAllRole();
            model.ListUser = _reportRepo.GetAllUser();
            model.ListUserRole = _reportRepo.GetAllUserRole();
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            ViewBag.hasViewPermission = CheckPermission(13, 1, currentUser);
            ViewBag.hasAddPermission = CheckPermission(13, 2, currentUser);
            ViewBag.hasUpdatePermission = CheckPermission(13, 4, currentUser);
            ViewBag.hasDeletePermission = CheckPermission(13, 8, currentUser);
            if (!ViewBag.hasViewPermission)
            {
                return RedirectToAction("NotPermission", "Home");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SavePermission(PermissionAction model)
        {
            var result = _reportRepo.SavePermission(model);
            var msg = "";
            if (result > 0) { msg = "Cập nhật thành công"; }
            else { msg = "cập nhât thất bại"; }
            return Json(new { status = result, message = msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveUserRole(UserRoleInfo model)
        {
            var result = _reportRepo.SaveUserRole(model);
            var msg = "";
            if (result > 0) { msg = "Cập nhật thành công"; }
            else { msg = "cập nhât thất bại"; }
            return Json(new { status = result, message = msg }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditRole(int id)
        {
            return View();
        }

        public ActionResult AddRole(Role model)
        {
            User user = (User)Session[CommonConstants.USER_SESSION];
            var result = _reportRepo.SaveRole(model);
            if (result == 1)
            {
                return Json(new { status = 1, message = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = 0, message = "Thêm thất bại" }, JsonRequestBehavior.AllowGet);
        }

    }
}