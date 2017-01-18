using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Muzoteka.Models;
using Muzoteka.ViewModel;

namespace Muzoteka.Controllers
{
    public class PlaylistasController : Controller
    {
        private muzotekaEntities db = new muzotekaEntities();

        [Authorize]
        public ActionResult Index()
        {
            var playlista = db.playlista.Where(p => p.uzytkownik.login.Equals(HttpContext.User.Identity.Name));
            return View(playlista.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            playlista playlista = db.playlista.Find(id);
            if (playlista == null)
            {
                return HttpNotFound();
            }
            return View(playlista);
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.iduzytkownik = db.uzytkownik.Where(u => u.login.Equals(HttpContext.User.Identity.Name)).First().id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "id,nazwa,data_utworzenia,opis")] playlista playlista)
        {
            playlista.iduzytkownik = db.uzytkownik.Where(u => u.login.Equals(HttpContext.User.Identity.Name)).First().id;
            if (ModelState.IsValid)
            {
                playlista.data_utworzenia = DateTime.Now;
                db.playlista.Add(playlista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iduzytkownik = playlista.iduzytkownik;
            return View(playlista);
        }

        [Authorize]
        public ActionResult InsertUtwor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistaUtworViewModel data = new PlaylistaUtworViewModel();

            utwor utwor = db.utwor.Where(u => u.id == id).First();
            var playlistas = db.playlista.Where(p => p.uzytkownik.login.Equals(HttpContext.User.Identity.Name)).ToList();

            data.utwor = utwor;
            data.playlistas = playlistas;
            data.chosenPlaylista = new playlista();

            HttpContext.Session.Add("utworId", utwor.id);

            var allPlaylistasList = db.playlista.Where(p => p.uzytkownik.login.Equals(HttpContext.User.Identity.Name)).ToList();
            data.AllPlaylistas = allPlaylistasList.Select(o => new SelectListItem
            {
                Text = o.nazwa,
                Value = o.id.ToString()
            });

            return View(data);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult InsertUtwor(PlaylistaUtworViewModel data)
        {
            var utworId = HttpContext.Session["utworId"];
            var selectedPlaylistas = db.playlista.Where(m => data.SelectedPlaylistas.Contains(m.id)).ToList();
            var playlistaList = (ICollection<playlista>)selectedPlaylistas;
            playlista playlista = playlistaList.First();

            utwor utwor = db.utwor.Where(u => u.id == (int)utworId).First();

            playlista.utwor.Add(utwor);

            db.Entry(playlista).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Utwors", new { });
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            playlista playlista = db.playlista.Find(id);
            if (playlista == null)
            {
                return HttpNotFound();
            }
            ViewBag.iduzytkownik = db.uzytkownik.Where(u => u.login.Equals(HttpContext.User.Identity.Name)).First().id;
            HttpContext.Session.Add("playlistaTime", playlista.data_utworzenia);
            return View(playlista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "id,nazwa,data_utworzenia,opis")] playlista playlista)
        {
            playlista.iduzytkownik = db.uzytkownik.Where(u => u.login.Equals(HttpContext.User.Identity.Name)).First().id;
            if (ModelState.IsValid)
            {
                playlista.data_utworzenia = (DateTime)HttpContext.Session["playlistaTime"];
                db.Entry(playlista).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iduzytkownik = new SelectList(db.uzytkownik, "id", "login", playlista.iduzytkownik);
            return View(playlista);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            playlista playlista = db.playlista.Find(id);
            if (playlista == null)
            {
                return HttpNotFound();
            }
            return View(playlista);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            playlista playlista = db.playlista.Where(p => p.id == id).Include(p => p.utwor).First();
            playlista.utwor = new HashSet<utwor>();
            db.Entry(playlista).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.playlista.Remove(playlista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
