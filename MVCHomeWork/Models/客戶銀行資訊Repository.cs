using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHomeWork.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(x => x.是否已刪除 == false); ;
        }
        public 客戶銀行資訊 Find(int id)
        {
            return base.All().FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<客戶銀行資訊> GetTop100()
        {
            return All().OrderByDescending(c => c.Id).Take(100);
        }
        public List<客戶銀行資訊> Search(string keyword)
        {
            return All().ToList().FindAll(x =>
            (x.帳戶名稱 != null && x.帳戶名稱.Contains(keyword)) ||
            (x.帳戶號碼 != null && x.帳戶號碼.Contains(keyword)) ||
            (x.銀行名稱 != null && x.銀行名稱.Contains(keyword)) ||
            (x.銀行代碼.ToString().Contains(keyword)) ||
            (x.客戶資料 != null && x.客戶資料.客戶名稱 != null && x.客戶資料.客戶名稱.Contains(keyword)));
        }
    }

    public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
    {

    }
}