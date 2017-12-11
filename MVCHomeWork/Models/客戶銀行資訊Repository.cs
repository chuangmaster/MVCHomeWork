using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All();
        }
        public 客戶銀行資訊 Find(int id)
        {
            return base.All().FirstOrDefault(c => c.Id == id);
        }
        public IQueryable<客戶銀行資訊> GetTop100()
        {
            return All().OrderByDescending(c => c.Id).Take(100);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}