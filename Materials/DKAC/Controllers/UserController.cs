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
    public class UserController : BaseController
    {
        UserRepository _userRepo = new UserRepository(); 
        // GET: User
        public ActionResult Index(string KeySearch, int page = 1, int pageSize = 20)
        {
            UserRequestModel model = _userRepo.GetByUser(KeySearch, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult EditUserInfo()
        {
            User u = (User)Session[CommonConstants.USER_SESSION];
            int? id = null;
            if (u != null) { id = u.id; }
            User emp = _userRepo.GetById(id);
            EmployeeInfo empInfo = new EmployeeInfo()
            {
                id = emp.id,
                FullName = emp.FullName,
                UserName = emp.UserName,
                PassWord = emp.PassWord,
                Birthday = emp.Birthday,
                Gender = emp.Gender,
                Email = emp.Email,
                CMND = emp.CMND,
                Tel = emp.Tel,
                Address = emp.Address,
                UserGroupId = emp.UserGroupId
            };
            return View("EditUserInfo", empInfo);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            User emp = _userRepo.GetById(id);
            EmployeeInfo empInfo = new EmployeeInfo()
            {
                id = emp.id,
                FullName = emp.FullName,
                UserName = emp.UserName,
                PassWord = emp.PassWord,
                Birthday = emp.Birthday,
                Gender = emp.Gender,
                Email = emp.Email,
                CMND = emp.CMND,
                Tel = emp.Tel,
                Address = emp.Address,
                UserGroupId = emp.UserGroupId
            };
            return View("Details", empInfo);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User emp = _userRepo.GetById(id);
            EmployeeInfo empInfo = new EmployeeInfo()
            {
                id = emp.id,
                FullName = emp.FullName,
                UserName = emp.UserName,
                PassWord = emp.PassWord,
                Birthday = emp.Birthday,
                Gender = emp.Gender,
                Email = emp.Email,
                CMND = emp.CMND,
                Tel = emp.Tel,
                Address = emp.Address,
                UserGroupId = emp.UserGroupId
            };
            return View("Edit", empInfo);
        }

        [HttpPost]
        public ActionResult Add(User employee)
        {
            User user = (User)Session[CommonConstants.USER_SESSION];
            employee.CreatedBy = user.id;
            employee.ModifyBy = user.id;
            employee.IsDeleted = 0;
            employee.PassWord = Encryption.EncryptPassword(employee.PassWord);
            var result = _userRepo.Add(employee);
            return Json(result == 1 ? new { status = 1, message = "Thêm thành công" } : new { status = 0, message = "Thêm thất bại" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditInfo(User employee)
        {
            User em = (User)Session[CommonConstants.USER_SESSION];
            employee.ModifyBy = employee.id;
            employee.ModifyDate = DateTime.Now;

            var result = _userRepo.EditInfo(employee);
            return Json(result == 1 ? new { status = 1, message = "Cập nhật thành công" } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(User employee)
        {
            User em = (User)Session[CommonConstants.USER_SESSION];
            employee.ModifyBy = em.id;
            employee.ModifyDate = DateTime.Now;
            employee.PassWord = Encryption.EncryptPassword(employee.PassWord);

            var result = _userRepo.Update(employee);
            return Json(result == 1 ? new { status = 1, message = "Cập nhật thành công" } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            User user = (User)Session[CommonConstants.USER_SESSION];
            var emp = _userRepo.GetById(id);
            emp.ModifyBy = user.id;
            var result = _userRepo.Delete(id);
            return Json(result == 1 ? new { status = 1, message = "Xóa thành công" } : new { status = 1, message = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CheckDuplicatedUserName(string UserName, int? id)
        {
            var name = _userRepo.GetByUserName(UserName, id);
            return Json(name == null, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel employee)
        {
            User em = (User)Session[CommonConstants.USER_SESSION];
            var userInfo = _userRepo.GetById(em.id);
            if (userInfo.PassWord != Encryption.EncryptPassword(employee.OldPassWord)) return Json(new { status = 0, message = "Kiểm tra lại mật khẩu cũ" }, JsonRequestBehavior.AllowGet);

            employee.NewPassWord = Encryption.EncryptPassword(employee.NewPassWord);
            var result = _userRepo.ChangePassword(em.id, employee.NewPassWord);
            return Json(result == 1 ? new { status = 1, message = "Cập nhật thành công" } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ResetPassword(int id)
        {
            var newPassword = "123456789";
            var encryptPassword = Encryption.EncryptPassword(newPassword);
            var result = _userRepo.ChangePassword(id, encryptPassword);
            return Json(result == 1 ? new { status = 1, message = "Mật khẩu mới là: "+  newPassword } : new { status = 0, message = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
        }
    }
}