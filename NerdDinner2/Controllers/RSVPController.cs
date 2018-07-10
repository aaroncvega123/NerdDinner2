using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner2.Models;

namespace NerdDinner2.Controllers
{
    public class RSVPController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        //
        // AJAX: /Dinners/RSVPForEvent/1

        [Authorize]
        public ActionResult Register(int id)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsUserRegistered(User.Identity.Name))
            {

                RSVP rsvp = new RSVP();
                rsvp.AttendeeEmail = User.Identity.Name;

                dinner.RSVPs.Add(rsvp);
                dinnerRepository.Save();
            }
            return View("~/Views/Dinners/Details.cshtml", dinner);
        }
    }
}