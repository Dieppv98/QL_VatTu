using DKAC.Common;
using DKAC.Models.EntityModel;
using DKAC.Repository;
using FlexCel.Core;
using FlexCel.Pdf;
using FlexCel.Render;
using FlexCel.XlsAdapter;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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


        protected ActionResult ViewReport(ExcelFile xls, string fileName, bool? exportExcel)
        {
            try
            {
                if (exportExcel != null && exportExcel == true)
                {
                    if (xls == null)
                        return new EmptyResult();

                    using (var ms = new MemoryStream())
                    {
                        xls.Save(ms);
                        ms.Position = 0;
                        return File(ms.ToArray(), "application/vnd.xlsx", fileName + ".xlsx");
                    }
                }

                if (xls == null)
                    return new EmptyResult();

                using (var pdf = new FlexCelPdfExport())
                {
                    pdf.Workbook = xls;
                    pdf.FontEmbed = TFontEmbed.Embed;
                    pdf.FontMapping = TFontMapping.ReplaceAllFonts;
                    using (var ms = new MemoryStream())
                    {
                        pdf.BeginExport(ms);
                        pdf.ExportAllVisibleSheets(false, "BaoCao");
                        pdf.EndExport();
                        ms.Position = 0;
                        return File(ms.ToArray(), "application/pdf");
                    }
                }
            }
            catch (Exception ex)
            {
                return new EmptyResult();
            }
        }

        protected ActionResult ExportPDF(ExcelFile xls, string fileName, bool? exportExcel)
        {
            try
            {
                if (xls == null)
                    return new EmptyResult();

                using (var pdf = new FlexCelPdfExport())
                {
                    pdf.Workbook = xls;
                    pdf.FontEmbed = TFontEmbed.Embed;
                    pdf.FontMapping = TFontMapping.ReplaceAllFonts;
                    using (var ms = new MemoryStream())
                    {
                        pdf.BeginExport(ms);
                        pdf.ExportAllVisibleSheets(false, "BaoCao");
                        pdf.EndExport();
                        ms.Position = 0;
                        return File(ms.ToArray(), "application/pdf", fileName + ".pdf");
                    }
                }
            }
            catch (Exception ex)
            {
                return new EmptyResult();
            }
        }

        protected XlsFile CreateXlsFile(ExcelPackage excel)
        {
            var result = new XlsFile(true);
            try
            {
                using (var memoryStream = new MemoryStream(excel.GetAsByteArray()))
                {
                    result.Open(memoryStream);
                }
                //Cách này để sau dùng nếu cần
                //string path = Server.MapPath("~/ExcelTemp/" + Guid.NewGuid() + ".xls");
                //FileInfo file = new FileInfo(path);
                //excel.SaveAs(file);
                //result.Open(file.FullName);
                //Log.Fatal("Tao file thanh cong");
            }
            finally
            {
                ReleaseObjectReport(excel.Workbook);
                excel.Dispose();
            }
            return result;
        }

        protected void ReleaseObjectReport(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}