using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bjsjhp.cn.Controllers
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
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult News(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult News_Show(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Produce_()
        {
            return View();
        }
        public ActionResult Product(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Product_Show(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
    }
}