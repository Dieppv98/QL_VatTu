using DKAC.IRepository;
using DKAC.Models.EntityModel;
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

        public User GetByUserName(string UserName, int? id)
        {
            var data = db.Users.Where(x => x.IsDeleted == 0 && x.id != id && x.UserName == UserName).FirstOrDefault();
            return data;
        }
    }
}