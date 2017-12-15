using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;

namespace MVCHomeWork.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => x.是否已刪除 == false);
        }
        public IQueryable<客戶資料> GetTop100(string sortBy="", string sortDirection="")
        {
            var orderBy = string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortDirection) ? "Id Desc" : string.Format($"{sortBy} {sortDirection}");
            return All().OrderBy(orderBy).Take(100);
        }
        public 客戶資料 Find(int id)
        {
            var result = base.All().FirstOrDefault(x => x.Id == id);
            return result;
        }
        public List<客戶資料> Search(string keyword)
        {
            return All().ToList().FindAll(x =>
            (!string.IsNullOrWhiteSpace(x.客戶名稱 ) && x.客戶名稱.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.統一編號) && x.統一編號.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.電話) && x.電話.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.傳真) && x.傳真.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.地址) && x.地址.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.Email)&& x.Email.Contains(keyword)) ||
            (!string.IsNullOrWhiteSpace(x.客戶分類) && x.客戶分類.Contains(keyword)));
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}