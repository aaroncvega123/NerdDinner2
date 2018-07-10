using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using NerdDinner2.Models;
using NerdDinner2.ViewModels;

namespace NerdDinner2.Controllers
{
    public class DinnersController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: Dinners
        /*public ActionResult Index()
        {
            var dinners = dinnerRepository.FindAllDinners().ToList();

            return View("Index", dinners);
        }*/

        public ActionResult Index(int? page)
        {
            const int pageSize = 10;
            var upcomingDinners = dinnerRepository.FindUpcomingDinners();
            /*var paginatedDinners = upcomingDinners.Skip((page ?? 0) * pageSize)
                                                        .Take(pageSize)
                                                        .ToList();*/
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);

            return View(paginatedDinners);
        }

        //
        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View("Details", dinner);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(new DinnerFormViewModel(dinner));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            try
            {

                UpdateModel(dinner);

                dinnerRepository.Save();

                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            catch
            {

                /*foreach (var issue in dinner.GetRuleViolations())
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }*/

                return View(new DinnerFormViewModel(dinner));
            }
        }

        //
        // GET: /Dinners/Create

        public ActionResult Create()
        {

            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };

            return View(new DinnerFormViewModel(dinner));
        }

        //
        //
        // POST: /Dinners/Create

        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {

            if (ModelState.IsValid)
            {
                dinner.HostedBy = User.Identity.Name;
                dinner.DinnerID = dinnerRepository.GetUnusedDinnerID();
                dinnerRepository.Add(dinner);
                dinnerRepository.Save();

                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }

            Dinner newDinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };

            return View(new DinnerFormViewModel(newDinner));
        }

        public ActionResult Delete(int id)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            if (dinner == null)
                return View("NotFound");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }

    }
}