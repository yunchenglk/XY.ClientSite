using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInfo.Controllers
{
    public class sheng
    {
        public int id { get; set; }
        public string name { get; set; }
        public sheng(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult StudentAdd()
        {
            return View();
        }
        public ActionResult upStudentFile()
        {
            return View();
        }
    }
}