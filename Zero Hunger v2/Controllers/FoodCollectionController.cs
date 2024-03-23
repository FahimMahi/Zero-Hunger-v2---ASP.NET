using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zero_Hunger_v2.Controllers
{
    public class FoodCollectionController : Controller
    {
        // GET: FoodCollection
        public ActionResult Index()
        {
            return View();
        }
    }
}