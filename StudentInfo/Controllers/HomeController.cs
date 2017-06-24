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

        [HttpPost]
        public JsonResult GetSheng(int? id)
        {
            List<sheng> list = new List<sheng>();
            string[] names = { "山西", "北京", "河北", "陕西", "河南", "广东", "深圳", "上海", "四川", "哈尔冰", "黑龙江", "吉林" };
            for (int i = 1; i < 11; i++)
            {

                list.Add(new sheng(i, names[i]));
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}