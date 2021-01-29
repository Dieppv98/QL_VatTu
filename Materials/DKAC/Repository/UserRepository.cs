using DKAC.IRepository;
using DKAC.Models.EntityModel;
using DKAC.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace DKAC.Repository
{
    public class UserRepository : IUserRepository
    {
        MaterialsContext db = new MaterialsContext();
        public int Add(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.ModifyDate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public int Delete(int id)
        {
            var data = db.Users.Where(x => x.id == id).FirstOrDefault();
            if (data == null) { return 0; }
            data.ModifyDate = DateTime.Now;
            data.IsDeleted = 1;
            db.SaveChanges();
            return 1;
        }

        public User GetById(int? id)
        {
            var data = db.Users.Where(x => x.IsDeleted == 0 && x.id == id).FirstOrDefault();
            if (data == null) { return new User(); }
            return data;
        }

        public UserRequestModel GetByUser(string KeySearch, int page, int pageSize)
        {
            UserRequestModel request = new UserRequestModel();
            List<User> lst = new List<User>();
            request.page = page;
            request.pageSize = pageSize;
            lst = db.Users.Where(x => x.IsDeleted == 0 && x.Position != "admin").ToList();
            int startRow = (page - 1) * pageSize;
            if (!string.IsNullOrEmpty(KeySearch))
            {
                KeySearch = KeySearch.ToLowerInvariant();
                lst = lst.Where(x => x.FullName.ToLowerInvariant().Contains(KeySearch) || 
                                     x.UserName.ToLowerInvariant().Contains(KeySearch)).ToList();
            }
            request.totalRecord = lst.Count;
            request.data = lst.OrderBy(x => x.id)
                              .Skip(startRow)
                              .Take(pageSize)
                              .ToList();
            int totalPage = 0;
            if (request.totalRecord % pageSize == 0)
            {
                totalPage = request.totalRecord / pageSize;
            }
            else
            {
                totalPage = request.totalRecord / pageSize + 1;
            }
            request.totalPage = totalPage;
            return request;
        }

        public int Update(User user)
        {
            var data = db.Users.Where(x => x.id == user.id).FirstOrDefault();
            if (data == null || data.UserName != user.UserName) { return 0; }
            data.id = user.id;
            data.FullName = user.FullName;
            data.UserName = user.UserName;
            data.Birthday = user.Birthday;
            data.Gender = user.Gender;
            data.CMND = user.CMND;
            data.Email = user.Email;
            data.Tel = user.Tel;
            data.Address = user.Address;
            data.IsDeleted = 0;
            data.UserGroupId = user.UserGroupId;
            data.PassWord = user.PassWord;
            data.ModifyDate = user.ModifyDate;
            data.ModifyBy = user.ModifyBy;
            db.SaveChanges();
            return 1;
        }

        public int EditInfo(User user)
        {
            var data = db.Users.Where(x => x.id == user.id).FirstOrDefault();
            if (data == null || data.UserName != user.UserName) { return 0; }
            data.id = user.id;
            data.FullName = user.FullName;
            data.Birthday = user.Birthday;
            data.Gender = user.Gender;
            data.CMND = user.CMND;
            data.Email = user.Email;
            data.Tel = user.Tel;
            data.Address = user.Address;
            data.IsDeleted = 0;
            data.ModifyDate = user.ModifyDate;
            data.ModifyBy = user.ModifyBy;
            db.SaveChanges();
            return 1;
        }

        public User GetByUserName(string UserName, int? id)
        {
            var data = db.Users.Where(x => x.IsDeleted == 0 && x.id != id && x.UserName == UserName).FirstOrDefault();
            return data;
        }
        public int ChangePassword(int id, string PassWord)
        {
            var data = db.Users.Where(x => x.id == id).FirstOrDefault();
            data.PassWord = "hoandeptrai";
            data.ModifyDate = DateTime.Now;
            db.SaveChanges();
            return 1;
        }
    }
}