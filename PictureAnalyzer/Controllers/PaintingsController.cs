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
    public class PaintingsController : Controller
    {
        private PictureAnalyzerContext db = new PictureAnalyzerContext();

        // GET: Paintings
        public ActionResult Index()
        {
            var paintings = db.Paintings.Include(p => p.Gallery).Include(p => p.Influence).Include(p => p.Painter).Include(p => p.Profile).Include(p => p.Type);
            return View(paintings.ToList());
        }

        // GET: Paintings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // GET: Paintings/Create
        public ActionResult Create()
        {
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name");
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name");
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name");
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name");
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CurrentOwner,HarmonyIndex,ConstrastIndex,LuminosityIndex,PainterID,TypeID,InfluenceID,ProfileID,GalleryID")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                db.Paintings.Add(painting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CurrentOwner,HarmonyIndex,ConstrastIndex,LuminosityIndex,PainterID,TypeID,InfluenceID,ProfileID,GalleryID")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(painting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryID = new SelectList(db.Galleries, "ID", "Name", painting.GalleryID);
            ViewBag.InfluenceID = new SelectList(db.Influences, "ID", "Name", painting.InfluenceID);
            ViewBag.PainterID = new SelectList(db.Painters, "ID", "Name", painting.PainterID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ID", "Name", painting.ProfileID);
            ViewBag.TypeID = new SelectList(db.Types, "ID", "Name", painting.TypeID);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Painting painting = db.Paintings.Find(id);
            if (painting == null)
            {
                return HttpNotFound();
            }
            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Painting painting = db.Paintings.Find(id);
            db.Paintings.Remove(painting);
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
