using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVCHomeWork.Models
{   
	public  class V_客戶統計資料Repository : EFRepository<V_客戶統計資料>, IV_客戶統計資料Repository
	{
        public override IQueryable<V_客戶統計資料> All()
        {
            return base.All();
        }
    }

	public  interface IV_客戶統計資料Repository : IRepository<V_客戶統計資料>
	{

	}
}