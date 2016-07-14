using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fangjingdian.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Product_Show(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        //企业形象/荣誉证书
        public ActionResult Support(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult News(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Case()
        {
            return View();
        }
        public ActionResult Solution()
        {
            return View();
        }
    }
}