using MVCHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHomeWork.Controllers
{
    public class BaseController : Controller
    {
        protected 客戶資料Repository _CustomerRepository = RepositoryHelper.Get客戶資料Repository();
        protected 客戶銀行資訊Repository _BankRepository = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶聯絡人Repository _ContactRepository = RepositoryHelper.Get客戶聯絡人Repository();
        protected V_客戶統計資料Repository _CustomerStaticsRepository = RepositoryHelper.GetV_客戶統計資料Repository();

    }
}