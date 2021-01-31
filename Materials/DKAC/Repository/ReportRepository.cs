using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using DKAC.Models.RequestModel;
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
            var query = db.DonHang.Where(t => (string.IsNullOrEmpty(request.Keywords) || t.so_lenh_sx.Contains(request.Keywords)));
            totalRecord = query.Count();
            return query.OrderByDescending(x => x.id).Skip(pageIndex * recordPerPage).Take(recordPerPage).ToList();
        }
    }
}