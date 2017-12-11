using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHomeWork.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().OrderByDescending(c => c.Id);
        }
        public IQueryable<客戶資料> GetTop100()
        {
            return All().Take(100);
        }
        public 客戶資料 Find(int id)
        {
            var result = base.All().FirstOrDefault(x=>x.Id==id);
            return result;
        }

    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}