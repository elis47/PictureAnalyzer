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
using PagedList;

namespace PictureAnalyzer.Controllers
{
    public class PaintersController : Controller
    {
        private PictureAnalyzerDb db = new PictureAnalyzerDb();

        // GET: Painters
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder; 
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "country_desc" : "Country";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var painters = from p in db.Painters
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                painters = painters.Where(p => p.Name.Contains(searchString)
                                       || p.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    painters = painters.OrderByDescending(p => p.Name);
                    break;
                case "Country":
                    painters = painters.OrderBy(p => p.Country);
                    break;
                case "country_desc":
                    painters = painters.OrderByDescending(p => p.Country);
                    break;
                case "Date":
                    painters = painters.OrderBy(p => p.BirthDate);
                    break;
                case "date_desc":
                    painters = painters.OrderByDescending(p => p.BirthDate);
                    break;
                default:
                    painters = painters.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(painters.ToPagedList(pageNumber, pageSize));
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
