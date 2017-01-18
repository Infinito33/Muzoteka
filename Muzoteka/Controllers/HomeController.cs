using Muzoteka.App_Start;
using Muzoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muzoteka.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(new utwor());
        }

        [AllowAnonymous]
        public ActionResult Search()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "O autorze";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Strona kontaktowa";
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            ViewBag.Message = "Witaj w Muzoteka!";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}