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

namespace MVCHomeWork.Controllers
{
    public class BankController : BaseController
    {

        // GET: Bank
        public ActionResult Index(string keyword)
        {
            List<客戶銀行資訊> 客戶銀行資訊 = null;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                客戶銀行資訊 = _BankRepository.GetTop100().ToList();
            }
            else
            {
                客戶銀行資訊 = _BankRepository.Search(keyword);
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = _BankRepository.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Bank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                _BankRepository.Add(客戶銀行資訊);
                _BankRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(_BankRepository.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = _BankRepository.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(_CustomerRepository.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: Bank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                var data = _BankRepository.Find(客戶銀行資訊.Id);
                data.銀行名稱 = 客戶銀行資訊.銀行名稱;
                data.銀行代碼 = 客戶銀行資訊.銀行代碼;
                data.分行代碼 = 客戶銀行資訊.分行代碼;
                data.帳戶名稱 = 客戶銀行資訊.帳戶名稱;
                data.帳戶號碼 = 客戶銀行資訊.帳戶號碼;
                _BankRepository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(_BankRepository.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.AllowGet);
            }
            客戶銀行資訊 客戶銀行資訊 = _BankRepository.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶銀行資訊資料" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = HttpStatusCode.OK, result = true, data = JsonConvert.SerializeObject(客戶銀行資訊) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "id不得為空值" }, JsonRequestBehavior.DenyGet);
            }
            客戶銀行資訊 客戶銀行資訊 = _BankRepository.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return Json(new { code = HttpStatusCode.BadRequest, result = false, message = "找不到客戶銀行資訊資料" }, JsonRequestBehavior.DenyGet);

            }
            客戶銀行資訊.是否已刪除 = true;
            _BankRepository.UnitOfWork.Commit();
            return Json(new { code = HttpStatusCode.OK, result = true }, JsonRequestBehavior.DenyGet);
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
