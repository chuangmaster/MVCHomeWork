using System;
using System.Linq;
using System.Collections.Generic;

namespace MVCHomeWork.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All();
        }
        public 客戶聯絡人 Find(int id)
        {
            return base.All().FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<客戶聯絡人> GetTop100()
        {
            return All().OrderByDescending(c => c.Id).Take(100);
        }
    }
    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}