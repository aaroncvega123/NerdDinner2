using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner2.Models;

namespace NerdDinner2.Controllers
{
    public class HomeController : Controller
    {
        public NerdDinnersDataContext nerdDinners = new NerdDinnersDataContext();

        public ActionResult Index()
        {
            var dinners = from d in nerdDinners.Dinners
                where d.EventDate > DateTime.Now
                select d;

            return View(dinners.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                nerdDinners.Dinners.Add(dinner);
                nerdDinners.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dinner);
        }
    }
}