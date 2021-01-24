using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DKAC.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult NotPermission()
        {
            return View();
        }
    }
}