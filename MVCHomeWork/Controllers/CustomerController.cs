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
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index(string keyword, string category, string sortBy = "", string sortDirection = "")
        {
            List<客戶資料> 客戶資料 = null;
            if (string.IsNullOrWhiteSpace(keyword) && string.IsNullOrWhiteSpace(category))
            {
                客戶資料 = _CustomerRepository.GetTop100(sortBy, sortDirection).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(category))
            {
                客戶資料 = _CustomerRepository.Search(category);
            }
            else
            {
                客戶資料 = _CustomerRepository.Search(keyword);
            }
            var list = _CustomerRepository.All().ToList();
            var customerCategory = (from c in list where !string.IsNullOrWhiteSpace(c.客戶分類) select new { 客戶分類 = c.客戶分類.Trim() }).Distinct().ToList();
            customerCategory.Add(new { 客戶分類 = string.Empty });
            ViewBag.客戶分類 = new SelectList(customerCategory, "客戶分類", "客戶分類", selectedValue: string.Empty);
            ViewBag.sortBy = GetSortDirectionSelectList();
            ViewBag.sortDirection = GetSortBySelectList();
            TempData["xlsTemp"] = 客戶資料;
            return View(客戶資料);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = _CustomerRepository.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                _CustomerRepository.Add(客戶資料);
                _CustomerRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = _CustomerRepository.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料 data = _CustomerRepository.Find(客戶資料.Id);
                data.傳真 = 客戶資料.傳真;
                data.地址 = 客戶資料.地址;
                data.客戶名稱 = 客戶資料.客戶名稱;
                data.統一編號 = 客戶資料.統一編號;
                data.電話 = 客戶資料.電話;
                data.客戶分類 = 客戶資料.客戶分類;

                _CustomerRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        //// GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.AllowGet);
            }
            客戶資料 客戶資料 = _CustomerRepository.Find(id.Value);
            if (客戶資料 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶資料" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = HttpStatusCode.OK, result = true, data = JsonConvert.SerializeObject(客戶資料) }, JsonRequestBehavior.AllowGet);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.DenyGet);
            }
            客戶資料 客戶資料 = _CustomerRepository.Find(id.Value);
            if (客戶資料 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶資料" }, JsonRequestBehavior.DenyGet);

            }
            客戶資料.是否已刪除 = true;
            _CustomerRepository.UnitOfWork.Commit();
            return Json(new { code = HttpStatusCode.OK, result = true }, JsonRequestBehavior.DenyGet);
        }
        public ActionResult ExportExcel()
        {
            IEnumerable<dynamic> contactData = null;
            if (TempData["xlsTemp"] != null)
            {
                contactData = TempData["xlsTemp"] as IEnumerable<客戶資料>;
                contactData = contactData.Select(x =>
                new { Id = x.Id, 客戶名稱 = x.客戶名稱, 統一編號 = x.統一編號, 電話 = x.電話, 傳真 = x.傳真, 地址 = x.地址, 客戶分類 = x.客戶分類 });
            }
            else
            {
                contactData = _CustomerRepository.All().Select(x =>
                new { Id = x.Id, 客戶名稱 = x.客戶名稱, 統一編號 = x.統一編號, 電話 = x.電話, 傳真 = x.傳真, 地址 = x.地址, 客戶分類 = x.客戶分類 }).AsEnumerable<dynamic>();
            }
            var header = new List<string>() { "Id", "姓名", "職稱", "手機", "電話" };
            var fileName = ExcelExportService.Export(Server.MapPath("~/App_Data"), "客戶資料", header, contactData);
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportFile.xlsx");
        }
        private SelectList GetSortBySelectList()
        {
            var items = new List<dynamic>() { new { key = "asc", desc = "升冪" }, new { key = "desc", desc = "降冪" } };
            var selectList = new SelectList(items, "key", "desc");

            var selected=  selectList.Where(x => x.Value == "desc").FirstOrDefault();
            selected.Selected = true;
            return selectList;

        }
        private SelectList GetSortDirectionSelectList()
        {
            var items = new List<dynamic>() {
                new { key = "客戶名稱", desc = "客戶名稱" },
                new { key = "統一編號", desc = "統一編號" },
                new { key = "Email", desc = "Email" },
                new { key = "電話", desc = "電話" },
                new { key = "傳真", desc = "傳真" },
                new { key = "地址", desc = "地址" },
                new { key = "客戶分類", desc = "客戶分類" },

            };
            var selectList = new SelectList(items, "key", "desc");

            foreach (var item in selectList)
            {
                if (item.Value == "客戶名稱")
                    item.Selected = true;
            }
            return selectList;
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
