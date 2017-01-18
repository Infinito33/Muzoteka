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
    public class AlbumsController : Controller
    {
        private muzotekaEntities db = new muzotekaEntities();

        // GET: Albums
        [Authorize]
        public ActionResult Index()
        {
            return View(db.album.ToList());
        }

        // GET: Albums/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var albumViewModel = new AlbumViewModel()
            {
                album = db.album.Include(i => i.wykonawca).Include(u => u.utwor).First(i => i.id == id),
            };

            if (albumViewModel.album == null)
                return HttpNotFound();

            return View(albumViewModel);

        }

        // GET: Albums/Create
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create()
        {
            AlbumViewModel albumViewModel = new AlbumViewModel()
            {
                album = new album()
            };
            albumViewModel.album.dddb = DateTime.Now;

            var allWykonawcasList = db.wykonawca.ToList();
            albumViewModel.AllWykonawcas = allWykonawcasList.Select(o => new SelectListItem
            {
                Text = o.pseudonim,
                Value = o.id.ToString()
            });

            var allUtworsList = db.utwor.ToList();
            albumViewModel.AllUtwors = allUtworsList.Select(u => new SelectListItem
            {
                Text = u.nazwa,
                Value = u.id.ToString()
            });

            return View(albumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Create(AlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                var selectedWykonawcas = db.wykonawca.Where(m => albumViewModel.SelectedWykonawcas.Contains(m.id)).ToList();
                albumViewModel.album.wykonawca = (ICollection<wykonawca>)selectedWykonawcas;

                var selectedUtwors = db.utwor.Where(m => albumViewModel.SelectedUtwors.Contains(m.id)).ToList();
                albumViewModel.album.utwor = (ICollection<utwor>)selectedUtwors;

                db.album.Add(albumViewModel.album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(albumViewModel);
        }

        // GET: Albums/Edit/5
        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var albumViewModel = new AlbumViewModel()
            {
                album = db.album.Include(i => i.wykonawca).First(i => i.id == id),
            };

            if (albumViewModel.album == null)
                return HttpNotFound();

            var allWykonawcasList = db.wykonawca.ToList();
            albumViewModel.AllWykonawcas = allWykonawcasList.Select(o => new SelectListItem
            {
                Text = o.pseudonim,
                Value = o.id.ToString()
            });

            var allUtworsList = db.utwor.ToList();
            albumViewModel.AllUtwors = allUtworsList.Select(u => new SelectListItem
            {
                Text = u.nazwa,
                Value = u.id.ToString()
            });

            return View(albumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult Edit(AlbumViewModel albumView)
        {
            if (albumView == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var albumToUpdate = db.album.Include(i => i.wykonawca).Include(i => i.utwor).First(i => i.id == albumView.album.id);

                if (TryUpdateModel(albumToUpdate, "album", new string[] { "nazwa", "data_wydania" }))
                {
                    var newWykonawcas = db.album.Where(m => albumView.SelectedWykonawcas.Contains(m.id)).ToList();
                    var updatedWykonawcas = new HashSet<int>(albumView.SelectedWykonawcas);
                    foreach (wykonawca w in db.wykonawca)
                    {
                        if (!updatedWykonawcas.Contains(w.id))
                        {
                            albumToUpdate.wykonawca.Remove(w);
                        }
                        else
                        {
                            albumToUpdate.wykonawca.Add((w));
                        }
                    }

                    var newUtwors = db.album.Where(m => albumView.SelectedUtwors.Contains(m.id)).ToList();
                    var updatedUtwors = new HashSet<int>(albumView.SelectedUtwors);
                    foreach (utwor u in db.utwor)
                    {
                        if (!updatedUtwors.Contains(u.id))
                        {
                            albumToUpdate.utwor.Remove(u);
                        }
                        else
                        {
                            albumToUpdate.utwor.Add(u);
                        }
                    }

                    db.Entry(albumToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(albumView);
        }

        // GET: Albums/Delete/5
        [AuthorizeRoles("ADMIN")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles("ADMIN")]
        public ActionResult DeleteConfirmed(int id)
        {
            album album = db.album.Where(a => a.id == id).Include(a => a.wykonawca).First();
            album.wykonawca = new HashSet<wykonawca>();
            db.Entry(album).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            db.album.Remove(album);
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
