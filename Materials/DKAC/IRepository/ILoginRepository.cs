using DKAC.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKAC.IRepository
{
    public interface ILoginRepository
    {
        bool Login(string UserName, string Password);
        bool SignUp(User model);
        User GetByUserName(string UserName, int? id);
    }
}