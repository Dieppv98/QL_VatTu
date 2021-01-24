using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.InfoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.Repository
{
    public class LoginRepository : ILoginRepository
    {
        MaterialsContext db = new MaterialsContext();
        public bool Login(string UserName, string Password)
        {
            var resultU = db.Users.Where(x => x.IsDeleted == 0 && x.UserName == UserName && x.PassWord == Password).FirstOrDefault();
            if (resultU != null) return true;
            return false;
        }

        public User GetUserByUserName(string userName)
        {
            return db.Users.Where(x => x.IsDeleted == 0 && x.UserName == userName).FirstOrDefault();
        }

        public bool SignUp(User model)
        {
            try
            {
                User em = new User();
                em.FullName = model.FullName;
                em.UserName = model.UserName;
                em.PassWord = model.PassWord;
                em.CreatedDate = DateTime.Now;
                em.ModifyDate = DateTime.Now;
                em.IsDeleted = 0;
                db.Users.Add(em);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AccountModulPageInfo GetAccountModulPageInfo(int UserId)
        {
            var model = new AccountModulPageInfo();
            var rs = db.UserRoles.Where(x => x.UserId == UserId).ToList();
            var lstPerAction = new List<PermissionActionInfo>();
            var lstModul = new List<Modul>();
            var lstPage = new List<Page>();
            foreach (var item in rs)
            {
                var query = from pa in db.PermissionActions.Where(x => x.RoleId == item.RoleId && x.ActionKey != 0)
                            join p in db.Pages on pa.PageId equals p.id into lp
                            from lppa in lp.DefaultIfEmpty()
                            select new PermissionActionInfo()
                            {
                                id = pa.id,
                                PageId = pa.PageId,
                                PageName = lppa.PageName,
                                RoleId = pa.RoleId,
                                ActionKey = pa.ActionKey,
                                ModulId = lppa.ModulId,
                                Url = lppa.Url,
                            };
                lstPerAction.AddRange(query.ToList());
            }
            foreach (var item in lstPerAction)
            {
                var modul = db.Moduls.FirstOrDefault(x => x.id == item.ModulId);
                var page = db.Pages.Where(x => x.id == item.PageId).FirstOrDefault();
                if (modul != null) lstModul.Add(modul);
                if (page != null) lstPage.Add(page);
            }
            model.ListPerAction = lstPerAction;
            model.ListModul = lstModul.Distinct().OrderBy(x => x.id).ToList();
            model.ListPage = lstPage.Distinct().OrderBy(x => x.id).ToList();
            return model;
        }

        public User GetByUserName(string UserName, int? id)
        {
            var data = db.Users.Where(x => x.IsDeleted == 0 && x.id != id && x.UserName == UserName).FirstOrDefault();
            return data;
        }
    }
}