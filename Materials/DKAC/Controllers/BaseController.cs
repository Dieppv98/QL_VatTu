using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DKAC.Controllers
{
    public class BaseController : Controller
    {
        MaterialsContext db = new MaterialsContext();
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext fillterContext)
        {
            var user = (User)Session[CommonConstants.USER_SESSION];
            if (user == null)
            {
                fillterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            base.OnActionExecuting(fillterContext);
        }

        public bool CheckPermission(int pageId, int action, User currentUser)
        {
            var lstRole = db.UserRoles.Where(x => x.UserId == currentUser.id).ToList();
            List<PermissionAction> lstPer = new List<PermissionAction>();
            foreach (var item in lstRole)
            {
                var lstPerAction = db.PermissionActions.Where(x => x.RoleId == item.RoleId).ToList();
                if (lstPerAction != null) lstPer.AddRange(lstPerAction);
            }
            lstPer.Distinct().ToList();
            if (currentUser.UserName == "admin") return true;
            int actionkey = 7;
            int action1 = 4;
            var r = actionkey & (byte)action1;
            var check = lstPer.Any(x => x.PageId == pageId && (x.ActionKey & (byte)action) == (byte)action);
            return check;
        }

    }
}