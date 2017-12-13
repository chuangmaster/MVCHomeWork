﻿using System;
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
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index(string keyword)
        {
            List<客戶資料> 客戶資料 = null;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                客戶資料 = _CustomerRepository.GetTop100().ToList();
            }
            else
            {
                客戶資料 = _CustomerRepository.Search(keyword);
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }
    }
}
