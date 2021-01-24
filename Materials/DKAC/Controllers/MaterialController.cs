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
        public ActionResult Index(string KeySearch, int page = 1, int pageSize = 20)
        {
            var currentUser = (User)Session[CommonConstants.USER_SESSION];
            //EmployeeRequestModel model = new EmployeeRequestModel();
            //model = _empRepo.GetListAllEmployee(employee, user, KeySearch, page, pageSize);
            //return View(model);
            return View();
        }

        public ActionResult Edit(int id)
        {
            var lstMaterial = _mater.GetAllMaterial();
            UserInfo u = new UserInfo();
            u.lstMaterialType = new List<MaterialType>();
            u.lstMaterial = lstMaterial.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.MaterialTypeName.ToString(),
            });
            u.lstMaterialType.AddRange(lstMaterial);
            //gọi hàm
            return View(u);
        }

        public ActionResult EditMaterial(int id)
        {
            if (id == (int)0)
            {
                UserInfo u = new UserInfo();
                return View(u);
            }
            //gọi hàm
            return View();
        }

        public ActionResult EditProductionOrder(int id)
        {
            if (id == (int)0)
            {
                UserInfo u = new UserInfo();
                return View(u);
            }
            //gọi hàm
            return View();
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
            var result = _mater.Search(model, model.page, model.pageSize, out var totalCount);
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
        public ActionResult SearchNameAutoComplete(string keySearch)
        {
            var lstUser = _mater.GetAllMaterialTest(keySearch);
            var rs = new List<string>();
            var lstResultSearch = lstUser.Select(x => x.MaterialTypeName).ToList();
            rs.AddRange(lstResultSearch);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}