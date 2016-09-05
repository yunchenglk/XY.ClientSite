using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace longsenkangju.com.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Products_list(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult Products_Show(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult News_list(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult News_show(string id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult About(string id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}