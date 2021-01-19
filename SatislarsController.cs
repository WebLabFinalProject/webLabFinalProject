using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLabProje.Models;

namespace WebLabProje.Controllers
{
    public class SatislarsController : Controller
    {
        private DbProjeEntities db = new DbProjeEntities();

        // GET: Satislars
        public ActionResult Index()
        {
            var satislar = db.Satislar.Include(s => s.Bayiler).Include(s => s.Musteriler);
            return View(satislar.ToList());
        }

        // GET: Satislars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satislar satislar = db.Satislar.Find(id);
            if (satislar == null)
            {
                return HttpNotFound();
            }
            return View(satislar);
        }

        // GET: Satislars/Create
        public ActionResult Create()
        {
            ViewBag.bayiID = new SelectList(db.Bayiler, "bayiID", "bayiAdi");
            ViewBag.musteriID = new SelectList(db.Musteriler, "musteriID", "musteriAdi");
            return View();
        }

        // POST: Satislars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "satisID,satisTarihi,musteriID,bayiID")] Satislar satislar)
        {
            if (ModelState.IsValid)
            {
                db.Satislar.Add(satislar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bayiID = new SelectList(db.Bayiler, "bayiID", "bayiAdi", satislar.bayiID);
            ViewBag.musteriID = new SelectList(db.Musteriler, "musteriID", "musteriAdi", satislar.musteriID);
            return View(satislar);
        }

        // GET: Satislars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satislar satislar = db.Satislar.Find(id);
            if (satislar == null)
            {
                return HttpNotFound();
            }
            ViewBag.bayiID = new SelectList(db.Bayiler, "bayiID", "bayiAdi", satislar.bayiID);
            ViewBag.musteriID = new SelectList(db.Musteriler, "musteriID", "musteriAdi", satislar.musteriID);
            return View(satislar);
        }

        // POST: Satislars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "satisID,satisTarihi,musteriID,bayiID")] Satislar satislar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(satislar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bayiID = new SelectList(db.Bayiler, "bayiID", "bayiAdi", satislar.bayiID);
            ViewBag.musteriID = new SelectList(db.Musteriler, "musteriID", "musteriAdi", satislar.musteriID);
            return View(satislar);
        }

        // GET: Satislars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Satislar satislar = db.Satislar.Find(id);
            if (satislar == null)
            {
                return HttpNotFound();
            }
            return View(satislar);
        }

        // POST: Satislars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Satislar satislar = db.Satislar.Find(id);
            db.Satislar.Remove(satislar);
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
