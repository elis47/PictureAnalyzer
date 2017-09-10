using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PictureAnalyzer.DAL;
using PictureAnalyzer.Models;

namespace PictureAnalyzer.Controllers
{
    public class InfluencesController : Controller
    {
        private PictureAnalyzerDb db = new PictureAnalyzerDb();

        // GET: Influences
        public ActionResult Index()
        {
            return View(db.Influences.ToList());
        }

        // GET: Influences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Influence influence = db.Influences.Find(id);
            if (influence == null)
            {
                return HttpNotFound();
            }
            return View(influence);
        }

        // GET: Influences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Influences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,BeginYear,EndYear")] Influence influence)
        {
            if (ModelState.IsValid)
            {
                db.Influences.Add(influence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(influence);
        }

        // GET: Influences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Influence influence = db.Influences.Find(id);
            if (influence == null)
            {
                return HttpNotFound();
            }
            return View(influence);
        }

        // POST: Influences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,BeginYear,EndYear")] Influence influence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(influence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(influence);
        }

        // GET: Influences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Influence influence = db.Influences.Find(id);
            if (influence == null)
            {
                return HttpNotFound();
            }
            return View(influence);
        }

        // POST: Influences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Influence influence = db.Influences.Find(id);
            db.Influences.Remove(influence);
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
