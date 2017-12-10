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
        public List<客戶資料> GetTop100()
        {
            var result = base.All().OrderByDescending(c => c.Id).Take(100).ToList();
            return result;
        }
        public 客戶資料 Find(int? id)
        {
            var result = base.All().First(x=>x.Id==id);
            return result;
        }

    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}