using MVCHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Filter
{
    public class ContactShareDataAttribute : ActionFilterAttribute
    {
        CustomerEntities db = new CustomerEntities();
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var jobCategory = (from item in db.客戶聯絡人 select new { 職稱分類 = item.職稱 }).Distinct().ToList();
            jobCategory.Add(new { 職稱分類 = "" });
            filterContext.Controller.ViewBag.職稱分類 = new SelectList(jobCategory, "職稱分類", "職稱分類", selectedValue: "");
        }
    }
}