using System;
using System.Web.Mvc;
using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
using DKAC.Repository;

namespace DKAC.Controllers
{
    public class SanPhamController : BaseController
    {
        private SanPhamRepository _sanPhamRepository = new SanPhamRepository();
        // Index
        public ActionResult Index(string KeySearch, int page = 1, int pageSize = 20)
        {
            SanPhamRequestModel model = _sanPhamRepository.GetBySanPham(KeySearch, page, pageSize);
            return View(model);
        }
        // Add
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(SanPham sanPham)
        {
            sanPham.created_date = DateTime.Now;
            sanPham.last_updated = DateTime.Now;
            var result = _sanPhamRepository.Add(sanPham);
            return Json(result == 1 ? new { status = 1, message = "Thêm thành công" } : new { status = 0, message = "Thêm thất bại" }, JsonRequestBehavior.AllowGet);
        }
        // Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            SanPham sanPham = _sanPhamRepository.GetById(id);
            SanPhamInfo sanPhamInfo = new SanPhamInfo()
            {
                id = sanPham.id,
                ma_san_pham = sanPham.ma_san_pham,
                ten_san_pham = sanPham.ten_san_pham,
                last_updated = sanPham.last_updated
            };
            return View("Details", sanPhamInfo);
        }
        // Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SanPham sanPham = _sanPhamRepository.GetById(id);
            SanPhamInfo sanPhamInfo = new SanPhamInfo()
            {
                id = sanPham.id,
                ma_san_pham = sanPham.ma_san_pham,
                ten_san_pham = sanPham.ten_san_pham,
                last_updated = sanPham.last_updated
            };
            return View("Edit", sanPhamInfo);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sanPham)
        {
            sanPham.last_updated = DateTime.Now;
            var result = _sanPhamRepository.Update(sanPham);
            return Json(result > 0 ? new { status = 1, message = "Cập nhật thành công" } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }
        // Delete
        public ActionResult Delete(int id)
        {
            var result = _sanPhamRepository.Delete(id);
            return Json(result == 1 ? new { status = 1, message = "Xóa thành công" } : new { status = 1, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckDuplicatedMaSanPham(string ma_san_pham)
        {
            var ma = _sanPhamRepository.GetByMaSanPham(ma_san_pham);
            return Json(ma == null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckDuplicatedTenSanPham(string ten_san_pham)
        {
            var name = _sanPhamRepository.GetByTenSanPham(ten_san_pham);
            return Json(name == null, JsonRequestBehavior.AllowGet);
        }
    }
}