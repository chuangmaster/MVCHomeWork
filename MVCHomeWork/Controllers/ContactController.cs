using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork.Models;
using Newtonsoft.Json;
using MVCHomeWork.Service;

namespace MVCHomeWork.Controllers
{
    public class ContactController : BaseController
    {
        //private CustomerEntities db = new CustomerEntities();

        // GET: Contact
        public ActionResult Index(string keyword, string job, string sortBy="", string sortDirection="")
        {
            List<客戶聯絡人> 客戶聯絡人 = null;
            if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(job))
            {
                客戶聯絡人 = _ContactRepository.GetTop100(sortBy, sortDirection).ToList();
            }
            else if(!string.IsNullOrWhiteSpace(job))
            {
                客戶聯絡人 = _ContactRepository.Search(job);
            }
            else
            {
                客戶聯絡人 = _ContactRepository.Search(keyword);
            }
            var list = _ContactRepository.All().ToList();
            var jobCategory = (from item in list select new { 職稱分類 = item.職稱 }).Distinct().ToList();
            jobCategory.Add(new { 職稱分類 = "" });
            ViewBag.職稱分類 = new SelectList(jobCategory, "職稱分類", "職稱分類", selectedValue: "");
            ViewBag.sortBy = new SelectList(GetSortDirection(), "key", "desc", selectedValue: "姓名");
            ViewBag.sortDirection = new SelectList(GetSortBy(), "key", "desc", selectedValue: "desc");
            
            TempData["xlsTemp"] = 客戶聯絡人;
            return View(客戶聯絡人);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = _ContactRepository.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                _ContactRepository.Add(客戶聯絡人);
                _ContactRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱");
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = _ContactRepository.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人 data = _ContactRepository.Find(客戶聯絡人.客戶Id);
                data.職稱 = 客戶聯絡人.職稱;
                data.姓名 = 客戶聯絡人.姓名;
                data.Email = 客戶聯絡人.Email;
                data.手機 = 客戶聯絡人.手機;
                data.電話 = 客戶聯絡人.電話;
                _ContactRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.AllowGet);
            }
            客戶聯絡人 客戶聯絡人 = _ContactRepository.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶聯絡人資料" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = HttpStatusCode.OK, result = true, data = JsonConvert.SerializeObject(客戶聯絡人) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.DenyGet);
            }
            客戶聯絡人 客戶聯絡人 = _ContactRepository.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶聯絡人資料" }, JsonRequestBehavior.DenyGet);

            }
            客戶聯絡人.是否已刪除 = true;
            _ContactRepository.UnitOfWork.Commit();
            return Json(new { code = HttpStatusCode.OK, result = true }, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ExportExcel()
        {
            IEnumerable<dynamic> contactData = null;
            if (TempData["xlsTemp"] != null)
            {
                contactData = TempData["xlsTemp"] as IEnumerable<客戶聯絡人>;
                contactData = contactData.Select(x =>
                new { Id=x.Id, 姓名 = x.姓名, 職稱 = x.職稱, 手機 = x.手機, 電話 = x.電話 });
            }
            else
            {
                contactData = _ContactRepository.All().Select(x =>
                new { Id = x.Id, 姓名 = x.姓名, 職稱 = x.職稱, 手機 = x.手機, 電話 = x.電話 }).AsEnumerable<dynamic>();
            }
            var header = new List<string>() { "Id", "姓名", "職稱", "手機", "電話" };
            var fileName = ExcelExportService.Export(Server.MapPath("~/App_Data"), "客戶聯絡人", header, contactData);
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportFile.xlsx");
        }

        private List<dynamic> GetSortBy()
        {
            return new List<dynamic>() { new {key= "asc", desc= "升冪" }, new { key = "desc", desc = "降冪" } };
        }
        private List<dynamic> GetSortDirection()
        {
            return new List<dynamic>() {
                new { key = "職稱", desc = "職稱" },
                new { key = "姓名", desc = "姓名" },
                new { key = "Email", desc = "Email" },
                new { key = "手機", desc = "手機" },
                new { key = "電話", desc = "電話" },
                new { key = "客戶id", desc = "客戶名稱" },
            };
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}
