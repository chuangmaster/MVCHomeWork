using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCHomeWork.Models;

namespace MVCHomeWork.Controllers
{
    public class CustomerStaticsController : BaseController
    {
        // GET: CustomerStatics
        public ActionResult Index()
        {
            return View(_CustomerStaticsRepository.All().ToList());
        }
    }
}
