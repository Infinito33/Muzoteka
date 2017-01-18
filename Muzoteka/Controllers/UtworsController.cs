using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Muzoteka.Models;
using Muzoteka.App_Start;

namespace Muzoteka.Controllers
{
    public class UtworsController : Controller
    {
        private muzotekaEntities db = new muzotekaEntities();

        [Authorize]
        public ActionResult Index()
        {
            var utwor = db.utwor.Include(u => u.album).Include(u => u.playlista).Include(u => u.wykonawca);
            return View(utwor.ToList());
        }

        [AllowAnonymous]
        public ActionResult SearchedTracks(string trackName)
        {
            if(trackName.Equals(string.Empty))
            {
                RedirectToAction("Index", "Home");
            }
            using (muzotekaEntities db = new muzotekaEntities())
            {                
                List<utwor> utwors = db.utwor.Where(t => t.nazwa.ToLower().Contains(trackName.ToLower())).Include(u => u.album).Include(u => u.playlista).Include(u => u.wykonawca).Take(10).ToList();
                return View(utwors);
            }


        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // GET: Utwors/Create
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create()
        {
            ViewBag.idalbum = new SelectList(db.album, "id", "nazwa");
            ViewBag.idplaylista = new SelectList(db.playlista, "id", "nazwa");
            ViewBag.idwyk = new SelectList(db.wykonawca, "id", "imie");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create([Bind(Include = "id,nazwa,gatunek,dlugosc,link,dddb,idalbum,idwyk,idplaylista")] utwor utwor)
        {
            if (ModelState.IsValid)
            {
                db.utwor.Add(utwor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idalbum = new SelectList(db.album, "id", "nazwa", utwor.idalbum);
            ViewBag.idplaylista = new SelectList(db.playlista, "id", "nazwa", utwor.idplaylista);
            ViewBag.idwyk = new SelectList(db.wykonawca, "id", "imie", utwor.idwyk);
            return View(utwor);
        }

        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idalbum = new SelectList(db.album, "id", "nazwa", utwor.idalbum);
            ViewBag.idplaylista = new SelectList(db.playlista, "id", "nazwa", utwor.idplaylista);
            ViewBag.idwyk = new SelectList(db.wykonawca, "id", "pseudonim", utwor.idwyk);
            return View(utwor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit([Bind(Include = "id,nazwa,gatunek,dlugosc,link,dddb,idalbum,idwyk,idplaylista")] utwor utwor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utwor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idalbum = new SelectList(db.album, "id", "nazwa", utwor.idalbum);
            ViewBag.idplaylista = new SelectList(db.playlista, "id", "nazwa", utwor.idplaylista);
            ViewBag.idwyk = new SelectList(db.wykonawca, "id", "imie", utwor.idwyk);
            return View(utwor);
        }

        [AuthorizeRoles("ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            utwor utwor = db.utwor.Find(id);
            db.utwor.Remove(utwor);
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
