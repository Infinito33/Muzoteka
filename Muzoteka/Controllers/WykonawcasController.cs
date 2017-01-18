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
using Muzoteka.App_Start;

namespace Muzoteka.Controllers
{
    public class WykonawcasController : Controller
    {
        private muzotekaEntities db = new muzotekaEntities();

        // GET: Wykonawcas
        [Authorize]
        public ActionResult Index()
        {
            return View(db.wykonawca.ToList());
        }

        // GET: Wykonawcas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wykonawcaViewModel = new WykonawcaViewModel()
            {
                wykonawca = db.wykonawca.Include(i => i.album).Include(i => i.utwor).First(i => i.id == id),
            };

            if (wykonawcaViewModel.wykonawca == null)
                return HttpNotFound();

            //var allAlbumsList = db.album.ToList();
            //wykonawcaViewModel.AllAlbums = allAlbumsList.Select(o => new SelectListItem
            //{
            //    Text = o.nazwa,
            //    Value = o.id.ToString()
            //});

            return View(wykonawcaViewModel);
        }

        // GET: Wykonawcas/Create
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create()
        {
            WykonawcaViewModel wykonawcaViewModel = new WykonawcaViewModel()
            {
                wykonawca = new wykonawca()
            };
            wykonawcaViewModel.wykonawca.dddb = DateTime.Now;

            var allAlbumsList = db.album.ToList();
            wykonawcaViewModel.AllAlbums = allAlbumsList.Select(o => new SelectListItem
            {
                Text = o.nazwa,
                Value = o.id.ToString()
            });

            var allUtworsList = db.utwor.ToList();
            wykonawcaViewModel.AllUtwors = allUtworsList.Select(u => new SelectListItem
            {
                Text = u.nazwa,
                Value = u.id.ToString()
            });

            return View(wykonawcaViewModel);
        }

        // POST: Wykonawcas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create(WykonawcaViewModel wykonawcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var selectedAlbums = db.album.Where(m => wykonawcaViewModel.SelectedAlbums.Contains(m.id)).ToList();
                wykonawcaViewModel.wykonawca.album = (ICollection<album>)selectedAlbums;

                var selectedUtwors = db.utwor.Where(m => wykonawcaViewModel.SelectedUtwors.Contains(m.id)).ToList();
                wykonawcaViewModel.wykonawca.utwor = (ICollection<utwor>)selectedUtwors;

                db.wykonawca.Add(wykonawcaViewModel.wykonawca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wykonawcaViewModel);
        }

        // GET: Wykonawcas/Edit/5
        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wykonawcaViewModel = new WykonawcaViewModel()
            {
                wykonawca = db.wykonawca.Include(i => i.album).Include(i => i.utwor).First(i => i.id == id)
            };

            if(wykonawcaViewModel == null)
            {
                return HttpNotFound();
            }

            var allAlbumsList = db.album.ToList();
            wykonawcaViewModel.AllAlbums = allAlbumsList.Select(o => new SelectListItem
            {
                Text = o.nazwa,
                Value = o.id.ToString()
            });

            var allUtworsList = db.utwor.ToList();
            wykonawcaViewModel.AllUtwors = allUtworsList.Select(u => new SelectListItem
            {
                Text = u.nazwa,
                Value = u.id.ToString()
            });

            return View(wykonawcaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit(WykonawcaViewModel wykonawcaView)
        {
            if (wykonawcaView == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var wykonawcaToUpdate = db.wykonawca.Include(i => i.album).Include(i => i.utwor).First(i => i.id == wykonawcaView.wykonawca.id);

                if (TryUpdateModel(wykonawcaToUpdate, "wykonawca", new string[] { "pseudonim", "imie", "nazwisko", "data_ur", "opis" }))
                {
                    var newAlbums = db.wykonawca.Where(m => wykonawcaView.SelectedAlbums.Contains(m.id)).ToList();
                    var updatedAlbums = new HashSet<int>(wykonawcaView.SelectedAlbums);
                    foreach (album a in db.album)
                    {
                        if (!updatedAlbums.Contains(a.id))
                        {
                            wykonawcaToUpdate.album.Remove(a);
                        }
                        else
                        {
                            wykonawcaToUpdate.album.Add((a));
                        }
                    }

                    var newUtwors = db.wykonawca.Where(m => wykonawcaView.SelectedUtwors.Contains(m.id)).ToList();
                    var updatedUtwors = new HashSet<int>(wykonawcaView.SelectedUtwors);
                    foreach (utwor u in db.utwor)
                    {
                        if (!updatedUtwors.Contains(u.id))
                        {
                            wykonawcaToUpdate.utwor.Remove(u);
                        }
                        else
                        {
                            wykonawcaToUpdate.utwor.Add(u);
                        }
                    }

                    db.Entry(wykonawcaToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(wykonawcaView);
        }

        // GET: Wykonawcas/Delete/5
        [AuthorizeRoles("ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wykonawca wykonawca = db.wykonawca.Find(id);
            if (wykonawca == null)
            {
                return HttpNotFound();
            }
            return View(wykonawca);
        }

        // POST: Wykonawcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            wykonawca wykonawca = db.wykonawca.Where(w => w.id == id).Include(w => w.album).First();
            wykonawca.album = new HashSet<album>();
            db.Entry(wykonawca).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.wykonawca.Remove(wykonawca);
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
