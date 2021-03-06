using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace MVCHomeWork.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }
        public 客戶聯絡人 Find(int id)
        {
            return base.All().FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<客戶聯絡人> GetTop100(string sortBy = "", string sortDirection = "")
        {
            var orderBy = string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortDirection) ? "Id Desc" : string.Format($"{sortBy} {sortDirection}");
            return All().OrderBy(orderBy).Take(100);
        }

        public List<客戶聯絡人> Search(string keyword)
        {
            return All().ToList().FindAll(x =>
            (x.職稱 != null && x.職稱.Contains(keyword)) ||
            (x.姓名.Contains(keyword) && x.姓名 != null) ||
            (x.Email != null && x.Email.Contains(keyword)) ||
            (x.手機 != null && x.手機.ToString().Contains(keyword)) ||
            (x.電話 != null && x.電話.Contains(keyword)) ||
            (x.客戶資料 != null & x.客戶資料.客戶名稱 != null && x.客戶資料.客戶名稱.Contains(keyword)));
        }
    }
    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}