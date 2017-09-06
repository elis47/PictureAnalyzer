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
    public class PaintersController : Controller
    {
        private PictureAnalyzerContext db = new PictureAnalyzerContext();

        // GET: Painters
        public ActionResult Index()
        {
            return View(db.Painters.ToList());
        }

        // GET: Painters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // GET: Painters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Painters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Country,BirthDate,PassDate")] Painter painter)
        {
            if (ModelState.IsValid)
            {
                db.Painters.Add(painter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(painter);
        }

        // GET: Painters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // POST: Painters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Country,BirthDate,PassDate")] Painter painter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(painter);
        }

        // GET: Painters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painter painter = db.Painters.Find(id);
            if (painter == null)
            {
                return HttpNotFound();
            }
            return View(painter);
        }

        // POST: Painters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painter painter = db.Painters.Find(id);
            db.Painters.Remove(painter);
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
