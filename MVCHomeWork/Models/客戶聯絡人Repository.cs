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

        public List<客戶聯絡人> Search(string keyword)
        {
            return All().ToList().FindAll(x =>
            x.職稱.Contains(keyword) ||
            x.姓名.Contains(keyword) ||
            x.Email.Contains(keyword) ||
            x.手機.ToString().Contains(keyword) ||
            x.電話.Contains(keyword)||
            x.客戶資料.客戶名稱.Contains(keyword) 
            );
        }
    }
    public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
    {

    }
}